using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Square : UserControl
    {
        public static event EventHandler FirstClick;
        public static event EventHandler SecondClick;
        public static Square PromotedSquare;
        static bool firstClick = true;
        public int File, Rank;
        public new string Name;
        static Square lastSquare;

        //private bool IsSelected;

        public Color DefaultColor;
        public Pieces Piece;
        public PieceColor PieceColor;

        bool isBlackSquare;
        public bool IsBlackSquare
        {
            get
            {
                return isBlackSquare;
            }
            set
            {
                isBlackSquare = value;
                colorSquare();
            }
        } // Used just and only to draw the board

        public Square()
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Center;
            label1.MouseUp += Square_Click;
        }

        public Square(int file, int rank) : this()
        {
            File = file;
            Rank = rank;
            Name = "abcdefgh"[file] + (Rank + 1).ToString();
        }

        private void colorSquare()
        {
            DefaultColor = IsBlackSquare ? Color.CornflowerBlue : Color.WhiteSmoke;
            BackColor = DefaultColor;
        }

        public bool IsEmpty
        {
            get
            {
                return PieceColor == PieceColor.None;
            }
        }

        public void SetPiece(Pieces piece, PieceColor color)
        {
            Piece = piece;
            PieceColor = color;
            BackgroundImage = piece == Pieces.None ? null : ChessContext.Core.GetPiece(piece, color);
        }


        public void HighLight(bool ok)
        {
            // ³¤Ç
            label1.Text = ok ? "Ç" : null;
        }

        private void Square_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                var x = this;

                if (ChessContext.Core.WhosPlaying == x.PieceColor || ChessContext.Core.HasNoTurns)
                {
                    x.BackColor = Color.LightGreen;
                    lastSquare = x;
                    ChessContext.Core.highLight.Go(x);
                    FirstClick?.Invoke(this, e);
                    firstClick = !firstClick;
                }
            }
            else
            {
                var x = this;
                lastSquare.BackColor = lastSquare.DefaultColor;

                if (lastSquare == x)
                    ChessContext.Core.highLight.Clear();

                if (!x.IsEmpty)
                    if(x.PieceColor == lastSquare.PieceColor)
                    {
                        ChessContext.Core.highLight.Clear();
                        x.BackColor = Color.LightGreen;
                        lastSquare = x;
                        ChessContext.Core.highLight.Go(x);
                        FirstClick?.Invoke(this, e);
                        return;
                    }

                SecondClick?.Invoke(this, e);
                firstClick = !firstClick;
                ChessContext.Core.highLight.Clear();
            }
        }

        public void ClearSquare()
        {
            BackgroundImage = null;
            BackColor = DefaultColor;
            Piece = Pieces.None;
            PieceColor = PieceColor.None;
        }
    }
}
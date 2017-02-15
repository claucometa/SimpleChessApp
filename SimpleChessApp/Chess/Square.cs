using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Square : UserControl
    {
        public static event EventHandler FirstClick;
        public static event EventHandler SecondClick;
        static bool firstClick = true;
        public int File, Rank;
        public new string Name;
        static Square lastSquare;
        static ChessPiece lastPiece;
        public static HighLightMoves light = new HighLightMoves();

        //private bool IsSelected;

        public Color DefaultColor;

        ChessPiece piece;
        public ChessPiece Piece
        {
            get
            {
                return piece;
            }
            set
            {
                piece = value;
                SetPiece(piece);
            }
        }

        public string SpecialName
        {
            get
            {
                return CurrentSquare + " " + Piece.ToString();
            }
        }

        public string CurrentSquare
        {
            get
            {
                return "abcdefgh"[File] + (Rank + 1).ToString();
            }
        }


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
            label1.ForeColor = IsBlackSquare ? Color.White : Color.Black;
        }

        public bool IsEmpty
        {
            get
            {
                return Piece == null;
            }
        }

        public bool IsGhost;

        public void SetPiece(ChessPiece p)
        {
            Piece = p;
            BackgroundImage = ChessContext.Core.GetPiece(p.Name, p.Color);
        }

        public void HighCheck(bool ok)
        {
            label1.Text = ok ? "K" : null;
        }

        public void HighLight(bool ok)
        {
            label1.Text = ok ? "Ç" : null;
        }

        private void Square_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                var x = this;

                if (ChessContext.Core.WhosPlaying == x.Piece.Color || ChessContext.Core.HasNoTurns)
                {
                    x.BackColor = Color.LightGreen;
                    lastSquare = x;
                    light.Go(x);
                    light.HighLightSquares();
                    FirstClick?.Invoke(this, e);
                    firstClick = !firstClick;
                }
            }
            else
            {
                var x = this;
                lastSquare.BackColor = lastSquare.DefaultColor;

                if (lastSquare == x)
                    light.Clear();

                if (!x.IsEmpty)
                    if (x.Piece.Color == lastSquare.Piece.Color)
                    {
                        light.Clear();
                        x.BackColor = Color.LightGreen;
                        lastSquare = x;
                        light.Go(x);
                        FirstClick?.Invoke(this, e);
                        return;
                    }

                SecondClick?.Invoke(this, e);
                firstClick = !firstClick;
                light.Clear();
            }
        }

        public void ClearSquare()
        {
            BackgroundImage = null;
            BackColor = DefaultColor;
            Piece = null;
        }

        internal static void ClearLightMoves(PossibleMoves[] high)
        {

        }
    }
}
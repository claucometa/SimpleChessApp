using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Square : UserControl
    {
        public int File, Rank;
        public new string Name;
        public static event EventHandler FirstClick;
        public static event EventHandler SecondClick;

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

                if (piece == null)
                {
                    piece = null;
                    BackgroundImage = null;
                    return;
                }

                BackgroundImage = u.GetPiece(piece.Name, piece.Color);
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

        ChessCore u
        {
            get
            {
                return ChessContext.Core;
            }
        }

        public Square()
        {
            InitializeComponent();
            MouseClick += Square_MouseClick;
            panel1.MouseClick += Square_MouseClick;
        }

        private void Square_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (u.firstClick)
                {
                    var x = this;

                    if (x.Piece == null) return;

                    if (u.WhosPlaying == x.Piece.Color || u.DisableTurn)
                    {
                        x.BackColor = Color.LightGreen;
                        u.lastSquare = x;
                        u.light.FindAllMoves(x);
                        u.light.HighLightMoveStyle();
                        FirstClick?.Invoke(this, e);
                        u.firstClick = !u.firstClick;
                    }
                }
                else
                {
                    var x = this;

                    // If click again on same piece do nothing
                    if (u.lastSquare == x) return;

                    u.lastSquare.BackColor = u.lastSquare.DefaultColor;

                    if (!x.IsEmpty)

                        if (x.Piece != null)
                        {
                            if (x.Piece.Color == u.lastSquare.Piece.Color)
                            {
                                u.light.Clear();
                                x.BackColor = Color.LightGreen;

                                u.lastSquare = x;
                                u.light.FindAllMoves(x);
                                u.light.HighLightMoveStyle();
                                FirstClick?.Invoke(this, e);
                                return;
                            }
                        }

                    SecondClick?.Invoke(this, e);
                    u.firstClick = !u.firstClick;
                    u.light.Clear();
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                MessageBox.Show($"{Piece?.Name}\r\n{File}-{Rank}\r\n{Name}");
            }
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
                return Piece == null;
            }
        }

        public void ShowCheck(bool ok)
        {
            panel1.BackgroundImage = ok ? Properties.Resources.triangle : null;
        }

        public void ShowMove(bool ok)
        {
            panel1.BackgroundImage = ok ? Properties.Resources.circle : null;
        }
    }
}
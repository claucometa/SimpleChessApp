using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Square : UserControl
    {
        public int File, Rank;
        public new string Name;
        //public static event EventHandler FirstClick;
        //public static event EventHandler SecondClick;
        Board Board;

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

                BackgroundImage = repo.GetPiece(piece.Kind, piece.Color);
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

        Repository repo
        {
            get
            {
                return ChessContext.repo;
            }
        }

        internal void ShowMove(object flipped)
        {
            throw new NotImplementedException();
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
                var x = this;

                if (Board.LastSquare != null)
                {
                    if (x.Piece == null)
                    {
                        if (Board.LastSquare != null) Board.LastSquare.BackColor = Board.LastSquare.DefaultColor;
                    }
                    else if (x.Piece.Color == Board.LastSquare.Piece.Color)
                    {
                        if (Board.LastSquare != null) Board.LastSquare.BackColor = Board.LastSquare.DefaultColor;
                        x.BackColor = Color.LightGreen;
                        Board.LastSquare = x;
                    }
                    else if (x.Piece.Color != Board.LastSquare.Piece.Color)
                    {
                        if (Board.LastSquare != null) Board.LastSquare.BackColor = Board.LastSquare.DefaultColor;
                        x.BackColor = Color.LightGreen;
                        Board.LastSquare = x;
                    }
                    else if (x.Piece != Board.LastSquare.Piece)
                    {
                        //x.BackColor = DefaultColor;
                        Board.LastSquare.BackColor = Board.LastSquare.DefaultColor;
                        Board.LastSquare = null;
                        return;
                    }
                }

                else if (Board.LastSquare != x)
                {
                    if (x.Piece != null)
                    {
                        if (Board.LastSquare != null) Board.LastSquare.BackColor = Board.LastSquare.DefaultColor;
                        x.BackColor = Color.LightGreen;
                        Board.LastSquare = x;                 
                    }
                }
            }
        }

        public Square(int file, int rank, Board Board) : this()
        {
            this.Board = Board;
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

        public void ShowCheck(bool ok, bool flip)
        {
            var img = flip ? Properties.Resources.Black : Properties.Resources.White;
            
            panel1.BackgroundImage = ok ? img : null;
        }

        public void ShowMove(bool ok, bool flip)
        {
            var img = flip ? Properties.Resources.White : Properties.Resources.Black;
            panel1.BackgroundImage = ok ? img : null;
        }
    }
}
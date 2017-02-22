using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Game
{
    public partial class SimpleSquare : UserControl
    {
 

        public new event EventHandler<MouseEventArgs> Click;
        Color DefaultColor;

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

        public void colorSquare()
        {
            DefaultColor = IsBlackSquare ? Color.CornflowerBlue : Color.WhiteSmoke;
            BackColor = DefaultColor;
        }

        // Redraw
        public Pieces Kind
        {
            set
            {
                Piece.Kind = value;
                Piece = Piece;
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

        public Repository repo
        {
            get
            {
                return ChessContext.repo;
            }
        }

        public SimpleSquare()
        {
            InitializeComponent();
            MouseClick += Square_MouseClick;
            panel1.MouseClick += Square_MouseClick;
        }

        private void Square_MouseClick(object sender, MouseEventArgs e)
        {
            Click?.Invoke(this, e);
        }

        public void HighLight()
        {
            BackColor = Color.ForestGreen;
        }

        public void ClearHighLight()
        {
            BackColor = DefaultColor;
        }

        public bool IsEmpty
        {
            get
            {
                return Piece == null;
            }
        }
    }
}
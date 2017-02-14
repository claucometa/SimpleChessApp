using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Square : UserControl
    {
        public static event EventHandler CliquedSquare;
        public static Square PromotedSquare;
        public int File, Rank;
        public new string Name;
        private bool IsSelected;
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
        static Square fromSquare;
        static Square toSquare;

        public Square()
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Center;
            MouseUp += Square_Click;
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

        private void Square_Click(object sender, EventArgs e)
        {
            CliquedSquare?.Invoke(sender, e);

            toSquare = (Square)sender;

            if (toSquare.IsSelected)
            {
                toSquare.IsSelected = false;
                toSquare.BackColor = toSquare.DefaultColor;
                fromSquare = null;
                return;
            }

            if (toSquare == fromSquare)
            {
                toSquare.IsSelected = !toSquare.IsSelected;
                if (toSquare.IsSelected) toSquare.BackColor = Color.LightGreen;
                return;
            }

            if (fromSquare != null)
            {
                fromSquare.BackColor = fromSquare.DefaultColor;
                fromSquare.IsSelected = false;
                ChessContext.Core.HandleUserAction(fromSquare, toSquare);                
            }

            toSquare.BackColor = Color.LightGreen;
            toSquare.IsSelected = true;
            fromSquare = toSquare;
        }

        internal void MakeGhost()
        {
            if (MoveValidation.GhostSquare != null)
                if (MoveValidation.GhostSquare.Piece == Pieces.GhostPawn)
                    MoveValidation.GhostSquare.ClearSquare();

            Piece = Pieces.GhostPawn;
            PieceColor = PieceColor.None;
            //BackColor = Color.Gray; Indicator for debugging purposes
            MoveValidation.GhostSquare = this;
        } // Handles Passant 

        public void ClearSquare()
        {
            BackgroundImage = null;
            BackColor = DefaultColor;
            Piece = Pieces.None;
            PieceColor = PieceColor.None;
        }
    }
}
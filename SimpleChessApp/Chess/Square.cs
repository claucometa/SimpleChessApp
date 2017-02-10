using System;
using System.Drawing;
using System.Windows.Forms;
using static SimpleChessApp.ChessSet;

namespace SimpleChessApp
{
    public partial class Square : UserControl
    {
        static Square LastSelectedSquare;
        static Square SelectedSquare;

        bool IsSelected;

        public Color DefaultColor;
        public Pieces Piece = Pieces.None;
        public bool IsBlack;

        bool isBlackSquare;
        public bool IsBlackSquare
        {
            get { return isBlackSquare; }
            set
            {
                isBlackSquare = value;
                drawSquare();
            }
        }

        private void drawSquare()
        {
            DefaultColor = IsBlackSquare ? Color.CornflowerBlue : Color.WhiteSmoke;
            BackColor = DefaultColor;
        }

        public void SetPiece(Pieces piece, bool side)
        {
            Piece = piece;
            IsBlack = side;
            BackgroundImage = getPiece(piece, side);
        }

        private Image getPiece(Pieces piece, bool side)
        {
            return piece == Pieces.None ? null : ChessContext.Set.GetPiece(piece, side);
        }

        public Square()
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Center;
            MouseUp += Square_Click;
        }

        private void Square_Click(object sender, System.EventArgs e)
        {
            SelectedSquare = (Square)sender;

            if (SelectedSquare.IsSelected)
            {
                SelectedSquare.IsSelected = false;
                SelectedSquare.BackColor = SelectedSquare.DefaultColor;
                LastSelectedSquare = null;
                return;
            }

            if (SelectedSquare != LastSelectedSquare)
            {
                if (LastSelectedSquare != null)
                {
                    LastSelectedSquare.BackColor = LastSelectedSquare.DefaultColor;
                    LastSelectedSquare.IsSelected = false;

                    if (SelectedSquare.Piece == Pieces.None)
                    {
                        // Move
                        if (LastSelectedSquare.Piece != Pieces.None)
                        {
                            if (PieceCanMove(SelectedSquare))
                            {
                                SelectedSquare.SetPiece(LastSelectedSquare.Piece, LastSelectedSquare.IsBlack);
                                LastSelectedSquare.ClearSquare();
                                return;
                            }
                        }
                    }
                    else
                    {
                        // Captura
                        if (LastSelectedSquare.Piece != Pieces.None)
                        {
                            // Avoid capture same color
                            if (LastSelectedSquare.IsBlack != SelectedSquare.IsBlack)
                            {
                                SelectedSquare.SetPiece(LastSelectedSquare.Piece, LastSelectedSquare.IsBlack);
                                LastSelectedSquare.ClearSquare();
                                return;
                            }
                        }
                    }
                }

                SelectedSquare.BackColor = Color.LightGreen;
                SelectedSquare.IsSelected = true;

                LastSelectedSquare = SelectedSquare;
            }
        }

        // TODO 2. Movement rules for each piece
        private bool PieceCanMove(Square x)
        {
            switch (x.Piece)
            {
                case Pieces.None:
                    break;
                case Pieces.Pawn:
                    break;
                case Pieces.Knight:
                    break;
                case Pieces.Bishop:
                    break;
                case Pieces.Rook:
                    break;
                case Pieces.King:
                    break;
                case Pieces.Queen:
                    break;
                default:
                    break;
            }

            return true;
        }

        private void ClearSquare()
        {
            BackgroundImage = null;
            Piece = Pieces.None;
        }
    }
}
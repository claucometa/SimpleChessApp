using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Square : UserControl
    {
        static Square SelectedSquare;
        static Square TargetSquare;
        public static Square PromotedSquare;

        public int File { get; set; }
        public int Rank { get; set; }

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

        public Square(int file, int rank)
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Center;
            MouseUp += Square_Click;
            File = file;
            Rank = rank;
        }

        private void Square_Click(object sender, System.EventArgs e)
        {
            TargetSquare = (Square)sender;

            if (TargetSquare.IsSelected)
            {
                TargetSquare.IsSelected = false;
                TargetSquare.BackColor = TargetSquare.DefaultColor;
                SelectedSquare = null;
                return;
            }

            if (TargetSquare == SelectedSquare) return;

            if (SelectedSquare != null)
            {
                SelectedSquare.BackColor = SelectedSquare.DefaultColor;
                SelectedSquare.IsSelected = false;

                if (TargetSquare.Piece == Pieces.None)
                {
                    // Move
                    if (SelectedSquare.Piece != Pieces.None)
                    {
                        if (new MoveValidation(SelectedSquare, TargetSquare).Validate)
                        {
                            TargetSquare.SetPiece(SelectedSquare.Piece, SelectedSquare.IsBlack);
                            SelectedSquare.ClearSquare();
                            return;
                        }
                        else
                        {
                            //Invalidate square selection if the move is not allowed
                            //TODO: Display a toast message
                            return;
                        }
                    }
                }

                // Capture
                if (TargetSquare.Piece != Pieces.None)
                {
                    if (SelectedSquare.Piece != Pieces.None)
                    {
                        // Move validation applies to all pieces except pawn
                        if (new MoveValidation(SelectedSquare, TargetSquare).Validate)
                        {
                            if (SelectedSquare.Piece == Pieces.Pawn)
                            {
                                // TODO: Handle capture pawn exception
                            }

                            // Avoid capture same color
                            if (SelectedSquare.IsBlack != TargetSquare.IsBlack)
                            {
                                TargetSquare.SetPiece(SelectedSquare.Piece, SelectedSquare.IsBlack);
                                SelectedSquare.ClearSquare();
                                return;
                            }
                        }
                    }
                }
            }

            TargetSquare.BackColor = Color.LightGreen;
            TargetSquare.IsSelected = true;

            SelectedSquare = TargetSquare;
        }

        private void ClearSquare()
        {
            BackgroundImage = null;
            Piece = Pieces.None;
        }
    }
}

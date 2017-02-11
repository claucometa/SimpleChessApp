using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Square : UserControl
    {
        static Square fromSquare;
        static Square toSquare;
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
            return piece == Pieces.None ? null : ChessContext.Core.GetPiece(piece, side);
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
            toSquare = (Square)sender;

            if (toSquare.IsSelected)
            {
                toSquare.IsSelected = false;
                toSquare.BackColor = toSquare.DefaultColor;
                fromSquare = null;
                return;
            }

            if (toSquare == fromSquare) return;

            if (fromSquare != null)
            {
                fromSquare.BackColor = fromSquare.DefaultColor;
                fromSquare.IsSelected = false;

                // Controls player turn
                if (ChessContext.Core.IsBlackPlaying == fromSquare.IsBlack)
                {
                    if (toSquare.Piece == Pieces.None)
                    {
                        // Move
                        if (fromSquare.Piece != Pieces.None)
                        {
                            if (new MoveValidation(fromSquare, toSquare).Validate)
                            {
                                toSquare.SetPiece(fromSquare.Piece, fromSquare.IsBlack);
                                fromSquare.ClearSquare();
                                ChessContext.Core.ChangeTurn();
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
                    if (toSquare.Piece != Pieces.None)
                    {
                        if (fromSquare.Piece != Pieces.None)
                        {
                            // Move validation applies to all pieces but pawn
                            if (new MoveValidation(fromSquare, toSquare,
                                fromSquare.Piece == Pieces.Pawn).Validate)
                            {
                                // Avoid capture same color
                                if (fromSquare.IsBlack != toSquare.IsBlack)
                                {
                                    toSquare.SetPiece(fromSquare.Piece, fromSquare.IsBlack);
                                    fromSquare.ClearSquare();
                                    ChessContext.Core.ChangeTurn();
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            toSquare.BackColor = Color.LightGreen;
            toSquare.IsSelected = true;

            fromSquare = toSquare;
        }

        private void ClearSquare()
        {
            BackgroundImage = null;
            Piece = Pieces.None;
        }
    }
}

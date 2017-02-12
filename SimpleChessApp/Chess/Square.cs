using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Square : UserControl
    {
        static Square fromSquare;
        static Square toSquare;
        static Square lastMove;
        public static Square PromotedSquare;

        public int File { get; set; }
        public int Rank { get; set; }

        private bool IsSelected;

        public Color DefaultColor;
        public Pieces Piece = Pieces.None;
        public bool IsBlack;

        // Just used to draw the board
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
                paintSquare();
            }
        }

        private void paintSquare()
        {
            DefaultColor = IsBlackSquare ? Color.CornflowerBlue : Color.WhiteSmoke;
            BackColor = DefaultColor;
        }

        public bool IsEmpty
        {
            get
            {
                return Piece != Pieces.None && Piece != Pieces.GhostPawn;
            }
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

                if (ChessContext.Core.IsBlackPlaying == fromSquare.IsBlack ||
                    ChessContext.Core.SwitchTurnOff) // Controls player turn
                {
                    if (toSquare.Piece == Pieces.None)
                    {
                        // Handle Move
                        if (fromSquare.Piece != Pieces.None)
                        {
                            if (new MoveValidation(fromSquare, toSquare).Validate)
                            {
                                #region Set Castling Flags
                                if (fromSquare.IsBlack)
                                {
                                    if (ChessContext.Core.BlackCanCastleKingSide || ChessContext.Core.BlackCanCastleQueenSide)
                                    {
                                        if (fromSquare.Piece == Pieces.King)
                                        {
                                            ChessContext.Core.BlackCanCastleKingSide = false;
                                            ChessContext.Core.BlackCanCastleQueenSide = false;
                                        }

                                        else if (fromSquare.Piece == Pieces.Rook && fromSquare.File == 0)
                                            ChessContext.Core.BlackCanCastleQueenSide = false;

                                        else if (fromSquare.Piece == Pieces.Rook && fromSquare.File == 7)
                                            ChessContext.Core.BlackCanCastleKingSide = false;

                                    }
                                }

                                if (!fromSquare.IsBlack)
                                {
                                    if (ChessContext.Core.WhiteCanCastleKingSide || ChessContext.Core.WhiteCanCastleQueenSide)
                                    {
                                        if (fromSquare.Piece == Pieces.King)
                                        {
                                            ChessContext.Core.WhiteCanCastleKingSide = false;
                                            ChessContext.Core.WhiteCanCastleQueenSide = false;
                                        }

                                        else if (fromSquare.Piece == Pieces.Rook && fromSquare.File == 0)
                                            ChessContext.Core.WhiteCanCastleQueenSide = false;

                                        else if (fromSquare.Piece == Pieces.Rook && fromSquare.File == 7)
                                            ChessContext.Core.WhiteCanCastleKingSide = false;

                                    }
                                }
                                #endregion  

                                lastMove = toSquare;
                                toSquare.SetPiece(fromSquare.Piece, fromSquare.IsBlack);
                                fromSquare.clearSquare();
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

                    // Handle Capture
                    if (toSquare.Piece != Pieces.None)
                    {
                        if (fromSquare.Piece != Pieces.None)
                        {
                            if (new MoveValidation(fromSquare, toSquare,
                                fromSquare.Piece == Pieces.Pawn).Validate)
                            {
                                // Avoid capture of same piece color
                                if (fromSquare.IsBlack != toSquare.IsBlack || toSquare.Piece == Pieces.GhostPawn)
                                {
                                    // Handles passant
                                    if (fromSquare.Piece == Pieces.Pawn)
                                    {
                                        if (toSquare.Piece == Pieces.GhostPawn)
                                        {
                                            if (ChessContext.Core.IsPassantActive)
                                                lastMove.SetPiece(Pieces.None, false);
                                            else
                                                return;
                                        }
                                    }

                                    if (fromSquare.Piece != Pieces.Pawn)
                                    {
                                        if (toSquare.Piece == Pieces.GhostPawn)
                                            toSquare.Piece = Pieces.None;
                                    }

                                    toSquare.SetPiece(fromSquare.Piece, fromSquare.IsBlack);
                                    fromSquare.clearSquare();
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

        private void clearSquare()
        {
            BackgroundImage = null;
            Piece = Pieces.None;
        }
    }
}

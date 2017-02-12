using System;

namespace SimpleChessApp.Chess
{
    // TODO 2. Movement rules for each piece
    internal class MoveValidation
    {
        Square from;
        Square to;
        public static Square GhostSquare;
        bool pawnCaptureException;

        public MoveValidation(Square From, Square To, bool PawnCaptureException = false)
        {
            from = From;
            to = To;
            pawnCaptureException = PawnCaptureException;
        }

        public bool Validate
        {
            get
            {
                switch (from.Piece)
                {
                    case Pieces.Pawn:
                        return handlePawn();
                    case Pieces.Knight:
                        return handleKnight();
                    case Pieces.Bishop:
                        return handleBishop();
                    case Pieces.Rook:
                        return handleRook();
                    case Pieces.King:
                        return handleKing();
                    case Pieces.Queen:
                        return handleQueen();
                    default:
                        return false;
                }
            }
        }

        private bool handleQueen()
        {
            return isMovingHorizontally || isMovingVertically || isMovingDiagonally;
        }

        private bool handleKing()
        {
            return isMovingHorizontally || isMovingVertically || isMovingDiagonally;
        }

        private bool handleRook()
        {
            return isMovingHorizontally || isMovingVertically;
        }

        private bool handleBishop()
        {
            return isMovingDiagonally;
        }

        private bool handleKnight()
        {
            return true;
        }

        private bool handlePawn()
        {
            var isBlack = from.IsBlack;
            var mult = isBlack ? -1 : 1;

            if (isMovingVertically)
            {
                // Allow pawn moving one step forward
                if (from.Rank - to.Rank == 1 * mult)
                {
                    // promotes when reach last rank 
                    if (to.Rank == (isBlack ? 7 : 0)) promotePawn();

                    return isPathFree();
                }

                // Allow pawn moving one two steps forward from home rank only
                if (isHomeRank)
                {
                    if (from.Rank - to.Rank == 2 * mult)
                    {
                        var ok = isPathFree();

                        if (ok)
                        {
                            GhostSquare = ChessContext.Core.ChessBoard.Squares[from.File, from.Rank - mult];
                            GhostSquare.Piece = Pieces.GhostPawn;
                            GhostSquare.IsBlack = isBlack;
                        }

                        ChessContext.Core.AllowPassant = ok;
                        return ok;
                    }
                }
            }
            else if (pawnCaptureException)
            {
                // Handles pawn capture
                return Math.Abs(from.File - to.File) == 1 && from.Rank - to.Rank == (1 * mult);
            }

            return false;
        }

        private bool isPathFree()
        {
            return new MoveInterception(from, to).Validate;
        }

        private void promotePawn()
        {
            ChessContext.Core.ShowPieceSelector(from);
            Square.PromotedSquare = to;
        }

        #region Logic Devices
        bool isMovingVertically { get { return from.File == to.File; } }
        bool isMovingHorizontally { get { return from.Rank == to.Rank; } }
        bool isMovingDiagonally
        {
            get
            {
                var a = from.Rank;
                var b = from.File;
                var c = to.Rank;
                var d = to.File;

                while (a < b && c < d)
                {
                    a++;
                    c++;
                }

                while (a > b && c > d)
                {
                    a--;
                    c--;
                }

                while (a < c && b > d)
                {
                    a++;
                    b--;
                }

                while (a > c && b < d)
                {
                    a--;
                    b++;
                }

                return (a == b && c == d) || (a == c && b == d);
            }
        }
        bool isHomeRank
        {
            get
            {
                var isBlack = from.IsBlack;
                return from.Rank == (isBlack ? 1 : 6);
            }
        }
        #endregion
    }
}
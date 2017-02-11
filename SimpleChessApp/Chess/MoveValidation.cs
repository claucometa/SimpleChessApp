
using System;

namespace SimpleChessApp.Chess
{
    // TODO 2. Movement rules for each piece

    internal class MoveValidation
    {
        Square from;
        Square to;
        bool pawnMoveCaptureException;

        public MoveValidation(Square From, Square To, bool PawnCaptureException = false)
        {
            from = From;
            to = To;
            pawnMoveCaptureException = PawnCaptureException;
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
            return true;
        }

        private bool handleKing()
        {
            return true;
        }

        private bool handleRook()
        {
            return true;
        }

        private bool handleBishop()
        {
            return true;
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
                        ChessContext.Core.AllowPassant = ok;
                        return ok;
                    }
                }
            }
            else if (pawnMoveCaptureException)
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
        bool isMovingHorizontally { get { return from.Rank == to.Rank; } } // Not in use yet
        bool isMovingDiagonally { get { return true; } } // Not implemented

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
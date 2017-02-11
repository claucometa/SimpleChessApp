
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
                        return checkPawn();
                    case Pieces.Knight:
                        return checkKnight();
                    case Pieces.Bishop:
                        return checkBishop();
                    case Pieces.Rook:
                        return checkRook();
                    case Pieces.King:
                        return checkKing();
                    case Pieces.Queen:
                        return checkQueen();
                    default:
                        return false;
                }
            }
        }

        private bool checkQueen()
        {
            return true;
        }

        private bool checkKing()
        {
            return true;
        }

        private bool checkRook()
        {
            return true;
        }

        private bool checkBishop()
        {
            return true;
        }

        private bool checkKnight()
        {
            return true;
        }

        private bool checkPawn()
        {
            var isBlack = from.IsBlack;
            var mult = isBlack ? -1 : 1;

            if (isMovingVertically)
            {
                // Allow pawn moving one or two steps forward from home rank
                if (isHomeRank)
                {
                    if (from.Rank - to.Rank == 1 * mult ||
                        from.Rank - to.Rank == 2 * mult)
                        return hasInterception();
                }

                // Allow pawn moving one step forward
                if (from.Rank - to.Rank == 1 * mult)
                {
                    // promotes when reach last rank 
                    if (to.Rank == (isBlack ? 7 : 0)) promotePawn();

                    return hasInterception();
                }
            }
            else if (pawnMoveCaptureException)
            {
                // Handles pawn capture
                return Math.Abs(from.File - to.File) == 1 && from.Rank - to.Rank == (1 * mult);
            }

            return false;
        }

        private bool hasInterception()
        {
            if (from.Piece == Pieces.Pawn)
            {

            }

            return true;
        }

        private void promotePawn()
        {
            ChessContext.Set.ShowPieceSelector(from);
            Square.PromotedSquare = to;
        }

        // For the time this logic 'devices' are being used to pawn movement

        bool isMovingVertically { get { return from.File == to.File; } }

        bool isHomeRank
        {
            get
            {
                var isBlack = from.IsBlack;
                return from.Rank == (isBlack ? 1 : 6);
            }
        }
    }
}
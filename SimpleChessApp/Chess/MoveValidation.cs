
namespace SimpleChessApp.Chess
{
    // TODO 2. Movement rules for each piece

    internal class MoveValidation
    {
        Square from;
        Square to;

        public MoveValidation(Square From, Square To)
        {
            from = From;
            to = To;
        }

        public bool CheckMove()
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
            if (from.File == to.File)
            {
                var isBlack = from.IsBlack;
                var rank = isBlack ? 1 : 6;
                var mult = isBlack ? -1 : 1;

                if (from.Rank == rank)
                {
                    if (from.Rank - to.Rank == 1 * mult ||
                        from.Rank - to.Rank == 2 * mult)
                        return true;
                }
                if (from.Rank - to.Rank == 1 * mult)
                {
                    if (to.Rank == (isBlack ? 7 : 0)) promotePawn();
                    return true;
                }
            }
            return false;
        }

        private void promotePawn()
        {
            ChessContext.Set.ShowPieceSelector(from);
            Square.PromotedSquare = to;
        }
    }
}
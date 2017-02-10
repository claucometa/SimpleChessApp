
namespace SimpleChessApp.Chess
{
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
                    return checkPawn(from, to);
                case Pieces.Knight:
                    return checkKnight(from, to);
                case Pieces.Bishop:
                    return checkBishop(from, to);
                case Pieces.Rook:
                    return checkRook(from, to);
                case Pieces.King:
                    return checkKing(from, to);
                case Pieces.Queen:
                    return checkQueen(from, to);
                default:
                    return false;
            }
        }

        private bool checkQueen(Square from, Square to)
        {
            return true;
        }

        private bool checkKing(Square from, Square to)
        {
            return true;
        }

        private bool checkRook(Square from, Square to)
        {
            return true;
        }

        private bool checkBishop(Square from, Square to)
        {
            return true;
        }

        private bool checkKnight(Square from, Square to)
        {
            return true;
        }

        private bool checkPawn(Square from, Square to)
        {
            if (from.File == to.File)
            {
                if (from.IsBlack)
                {
                    if (from.Rank == 1)
                    {
                        if (from.Rank - to.Rank == -1 ||
                            from.Rank - to.Rank == -2)
                            return true;
                    }
                    if (from.Rank - to.Rank == -1)
                    {
                        if (to.Rank == 7) promotePawn(from, to);
                        return true;
                    }
                }

                if (!from.IsBlack)
                {
                    if (from.Rank == 6)
                    {
                        if (from.Rank - to.Rank == 1 ||
                            from.Rank - to.Rank == 2)

                            return true;

                    }

                    if (from.Rank - to.Rank == 1)
                    {
                        if (to.Rank == 0) promotePawn(from, to);
                        return true;
                    }
                }
            }
            return false;
        }

        private void promotePawn(Square from, Square to)
        {
            ChessContext.Set.ShowPieceSelector(from);
            Square.PromotedSquare = to;
        }
    }
}
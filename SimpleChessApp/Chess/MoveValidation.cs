
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
                        if (to.Rank == 7) promotePawn();
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
                        if (to.Rank == 0) promotePawn();
                        return true;
                    }
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
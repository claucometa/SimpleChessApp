namespace SimpleChessApp.Chess
{
    class DebugChess
    {
        Board b;

        internal DebugChess(Board board)
        {
            b = board;
        }

        internal void TestSinglePiece(Pieces x)
        {
            b.ClearBoard();
            b.addWhite(4, 4, x);
        }

        internal void TestPassant()
        {
            b.ClearBoard();

            b.addWhite(1, 1, Pieces.Pawn);
            b.addWhite(3, 1, Pieces.Pawn);
            b.addWhite(5, 4, Pieces.Pawn);

            b.addBlack(2, 3, Pieces.Pawn);
            b.addBlack(4, 6, Pieces.Pawn);
            b.addBlack(6, 6, Pieces.Pawn);
        }

        internal void TestPromotion()
        {
            b.ClearBoard();
            b.addBlack(4, 1, Pieces.Pawn);
            b.addWhite(4, 6, Pieces.Pawn);
            b.addBlack(3, 7, Pieces.King);
            b.addWhite(3, 0, Pieces.King);
        }

        internal void TestCastling()
        {
            b.ClearBoard();
            b.addWhite(0, 7, Pieces.Rook);
            b.addWhite(4, 7, Pieces.King);
            b.addWhite(7, 7, Pieces.Rook);
            b.addBlack(0, 0, Pieces.Rook);
            b.addBlack(4, 0, Pieces.King);
            b.addBlack(7, 0, Pieces.Rook);
        }
    }
}

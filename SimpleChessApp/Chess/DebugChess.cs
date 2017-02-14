namespace SimpleChessApp.Chess
{
    class DebugChess
    {
        Board x;

        internal DebugChess(Board board)
        {
            x = board;
        }

        internal void TestPassant()
        {
            x.ClearBoard();
            x[1, 1].SetPiece(Pieces.Pawn, PieceColor.White);
            x[2, 3].SetPiece(Pieces.Pawn, PieceColor.Black);
            x[3, 1].SetPiece(Pieces.Pawn, PieceColor.White);

            x[4, 6].SetPiece(Pieces.Pawn, PieceColor.Black);
            x[5, 4].SetPiece(Pieces.Pawn, PieceColor.White);
            x[6, 6].SetPiece(Pieces.Pawn, PieceColor.Black);
        }

        internal void TestSinglePiece(Pieces x)
        {
            this.x.ClearBoard();
            this.x[4, 4].SetPiece(x, PieceColor.Black);
        }

        internal void TestCastling()
        {
            x.ClearBoard();
            x[0, 7].SetPiece(Pieces.Rook, PieceColor.Black);
            x[4, 7].SetPiece(Pieces.King, PieceColor.Black);
            x[7, 7].SetPiece(Pieces.Rook, PieceColor.Black);

            x[0, 0].SetPiece(Pieces.Rook, PieceColor.White);
            x[4, 0].SetPiece(Pieces.King, PieceColor.White);
            x[7, 0].SetPiece(Pieces.Rook, PieceColor.White);
        }
    }
}

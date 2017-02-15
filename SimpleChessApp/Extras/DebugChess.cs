namespace SimpleChessApp.Chess
{
    class DebugChess
    {
        Board b;

        internal DebugChess(Board board)
        {
            b = board;
        }

        internal void TestPassant()
        {
            b.ClearBoard();
            b[1, 1].Piece = new ChessPiece(b[1, 1], Pieces.Pawn, PieceColor.White);
            b[2, 3].Piece = new ChessPiece(b[2, 3], Pieces.Pawn, PieceColor.Black);
            b[3, 1].Piece = new ChessPiece(b[3, 1], Pieces.Pawn, PieceColor.White);
            b[4, 6].Piece = new ChessPiece(b[4, 6], Pieces.Pawn, PieceColor.Black);
            b[5, 4].Piece = new ChessPiece(b[5, 4], Pieces.Pawn, PieceColor.White);
            b[6, 6].Piece = new ChessPiece(b[6, 6], Pieces.Pawn, PieceColor.Black);
        }

        internal void TestSinglePiece(Pieces x)
        {
            b.ClearBoard();
            b[4, 4].Piece = new ChessPiece(b[4, 4], x, PieceColor.White);
        }

        internal void TestCastling()
        {
            b.ClearBoard();
            b[0, 7].Piece = new ChessPiece(b[0, 7], Pieces.Rook, PieceColor.Black);
            b[4, 7].Piece = new ChessPiece(b[4, 7], Pieces.King, PieceColor.Black);
            b[7, 7].Piece = new ChessPiece(b[7, 7], Pieces.Rook, PieceColor.Black);
            b[0, 0].Piece = new ChessPiece(b[0, 0], Pieces.Rook, PieceColor.White);
            b[4, 0].Piece = new ChessPiece(b[4, 0], Pieces.King, PieceColor.White);
            b[7, 0].Piece = new ChessPiece(b[7, 0], Pieces.Rook, PieceColor.White);
        }
    }
}

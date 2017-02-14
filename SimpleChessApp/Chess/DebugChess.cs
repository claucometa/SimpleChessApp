using System.Text;


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
            b[1, 1].SetPiece(Pieces.Pawn, PieceColor.White);
            b[2, 3].SetPiece(Pieces.Pawn, PieceColor.Black);
            b[3, 1].SetPiece(Pieces.Pawn, PieceColor.White);

            b[4, 6].SetPiece(Pieces.Pawn, PieceColor.Black);
            b[5, 4].SetPiece(Pieces.Pawn, PieceColor.White);
            b[6, 6].SetPiece(Pieces.Pawn, PieceColor.Black);
        }

        internal void TestSinglePiece(Pieces x)
        {
            b.ClearBoard();
            b[4, 4].SetPiece(x, PieceColor.White);
        }

        internal void TestCastling()
        {
            b.ClearBoard();
            b[0, 7].SetPiece(Pieces.Rook, PieceColor.Black);
            b[4, 7].SetPiece(Pieces.King, PieceColor.Black);
            b[7, 7].SetPiece(Pieces.Rook, PieceColor.Black);

            b[0, 0].SetPiece(Pieces.Rook, PieceColor.White);
            b[4, 0].SetPiece(Pieces.King, PieceColor.White);
            b[7, 0].SetPiece(Pieces.Rook, PieceColor.White);
        }
    }
}

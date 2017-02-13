using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public class Board : Panel
    {
        // Avoid accessing directly, use this[,]
        // instead, with indexes ranging from 1 to 8
        public Square[,] Squares = new Square[8, 8];

        public Square this[int File, int Rank]
        {
            get
            {
                return Squares[File, Rank];
            }
        }

        public void Build()
        {
            #region Set Board
            var w = Width / 8;
            var h = Height / 8;
            int count = 1;
            bool isBlack;

            for (int rank = 0; rank < 8; rank++)
            {
                isBlack = (count++ % 2) == 0;
                for (int file = 0; file < 8; file++)
                {
                    var x = new Square(file, rank);
                    x.IsBlackSquare = isBlack;
                    isBlack = (count++ % 2) == 0;
                    x.Width = w;
                    x.Height = h;
                    x.Location = new Point(file * w, Math.Abs(rank - 7) * h);
                    Squares[file, rank] = x;
                    Controls.Add(x);
                }
            }
            #endregion

            setBlackPieces();

            setWhitePieces();
        }

        internal void Restart()
        {
            ClearBoard();
            setBlackPieces();
            setWhitePieces();
        }

        internal void ClearBoard()
        {
            for (int i = 0; i < 8; i++)
                for (int x = 0; x < 8; x++)
                    Squares[i, x].SetPiece(Pieces.None, PieceColor.None);
        }

        private void setBlackPieces()
        {
            for (int i = 0; i < 8; i++)
                this[i, 6].SetPiece(Pieces.Pawn, PieceColor.Black);

            this[0, 7].SetPiece(Pieces.Rook, PieceColor.Black);
            this[1, 7].SetPiece(Pieces.Knight, PieceColor.Black);
            this[2, 7].SetPiece(Pieces.Bishop, PieceColor.Black);
            this[3, 7].SetPiece(Pieces.Queen, PieceColor.Black);
            this[4, 7].SetPiece(Pieces.King, PieceColor.Black);
            this[5, 7].SetPiece(Pieces.Bishop, PieceColor.Black);
            this[6, 7].SetPiece(Pieces.Knight, PieceColor.Black);
            this[7, 7].SetPiece(Pieces.Rook, PieceColor.Black);
        }

        private void setWhitePieces()
        {
            for (int i = 0; i < 8; i++)
                this[i, 1].SetPiece(Pieces.Pawn, PieceColor.White);

            this[0, 0].SetPiece(Pieces.Rook, PieceColor.White);
            this[1, 0].SetPiece(Pieces.Knight, PieceColor.White);
            this[2, 0].SetPiece(Pieces.Bishop, PieceColor.White);
            this[3, 0].SetPiece(Pieces.Queen, PieceColor.White);
            this[4, 0].SetPiece(Pieces.King, PieceColor.White);
            this[5, 0].SetPiece(Pieces.Bishop, PieceColor.White);
            this[6, 0].SetPiece(Pieces.Knight, PieceColor.White);
            this[7, 0].SetPiece(Pieces.Rook, PieceColor.White);
        }

        #region DEBUG
        internal void TestPassant()
        {
            ClearBoard();
            Squares[1, 1].SetPiece(Pieces.Pawn, PieceColor.White);
            Squares[2, 3].SetPiece(Pieces.Pawn, PieceColor.Black);
            Squares[3, 1].SetPiece(Pieces.Pawn, PieceColor.White);

            Squares[4, 6].SetPiece(Pieces.Pawn, PieceColor.Black);
            Squares[5, 4].SetPiece(Pieces.Pawn, PieceColor.White);
            Squares[6, 6].SetPiece(Pieces.Pawn, PieceColor.Black);
        }

        internal void TestSinglePiece(Pieces x)
        {
            ClearBoard();
            Squares[4, 4].SetPiece(x, PieceColor.Black);
        }

        internal void TestCastling()
        {
            ClearBoard();
            Squares[0, 7].SetPiece(Pieces.Rook, PieceColor.Black);
            Squares[4, 7].SetPiece(Pieces.King, PieceColor.Black);
            Squares[7, 7].SetPiece(Pieces.Rook, PieceColor.Black);

            Squares[0, 0].SetPiece(Pieces.Rook, PieceColor.White);
            Squares[4, 0].SetPiece(Pieces.King, PieceColor.White);
            Squares[7, 0].SetPiece(Pieces.Rook, PieceColor.White);
        }
        #endregion
    }
}

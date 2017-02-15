using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public class Board : Panel
    {
        public Square[,] Squares = new Square[8, 8];

        public BindingList<ChessPiece> WhitePieces = new BindingList<ChessPiece>();
        public BindingList<ChessPiece> BlackPieces = new BindingList<ChessPiece>();

        int idd = 0;
        public int id
        {
            get
            {
                return ++idd;
            }
        }

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
                    Squares[i, x].ClearSquare();

            WhitePieces.Clear();
            BlackPieces.Clear();
        }

        private void setBlackPieces()
        {
            for (int i = 0; i < 8; i++)
            {
                var p = new ChessPiece(Pieces.Pawn, PieceColor.Black);
                this[i, 6].SetPiece(p);
                BlackPieces.Add(p);
            }

            this[0, 7].SetPiece(new ChessPiece(Pieces.Rook, PieceColor.Black));
            this[1, 7].SetPiece(new ChessPiece(Pieces.Knight, PieceColor.Black));
            this[3, 7].SetPiece(new ChessPiece(Pieces.Bishop, PieceColor.Black));
            this[4, 7].SetPiece(new ChessPiece(Pieces.Queen, PieceColor.Black));
            this[5, 7].SetPiece(new ChessPiece(Pieces.King, PieceColor.Black));
            this[6, 7].SetPiece(new ChessPiece(Pieces.Bishop, PieceColor.Black));
            this[2, 7].SetPiece(new ChessPiece(Pieces.Knight, PieceColor.Black));
            this[7, 7].SetPiece(new ChessPiece(Pieces.Rook, PieceColor.Black));

            for (int i = 0; i < 8; i++) BlackPieces.Add(this[i, 7].Piece);
        }

        private void setWhitePieces()
        {
            for (int i = 0; i < 8; i++)
            {
                var p = new ChessPiece(Pieces.Pawn, PieceColor.White);
                this[i, 1].SetPiece(p);
                WhitePieces.Add(p);
            }

            this[0, 0].SetPiece(new ChessPiece(Pieces.Rook, PieceColor.White));
            this[1, 0].SetPiece(new ChessPiece(Pieces.Knight, PieceColor.White));
            this[3, 0].SetPiece(new ChessPiece(Pieces.Bishop, PieceColor.White));
            this[4, 0].SetPiece(new ChessPiece(Pieces.Queen, PieceColor.White));
            this[5, 0].SetPiece(new ChessPiece(Pieces.King, PieceColor.White));
            this[6, 0].SetPiece(new ChessPiece(Pieces.Bishop, PieceColor.White));
            this[2, 0].SetPiece(new ChessPiece(Pieces.Knight, PieceColor.White));
            this[7, 0].SetPiece(new ChessPiece(Pieces.Rook, PieceColor.White));

            for (int i = 0; i < 8; i++) WhitePieces.Add(this[i, 0].Piece);
        }

        internal void RemovePiece(Square from)
        {
            if (from.Piece.Color == PieceColor.Black)
                BlackPieces.Remove(from.Piece);

            if (from.Piece.Color == PieceColor.White)
                WhitePieces.Remove(from.Piece);
        }
    }
}

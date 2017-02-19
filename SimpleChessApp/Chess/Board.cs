using System;
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
        public BindingList<ChessPiece> WhiteCaptured = new BindingList<ChessPiece>();
        public BindingList<ChessPiece> BlackCaptured = new BindingList<ChessPiece>();

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

        public Square this[Square x]
        {
            get
            {
                return Squares[x.File, x.Rank];
            }
        }

        public void Build(Panel p)
        {
            #region Set Board
            var w = p.Width / 8;
            var h = p.Height / 8;
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
                    Squares[i, x].Piece = null;

            WhitePieces.Clear();
            BlackPieces.Clear();
        }

        void setBlackPieces()
        {
            for (int i = 0; i < 8; i++) addBlack(i, 6, Pieces.Pawn);
            addBlack(0, 7, Pieces.Rook);
            addBlack(1, 7, Pieces.Knight);
            addBlack(2, 7, Pieces.Bishop);
            addBlack(3, 7, Pieces.Queen);
            addBlack(4, 7, Pieces.King);
            addBlack(5, 7, Pieces.Bishop);
            addBlack(6, 7, Pieces.Knight);
            addBlack(7, 7, Pieces.Rook);
        }

        void setWhitePieces()
        {
            for (int i = 0; i < 8; i++) addWhite(i, 1, Pieces.Pawn);
            addWhite(0, 0, Pieces.Rook);
            addWhite(1, 0, Pieces.Knight);
            addWhite(2, 0, Pieces.Bishop);
            addWhite(3, 0, Pieces.Queen);
            addWhite(4, 0, Pieces.King);
            addWhite(5, 0, Pieces.Bishop);
            addWhite(6, 0, Pieces.Knight);
            addWhite(7, 0, Pieces.Rook);
        }

        void add(int v1, int v2, Pieces p, PieceColor c)
        {
            var x = this[v1, v2];
            x.Piece = new ChessPiece(x, p, c);
            if (c == PieceColor.White) WhitePieces.Add(x.Piece);
            if (c == PieceColor.Black) BlackPieces.Add(x.Piece);
        }

        void addWhite(int v1, int v2, Pieces p)
        {
            add(v1, v2, p, PieceColor.White);
        }

        void addBlack(int v1, int v2, Pieces p)
        {
            add(v1, v2, p, PieceColor.Black);
        }

        internal void RemovePiece(ChessPiece x)
        {
            if (x.Color == PieceColor.Black)
            {
                BlackCaptured.Add(x);
                BlackPieces.Remove(x);
            }

            if (x.Color == PieceColor.White)
            {
                WhitePieces.Remove(x);
                WhiteCaptured.Add(x);
            }
        }
    }
}

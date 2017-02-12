using System;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Board : UserControl
    {
        // Avoid accessing directly, use this[,]
        // instead, with indexes ranging from 1 to 8
        public Square[,] Squares = new Square[8, 8];

        public Square this[int File, int Rank]
        {
            get
            {
                // The board is settled from up-left-down
                // so it's need to be 'inverted' from down-left-up
                // reflecting the REAL positions of the squares
                var f = File - 1;
                var x = Math.Abs(Rank - 8);
                return Squares[f, x];
            }
        }

        public Board()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            #region Build Squares
            int count = 1;
            bool isBlack;
            for (int rank = 0; rank < 8; rank++)
            {
                isBlack = (count++ % 2) == 0;
                for (int file = 0; file < 8; file++)
                {
                    var x = new Square(rank, file);
                    x.IsBlackSquare = isBlack;
                    isBlack = (count++ % 2) == 0;
                    Squares[rank, file] = x;
                    this.tableLayoutPanel1.Controls.Add(x, rank, file);
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
            {
                for (int x = 0; x < 8; x++)
                {
                    Squares[i, x].SetPiece(Pieces.None, PieceColor.White);
                }
            }
        }

        private void setWhitePieces()
        {
            for (int i = 1; i < 9; i++)
                this[i, 2].SetPiece(Pieces.Pawn, PieceColor.White);

            this[1, 1].SetPiece(Pieces.Rook, PieceColor.White);
            this[2, 1].SetPiece(Pieces.Knight, PieceColor.White);
            this[3, 1].SetPiece(Pieces.Bishop, PieceColor.White);
            this[4, 1].SetPiece(Pieces.Queen, PieceColor.White);
            this[5, 1].SetPiece(Pieces.King, PieceColor.White);
            this[6, 1].SetPiece(Pieces.Bishop, PieceColor.White);
            this[7, 1].SetPiece(Pieces.Knight, PieceColor.White);
            this[8, 1].SetPiece(Pieces.Rook, PieceColor.White);
        }

        private void setBlackPieces()
        {
            for (int i = 1; i < 9; i++)
                this[i, 7].SetPiece(Pieces.Pawn, PieceColor.Black);

            this[1, 8].SetPiece(Pieces.Rook, PieceColor.Black);
            this[2, 8].SetPiece(Pieces.Knight, PieceColor.Black);
            this[3, 8].SetPiece(Pieces.Bishop, PieceColor.Black);
            this[4, 8].SetPiece(Pieces.Queen, PieceColor.Black);
            this[5, 8].SetPiece(Pieces.King, PieceColor.Black);
            this[6, 8].SetPiece(Pieces.Bishop, PieceColor.Black);
            this[7, 8].SetPiece(Pieces.Knight, PieceColor.Black);
            this[8, 8].SetPiece(Pieces.Rook, PieceColor.Black);
        }

        #region DEBUG
        internal void TestPassant()
        {
            ClearBoard();
            Squares[1, 6].SetPiece(Pieces.Pawn, PieceColor.White);
            Squares[2, 4].SetPiece(Pieces.Pawn, PieceColor.Black);
            Squares[3, 6].SetPiece(Pieces.Pawn, PieceColor.White);

            Squares[4, 1].SetPiece(Pieces.Pawn, PieceColor.Black);
            Squares[5, 3].SetPiece(Pieces.Pawn, PieceColor.White);
            Squares[6, 1].SetPiece(Pieces.Pawn, PieceColor.Black);
        }

        internal void TestSinglePiece(Pieces x)
        {
            ClearBoard();
            Squares[4, 4].SetPiece(x, PieceColor.Black);
        }

        internal void TestCastling()
        {
            ClearBoard();
            Squares[0, 0].SetPiece(Pieces.Rook, PieceColor.Black);
            Squares[4, 0].SetPiece(Pieces.King, PieceColor.Black);
            Squares[7, 0].SetPiece(Pieces.Rook, PieceColor.Black);

            Squares[0, 7].SetPiece(Pieces.Rook, PieceColor.White);
            Squares[4, 7].SetPiece(Pieces.King, PieceColor.White);
            Squares[7, 7].SetPiece(Pieces.Rook, PieceColor.White);
        }
        #endregion
    }
}

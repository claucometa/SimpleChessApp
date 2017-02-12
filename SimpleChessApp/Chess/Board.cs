using System;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Board : UserControl
    {
        Square[,] Squares = new Square[8, 8];

        public Square this[int File, int Rank]
        {
            get
            {
                return Squares[File, Rank];
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
            for (int i = 0; i < 8; i++)
                Squares[i, 6].SetPiece(Pieces.Pawn, PieceColor.White);

            Squares[0, 7].SetPiece(Pieces.Rook, PieceColor.White);
            Squares[1, 7].SetPiece(Pieces.Knight, PieceColor.White);
            Squares[2, 7].SetPiece(Pieces.Bishop, PieceColor.White);
            Squares[3, 7].SetPiece(Pieces.Queen, PieceColor.White);
            Squares[4, 7].SetPiece(Pieces.King, PieceColor.White);
            Squares[5, 7].SetPiece(Pieces.Bishop, PieceColor.White);
            Squares[6, 7].SetPiece(Pieces.Knight, PieceColor.White);
            Squares[7, 7].SetPiece(Pieces.Rook, PieceColor.White);
        }

        private void setBlackPieces()
        {
            for (int i = 0; i < 8; i++)
                Squares[i, 1].SetPiece(Pieces.Pawn, PieceColor.Black);

            Squares[0, 0].SetPiece(Pieces.Rook, PieceColor.Black);
            Squares[1, 0].SetPiece(Pieces.Knight, PieceColor.Black);
            Squares[2, 0].SetPiece(Pieces.Bishop, PieceColor.Black);
            Squares[3, 0].SetPiece(Pieces.Queen, PieceColor.Black);
            Squares[4, 0].SetPiece(Pieces.King, PieceColor.Black);
            Squares[5, 0].SetPiece(Pieces.Bishop, PieceColor.Black);
            Squares[6, 0].SetPiece(Pieces.Knight, PieceColor.Black);
            Squares[7, 0].SetPiece(Pieces.Rook, PieceColor.Black);
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

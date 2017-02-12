using System;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Board : UserControl
    {
        Square[,] Squares = new Square[8, 8];
        bool isBlack;

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

        private void setWhitePieces()
        {
            for (int i = 0; i < 8; i++)
                Squares[i, 6].SetPiece(Pieces.Pawn, false);

            Squares[0, 7].SetPiece(Pieces.Rook, false);
            Squares[1, 7].SetPiece(Pieces.Knight, false);
            Squares[2, 7].SetPiece(Pieces.Bishop, false);
            Squares[3, 7].SetPiece(Pieces.Queen, false);
            Squares[4, 7].SetPiece(Pieces.King, false);
            Squares[5, 7].SetPiece(Pieces.Bishop, false);
            Squares[6, 7].SetPiece(Pieces.Knight, false);
            Squares[7, 7].SetPiece(Pieces.Rook, false);
        }

        internal void Restart()
        {
            clearBoard();
            setBlackPieces();
            setWhitePieces();
        }

        internal void TestPassant()
        {
            clearBoard();
            Squares[1, 6].SetPiece(Pieces.Pawn, false);
            Squares[2, 4].SetPiece(Pieces.Pawn, true);
            Squares[3, 6].SetPiece(Pieces.Pawn, false);

            Squares[4, 1].SetPiece(Pieces.Pawn, true);
            Squares[5, 3].SetPiece(Pieces.Pawn, false);
            Squares[6, 1].SetPiece(Pieces.Pawn, true);
        }

        private void clearBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Squares[i, x].SetPiece(Pieces.None, false);
                }
            }
        }

        internal void TestSinglePiece(Pieces x)
        {
            clearBoard();
            Squares[4, 4].SetPiece(x, true);
        }

        private void setBlackPieces()
        {
            for (int i = 0; i < 8; i++)
                Squares[i, 1].SetPiece(Pieces.Pawn, true);

            Squares[0, 0].SetPiece(Pieces.Rook, true);
            Squares[1, 0].SetPiece(Pieces.Knight, true);
            Squares[2, 0].SetPiece(Pieces.Bishop, true);
            Squares[3, 0].SetPiece(Pieces.Queen, true);
            Squares[4, 0].SetPiece(Pieces.King, true);
            Squares[5, 0].SetPiece(Pieces.Bishop, true);
            Squares[6, 0].SetPiece(Pieces.Knight, true);
            Squares[7, 0].SetPiece(Pieces.Rook, true);
        }

        internal void TestCastling()
        {
            clearBoard();
            Squares[0, 0].SetPiece(Pieces.Rook, true);
            Squares[4, 0].SetPiece(Pieces.King, true);
            Squares[7, 0].SetPiece(Pieces.Rook, true);

            Squares[0, 7].SetPiece(Pieces.Rook, false);
            Squares[4, 7].SetPiece(Pieces.King, false);
            Squares[7, 7].SetPiece(Pieces.Rook, false);
        }
    }
}

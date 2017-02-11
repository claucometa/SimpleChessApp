using System;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Board : UserControl
    {
        public Square[,] Squares = new Square[8, 8];
        bool isBlack;

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

            #region Set Black Pieces
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
            #endregion

            #region Set White Pieces
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
            #endregion
        }
    }
}

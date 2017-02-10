using System;
using System.Windows.Forms;

namespace SimpleChessApp
{
    public partial class Board : UserControl
    {
        Square[,] squares = new Square[8, 8];
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
                    squares[rank, file] = x;
                    this.tableLayoutPanel1.Controls.Add(x, rank, file);
                }
            }
            #endregion

            #region Set Black Pieces
            for (int i = 0; i < 8; i++)
                squares[i, 1].SetPiece(ChessSet.Pieces.Pawn, true);

            squares[0, 0].SetPiece(ChessSet.Pieces.Rook, true);
            squares[1, 0].SetPiece(ChessSet.Pieces.Knight, true);
            squares[2, 0].SetPiece(ChessSet.Pieces.Bishop, true);
            squares[3, 0].SetPiece(ChessSet.Pieces.Queen, true);
            squares[4, 0].SetPiece(ChessSet.Pieces.King, true);
            squares[5, 0].SetPiece(ChessSet.Pieces.Bishop, true);
            squares[6, 0].SetPiece(ChessSet.Pieces.Knight, true);
            squares[7, 0].SetPiece(ChessSet.Pieces.Rook, true);
            #endregion

            #region Set White Pieces
            for (int i = 0; i < 8; i++)
                squares[i, 6].SetPiece(ChessSet.Pieces.Pawn, false);

            squares[0, 7].SetPiece(ChessSet.Pieces.Rook, false);
            squares[1, 7].SetPiece(ChessSet.Pieces.Knight, false);
            squares[2, 7].SetPiece(ChessSet.Pieces.Bishop, false);
            squares[3, 7].SetPiece(ChessSet.Pieces.Queen, false);
            squares[4, 7].SetPiece(ChessSet.Pieces.King, false);
            squares[5, 7].SetPiece(ChessSet.Pieces.Bishop, false);
            squares[6, 7].SetPiece(ChessSet.Pieces.Knight, false);
            squares[7, 7].SetPiece(ChessSet.Pieces.Rook, false);
            #endregion
        }
    }
}

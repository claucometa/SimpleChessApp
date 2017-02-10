using System;
using System.Windows.Forms;

namespace SimpleChessApp
{
    public partial class Board : UserControl
    {
        Square[,] squares = new Square[8, 8];

        public Board()
        {
            InitializeComponent();
        }

        bool isblack;

        protected override void OnLoad(EventArgs e)
        {
            int isBlack = 1;

            for (int z = 0; z < 8; z++)
            {
                isblack = (isBlack++ % 2) == 0;
                for (int w = 0; w < 8; w++)
                {
                    var x = new Square();
                    x.IsBlackSquare = isblack;
                    isblack = (isBlack++ % 2) == 0;
                    squares[w, z] = x;
                    this.tableLayoutPanel1.Controls.Add(x, w, z);
                }
            }

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

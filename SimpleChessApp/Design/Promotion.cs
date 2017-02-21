using SimpleChessApp.Game;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp
{
    public partial class Promotion : Form
    {
        public Pieces Piece;

        public Promotion(Point p)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.Manual;
            Location = p;

            square1.Piece = new ChessPiece(Pieces.Queen, PieceColor.White);
            square2.Piece = new ChessPiece(Pieces.Rook, PieceColor.White);
            square3.Piece = new ChessPiece(Pieces.Knight, PieceColor.White);
            square4.Piece = new ChessPiece(Pieces.Bishop, PieceColor.White);

            square1.Click += _Click;
            square2.Click += _Click;
            square3.Click += _Click;
            square4.Click += _Click;
        }

        private void _Click(object sender, MouseEventArgs e)
        {
            var x = sender as SimpleSquare;
            Piece = x.Piece.Kind;
            Close();
        }
    }
}

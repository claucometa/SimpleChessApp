using System.ComponentModel;
using System.Drawing;

namespace SimpleChessApp
{
    public partial class ChessSet : Component
    {
        public ChessSet()
        {
            InitializeComponent();
        }

        public ChessSet(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public Image GetPiece(Pieces name, bool IsBlack)
        {
            var i = ((int)name) - 1;

            return IsBlack ? blackList.Images[i] : whiteList.Images[i];
        }

        public enum Pieces
        {
            None,
            Pawn,
            Knight,
            Bishop,
            Rook,
            King,
            Queen,
        }
    }
}

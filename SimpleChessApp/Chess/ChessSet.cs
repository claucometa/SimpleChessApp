using System.ComponentModel;
using System.Drawing;

namespace SimpleChessApp
{
    /// <summary>
    /// Has the method GetPiece(name, isBlack)
    /// </summary>      
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

        /// <summary>
        /// Returns the image of a chess piece
        /// </summary> 
        /// <param name="name">The name of the piece</param>
        /// <param name="IsBlack">The color of the piece</param>
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

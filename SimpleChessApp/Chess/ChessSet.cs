using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    /// <summary>
    /// Has the method GetPiece(name, isBlack)
    /// </summary>      
    public partial class ChessSet : Component
    {
        public event EventHandler Promoted;

        public ChessSet()
        {
            InitializeComponent();

            #region Pawn Promotion Menu Initializer
            queenToolStripMenuItem.Tag = Pieces.Queen;
            knightToolStripMenuItem.Tag = Pieces.Knight;
            rookToolStripMenuItem.Tag = Pieces.Rook;
            bishopToolStripMenuItem.Tag = Pieces.Bishop;
            queenToolStripMenuItem.Click += QueenToolStripMenuItem_Click;
            knightToolStripMenuItem.Click += QueenToolStripMenuItem_Click;
            rookToolStripMenuItem.Click += QueenToolStripMenuItem_Click;
            bishopToolStripMenuItem.Click += QueenToolStripMenuItem_Click;
            #endregion
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

        internal void ShowPieceSelector(Control x)
        {
            pawnPromotion.Show(x.Parent, x.Location);
        }

        private void QueenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = (ToolStripMenuItem)sender;
            Promoted?.Invoke((Pieces)x.Tag, e);
        }
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

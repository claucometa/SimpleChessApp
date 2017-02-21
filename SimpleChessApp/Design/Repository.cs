using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleChessApp.Game
{
    public partial class Repository : Component
    {
        public Square PromotedSquare;
        public Board ChessBoard;

        public Repository()
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

        #region Pawn Promotion
        /// <summary>
        /// The param x is just for location purpuses
        /// </summary>
        /// <param name="x"></param>
        internal void ShowPieceSelector(Control x)
        {
            PawnPromotionDialog.Show(x.Parent, x.Location);
        }
        #endregion

        private void QueenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = (Pieces)((ToolStripMenuItem)sender).Tag;
            PromotedSquare.Piece.Kind = x;
            ChessBoard[PromotedSquare].Piece = PromotedSquare.Piece;

            if (PromotedSquare.Piece.Color == PieceColor.White)
            {
                var w = ChessBoard.WhitePieces[PromotedSquare.Piece.Id];
                if (w != null) w.Kind = x;
            }

            if (PromotedSquare.Piece.Color == PieceColor.Black)
            {
                var w = ChessBoard.BlackPieces[PromotedSquare.Piece.Id];
                if (w != null) w.Kind = x;
            }
        }

        /// <summary>
        /// Returns the image of a chess piece
        /// </summary> 
        /// <param name="name">The name of the piece</param>
        /// <param name="color">The color of the piece</param>
        public Image GetPiece(Pieces name, PieceColor color)
        {
            var i = ((int)name) - 1;
            //return color == PieceColor.Black ? BlackPieces.Images[i] : WhitePieces.Images[i];
            return color == PieceColor.Black ? imageList1.Images[i] : imageList2.Images[i];
        }


        public Repository(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}

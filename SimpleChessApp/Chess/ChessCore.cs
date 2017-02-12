using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    /// <summary>
    /// Has the method GetPiece(name, isBlack)
    /// </summary>      
    public partial class ChessCore : Component
    {
        public event EventHandler GameStatus;

        public bool AllowPassant;
        public bool IsPassantActive;
        public bool SwitchTurnOff;
        public bool WhiteCanCastleKingSide = true;
        public bool BlackCanCastleKingSide = true;
        public bool WhiteCanCastleQueenSide = true;
        public bool BlackCanCastleQueenSide = true;

        // White always starts so it makes sense to
        // call this variable like this once it's false
        public bool IsBlackPlaying;

        public ChessCore()
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

        public void ChangeTurn()
        {
            IsPassantActive = AllowPassant;
            if (!AllowPassant)
                IsPassantActive = false;
            AllowPassant = false;
            IsBlackPlaying = !IsBlackPlaying;
            GameStatus?.Invoke(this, null);
        }

        internal void RestartGame()
        {
            ChessBoard.Restart();
            resetFlags();
            GameStatus?.Invoke(this, null);
        }

        internal void TestPassant()
        {
            ChessBoard.TestPassant();
            resetFlags(true);
            GameStatus?.Invoke(this, null);
        }

        internal void TestSinglePiece(Pieces x)
        {
            ChessBoard.TestSinglePiece(x);
            resetFlags(true);
            GameStatus?.Invoke(this, null);
        }

        public ChessCore(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        internal void TestCastling()
        {
            ChessBoard.TestCastling();
            resetFlags(true);
            GameStatus?.Invoke(this, null);
        }

        void resetFlags(bool turn = false)
        {
            SwitchTurnOff = turn;
            WhiteCanCastleKingSide = true;
            BlackCanCastleKingSide = true;
            WhiteCanCastleQueenSide = true;
            BlackCanCastleQueenSide = true;
            IsBlackPlaying = false;
            IsPassantActive = false;
        }

        /// <summary>
        /// Returns the image of a chess piece
        /// </summary> 
        /// <param name="name">The name of the piece</param>
        /// <param name="IsBlack">The color of the piece</param>
        public Image GetPiece(Pieces name, bool IsBlack)
        {
            var i = ((int)name) - 1;

            return IsBlack ? BlackPieces.Images[i] : WhitePieces.Images[i];
        }

        internal void ShowPieceSelector(Control x)
        {
            PawnPromotionDialog.Show(x.Parent, x.Location);
        }

        private void QueenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var square = Square.PromotedSquare;
            square.SetPiece((Pieces)((ToolStripMenuItem)sender).Tag, square.IsBlack);
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
        GhostPawn,
    }
}

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
        public PieceColor WhosPlaying;
        public bool AllowPassant;
        public bool IsPassantActive;
        public bool SwitchTurnOff;
        public bool WhiteCanCastleKingSide;
        public bool BlackCanCastleKingSide;
        public bool WhiteCanCastleQueenSide;
        public bool BlackCanCastleQueenSide;

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

            resetFlags();
        }

        public void ChangeTurn()
        {
            IsPassantActive = AllowPassant;
            if (!AllowPassant)
                IsPassantActive = false;
            AllowPassant = false;

            if (WhosPlaying == PieceColor.Black)
                WhosPlaying = PieceColor.White;
            else
                WhosPlaying = PieceColor.Black;

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
            WhosPlaying = PieceColor.White;
            IsPassantActive = false;
        }

        /// <summary>
        /// Returns the image of a chess piece
        /// </summary> 
        /// <param name="name">The name of the piece</param>
        /// <param name="color">The color of the piece</param>
        public Image GetPiece(Pieces name, PieceColor color)
        {
            var i = ((int)name) - 1;
            return color == PieceColor.Black ? BlackPieces.Images[i] : WhitePieces.Images[i];
        }

        internal void ShowPieceSelector(Control x)
        {
            PawnPromotionDialog.Show(x.Parent, x.Location);
        }

        private void QueenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var square = Square.PromotedSquare;
            square.SetPiece((Pieces)((ToolStripMenuItem)sender).Tag, square.PieceColor);
        }
    }

    public enum PieceColor
    {
        None,
        Black,
        White
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    /// <summary>
    /// Has mostly everything
    /// </summary>      
    public partial class ChessCore : Component
    {
        public event EventHandler GameStatus;
        public PieceColor WhosPlaying;
        public bool AllowPassant;
        public bool IsPassantActive;
        public bool HasNoTurns;
        public bool WhiteCanCastleKingSide;
        public bool BlackCanCastleKingSide;
        public bool WhiteCanCastleQueenSide;
        public bool BlackCanCastleQueenSide;
        public BindingList<Annotattion> MoveList = new BindingList<Annotattion>();
        public BindingList<Annotattion> MoveList2 = new BindingList<Annotattion>();

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

        public void BuildBoard()
        {
            ChessBoard.Build();
        }

        public void ChangeTurn()
        {
            IsPassantActive = AllowPassant;
            if (!AllowPassant)
            {
                if (MoveValidation.GhostSquare != null)
                    if(MoveValidation.GhostSquare.Piece == Pieces.GhostPawn)
                        MoveValidation.GhostSquare.ClearSquare();
                IsPassantActive = false;
            }

            AllowPassant = false;

            if (WhosPlaying == PieceColor.Black)
                WhosPlaying = PieceColor.White;
            else
                WhosPlaying = PieceColor.Black;

            GameStatus?.Invoke(this, null);
        }

        public ChessCore(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        internal void RestartGame()
        {
            ChessBoard.Restart();
            resetFlags();
        }

        void resetFlags(bool turn = false)
        {
            HasNoTurns = turn;
            WhiteCanCastleKingSide = true;
            BlackCanCastleKingSide = true;
            WhiteCanCastleQueenSide = true;
            BlackCanCastleQueenSide = true;
            WhosPlaying = PieceColor.White;
            IsPassantActive = false;
            MoveList.Clear();
            MoveList2.Clear();
            GameStatus?.Invoke(this, null);
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

        internal void ShowPieceSelector(Control x)
        {
            PawnPromotionDialog.Show(x.Parent, x.Location);
        }

        private void QueenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var square = Square.PromotedSquare;
            square.SetPiece((Pieces)((ToolStripMenuItem)sender).Tag, square.PieceColor);
        }

        #region DEBUG 
        internal void TestPassant()
        {
            ChessBoard.TestPassant();
            resetFlags(true);
        }

        internal void TestSinglePiece(Pieces x)
        {
            ChessBoard.TestSinglePiece(x);
            resetFlags(true);
        }

        internal void TestCastling()
        {
            ChessBoard.TestCastling();
            resetFlags(true);
        }
        #endregion
    }

    public enum PieceColor
    {
        None, // <-- Required by GhostPawn
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
        GhostPawn // <-- Receives None Color
    }
}
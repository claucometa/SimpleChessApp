using System;
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
        static Square lastMove;
        public BindingList<Annotattion> MoveList = new BindingList<Annotattion>();
        public BindingList<Annotattion> MoveList2 = new BindingList<Annotattion>();

        private static void addMoveAnnotation(Square from, Square to)
        {
            if (ChessContext.Core.WhosPlaying == PieceColor.White)
                ChessContext.Core.MoveList.Add(new Annotattion(from, to));

            if (ChessContext.Core.WhosPlaying == PieceColor.Black)
                ChessContext.Core.MoveList2.Add(new Annotattion(from, to));
        }

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
                    if (MoveValidation.GhostSquare.Piece == Pieces.GhostPawn)
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
        /// Handles all logic of move and capture
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        internal void HandleUserAction(Square from, Square to)
        {
            if (ChessContext.Core.WhosPlaying == from.PieceColor ||
                        ChessContext.Core.HasNoTurns) // Controls player turn
            {
                #region Handle Move
                if (to.Piece == Pieces.None)
                {
                    if (from.Piece != Pieces.None)
                    {
                        if (new MoveValidation(from, to).Validate)
                        {
                            #region Set Castling Flags For Black
                            if (from.PieceColor == PieceColor.Black)
                            {
                                if (ChessContext.Core.BlackCanCastleKingSide || ChessContext.Core.BlackCanCastleQueenSide)
                                {
                                    if (from.Piece == Pieces.King)
                                    {
                                        ChessContext.Core.BlackCanCastleKingSide = false;
                                        ChessContext.Core.BlackCanCastleQueenSide = false;
                                    }

                                    else if (from.Piece == Pieces.Rook && from.File == 0)
                                        ChessContext.Core.BlackCanCastleQueenSide = false;

                                    else if (from.Piece == Pieces.Rook && from.File == 7)
                                        ChessContext.Core.BlackCanCastleKingSide = false;

                                }
                            }
                            #endregion

                            #region Set Castling Flags For White
                            if (from.PieceColor == PieceColor.White)
                            {
                                if (ChessContext.Core.WhiteCanCastleKingSide || ChessContext.Core.WhiteCanCastleQueenSide)
                                {
                                    if (from.Piece == Pieces.King)
                                    {
                                        ChessContext.Core.WhiteCanCastleKingSide = false;
                                        ChessContext.Core.WhiteCanCastleQueenSide = false;
                                    }

                                    else if (from.Piece == Pieces.Rook && from.File == 0)
                                        ChessContext.Core.WhiteCanCastleQueenSide = false;

                                    else if (from.Piece == Pieces.Rook && from.File == 7)
                                        ChessContext.Core.WhiteCanCastleKingSide = false;

                                }
                            }
                            #endregion

                            lastMove = to;
                            to.SetPiece(from.Piece, from.PieceColor);
                            from.ClearSquare();
                            addMoveAnnotation(from, to);
                            ChessContext.Core.ChangeTurn();
                            return;
                        }
                        else
                        {
                            //Invalidate square selection if the move is not allowed
                            //TODO: Display a toast message
                            return;
                        }
                    }
                }
                #endregion

                #region Handle Capture
                if (to.Piece != Pieces.None)
                {
                    if (from.Piece != Pieces.None)
                    {
                        if (new MoveValidation(from, to,
                            from.Piece == Pieces.Pawn).Validate)
                        {
                            // Avoid capturing piece of same color
                            if (from.PieceColor != to.PieceColor)
                            {
                                #region Handle Passant
                                if (ChessContext.Core.IsPassantActive &&
                   from.Piece == Pieces.Pawn && to.Piece == Pieces.GhostPawn)
                                {
                                    if (lastMove.PieceColor != from.PieceColor)
                                        lastMove.SetPiece(Pieces.None, PieceColor.None);
                                }
                                #endregion

                                to.SetPiece(from.Piece, from.PieceColor);
                                from.ClearSquare();
                                addMoveAnnotation(from, to);

                                if (to.Piece == Pieces.Pawn)
                                {
                                    if (to.PieceColor == PieceColor.Black
                                        && to.Rank == 0)
                                    {
                                        ChessContext.Core.ShowPieceSelector(from);
                                        Square.PromotedSquare = to;
                                    }

                                    if (to.PieceColor == PieceColor.White
                                        && to.Rank == 7)
                                    {
                                        ChessContext.Core.ShowPieceSelector(from);
                                        Square.PromotedSquare = to;
                                    }
                                }

                                ChessContext.Core.ChangeTurn();
                                return;
                            }
                        }
                    }
                }
                #endregion
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
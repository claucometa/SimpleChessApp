using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace SimpleChessApp.Chess
{
    public partial class ChessCore : Component
    {
        public event EventHandler NextTurn;
        public event EventHandler<ActionEventArgs> ActionChanged;
        public PieceColor WhosPlaying;
        public bool HasNoTurns;
        public bool WhiteCanCastleKingSide;
        public bool BlackCanCastleKingSide;
        public bool WhiteCanCastleQueenSide;
        public bool BlackCanCastleQueenSide;
        //static Square lastMove;
        public BindingList<Annotattion> MoveList = new BindingList<Annotattion>();
        public BindingList<Annotattion> MoveList2 = new BindingList<Annotattion>();
        public HighLightMoves highLight = new HighLightMoves();
        Square to, from;

        public static List<Square> GhostPawn;

        #region Contructors
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

            Square.FirstClick += Square_FirstClick;
            Square.SecondClick += Square_SecondClick;

            GhostPawn = new List<Square>();

            resetFlags();
        }

        private void Square_SecondClick(object sender, EventArgs e)
        {
            to = (Square)sender;

            if (highLight.MoveList.Count == 0)
            {
                ActionChanged(to, new ActionEventArgs(UserAction.None));
                return;
            }

            var move = highLight.MoveList.FirstOrDefault(t => t.Square == to);

            if (move == null)
            {
                ActionChanged(to, new ActionEventArgs(UserAction.Invalid_Move));
                return;
            }

            if (move.Kind == UserAction.Move)
            {
                to.SetPiece(from.Piece, from.PieceColor);
                from.ClearSquare();
            }

            if (move.Kind == UserAction.Capture)
            {
                to.SetPiece(from.Piece, from.PieceColor);
                from.ClearSquare();
            }

            if (to.Piece == Pieces.Pawn)
            {
                if (from.Rank == 1 || from.Rank == 6)
                {
                    if (to.Rank == 3 || to.Rank == 4)
                    {
                        var a = to.File;
                        var b = to.Rank + (to.PieceColor == PieceColor.White ? -1 : +1);
                        var sq = ChessContext.Core.ChessBoard[a, b];
                        GhostPawn.Add(sq);
                        sq.Piece = Pieces.GhostPawn;
                        sq.PieceColor = to.PieceColor;
                        // sq.BackColor = Color.Purple; Debug purposes
                    }
                    else
                        destroyGhostPawn();
                }
                else
                    destroyGhostPawn();
            }
            else
                destroyGhostPawn();

            ActionChanged(to, new ActionEventArgs(move.Kind));
            ChangeTurn();
        }

        private static void destroyGhostPawn()
        {
            foreach (var item in ChessCore.GhostPawn)
            {
                if (item.Piece == Pieces.GhostPawn)
                    item.ClearSquare();
            }
            ChessCore.GhostPawn.Clear();
        }

        private void Square_FirstClick(object sender, EventArgs e)
        {
            from = (Square)sender;
            callAction(UserAction.Piece_Selected, from);
        }

        private void callAction(UserAction action, Square from)
        {
            ActionChanged(from, new ActionEventArgs(action));
        }

        public ChessCore(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// Call it first to build the board, then add the ChessBoard to a panel
        /// </summary>
        public void BuildBoard()
        {
            ChessBoard.Build();
        }

        public void ChangeTurn()
        {
            if (WhosPlaying == PieceColor.Black)
                WhosPlaying = PieceColor.White;
            else
                WhosPlaying = PieceColor.Black;

            if (GhostPawn.Count == 2)
            {
                GhostPawn[0].ClearSquare();
                GhostPawn.Remove(GhostPawn[0]);
            }

            NextTurn?.Invoke(this, null);
        }

        public void RestartGame()
        {
            resetFlags(); // always first
            ChessBoard.Restart();
        }

        void resetFlags(bool turn = false)
        {
            HasNoTurns = turn;
            WhiteCanCastleKingSide = true;
            BlackCanCastleKingSide = true;
            WhiteCanCastleQueenSide = true;
            BlackCanCastleQueenSide = true;
            WhosPlaying = PieceColor.White;
            highLight.Clear();
            destroyGhostPawn();
            MoveList.Clear();
            MoveList2.Clear();
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

        void addMoveAnnotation(Square from, Square to)
        {
            if (ChessContext.Core.WhosPlaying == PieceColor.White)
                ChessContext.Core.MoveList.Add(new Annotattion(from, to));

            if (ChessContext.Core.WhosPlaying == PieceColor.Black)
                ChessContext.Core.MoveList2.Add(new Annotattion(from, to));
        }

        #region Pawn Promotion
        internal void ShowPieceSelector(Control x)
        {
            PawnPromotionDialog.Show(x.Parent, x.Location);
        }

        private void QueenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var square = Square.PromotedSquare;
            square.SetPiece((Pieces)((ToolStripMenuItem)sender).Tag, square.PieceColor);
        }
        #endregion

        #region DEBUG 
        internal void TestPassant()
        {
            new DebugChess(ChessBoard).TestPassant();
            resetFlags(true);
        }

        internal void TestSinglePiece(Pieces x)
        {
            new DebugChess(ChessBoard).TestSinglePiece(x);
            resetFlags(true);
        }

        internal void TestCastling()
        {
            new DebugChess(ChessBoard).TestCastling();
            resetFlags(true);
        }
        #endregion
    }

    public class ActionEventArgs : EventArgs
    {
        public UserAction Action;

        public ActionEventArgs(UserAction u)
        {
            Action = u;
        }
    }

    public enum PieceColor
    {
        None, // <-- Used by empty squares or GhostPawn
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
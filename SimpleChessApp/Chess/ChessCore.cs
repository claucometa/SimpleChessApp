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
        PossibleMoves move;
        public bool HasNoTurns;
        public int TurnId = 1;
        public List<Square> PieceWhoChecked;
        public bool WhiteCanCastleKingSide;
        public bool BlackCanCastleKingSide;
        public bool WhiteCanCastleQueenSide;
        public bool BlackCanCastleQueenSide;
        Square to, from;
        public Square PromotedSquare;
        public List<Square> GhostPawn;
        public NotationManager notes = new NotationManager();
        //HighLightMoves checkDetector = new HighLightMoves();

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

            #region CheckDetection
            //if (PieceWhoChecked.Count > 0)
            //{
            //    var checkDetector = new HighLightMoves();
            //    var sq = ChessBoard[to.File, to.Rank];
            //    var old = sq.PieceColor;
            //    sq.PieceColor = PieceWhoChecked[0].PieceColor;
            //    // Debug Purposes
            //    sq.BackColor = Color.Purple;
            //    foreach (var item in PieceWhoChecked)
            //    {
            //        checkDetector.Go(item);
            //    }

            //    sq.PieceColor = old;
            //    var x = checkDetector.MoveList.FirstOrDefault(t => t.Square.Piece == Pieces.King);
            //    if (x != null)
            //    {
            //        move.Kind = UserAction.Check;
            //        return;
            //    }
            //}
            #endregion

            //checkDetector = new HighLightMoves();

            if (Square.light.MoveList.Count == 0)
            {
                ActionChanged(to, new ActionEventArgs(UserAction.None));
                return;
            }

            move = Square.light.MoveList.FirstOrDefault(t => t.Square == to);

            if (move == null)
            {
                ActionChanged(to, new ActionEventArgs(UserAction.Invalid_Move));
                return;
            }

            if (move.Kind == UserAction.Move)
            {
                to.Piece = from.Piece;
                to.Piece.Current = to;

                if (to.Piece.Name == Pieces.King)
                {
                    if (blackCanCastle || whiteCanCastle)
                        handleCastling();
                    shutOffCastling();
                }

                if (to.Piece.Name == Pieces.Rook)
                    shutOffCastling();

                from.Piece = null;

                addMoveNote();
            }

            destroyGhostPawn();

            if (move.Kind == UserAction.Capture)
            {
                ChessBoard.RemovePiece(to);
                to.Piece = from.Piece;
                to.Piece.Current = to;
                ChessBoard.AddCaptured(from.Piece);
                addMoveNote();
                from.Piece = null;
            }

            if (to.Piece.Name == Pieces.Pawn)
            {
                if (from.Rank == 1 || from.Rank == 6)
                {
                    if (to.Rank == 3 || to.Rank == 4)
                    {
                        var a = to.File;
                        var b = to.Rank + (to.Piece.Color == PieceColor.White ? -1 : +1);
                        var sq = ChessContext.Core.ChessBoard[a, b];
                        GhostPawn.Add(sq);
                        sq.IsGhost = true;
                        // Debug purposes
                        // sq.BackColor = Color.Purple;
                    }
                }

                if (to.Rank == 0 || to.Rank == 7)
                {
                    PromotedSquare = to;
                    ShowPieceSelector(from);
                }
            }

            #region Check Detection
            //if (to.PieceColor == PieceColor.Black)
            //{
            //    PieceWhoChecked = new List<Square>();
            //    var z = ChessBoard.WhitePieces.Where(t => t.PieceColor == PieceColor.White);
            //    foreach (var item in z)
            //    {
            //        // Check detector
            //        if (item.Piece != Pieces.King)
            //        {
            //            var checkDetector = new HighLightMoves();
            //            checkDetector.Go(item);
            //            // Debug Purposes
            //            // checkDetector.HighLightCheck();
            //            var x = checkDetector.MoveList.FirstOrDefault(t => t.Square.Piece == Pieces.King);
            //            if (x != null)
            //            {
            //                PieceWhoChecked.Add(item);
            //                move.Kind = UserAction.Check;
            //            }
            //        }
            //    }

            //    if (PieceWhoChecked.Count > 0)
            //        if (PieceWhoChecked.First().PieceColor != WhosPlaying)
            //            MessageBox.Show("oops");
            //}
            #endregion

            ActionChanged(to, new ActionEventArgs(move.Kind));

            ChangeTurn();
        }

        private bool blackCanCastle
        {
            get
            {
                return to.Piece.Color == PieceColor.Black
                            && BlackCanCastleQueenSide || BlackCanCastleKingSide;
            }
        }

        private bool whiteCanCastle
        {
            get
            {
                return to.Piece.Color == PieceColor.White && WhiteCanCastleKingSide || WhiteCanCastleQueenSide;
            }
        }

        private void handleCastling()
        {
            var isBlack = to.Piece.Color == PieceColor.Black;
            var m = isBlack ? 7 : 0;
            var castledKingSide = from.File - to.File == -2;
            var castledQueenSide = from.File - to.File == 2;

            if (castledKingSide)
            {
                var rook = ChessBoard[7, m].Piece;
                ChessBoard[7, m].Piece = null;
                ChessBoard[5, m].Piece = rook;
            }

            if (castledQueenSide)
            {
                var rook = ChessBoard[0, m].Piece;
                ChessBoard[0, m].Piece = null;
                ChessBoard[3, m].Piece = rook;
            }
        }

        private void shutOffCastling()
        {
            if (to.Piece.Color == PieceColor.Black)
            {
                if (blackCanCastle)
                {
                    BlackCanCastleQueenSide = false;
                    BlackCanCastleKingSide = false;
                }
            }
            else
            {
                if (whiteCanCastle)
                {
                    WhiteCanCastleQueenSide = false;
                    WhiteCanCastleKingSide = false;
                }
            }
        }

        private void destroyGhostPawn()
        {
            foreach (var item in GhostPawn)
            {
                if (item.IsGhost == true)
                    item.Piece = null;
            }
            GhostPawn.Clear();
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
            {
                WhosPlaying = PieceColor.Black;
                TurnId++;
            }

            if (GhostPawn.Count == 2)
            {
                GhostPawn[0].Piece = null;
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
            TurnId = 1;
            Square.light.Clear();
            PieceWhoChecked = new List<Square>();
            destroyGhostPawn();
            notes.Clear();
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

        void addMoveNote()
        {
            if (!ChessContext.Core.HasNoTurns)
            {
                if (ChessContext.Core.WhosPlaying == PieceColor.White)
                    notes.Moves.Add(new Turn { Id = ChessContext.Core.TurnId, x = new Notation(from, to) });
                else
                    notes.Moves.Last().y = new Notation(from, to);
            }
        }

        #region Pawn Promotion
        internal void ShowPieceSelector(Control x)
        {
            PawnPromotionDialog.Show(x.Parent, x.Location);
        }

        private void QueenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = (Pieces)((Control)sender).Tag;
            PromotedSquare.Piece = new ChessPiece(PromotedSquare, x, ChessContext.Core.WhosPlaying);
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
        Queen
    }
}
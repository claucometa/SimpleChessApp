using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace SimpleChessApp.Chess
{
    public partial class ChessCore : Component
    {
        // Turn handling
        public event EventHandler NextTurn;
        public event EventHandler<ActionEventArgs> ActionChanged;
        public PieceColor WhosPlaying;
        public bool DisableTurn;
        public int TurnId = 1;

        // Castling handling
        public bool WhiteCanCastleKingSide;
        public bool BlackCanCastleKingSide;
        public bool WhiteCanCastleQueenSide;
        public bool BlackCanCastleQueenSide;

        // Check handling
        public Square lastCheckPiece;
        public Square ghostCheckPiece;
        public HighLightMoves checks = new HighLightMoves(true);

        // Move handling
        PossibleMoves move;
        public Square LastMove;
        public Square PromotedSquare;
        public NotationManager Turns = new NotationManager();
        Square to, from;

        // Used by Square class
        public bool firstClick = true;
        public Square lastSquare;
        public HighLightMoves light = new HighLightMoves();

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

            resetFlags();
        }

        private void Square_SecondClick(object sender, EventArgs e)
        {
            to = (Square)sender;

            if (light.MoveList.Count == 0)
            {
                ActionChanged?.Invoke(to, new ActionEventArgs(UserAction.None));
                return;
            }

            move = light.MoveList.FirstOrDefault(t => t.Square == to);

            if (move == null)
            {
                ActionChanged?.Invoke(to, new ActionEventArgs(UserAction.Invalid_Move));
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

                if (to.Piece.Name == Pieces.Pawn)
                {
                    if ((from.Rank == 1 && to.Rank == 3) || (from.Rank == 6 && to.Rank == 4))
                        to.Piece.Passant = true;

                    if (to.Rank == 0 || to.Rank == 7)
                    {
                        PromotedSquare = to;
                        ShowPieceSelector(from);
                    }

                    // Capture passant
                    if (Math.Abs(from.File - to.File) == 1)
                    {
                        LastMove.Piece = null;
                        move.Kind = UserAction.Capture;
                    }
                }
            }
            else if (move.Kind == UserAction.Capture)
            {
                ChessBoard.RemovePiece(to.Piece);
                to.Piece = from.Piece;
                to.Piece.Current = to;
            }

            #region Detect Check
            if (to.Piece.Name != Pieces.King)
            {
                if (lastCheckPiece == null)
                {
                    checks.Clear();
                    checks.FindAllMoves(to);
                    checks.HighLightCheckStyle();
                    foreach (var item in checks.MoveList)
                    {
                        if (ChessBoard[item.Square].Piece == null) continue;
                        if (ChessBoard[item.Square].Piece.Name == Pieces.King)
                        {
                            move.Kind = UserAction.Check;
                            ghostCheckPiece = lastCheckPiece = to;
                            break;
                        }
                    }
                }
                else
                {
                    checks.Clear();
                    checks.FindAllMoves(lastCheckPiece);
                    checks.HighLightCheckStyle();
                    lastCheckPiece = null;

                    foreach (var item in checks.MoveList)
                    {
                        if (ChessBoard[item.Square].Piece == null) continue;
                        if (ChessBoard[item.Square].Piece.Name == Pieces.King)
                        {
                            move.Kind = UserAction.Check;

                            // Move back
                            from.Piece = to.Piece;
                            to.Piece = null;

                            checks.Clear();
                            checks.FindAllMoves(ghostCheckPiece);
                            checks.HighLightCheckStyle();
                            lastCheckPiece = null;
                            foreach (var item2 in checks.MoveList)
                            {
                                if (ChessBoard[item2.Square].Piece == null) continue;
                                if (ChessBoard[item2.Square].Piece.Name == Pieces.King)
                                {
                                    move.Kind = UserAction.Check;
                                    lastCheckPiece = ghostCheckPiece;
                                    return;
                                }
                            }
                            return;
                        }
                    }
                }
            }
            else
            {
                if (lastCheckPiece != null)
                {
                    // Move King
                    LastMove = to;
                    from.Piece = null;

                    checks.Clear();
                    checks.FindAllMoves(lastCheckPiece);
                    checks.HighLightCheckStyle();

                    foreach (var item in checks.MoveList)
                    {
                        if (ChessBoard[item.Square].Piece == null) continue;
                        if (ChessBoard[item.Square].Piece.Name == Pieces.King)
                        {
                            move.Kind = UserAction.Check;

                            // Move back
                            from.Piece = to.Piece;
                            to.Piece = null;

                            return;
                        }
                    }

                    lastCheckPiece = null;
                }
            }
            #endregion

            ActionChanged?.Invoke(to, new ActionEventArgs(move.Kind));
            ChangeTurn();
        }

        internal void MoveTurnBack()
        {
            throw new NotImplementedException();
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

        private void Square_FirstClick(object sender, EventArgs e)
        {
            from = (Square)sender;
            ActionChanged?.Invoke(from, new ActionEventArgs(UserAction.Piece_Selected));
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
        public void BuildBoard(Panel p)
        {
            ChessBoard.Build(p);
            p.Controls.Add(ChessBoard);
        }

        public void ChangeTurn()
        {
            addMoveNote();

            LastMove = to;
            from.Piece = null;

            if (WhosPlaying == PieceColor.Black)
                WhosPlaying = PieceColor.White;
            else
            {
                WhosPlaying = PieceColor.Black;
                TurnId++;
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
            DisableTurn = turn;
            WhiteCanCastleKingSide = true;
            BlackCanCastleKingSide = true;
            WhiteCanCastleQueenSide = true;
            BlackCanCastleQueenSide = true;
            WhosPlaying = PieceColor.White;
            TurnId = 1;
            lastCheckPiece = null;
            light.Clear(); // highlighted moves
            checks.Clear(); // highlighted checks
            Turns.Clear();
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
            if (!ChessContext.Core.DisableTurn)
            {
                if (ChessContext.Core.WhosPlaying == PieceColor.White)
                    Turns.Moves.Add(new Turn { Id = ChessContext.Core.TurnId, White = new Notation(from, to) });
                else
                    Turns.Moves.Last().Black = new Notation(from, to);
            }
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

        private void QueenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = (Pieces)((ToolStripMenuItem)sender).Tag;
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
}
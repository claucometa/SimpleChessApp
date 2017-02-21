using System;

namespace SimpleChessApp.Game
{
    public class ChessCore
    {
        public Board ChessBoard;

        // Turn handling
        public event EventHandler NextTurn;
        //public event EventHandler Action;
        public PieceColor WhosPlaying;
        public bool DisableTurn;
        public int TurnId = 1;

        // Check handling
        public Square lastCheckPiece;
        public Square ghostCheckPiece;
        //public HighLightMoves checks = new HighLightMoves(true);
        bool catchOnDeepCheck = false;

        // Move handling
        PossibleMoves move;
        public Square LastMove;

        public NotationManager Turns = new NotationManager();
        Square to, from;

        // Used by Square class
        public bool firstClick = true;
        public Square lastSquare;

        

        // public HighLightMoves light = new HighLightMoves();

        #region Contructors
        public ChessCore(Board b)
        {
            ChessBoard = b;
            //Square.FirstClick += Square_FirstClick;
            //Square.SecondClick += Square_SecondClick;
            resetFlags();
        }

        public ChessCore()
        {

        }

        private void Square_SecondClick(object sender, EventArgs e)
        {
            //to = (Square)sender;

            //if (light.MoveList.Count == 0)
            //{
            //    ActionChanged?.Invoke(to, new ActionEventArgs(UserAction.None));
            //    return;
            //}

            //move = light.MoveList.FirstOrDefault(t => t.Square == to);

            //if (move == null)
            //{
            //    ActionChanged?.Invoke(to, new ActionEventArgs(UserAction.Invalid_Move));
            //    return;
            //}

            //if (move.Kind == UserAction.Move)
            //{
            //    to.Piece = from.Piece;
            //    to.Piece.Current = to;

            //    if (to.Piece.Kind == Pieces.King)
            //    {
            //        if (blackCanCastle || whiteCanCastle)
            //            handleCastling();
            //        shutOffCastling();
            //    }

            //    if (to.Piece.Kind == Pieces.Rook)
            //        shutOffCastling();

            //    if (to.Piece.Kind == Pieces.Pawn)
            //    {
            //        if ((from.Rank == 1 && to.Rank == 3) || (from.Rank == 6 && to.Rank == 4))
            //            to.Piece.Passant = true;

            //        // Capture passant
            //        if (Math.Abs(from.File - to.File) == 1)
            //        {
            //            LastMove.Piece = null;
            //            move.Kind = UserAction.Capture;
            //        }
            //    }
            //}
            //else if (move.Kind == UserAction.Capture)
            //{
            //    ChessBoard.RemovePiece(to.Piece);
            //    to.Piece = from.Piece;
            //    to.Piece.Current = to;
            //}

            //if (move.Kind == UserAction.Move || move.Kind == UserAction.Capture)
            //{
            //    if (to.Piece.Kind == Pieces.Pawn)
            //    {
            //        if ((from.Rank == 1 && to.Rank == 3) || (from.Rank == 6 && to.Rank == 4))
            //            to.Piece.Passant = true;

            //        if (to.Rank == 0 || to.Rank == 7)
            //        {
            //            //PromotedSquare = to;
            //            //ShowPieceSelector(from);
            //        }
            //    }
            //}

            //var validMove = true;

            //if (!catchOnDeepCheck) validMove = detectCheck();

            //if (validMove)
            //{
            //    // Move piece
            //    LastMove = to;
            //    from.Piece = null;

            //    validMove = deepCheckDetect();
            //    catchOnDeepCheck = !validMove;
            //}

            //if (validMove)
            //{
            //    lastCheckPiece = null;
            //    ChangeTurn();
            //}
            //else
            //{
            //    // Cancel Move
            //    from.Piece = to.Piece;
            //    ChessPiece a;

            //    if (WhosPlaying == PieceColor.White)
            //    {
            //        a = ChessBoard.WhitePieces.FirstOrDefault(t => t.Id == from.Piece.Id);
            //        from.Piece.Current = to;
            //        a.Current = from;
            //    }

            //    if (WhosPlaying == PieceColor.Black)
            //    {
            //        a = ChessBoard.BlackPieces.FirstOrDefault(t => t.Id == from.Piece.Id);
            //        a.Current = from;
            //    }

            //    to.Piece = null;
            //    move.Kind = UserAction.Invalid_Move;
            //}

            //ActionChanged?.Invoke(to, new ActionEventArgs(move.Kind));
        }

        private bool deepCheckDetect()
        {
            //if (WhosPlaying == PieceColor.Black)
            //{
            //    checks.Clear();
            //    foreach (var piece in ChessBoard.WhitePieces)
            //    {
            //        checks.FindAllMoves(piece.Current);
            //        foreach (var item in checks.MoveList)
            //        {
            //            if (ChessBoard[item.Square].Piece == null) continue;
            //            if (ChessBoard[item.Square].Piece.Kind == Pieces.King)
            //            {
            //                move.Kind = UserAction.Check;
            //                lastCheckPiece = to;
            //                return false;
            //            }
            //        }
            //    }
            //}

            //if (WhosPlaying == PieceColor.White)
            //{
            //    checks.Clear();
            //    foreach (var piece in ChessBoard.BlackPieces)
            //    {
            //        checks.FindAllMoves(piece.Current);
            //        foreach (var item in checks.MoveList)
            //        {
            //            if (ChessBoard[item.Square].Piece == null) continue;
            //            if (ChessBoard[item.Square].Piece.Kind == Pieces.King)
            //            {
            //                move.Kind = UserAction.Check;
            //                lastCheckPiece = to;
            //                return false;
            //            }
            //        }
            //    }
            //}

            return true;
        }

        private bool detectCheck()
        {
            //if (to.Piece.Kind != Pieces.King)
            //{
            //    if (lastCheckPiece == null)
            //    {
            //        checks.Clear();
            //        checks.FindAllMoves(to);
            //        //checks.HighLightCheckStyle();
            //        foreach (var item in checks.MoveList)
            //        {
            //            if (ChessBoard[item.Square].Piece == null) continue;
            //            if (ChessBoard[item.Square].Piece.Kind == Pieces.King)
            //            {
            //                move.Kind = UserAction.Check;
            //                ghostCheckPiece = lastCheckPiece = to;
            //                break;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        checks.Clear();
            //        checks.FindAllMoves(lastCheckPiece);
            //        //checks.HighLightCheckStyle();
            //        lastCheckPiece = null;

            //        foreach (var item in checks.MoveList)
            //        {
            //            if (ChessBoard[item.Square].Piece == null) continue;
            //            if (ChessBoard[item.Square].Piece.Kind == Pieces.King)
            //            {
            //                move.Kind = UserAction.Check;
            //                checks.Clear();
            //                checks.FindAllMoves(ghostCheckPiece);
            //                //checks.HighLightCheckStyle();
            //                lastCheckPiece = null;
            //                foreach (var item2 in checks.MoveList)
            //                {
            //                    if (ChessBoard[item2.Square].Piece == null) continue;
            //                    if (ChessBoard[item2.Square].Piece.Kind == Pieces.King)
            //                    {
            //                        move.Kind = UserAction.Check;
            //                        lastCheckPiece = ghostCheckPiece;
            //                        return false;
            //                    }
            //                }
            //                return false;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    if (lastCheckPiece != null)
            //    {
            //        // Move King
            //        LastMove = to;
            //        from.Piece = null;

            //        checks.Clear();
            //        checks.FindAllMoves(lastCheckPiece);
            //        //checks.HighLightCheckStyle();

            //        foreach (var item in checks.MoveList)
            //        {
            //            if (ChessBoard[item.Square].Piece == null) continue;
            //            if (ChessBoard[item.Square].Piece.Kind == Pieces.King)
            //            {
            //                move.Kind = UserAction.Check;
            //                return false;
            //            }
            //        }

            //        lastCheckPiece = null;
            //    }
            //}

            return true;
        }

        internal void MoveTurnBack()
        {
            throw new NotImplementedException();
        }

        //private bool blackCanCastle
        //{
        //    get
        //    {
        //        return to.Piece.Color == PieceColor.Black
        //                    && BlackCanCastleQueenSide || BlackCanCastleKingSide;
        //    }
        //}

        //private bool whiteCanCastle
        //{
        //    get
        //    {
        //        return to.Piece.Color == PieceColor.White && WhiteCanCastleKingSide || WhiteCanCastleQueenSide;
        //    }
        //}

        private void handleCastling()
        {
            var isBlack = to.Piece.Color == PieceColor.Black;
            var m = isBlack ? 7 : 0;
            var castledKingSide = from.File - to.File == -2;
            var castledQueenSide = from.File - to.File == 2;
            ChessPiece rook = null;

            if (castledKingSide)
            {
                rook = ChessBoard[7, m].Piece;
                ChessBoard[7, m].Piece = null;
                ChessBoard[5, m].Piece = rook;
                rook.Current = ChessBoard[5, m];
            }

            if (castledQueenSide)
            {
                rook = ChessBoard[0, m].Piece;
                ChessBoard[0, m].Piece = null;
                ChessBoard[3, m].Piece = rook;
                rook.Current = ChessBoard[3, m];
            }
        }

        //private void shutOffCastling()
        //{
        //    if (to.Piece.Color == PieceColor.Black)
        //    {
        //        if (blackCanCastle)
        //        {
        //            BlackCanCastleQueenSide = false;
        //            BlackCanCastleKingSide = false;
        //        }
        //    }
        //    else
        //    {
        //        if (whiteCanCastle)
        //        {
        //            WhiteCanCastleQueenSide = false;
        //            WhiteCanCastleKingSide = false;
        //        }
        //    }
        //}

        //private void Square_FirstClick(object sender, EventArgs e)
        //{
        //    from = (Square)sender;
        //    ActionChanged?.Invoke(from, new ActionEventArgs(UserAction.Piece_Selected));
        //}

        #endregion

        /// <summary>
        /// Call it first to build the board, then add the ChessBoard to a panel
        /// </summary>        

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

            //NextTurn?.Invoke(this, null);
        }

        public void RestartGame()
        {
            resetFlags(); // always first
            ChessBoard.Restart();
        }

        void resetFlags(bool turn = false)
        {
            DisableTurn = turn;
            //WhiteCanCastleKingSide = true;
            //BlackCanCastleKingSide = true;
            //WhiteCanCastleQueenSide = true;
            //BlackCanCastleQueenSide = true;
            WhosPlaying = PieceColor.White;
            TurnId = 1;
            lastCheckPiece = null;
            //light.Clear(); // highlighted moves
            //checks.Clear(); // highlighted checks
            Turns.Clear();
        }

        void addMoveNote()
        {
            if (!ChessContext.Core[0].DisableTurn)
            {
                //if (ChessContext.Core.WhosPlaying == PieceColor.White)
                //    Turns.Moves.Add(new Turn { Id = ChessContext.Core.TurnId, White = new Notation(from, to) });
                //else
                //    Turns.Moves.Last().Black = new Notation(from, to);
            }
        }

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

        internal void TestPromotion()
        {
            new DebugChess(ChessBoard).TestPromotion();
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
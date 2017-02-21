using System;

namespace SimpleChessApp.Game
{
    public class ChessCore
    {
        public Board ChessBoard;
        public PieceColor WhosPlaying;
        public bool DisableTurn;
        public int TurnId = 1;

        // Check handling
        public Square lastCheckPiece;
        public Square ghostCheckPiece;

        public Square LastMove;

        public NotationManager Turns = new NotationManager();

        // Used by Square class
        public bool firstClick = true;
        public Square lastSquare;

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

        public void RestartGame()
        {
            resetFlags(); // always first
            ChessBoard.Restart();
        }

        void resetFlags(bool turn = false)
        {
            DisableTurn = turn;
            WhosPlaying = PieceColor.White;
            TurnId = 1;
            lastCheckPiece = null;
            Turns.Clear();
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

        internal void TestCheck()
        {
            new DebugChess(ChessBoard).TestCheck();
            resetFlags(true);
        }

        internal void TestDiscoverCheck()
        {
            new DebugChess(ChessBoard).TestDiscoverCheck();
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
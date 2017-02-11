using SimpleChessApp.Chess;

namespace SimpleChessApp
{
    class MoveInterception
    {
        private Square from;
        private Square to;

        public MoveInterception(Square From, Square To)
        {
            from = From;
            to = To;
        }

        internal bool Check()
        {
            switch (from.Piece)
            {
                case Pieces.King:
                    return handleKing();
                case Pieces.Pawn:
                    return handlePawn();
                case Pieces.Bishop:
                    return handleBishop();
                case Pieces.Rook:
                    return handleRook();
                case Pieces.Queen:
                    return handleQueen();
            }
            return true; // Handle Knight :)
        }

        private bool handleQueen()
        {
            return true;
        }

        private bool handleKing()
        {
            return true;
        }

        private bool handleBishop()
        {
            return true;
        }

        private bool handleRook()
        {
            return true;
        }

        private bool handlePawn()
        {
            var init = from.Rank + (from.IsBlack ? 1 : -1);
            var end = to.Rank;
            if (init > end)
            {
                end = init;
                init = to.Rank;
            }

            // Navigates from point A to Be vertically
            for (int i = init; i <= end; i++)
            {
                var square = ChessContext.Set.ChessBoard.Squares[from.File, i];
                if (square.Piece != Pieces.None)
                    return false;
            }

            return true;
        }
    }
}

using System.Collections.Generic;

namespace SimpleChessApp.Chess
{
    class HighLightMoves
    {
        List<Square> highlightedSquares = new List<Square>();

        public void Go(Square x)
        {
            foreach (var item in highlightedSquares) item.HighLight(false);
            highlightedSquares.Clear();

            switch (x.Piece)
            {
                case Pieces.None:
                    break;
                case Pieces.Pawn:
                    break;
                case Pieces.Knight:
                    handleKnight(x);
                    break;
                case Pieces.Bishop:
                    break;
                case Pieces.Rook:
                    break;
                case Pieces.King:
                    break;
                case Pieces.Queen:
                    break;
                case Pieces.GhostPawn:
                    break;
                default:
                    break;
            }

            foreach (var item in highlightedSquares) item.HighLight(true);
        }

        private void handleKnight(Square x)
        {
            int[] w = new int[] { +1, +1, -1, -1, +2, +2, -2, -2 };
            int[] z = new int[] { +2, -2, +2, -2, +1, -1, +1, -1 };

            for (int i = 0; i < 8; i++)
            {
                var a = x.File + w[i];
                var b = x.Rank + z[i];

                if (a < 0 || b < 0 || a > 7 || b > 7) continue;

                highlightedSquares.Add(
                ChessContext.Core.ChessBoard[a, b]);
            }
        }
    }
}

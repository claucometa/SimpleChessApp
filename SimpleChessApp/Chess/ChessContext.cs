using System.Collections.Generic;

namespace SimpleChessApp.Chess
{
    public static class ChessContext
    {
        public static List<ChessCore> Core = new List<ChessCore>();
        public static Repository repo = new Repository();
    }
}

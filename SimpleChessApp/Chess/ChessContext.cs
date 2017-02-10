namespace SimpleChessApp.Chess
{
    /// <summary>
    /// Instanciate ChessSet in Set
    /// </summary>

    public static class ChessContext
    {
        public static ChessSet Set = new ChessSet();

        static ChessContext()
        {
            Set.Promoted += Set_Promoted;
        }

        private static void Set_Promoted(object sender, System.EventArgs e)
        {
            var square = Square.PromotedSquare;
            square.SetPiece((Pieces)sender, square.IsBlack); 
        }
    }
}

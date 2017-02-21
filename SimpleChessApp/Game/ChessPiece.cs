namespace SimpleChessApp.Game
{
    public class ChessPiece
    {
        public int Id;
        public Pieces Kind;
        public PieceColor Color;
        public Square Home;
        public bool Passant;
        public Square Current;

        static int idd = 0;

        public ChessPiece(Square h, Pieces p, PieceColor c)
        {
            Id = idd++;
            Kind = p;
            Color = c;
            Current = Home = h;
        }

        public ChessPiece(Pieces p, PieceColor c)
        {
            Id = idd++;
            Kind = p;
            Color = c;
        }

        public string SpecialName
        {
            get
            {
                return $"{Current.Name} - {Kind}";
            }
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

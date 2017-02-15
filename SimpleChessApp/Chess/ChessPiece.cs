namespace SimpleChessApp.Chess
{
    public class ChessPiece : Notify
    {
        public int Id { get; set; }
        public Pieces Name { get; set; }
        public PieceColor Color { get; set; }
        public Square Home { get; set; }

        public Square c;
        public Square Current
        {
            get { return c; }
            set
            {
                SetField(ref c, value, "Current");
            }
        }

        static int idd = 0;

        public ChessPiece(Square h, Pieces p, PieceColor c)
        {
            Id = idd++;
            Name = p;
            Color = c;
            Current = Home = h;
        }

        public string SpecialName
        {
            get
            {
                return Current.Name + " " + Name;
            }
        }
    }
}

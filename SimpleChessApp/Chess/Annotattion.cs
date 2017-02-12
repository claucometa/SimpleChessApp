namespace SimpleChessApp.Chess
{
    public class Annotattion
    {
        public string Move { get; set; }
        public Annotattion(Square from, Square to)
        {
            var a = from.File;
            var b = from.Rank - 4;
            var c = to.File;
            var d = to.Rank ;
            Move += from.Name + " " + to.Name;
        }

        public override string ToString()
        {
            return Move;
        }
    }
}
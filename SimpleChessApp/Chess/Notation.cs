using System.ComponentModel;

namespace SimpleChessApp.Chess
{
    public class NotationManager
    {
        public BindingList<Turn> Moves { get; set; }

        public NotationManager()
        {            
            Moves = new BindingList<Turn>();
        }

        internal void Clear()
        {
            Moves.Clear();
        }
    }

    public class Turn : Notify
    {
        public int Id;
        public Notation x;

        Notation k;
        public Notation y
        {
            get { return k; }
            set { SetField(ref k, value, "y"); }
        }

        public override string ToString()
        {
            return $"{Id} {x}-{y}";
        }
    }

    public class Notation
    {
        public string Move { get; set; }

        public Notation(Square from, Square to)
        {
            var a = from.File;
            var b = from.Rank - 4;
            var c = to.File;
            var d = to.Rank;
            Move += from.Name + " " + to.Name;
        }

        public override string ToString()
        {
            return Move;
        }
    }
}
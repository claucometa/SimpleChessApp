using System.ComponentModel;

namespace SimpleChessApp.Chess
{
    public class NotationManager
    {
        public BindingList<Notation> MoveList = new BindingList<Notation>();
        public BindingList<Notation> MoveList2 = new BindingList<Notation>();

        internal void Clear()
        {
            MoveList.Clear();
            MoveList2.Clear();
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
            var d = to.Rank ;
            Move += from.Name + " " + to.Name;
        }

        public override string ToString()
        {
            return Move;
        }
    }
}
using System.ComponentModel;

namespace SimpleChessApp.Game
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
        public Notation White;

        Notation black;
        public Notation Black
        {
            get { return black; }
            set { SetField(ref black, value, "y"); }
        }

        public override string ToString()
        {
            return $"{Id} {White}-{Black}";
        }
    }

    public class Notation
    {
        public string Move
        {
            get
            {

                return from.CurrentSquare + " " + to.CurrentSquare;
            }
        }

        public Square from;
        public Square to;

        public Notation(Square f, Square t, Board bo)
        {
            var a = new Square(f.File, f.Rank, bo);
            var b = new Square(t.File, t.Rank, bo);
            a.Piece = new ChessPiece(f, t.Piece.Kind, t.Piece.Color);
            b.Piece = new ChessPiece(t, t.Piece.Kind, t.Piece.Color);
            from = a;
            to = b;
        }

        public override string ToString()
        {
            return Move;
        }
    }
}
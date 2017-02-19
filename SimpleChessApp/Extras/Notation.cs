﻿using System.ComponentModel;

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

        public Notation(Square f, Square t)
        {
            var a = new Square(f.File, f.Rank);
            var b = new Square(t.File, t.Rank);
            a.Piece = new ChessPiece(f, t.Piece.Name, t.Piece.Color);
            b.Piece = new ChessPiece(t, t.Piece.Name, t.Piece.Color);
            from = a;
            to = b;
        }

        public override string ToString()
        {
            return Move;
        }
    }
}
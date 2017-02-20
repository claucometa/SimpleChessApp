﻿namespace SimpleChessApp.Chess
{
    public class ChessPiece : Notify
    {
        public int Id { get; set; }
        public Pieces Kind { get; set; }
        public PieceColor Color { get; set; }
        public Square Home { get; set; }
        public bool Passant { get; set; }

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
            Kind = p;
            Color = c;
            Current = Home = h;
        }

        public string SpecialName
        {
            get
            {
                return Current.Name + " " + Kind;
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

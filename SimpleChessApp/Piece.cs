using SimpleChessApp.Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChessApp
{
    public class ChessPiece
    {
        public int Id { get; set; }
        public Pieces Name { get; set; }
        public PieceColor Color { get; set; }
        static int idd = 0;

        public ChessPiece(Pieces p, PieceColor c)
        {
            Id = idd++;
            Name = p;
            Color = c;
        }
    }
}

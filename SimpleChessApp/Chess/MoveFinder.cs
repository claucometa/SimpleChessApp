using System;
using System.Linq;
using System.Collections.Generic;

namespace SimpleChessApp.Chess
{
    public class MoveFinder
    {
        public Dictionary<int, List<PossibleMoves>> MoveList = new Dictionary<int, List<PossibleMoves>>();

        Board board;

        public MoveFinder(Board b)
        {
            this.board = b;
        }

        public void FindAllMoves()
        {
            Clear();

            foreach (var item in board.WhitePieces.Values)
                FindMoveFrom(item.Current);

            foreach (var item in board.BlackPieces.Values)
                FindMoveFrom(item.Current);

            if (board.ShowAllMoves) HighLightMoveStyle();
        }

        public void FindMoveFrom(Square x)
        {
            switch (x.Piece.Kind)
            {
                case Pieces.Pawn:
                    handlePawn(x);
                    break;
                case Pieces.Knight:
                    handleKnight(x);
                    break;
                case Pieces.Bishop:
                    handleBishop(x);
                    break;
                case Pieces.Rook:
                    handleRook(x);
                    break;
                case Pieces.King:
                    handleKing(x);
                    break;
                case Pieces.Queen:
                    handleQueen(x);
                    break;
            }
        }

        public void HighLightMoveStyle()
        {
            var moves = MoveList.Values.SelectMany(t => t);
            foreach (var item in moves)
            {
                if (item.Piece.Color == PieceColor.White)
                    item.Square.ShowMove();
                if (item.Piece.Color == PieceColor.Black)
                    item.Square.ShowCheck();
            }
        }

        //public void HighLightCheckStyle()
        //{
        //    var moves = MoveList.Values.SelectMany(t => t);
        //    foreach (var item in moves) item.Square.ShowCheck(true);
        //}

        public void Clear()
        {
            var moves = MoveList.Values.SelectMany(t => t);
            foreach (var item in moves)
                item.Square.HideMove();

            foreach (var item in MoveList.Values)
                item.Clear();
        }

        void handlePawn(Square x)
        {
            Square sq;
            int a = 0;
            int b = 0;

            // black or white?
            int homeRank = x.Piece.Color == PieceColor.White ? 1 : 6;
            int m = x.Piece.Color == PieceColor.White ? 1 : -1;

            // Moves
            a = x.File;
            b = x.Rank + 1 * m;
            if (b >= 0 && b < 8)
            {
                sq = board[a, b];
                if (sq.IsEmpty) addMove(a, b, x);
            }

            // Passant
            sq = board.From;
            if (sq != null)
            {
                if (sq.Piece != null)
                {
                    if (sq.Piece.Passant)
                    {
                        if (x.Piece.Color == PieceColor.White && x.Rank == 4)
                        {
                            if (sq.Rank == 4 && Math.Abs(sq.File - x.File) == 1)
                                addMove(sq.File, 5, x);
                        }

                        if (x.Piece.Color == PieceColor.Black && x.Rank == 3)
                        {
                            if (sq.Rank == 3 && Math.Abs(sq.File - x.File) == 1)
                                addMove(sq.File, 2, x);
                        }

                        sq.Piece.Passant = false;
                    }
                }
            }

            // Move from home rank
            if (x.Rank == homeRank)
            {
                b = x.Rank + 2 * m;
                addMove(a, b, x);
            }

            // Captures
            a = x.File - 1;
            b = x.Rank + 1 * m;
            if (a >= 0 && b >= 0 && b < 8)
            {
                sq = board[a, b];
                if (!sq.IsEmpty) addMove(a, b, x);
            }

            a = x.File + 1;
            b = x.Rank + 1 * m;
            if (a < 8 && b >= 0 && b < 8)
            {
                sq = board[a, b];
                if (!sq.IsEmpty) addMove(a, b, x);
            }
        }

        void handleQueen(Square x)
        {
            handleRook(x);
            handleBishop(x);
        }

        void handleKing(Square x)
        {
            addMove(x.File + 1, x.Rank + 1, x);
            addMove(x.File + 1, x.Rank - 1, x);
            addMove(x.File - 1, x.Rank + 1, x);
            addMove(x.File - 1, x.Rank - 1, x);
            addMove(x.File + 1, x.Rank, x);
            addMove(x.File - 1, x.Rank, x);
            addMove(x.File, x.Rank + 1, x);
            addMove(x.File, x.Rank - 1, x);

            if (x.Piece.Color == PieceColor.White)
            {
                if (board.WhiteCanCastleKingSide)
                    handleSmallCastling(x);

                if (board.WhiteCanCastleQueenSide)
                    handleBigCastling(x);
            }

            if (x.Piece.Color == PieceColor.Black)
            {
                if (board.BlackCanCastleKingSide)
                    handleSmallCastling(x);

                if (board.BlackCanCastleQueenSide)
                    handleBigCastling(x);
            }
        }

        private void handleBigCastling(Square x)
        {
            var s1 = board[x.File - 1, x.Rank];
            var s2 = board[x.File - 2, x.Rank];
            var s3 = board[x.File - 3, x.Rank];
            if (s1.IsEmpty && s2.IsEmpty && s3.IsEmpty) addMove(x.File - 2, x.Rank, x);
        }

        private void handleSmallCastling(Square x)
        {
            var s1 = board[x.File + 1, x.Rank];
            var s2 = board[x.File + 2, x.Rank];
            if (s1.IsEmpty && s2.IsEmpty) addMove(x.File + 2, x.Rank, x);
        }

        void handleRook(Square x)
        {
            for (int i = 1; i < 8; i++)
            {
                var b = x.Rank + i;
                if (addMove(x.File, b, x)) break;
            }

            for (int i = 1; i < 8; i++)
            {
                var b = x.Rank - i;
                if (addMove(x.File, b, x)) break;
            }

            for (int i = 1; i < 8; i++)
            {
                var b = x.File + i;
                if (addMove(b, x.Rank, x)) break;
            }

            for (int i = 1; i < 8; i++)
            {
                var b = x.File - i;
                if (addMove(b, x.Rank, x)) break;
            }
        }

        void handleBishop(Square x)
        {


            for (int i = 1; i < 8; i++)
            {
                var a = x.File + i;
                var b = x.Rank + i;
                if (addMove(a, b, x)) break;
            }

            for (int i = 1; i < 8; i++)
            {
                var a = x.File + i;
                var b = x.Rank - i;
                if (addMove(a, b, x)) break;
            }

            for (int i = 1; i < 8; i++)
            {
                var a = x.File - i;
                var b = x.Rank - i;
                if (addMove(a, b, x)) break;
            }

            for (int i = 1; i < 8; i++)
            {
                var a = x.File - i;
                var b = x.Rank + i;
                if (addMove(a, b, x)) break;
            }
        }

        void handleKnight(Square x)
        {
            var p = x.Piece;

            int[] w = new int[] { +1, +1, -1, -1, +2, +2, -2, -2 };
            int[] z = new int[] { +2, -2, +2, -2, +1, -1, +1, -1 };

            for (int i = 0; i < 8; i++)
            {
                var a = x.File + w[i];
                var b = x.Rank + z[i];

                // Avoid going out of the board (overflow)
                if (a < 0 || b < 0 || a > 7 || b > 7) continue;

                var sq = board[a, b];
                if (sq.IsEmpty)
                    MoveList[p.Id].Add(new PossibleMoves(p, sq, UserAction.Move));
                else if (sq.Piece.Color != x.Piece.Color)
                    MoveList[p.Id].Add(new PossibleMoves(p, sq, UserAction.Capture));
            }
        }

        bool addMove(int a, int b, Square x)
        {
            if (a < 0 || a > 7 || b < 0 || b > 7) return true;

            var p = x.Piece;
            //MoveList.Add(p, new List<PossibleMoves>());

            var sq = board[a, b];
            if (sq.IsEmpty)
                MoveList[p.Id].Add(new PossibleMoves(p, sq, UserAction.Move));
            else if (sq.Piece.Color != x.Piece.Color)
            {
                MoveList[p.Id].Add(new PossibleMoves(p, sq, UserAction.Capture));
                return true;
            }
            else
                return true;
            return false; // Valid move
        }
    }

    public class PossibleMoves
    {
        public Square Square;
        public UserAction Kind;
        public ChessPiece Piece;

        public PossibleMoves(ChessPiece p, Square sq, UserAction kind)
        {
            Piece = p;
            Square = sq;
            Kind = kind;
        }
    }

    public enum UserAction
    {
        None,
        Move,
        Capture,
        Invalid_Move,
        Piece_Selected,
        Check
    }
}

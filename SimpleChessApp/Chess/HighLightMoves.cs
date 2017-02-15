using System;
using System.Collections.Generic;

namespace SimpleChessApp.Chess
{
    public class HighLightMoves
    {
        public List<PossibleMoves> MoveList = new List<PossibleMoves>();


        public void Go(Square x)
        {
            switch (x.Piece.Name)
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

        public void HighLightSquares()
        {
            foreach (var item in MoveList) item.Square.HighLight(true);
        }

        public void HighLightCheck()
        {
            foreach (var item in MoveList) item.Square.HighCheck(true);
        }

        public void Clear()
        {
            foreach (var item in MoveList)
                item.Square.HighLight(false);
            MoveList.Clear();
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
                sq = ChessContext.Core.ChessBoard[a, b];
                if (sq.IsEmpty) addMove(a, b, x);
            }

            // Passant
            sq = ChessContext.Core.LastMove;
            if (sq != null)
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
                sq = ChessContext.Core.ChessBoard[a, b];
                if (!sq.IsEmpty) addMove(a, b, x);
            }

            a = x.File + 1;
            b = x.Rank + 1 * m;
            if (a < 8 && b >= 0 && b < 8)
            {
                sq = ChessContext.Core.ChessBoard[a, b];
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
                if (ChessContext.Core.WhiteCanCastleKingSide)
                    handleSmallCastling(x);

                if (ChessContext.Core.WhiteCanCastleQueenSide)
                    handleBigCastling(x);
            }

            if (x.Piece.Color == PieceColor.Black)
            {
                if (ChessContext.Core.BlackCanCastleKingSide)
                    handleSmallCastling(x);

                if (ChessContext.Core.BlackCanCastleQueenSide)
                    handleBigCastling(x);
            }
        }

        private void handleBigCastling(Square x)
        {
            var s1 = ChessContext.Core.ChessBoard[x.File - 1, x.Rank];
            var s2 = ChessContext.Core.ChessBoard[x.File - 2, x.Rank];
            var s3 = ChessContext.Core.ChessBoard[x.File - 3, x.Rank];
            if (s1.IsEmpty && s2.IsEmpty && s3.IsEmpty)
            {
                addMove(x.File - 2, x.Rank, x);
            }
        }

        private void handleSmallCastling(Square x)
        {
            var s1 = ChessContext.Core.ChessBoard[x.File + 1, x.Rank];
            var s2 = ChessContext.Core.ChessBoard[x.File + 2, x.Rank];
            if (s1.IsEmpty && s2.IsEmpty)
            {
                addMove(x.File + 2, x.Rank, x);
            }
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
            int[] w = new int[] { +1, +1, -1, -1, +2, +2, -2, -2 };
            int[] z = new int[] { +2, -2, +2, -2, +1, -1, +1, -1 };

            for (int i = 0; i < 8; i++)
            {
                var a = x.File + w[i];
                var b = x.Rank + z[i];

                // Avoid going out of the board (overflow)
                if (a < 0 || b < 0 || a > 7 || b > 7) continue;

                var sq = ChessContext.Core.ChessBoard[a, b];
                if (sq.IsEmpty)
                    MoveList.Add(new PossibleMoves(sq, UserAction.Move));
                else if (sq.Piece.Color != x.Piece.Color)
                    MoveList.Add(new PossibleMoves(sq, UserAction.Capture));
            }
        }

        bool addMove(int a, int b, Square x)
        {
            if (a < 0 || a > 7 || b < 0 || b > 7) return true;

            var sq = ChessContext.Core.ChessBoard[a, b];
            if (sq.IsEmpty)
                MoveList.Add(new PossibleMoves(sq, UserAction.Move));
            else if (sq.Piece.Color != x.Piece.Color)
            {
                MoveList.Add(new PossibleMoves(sq, UserAction.Capture));
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

        public PossibleMoves(Square sq, UserAction kind)
        {
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

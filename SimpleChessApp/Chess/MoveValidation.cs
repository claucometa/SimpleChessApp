using System;

namespace SimpleChessApp.Chess
{
    // TODO 2. Movement rules for each piece
    internal class MoveValidation
    {
        Square from;
        Square to;
        public static Square GhostSquare;
        bool pawnCaptureException;

        public MoveValidation(Square From, Square To, bool PawnCaptureException = false)
        {
            from = From;
            to = To;
            pawnCaptureException = PawnCaptureException;
        }

        public bool Validate
        {
            get
            {
                switch (from.Piece)
                {
                    case Pieces.Pawn:
                        return handlePawn();
                    case Pieces.Knight:
                        return handleKnight();
                    case Pieces.Bishop:
                        return handleBishop();
                    case Pieces.Rook:
                        return handleRook();
                    case Pieces.King:
                        return handleKing();
                    case Pieces.Queen:
                        return handleQueen();
                    default:
                        return false;
                }
            }
        }

        bool handleQueen()
        {
            return isMovingHorizontally || isMovingVertically || isMovingDiagonally;
        }

        bool handleKing()
        {
            #region Check Castling
            if (from.PieceColor == PieceColor.Black)
            {
                if (ChessContext.Core.BlackCanCastleKingSide)
                    if (from.Rank == 0 && from.File - to.File == -2)
                    {
                        ChessContext.Core.ChessBoard[7, 0].SetPiece(Pieces.None, PieceColor.Black);
                        ChessContext.Core.ChessBoard[5, 0].SetPiece(Pieces.Rook, PieceColor.Black);
                        return true;
                    }

                if (ChessContext.Core.BlackCanCastleQueenSide)
                    if (from.Rank == 0 && from.File - to.File == 2)
                    {
                        ChessContext.Core.ChessBoard[0, 0].SetPiece(Pieces.None, PieceColor.Black);
                        ChessContext.Core.ChessBoard[3, 0].SetPiece(Pieces.Rook, PieceColor.Black);
                        return true;
                    }
            }

            if (from.PieceColor == PieceColor.White)
            {
                if (ChessContext.Core.WhiteCanCastleKingSide)
                    if (from.Rank == 7 && from.File - to.File == -2)
                    {
                        ChessContext.Core.ChessBoard[7, 7].SetPiece(Pieces.None, PieceColor.White);
                        ChessContext.Core.ChessBoard[5, 7].SetPiece(Pieces.Rook, PieceColor.White);
                        return true;
                    }

                if (ChessContext.Core.WhiteCanCastleQueenSide)
                    if (from.Rank == 7 && from.File - to.File == 2)
                    {
                        ChessContext.Core.ChessBoard[0, 7].SetPiece(Pieces.None, PieceColor.White);
                        ChessContext.Core.ChessBoard[3, 7].SetPiece(Pieces.Rook, PieceColor.White);
                        return true;
                    }
            }
            #endregion

            var x = Math.Abs(from.Rank - to.Rank) == 1 || Math.Abs(from.File - to.File) == 1;

            return x;
        }

        bool handleRook()
        {
            return isMovingHorizontally || isMovingVertically;
        }

        bool handleBishop()
        {
            return isMovingDiagonally;
        }

        bool handleKnight()
        {
            var x = Math.Abs(from.File - to.File);
            var y = Math.Abs(from.Rank - to.Rank);
            var c = x + y;

            return (x == 1 && y == 2 || x == 2 && y == 1);
        }

        bool handlePawn()
        {
            var isBlack = from.PieceColor == PieceColor.Black;
            var mult = isBlack ? -1 : 1;

            if (isMovingVertically)
            {
                // Allow pawn moving one step forward
                if (from.Rank - to.Rank == 1 * mult)
                {
                    // promotes when reach last rank 
                    if (to.Rank == (isBlack ? 7 : 0)) promotePawn();

                    return isPathFree(mult);
                }

                // Allow pawn moving one two steps forward from home rank only
                if (isHomeRank)
                {
                    if (from.Rank - to.Rank == 2 * mult)
                    {
                        var ok = isPathFree(mult);

                        if (ok)
                        {
                            GhostSquare = ChessContext.Core.ChessBoard[from.File, from.Rank - mult];
                            GhostSquare.Piece = Pieces.GhostPawn;
                            GhostSquare.PieceColor = PieceColor.None;
                        }

                        ChessContext.Core.AllowPassant = ok;
                        return ok;
                    }
                }
            }
            else if (pawnCaptureException)
            {
                // Handles pawn capture
                return Math.Abs(from.File - to.File) == 1 && from.Rank - to.Rank == (1 * mult);
            }

            return false;
        }

        // Only used to pawn
        bool isPathFree(int mult)
        {
            var init = from.Rank + mult * -1;
            var end = to.Rank;
            if (init > end)
            {
                end = init;
                init = to.Rank;
            }

            // Navigates from point A to B vertically
            for (int i = init; i <= end; i++)
            {
                var square = ChessContext.Core.ChessBoard[from.File, i];
                if (square.Piece != Pieces.None)
                    return false;
            }

            return true;
        }

        void promotePawn()
        {
            ChessContext.Core.ShowPieceSelector(from);
            Square.PromotedSquare = to;
        }

        #region Move Devices
        bool isMovingVertically
        {
            get
            {
                if (from.File != to.File) return false;

                // Check for move interception
                var a = from.Rank;
                var b = to.Rank;

                if (a > b)
                {
                    a = b + 1;
                    b = from.Rank + 1;
                }

                for (int i = a; i < b; i++)
                    if (i != from.Rank)
                        if (ChessContext.Core.ChessBoard[from.File, i].IsEmpty) return false;

                return true;
            }
        }

        bool isMovingHorizontally
        {
            get
            {
                if (from.Rank != to.Rank) return false;

                // Check for move interception
                var a = from.File;
                var b = to.File;

                if (a > b)
                {
                    a = b + 1;
                    b = from.File + 1;
                }

                for (int i = a; i < b; i++)
                    if (i != from.File)
                        if (ChessContext.Core.ChessBoard[i, from.Rank].IsEmpty) return false;

                return true;
            }
        }


        bool isMovingDiagonally
        {
            get
            {
                var a = from.File;
                var b = from.Rank;
                var c = to.Rank;
                var d = to.File;

                while (b < c && a < d)
                {
                    if ((b != from.Rank && a != from.File) && (b != to.Rank && a != to.File))
                        if (ChessContext.Core.ChessBoard[a, b].IsEmpty) return false;
                    b++;
                    a++;
                }

                while (b > c && a > d)
                {
                    if ((b != from.Rank && a != from.File) && (b != to.Rank && a != to.File))
                        if (ChessContext.Core.ChessBoard[a, b].IsEmpty) return false;
                    b--;
                    a--;
                }

                while (b < c && a > d)
                {
                    if ((b != from.Rank && a != from.File) && (b != to.Rank && a != to.File))
                        if (ChessContext.Core.ChessBoard[a, b].IsEmpty) return false;
                    b++;
                    a--;
                }

                while (b > c && a < d)
                {
                    if ((b != from.Rank && a != from.File) && (b != to.Rank && a != to.File))
                        if (ChessContext.Core.ChessBoard[a, b].IsEmpty) return false;
                    b--;
                    a++;
                }

                return (b == a && c == d) || (b == c && a == d);
            }
        }

        bool isHomeRank
        {
            get
            {
                return from.Rank == (from.PieceColor == PieceColor.Black ? 1 : 6);
            }
        }
        #endregion
    }
}
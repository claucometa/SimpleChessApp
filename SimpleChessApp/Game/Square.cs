﻿using System;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;
using static ImageUtil;
using System.Drawing;

namespace SimpleChessApp.Game
{
    [ToolboxItem(false)]
    public partial class Square : SimpleSquare
    {
        public int File, Rank;
        public new string Name;
        public static event EventHandler Action;
        public Board Board;
        string msg;

        public string SpecialName
        {
            get
            {
                return CurrentSquare + " " + Piece.ToString();
            }
        }

        public string CurrentSquare
        {
            get
            {
                return "abcdefgh"[File] + (Rank + 1).ToString();
            }
        }

        public static void action(string msg)
        {
            Action?.Invoke(msg, null);
        }

        public Square(int file, int rank, Board Board) : this()
        {
            this.Board = Board;
            File = file;
            Rank = rank;
            Name = "abcdefgh"[file] + (Rank + 1).ToString();
        }

        public Square()
        {
            InitializeComponent();
            Click += Square_MouseClick;
        }

        void SwitchPlayer()
        {
            if (!Board.DisableTurns)
            {
                if (Board.WhosPlaying == PieceColor.White)

                    Board.WhosPlaying = PieceColor.Black;
                else
                    Board.WhosPlaying = PieceColor.White;
            }
        }

        bool getSide(Square x)
        {
            return Board.DisableTurns || Board.WhosPlaying == x.Piece.Color;
        }

        private void Square_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var to = this;
                var great = Board.From != null && Board.From.Piece != null;

                if (to.Piece == null && great)
                {
                    #region Validação de movimento + Captura passant
                    var moves = Board.lights.MoveList[Board.From.Piece.Id];
                    msg = $"Movimento inválido: {moves.Count}";
                    Board.From.ClearHighLight();
                    if (moves.Exists(t => t.Square == to))
                    {
                        msg = "Movimento permitido!";

                        if (Board.From.Piece.Kind == Pieces.Pawn)
                        {
                            var s = moves.First(t => t.Square == to);
                            if (s.Kind == UserAction.Capture)
                            {
                                msg = "Captura passant!";
                                var isWhite = Board.From.Piece.Color == PieceColor.White;
                                var list = isWhite ? Board.BlackPieces : Board.WhitePieces;

                                var w = Board[s.Square.File, s.Square.Rank + (isWhite ? -1 : 1)];
                                if (Board.lastPassantPawn == w.Piece)
                                {
                                    list.Remove(w.Piece.Id);
                                    w.Piece = null;
                                }
                                else
                                {
                                    action("Movimento inválido");
                                    return;
                                }
                            }
                        }
                        to.MovePawn(Board.From);
                    }
                    else
                    {
                        Board.HidePieceMoves(Board.From);
                        Board.From = null;
                    }
                    #endregion
                }
                else if (great)
                {
                    if (to.Piece.Color != Board.From.Piece.Color)
                    {
                        #region Pedra oposta ou captura
                        msg = "Pedra oposta";
                        var moves = Board.lights.MoveList[Board.From.Piece.Id];
                        if (moves.Exists(t => t.Square == to))
                        {
                            msg = "Captura";
                            Board.From.ClearHighLight();
                            to.CaptPawn(Board.From);
                        }
                        else if (Board.DisableTurns)
                        {
                            #region Seleção
                            hideMoves(Board.From);
                            Board.From.ClearHighLight();
                            to.HighLight();
                            Board.From = to;
                            msg = "Seleção";
                            Board.ShowPieceMoves(to);
                            #endregion
                        }
                        #endregion  
                    }
                    else if (getSide(Board.From))
                    {
                        #region Seleção: mesma peça ou próxima peça
                        if (to.Piece.Id == Board.From.Piece.Id)
                        {
                            Board.From.ClearHighLight();
                            to.HighLight();
                            msg = "Mesma peça";
                            Board.ShowPieceMoves(to);
                        }
                        else if (Board.From.Piece.Color == to.Piece.Color)
                        {
                            hideMoves(Board.From);
                            to.HighLight();
                            Board.From = to;
                            msg = "Seleção (próxima peça)";
                            Board.ShowPieceMoves(to);
                        }
                        #endregion
                    }
                }
                else if (to.Piece != null && getSide(to))
                {
                    #region Seleção
                    to.HighLight();
                    Board.From = to;
                    msg = "Seleção";
                    Board.ShowPieceMoves(to);
                    #endregion
                }
                else
                    msg = "Pedra oposta";
            }
            action(msg);
        }

        private void hideMoves(Square to)
        {
            if (Board.From != null)
            {
                Board.From.ClearHighLight();
                Board.HidePieceMoves(to);
            }
        }

        private void CaptPawn(Square from)
        {
            if (Piece.Color == PieceColor.White)
                Board.WhitePieces.Remove(Piece.Id);
            else
                Board.BlackPieces.Remove(Piece.Id);

            if (from.Piece.Color == PieceColor.White)
                Board.WhitePieces[from.Piece.Id].Current = this;
            else
                Board.BlackPieces[from.Piece.Id].Current = this;

            Piece = from.Piece;

            var promotePawn = false;
            var isWhite = Piece.Color == PieceColor.White;

            if (isWhite)
            {
                Board.WhitePieces[Piece.Id].Current = this;
                handleWhiteCastling();
                if (Piece.Kind == Pieces.Pawn && Piece.Current.Rank == 7)
                    promotePawn = true;
            }

            if (Piece.Color == PieceColor.Black)
            {
                Board.BlackPieces[Piece.Id].Current = this;
                handleBlackCastling();
                if (Piece.Kind == Pieces.Pawn && Piece.Current.Rank == 0)
                    promotePawn = true;
            }

            from.Piece = null;

            if (promotePawn)
            {
                using (var w = new Promotion(Cursor.Position))
                {
                    w.ShowDialog();
                    Kind = w.Piece; // Redraw the piece
                }
            }

            Board.lights.FindAllMoves();

            Extras.PlaySound.Play();

            SwitchPlayer();
        }

        private void MovePawn(Square from)
        {
            ChessPiece hasJustCastled = null;

            Piece = from.Piece;

            Board.lastPassantPawn = null;

            if (Piece.Kind == Pieces.Pawn)
            {
                if (Rank == 3 || Rank == 4)
                    Board.lastPassantPawn = Piece;
            }

            var promotePawn = false;
            var isWhite = Piece.Color == PieceColor.White;

            if (isWhite)
            {
                Board.WhitePieces[Piece.Id].Current = this;
                hasJustCastled = handleWhiteCastling();
                if (Piece.Kind == Pieces.Pawn && Piece.Current.Rank == 7)
                    promotePawn = true;
            }
            else
            {
                Board.BlackPieces[Piece.Id].Current = this;
                hasJustCastled = handleBlackCastling();
                if (Piece.Kind == Pieces.Pawn && Piece.Current.Rank == 0)
                    promotePawn = true;
            }

            from.Piece = null;

            if (promotePawn)
            {
                using (var w = new Promotion(Cursor.Position))
                {
                    w.ShowDialog();
                    Kind = w.Piece; // Redraw the piece
                }
            }

            Board.lights.IsOnCheck = false;

            Board.lights.FindAllMoves();

            if (Board.lights.IsOnCheck)
            {
                if (Board.lights.KingColorOnCheck == Piece.Color)
                {
                    var invalidMove = true;

                    if (Piece.Kind == Pieces.King)
                    {
                        invalidMove = Board.lights.IsOnCheck;
                    }

                    if (invalidMove)
                    {
                        from.Piece = Piece;
                        Piece = null;
                        msg = "Inválido! O rei está em cheque 1";

                        if (isWhite)
                            Board.WhitePieces[from.Piece.Id].Current = from;
                        else
                            Board.BlackPieces[from.Piece.Id].Current = from;

                        if (hasJustCastled != null)
                        {
                            var f = hasJustCastled.Current.File;
                            var r = hasJustCastled.Current.Rank;
                            Square s = f == 5 ? Board[7, r] : Board[0, r];

                            s.Piece = hasJustCastled;

                            if (isWhite)
                            {
                                Board.WhitePieces[hasJustCastled.Id].Current = s;
                                Board.WhiteCanCastleQueenSide = true;
                                Board.WhiteCanCastleKingSide = true;
                            }
                            else
                            {
                                Board.BlackPieces[hasJustCastled.Id].Current = s;
                                Board.BlackCanCastleQueenSide = true;
                                Board.BlackCanCastleKingSide = true;
                            }

                            Board[f, r].Piece = null;
                        }

                        Board.lights.FindAllMoves();

                        return;
                    }
                }
                else
                {
                    msg = "Cheque";
                }
            }

            Extras.PlaySound.Play();

            SwitchPlayer();
        }

        private ChessPiece handleBlackCastling()
        {
            ChessPiece rook = null;

            if (Board.BlackCanCastleKingSide || Board.BlackCanCastleQueenSide)
            {
                if (Piece.Kind == Pieces.King || Piece.Kind == Pieces.Rook)
                {
                    if (Piece.Kind == Pieces.Rook)
                    {
                        if (Piece.Home.Name == "a8")
                            Board.BlackCanCastleQueenSide = false;
                        else
                            Board.BlackCanCastleKingSide = false;
                    }
                    else
                    {
                        if (Piece.Current.File == 2)
                        {
                            var s = Board[3, 7];
                            s.Piece = Board[0, 7].Piece;
                            Board[0, 7].Piece = null;
                            Board.BlackPieces[s.Piece.Id].Current = s;
                            rook = s.Piece;
                        }
                        if (Piece.Current.File == 6)
                        {
                            var s = Board[5, 7];
                            s.Piece = Board[7, 7].Piece;
                            Board[7, 7].Piece = null;
                            Board.BlackPieces[s.Piece.Id].Current = s;
                            rook = s.Piece;
                        }

                        Board.BlackCanCastleQueenSide = false;
                        Board.BlackCanCastleKingSide = false;
                    }
                }
            }
            return rook;
        }

        private ChessPiece handleWhiteCastling()
        {
            ChessPiece rook = null;

            if (Board.WhiteCanCastleKingSide || Board.WhiteCanCastleKingSide)
            {
                if (Piece.Kind == Pieces.King || Piece.Kind == Pieces.Rook)
                {
                    if (Piece.Kind == Pieces.Rook)
                    {
                        if (Piece.Home.Name == "a1")
                            Board.WhiteCanCastleQueenSide = false;
                        else
                            Board.WhiteCanCastleKingSide = false;
                    }
                    else
                    {
                        if (Piece.Current.File == 2)
                        {
                            var s = Board[3, 0];
                            s.Piece = Board[0, 0].Piece;
                            Board[0, 0].Piece = null;
                            Board.WhitePieces[s.Piece.Id].Current = s;
                            rook = s.Piece;
                        }
                        if (Piece.Current.File == 6)
                        {
                            var s = Board[5, 0];
                            s.Piece = Board[7, 0].Piece;
                            Board[7, 0].Piece = null;
                            Board.WhitePieces[s.Piece.Id].Current = s;
                            rook = s.Piece;
                        }

                        Board.WhiteCanCastleQueenSide = false;
                        Board.WhiteCanCastleKingSide = false;
                    }
                }
            }
            return rook;
        }

        public void ShowAllMoves()
        {
            var img = Board.Flipped ? Properties.Resources.c2 : Properties.Resources.c3;

            panel1.BackgroundImage = img.Opacity(0.5);
        }

        public void ShowSelectedPieceMoves()
        {
            //var img = Board.Flipped ? Properties.Resources.c2 : 
            var img = Properties.Resources.c2;
            panel1.BackgroundImage = img.Opacity(0.5);
        }

        public void HideMove()
        {
            panel1.BackgroundImage = null;
        }
    }
}
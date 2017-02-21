using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    [ToolboxItem(false)]
    public partial class Square : SimpleSquare
    {
        public int File, Rank;
        public new string Name;
        public static event EventHandler Action;
        public Board Board;

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

        Repository repo
        {
            get
            {
                return ChessContext.repo;
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

        private void colorSquare()
        {
            DefaultColor = IsBlackSquare ? Color.CornflowerBlue : Color.WhiteSmoke;
            BackColor = DefaultColor;
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


        private void Square_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var to = this;

                if (Board.From != null)
                {
                    if (to.Piece == null)
                    {
                        // Move
                        if (Board.From != null && Board.From.Piece != null)
                        {
                            var moves = Board.lights.MoveList[Board.From.Piece.Id];
                            var msg = $"Movimento inválido: {moves.Count}";
                            Board.From.BackColor = Board.From.DefaultColor;
                            if (moves.Exists(t => t.Square == to))
                            {
                                msg = "Movimento permitido!";
                                to.MoveFrom(Board.From);
                                SwitchPlayer();
                                action(msg);
                                return;
                            }
                            if (Board.ShowSelectedPieceMoves)
                                Board.HidePieceMoves(Board.From);
                            Board.From = null;
                            action(msg);
                            return;
                        }
                    }
                    else if (Board.From.Piece == null)
                    {
                        if (to.Piece != null)
                        {
                            if (Board.WhosPlaying == to.Piece.Color || Board.DisableTurns)
                            {
                                to.BackColor = Color.LightGreen;
                                Board.From = to;
                                action("Seleção 2");
                                if (Board.ShowSelectedPieceMoves)
                                    Board.ShowPieceMoves(to);
                            }
                        }
                    }
                    else if (to.Piece.Id == Board.From.Piece.Id)
                    {
                        if (Board.WhosPlaying == Board.From.Piece.Color || Board.DisableTurns)
                        {
                            if (Board.From != null)
                            {
                                Board.From.BackColor = Board.From.DefaultColor;
                                to.BackColor = Color.LightGreen;
                                action("Mesma peça");
                            }
                        }
                    }
                    else if (to.Piece.Color != Board.From.Piece.Color)
                    {
                        var msg = "Pedra oposta";
                        var moves = Board.lights.MoveList[Board.From.Piece.Id];
                        if (moves.Exists(t => t.Square == to))
                        {
                            msg = "Captura";
                            Board.From.BackColor = Board.From.DefaultColor;
                            to.Capt(Board.From);
                            SwitchPlayer();
                        }
                        action(msg);
                    }
                    else if (to.Piece != Board.From.Piece && to.Piece.Color != Board.From.Piece.Color)
                    {
                        Board.From.BackColor = Board.From.DefaultColor;
                        Board.From = null;
                        action("Captura");
                        return;
                    }
                    else if (Board.From.Piece.Color == to.Piece.Color)
                    {
                        if (Board.WhosPlaying == Board.From.Piece.Color || Board.DisableTurns)
                        {
                            hideMoves(Board.From);
                            to.BackColor = Color.LightGreen;
                            Board.From = to;
                            action("Seleção (próxima peça)");
                            if (Board.ShowSelectedPieceMoves)
                                Board.ShowPieceMoves(to);
                        }
                    }
                }

                else if (Board.From != to)
                {
                    if (to.Piece != null)
                    {
                        if (Board.WhosPlaying == to.Piece.Color || Board.DisableTurns)
                        {
                            hideMoves(Board.From);
                            to.BackColor = Color.LightGreen;
                            Board.From = to;
                            action("Seleção");
                            if (Board.ShowSelectedPieceMoves)
                                Board.ShowPieceMoves(to);
                        }
                    }
                }
            }
        }

        private void hideMoves(Square to)
        {
            if (Board.From != null)
            {
                Board.From.BackColor = Board.From.DefaultColor;
                if (Board.ShowSelectedPieceMoves)
                    Board.HidePieceMoves(to);
            }
        }

        private void Capt(Square from)
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
            from.Piece = null;

            Board.lights.FindAllMoves();
        }

        private void MoveFrom(Square from)
        {
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
        }

        private void handleBlackCastling()
        {
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
                            Board[3, 7].Piece = Board[0, 7].Piece;
                            Board[0, 7].Piece = null;
                            Board.BlackPieces[Board[3, 7].Piece.Id].Current = Board[3, 7];
                        }
                        if (Piece.Current.File == 6)
                        {
                            Board[5, 7].Piece = Board[7, 7].Piece;
                            Board[7, 7].Piece = null;
                            Board.BlackPieces[Board[5, 7].Piece.Id].Current = Board[5, 7];
                        }

                        Board.BlackCanCastleQueenSide = false;
                        Board.BlackCanCastleKingSide = false;
                    }
                }
            }
        }

        private void handleWhiteCastling()
        {
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
                            Board[3, 0].Piece = Board[0, 0].Piece;
                            Board[0, 0].Piece = null;
                            Board.WhitePieces[Board[3, 0].Piece.Id].Current = Board[3, 0];
                        }
                        if (Piece.Current.File == 6)
                        {
                            Board[5, 0].Piece = Board[7, 0].Piece;
                            Board[7, 0].Piece = null;
                            Board.WhitePieces[Board[5, 0].Piece.Id].Current = Board[5, 0];
                        }

                        Board.WhiteCanCastleQueenSide = false;
                        Board.WhiteCanCastleKingSide = false;
                    }
                }
            }
        }

        public void ShowCheck()
        {
            var img = Board.Flipped ? Properties.Resources.Black : Properties.Resources.White;

            panel1.BackgroundImage = img;
        }

        public void ShowMove()
        {
            var img = Board.Flipped ? Properties.Resources.White : Properties.Resources.Black;
            panel1.BackgroundImage = img;
        }

        public void HideMove()
        {
            panel1.BackgroundImage = null;
        }
    }
}
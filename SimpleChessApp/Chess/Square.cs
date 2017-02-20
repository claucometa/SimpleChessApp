using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Square : UserControl
    {
        public int File, Rank;
        public new string Name;
        public static event EventHandler Action;

        Board Board;

        //private bool IsSelected;

        public Color DefaultColor;

        ChessPiece piece;
        public ChessPiece Piece
        {
            get
            {
                return piece;
            }
            set
            {
                piece = value;

                if (piece == null)
                {
                    piece = null;
                    BackgroundImage = null;
                    return;
                }

                BackgroundImage = repo.GetPiece(piece.Kind, piece.Color);
            }
        }

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

        bool isBlackSquare;
        public bool IsBlackSquare
        {
            get
            {
                return isBlackSquare;
            }
            set
            {
                isBlackSquare = value;
                colorSquare();
            }
        } // Used just and only to draw the board

        Repository repo
        {
            get
            {
                return ChessContext.repo;
            }
        }

        public Square()
        {
            InitializeComponent();
            MouseClick += Square_MouseClick;
            panel1.MouseClick += Square_MouseClick;
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
                        if (Board.From != null)
                        {
                            var moves = Board.lights.MoveList[Board.From.Piece.Id];
                            var msg = $"Movimento inválido: {moves.Count}";
                            Board.From.BackColor = Board.From.DefaultColor;
                            if (moves.Exists(t => t.Square == to))
                            {
                                msg = "Movimento permitido!";
                                to.MoveTo(Board.From);
                                //Board.lights.FindAllMoves();
                                return;
                            }
                            Board.From = null;
                            action(msg);
                            return;
                        }
                    }
                    else if (Board.From.Piece == null)
                    {
                        if (to.Piece != null)
                        {
                            // hideMoves(Board.From);
                            to.BackColor = Color.LightGreen;
                            Board.From = to;
                            action("Seleção 2");
                            if (Board.ShowSelectedPieceMoves)
                                Board.ShowPieceMoves(to);
                        }
                    }
                    else if (to.Piece.Id == Board.From.Piece.Id)
                    {
                        if (Board.From != null)
                        {
                            Board.From.BackColor = Board.From.DefaultColor;
                            to.BackColor = Color.LightGreen;
                            action("Mesma peça");
                        }
                    }
                    else if (to.Piece.Color != Board.From.Piece.Color)
                    {
                        var msg = "Pedra oposta";
                        hideMoves(Board.From);

                        var moves = Board.lights.MoveList[Board.From.Piece.Id];
                        if (moves.Exists(t => t.Square == to))
                        {
                            msg = "Capturar";
                            to.Capturar(Board.From);
                        }

                        Board.From = to;
                        to.BackColor = Color.LightGreen;
                        //if (Board.ShowSelectedPieceMoves)
                        //    Board.ShowPieceMoves(to);
                        action(msg);

                    }
                    else if (to.Piece != Board.From.Piece && to.piece.Color != Board.From.Piece.Color)
                    {
                        //x.BackColor = DefaultColor;
                        Board.From.BackColor = Board.From.DefaultColor;
                        Board.From = null;
                        action("Captura");
                        return;
                    }
                    else if (Board.From.Piece.Color == to.Piece.Color)
                    {
                        hideMoves(Board.From);
                        to.BackColor = Color.LightGreen;
                        Board.From = to;
                        action("Seleção (próxima peça)");
                        if (Board.ShowSelectedPieceMoves)
                            Board.ShowPieceMoves(to);
                    }
                }

                else if (Board.From != to)
                {
                    if (to.Piece != null)
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

        private void hideMoves(Square to)
        {
            if (Board.From != null)
            {
                Board.From.BackColor = Board.From.DefaultColor;
                if (Board.ShowSelectedPieceMoves)
                    Board.HidePieceMoves(to);
            }
        }

        private void Capturar(Square from)
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

        private void MoveTo(Square from)
        {
            Piece = from.Piece;

            if (Piece.Color == PieceColor.White)
                Board.WhitePieces[Piece.Id].Current = this;
            else
                Board.BlackPieces[Piece.Id].Current = this;

            from.Piece = null;

            Board.lights.FindAllMoves();
        }

        private void action(string v)
        {
            Action?.Invoke(v, null);
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

        public bool IsEmpty
        {
            get
            {
                return Piece == null;
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
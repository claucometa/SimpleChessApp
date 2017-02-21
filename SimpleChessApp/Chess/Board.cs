using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Board : UserControl
    {
        public Square[,] Squares = new Square[8, 8];
        public Dictionary<int, ChessPiece> WhitePieces = new Dictionary<int, ChessPiece>();
        public Dictionary<int, ChessPiece> BlackPieces = new Dictionary<int, ChessPiece>();
        public Square From;
        public MoveFinder lights;
        public bool ShowAllMoves;
        public bool ShowSelectedPieceMoves;
        public PieceColor WhosPlaying;

        // Castling handling
        public bool WhiteCanCastleKingSide;
        public bool BlackCanCastleKingSide;
        public bool WhiteCanCastleQueenSide;
        public bool BlackCanCastleQueenSide;

        int idd = 0;
        public bool Flipped;

        public Board()
        {
            InitializeComponent();
        }

        public Board(Panel p, ImageLayout layout = ImageLayout.Center, bool flipped = false, bool allMoves = false, bool selected = false) : this()
        {
            ShowSelectedPieceMoves = selected;
            ShowAllMoves = allMoves;
            Flipped = flipped;
            Dock = DockStyle.Fill;
            lights = new MoveFinder(this);
            build(layout, flipped);
            p.Controls.Add(this);
        }

        public int id
        {
            get
            {
                return ++idd;
            }
        }

        public Square this[int File, int Rank]
        {
            get
            {
                return Squares[File, Rank];
            }
        }

        public Square this[Square x]
        {
            get
            {
                return Squares[x.File, x.Rank];
            }
        }

        void build(ImageLayout layout, bool flipped)
        {
            #region Assemble Board
            int count = flipped ? 1 : 0;  // Right corner = white square
            bool isBlack;

            table.Controls.Clear();
            for (int rank = 0; rank < 8; rank++)
            {
                isBlack = (count++ % 2) == 0;
                for (int file = 0; file < 8; file++)
                {
                    var x = new Square(file, rank, this);
                    x.Dock = DockStyle.Fill;
                    x.IsBlackSquare = isBlack;
                    x.BackgroundImageLayout = layout;
                    isBlack = (count++ % 2) == 0;
                    Squares[file, rank] = x;
                    table.Controls.Add(x, file, flipped ? rank : 7 - rank);
                }
            }
            #endregion
            Restart();
        }

        internal void Restart()
        {
            ClearBoard();
            setBlackPieces();
            setWhitePieces();
            lights.FindAllMoves();
        }

        internal void ClearBoard()
        {
            idd = 0;

            for (int i = 0; i < 8; i++)
                for (int x = 0; x < 8; x++)
                    Squares[i, x].Piece = null;

            From = null;

            if (lights == null)
                lights = new MoveFinder(this);

            lights.Clear();

            WhosPlaying = PieceColor.White;

            WhiteCanCastleKingSide = true;
            BlackCanCastleKingSide = true;
            WhiteCanCastleQueenSide = true;
            BlackCanCastleQueenSide = true;

            WhitePieces.Clear();
            BlackPieces.Clear();
        }

        void setBlackPieces()
        {
            for (int i = 0; i < 8; i++) addBlack(i, 6, Pieces.Pawn);
            addBlack(0, 7, Pieces.Rook);
            addBlack(1, 7, Pieces.Knight);
            addBlack(2, 7, Pieces.Bishop);
            addBlack(3, 7, Pieces.Queen);
            addBlack(4, 7, Pieces.King);
            addBlack(5, 7, Pieces.Bishop);
            addBlack(6, 7, Pieces.Knight);
            addBlack(7, 7, Pieces.Rook);
        }

        void setWhitePieces()
        {
            for (int i = 0; i < 8; i++) addWhite(i, 1, Pieces.Pawn);
            addWhite(0, 0, Pieces.Rook);
            addWhite(1, 0, Pieces.Knight);
            addWhite(2, 0, Pieces.Bishop);
            addWhite(3, 0, Pieces.Queen);
            addWhite(4, 0, Pieces.King);
            addWhite(5, 0, Pieces.Bishop);
            addWhite(6, 0, Pieces.Knight);
            addWhite(7, 0, Pieces.Rook);
        }

        void add(int v1, int v2, Pieces p, PieceColor c)
        {
            var x = this[v1, v2];
            x.Piece = new ChessPiece(x, p, c);
            if (c == PieceColor.White)
            {
                WhitePieces.Add(x.Piece.Id, x.Piece);
                lights.MoveList.Add(x.Piece.Id, new List<PossibleMoves>());
            }
            if (c == PieceColor.Black)
            {
                BlackPieces.Add(x.Piece.Id, x.Piece);
                lights.MoveList.Add(x.Piece.Id, new List<PossibleMoves>());
            }
        }

        internal void ShowPieceMoves(Square to)
        {
            foreach (var item in lights.MoveList[to.Piece.Id])
                item.Square.ShowMove();
        }

        internal void HidePieceMoves(Square to)
        {
            foreach (var item in lights.MoveList[to.Piece.Id])
                item.Square.HideMove();
        }

        public void addWhite(int v1, int v2, Pieces p)
        {
            add(v1, v2, p, PieceColor.White);
        }

        public void addBlack(int v1, int v2, Pieces p)
        {
            add(v1, v2, p, PieceColor.Black);
        }
    }
}

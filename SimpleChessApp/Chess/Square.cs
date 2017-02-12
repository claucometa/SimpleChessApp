﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp.Chess
{
    public partial class Square : UserControl
    {
        static Square fromSquare;
        static Square toSquare;
        static Square lastMove;

        public static Square PromotedSquare;

        public int File { get; set; }
        public int Rank { get; set; }
        public new string Name { get; set; }

        private bool IsSelected;

        public Color DefaultColor;
        public Pieces Piece = Pieces.None;
        public PieceColor PieceColor;

        // Just used to draw the board
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
                return Piece != Pieces.None && Piece != Pieces.GhostPawn;
            }
        }

        public void SetPiece(Pieces piece, PieceColor color)
        {
            Piece = piece;
            PieceColor = color;
            BackgroundImage = getPiece(piece, color);
        }

        private Image getPiece(Pieces piece, PieceColor color)
        {
            return piece == Pieces.None ? null : ChessContext.Core.GetPiece(piece, color);
        }

        public Square()
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Center;
            
            MouseUp += Square_Click;
        }

        public Square(int file, int rank) : this()
        {
            File = file;
            Rank = rank;
            Name = "abcdefgh"[file] + Math.Abs(rank - 8).ToString();
        }

        private void Square_Click(object sender, System.EventArgs e)
        {
            toSquare = (Square)sender;

            if (toSquare.IsSelected)
            {
                toSquare.IsSelected = false;
                toSquare.BackColor = toSquare.DefaultColor;
                fromSquare = null;
                return;
            }

            if (toSquare == fromSquare)
            {
                toSquare.IsSelected = !toSquare.IsSelected;
                if (toSquare.IsSelected) toSquare.BackColor = Color.LightGreen;
                return;
            }

            if (fromSquare != null)
            {
                fromSquare.BackColor = fromSquare.DefaultColor;
                fromSquare.IsSelected = false;

                if (ChessContext.Core.WhosPlaying == fromSquare.PieceColor ||
                    ChessContext.Core.HasNoTurns) // Controls player turn
                {
                    if (toSquare.Piece == Pieces.None)
                    {
                        // Handle Move
                        if (fromSquare.Piece != Pieces.None)
                        {
                            if (new MoveValidation(fromSquare, toSquare).Validate)
                            {
                                #region Set Castling Flags For Black
                                if (fromSquare.PieceColor == PieceColor.Black)
                                {
                                    if (ChessContext.Core.BlackCanCastleKingSide || ChessContext.Core.BlackCanCastleQueenSide)
                                    {
                                        if (fromSquare.Piece == Pieces.King)
                                        {
                                            ChessContext.Core.BlackCanCastleKingSide = false;
                                            ChessContext.Core.BlackCanCastleQueenSide = false;
                                        }

                                        else if (fromSquare.Piece == Pieces.Rook && fromSquare.File == 0)
                                            ChessContext.Core.BlackCanCastleQueenSide = false;

                                        else if (fromSquare.Piece == Pieces.Rook && fromSquare.File == 7)
                                            ChessContext.Core.BlackCanCastleKingSide = false;

                                    }
                                }
                                #endregion

                                #region Set Castling Flags For White
                                if (fromSquare.PieceColor == PieceColor.White)
                                {
                                    if (ChessContext.Core.WhiteCanCastleKingSide || ChessContext.Core.WhiteCanCastleQueenSide)
                                    {
                                        if (fromSquare.Piece == Pieces.King)
                                        {
                                            ChessContext.Core.WhiteCanCastleKingSide = false;
                                            ChessContext.Core.WhiteCanCastleQueenSide = false;
                                        }

                                        else if (fromSquare.Piece == Pieces.Rook && fromSquare.File == 0)
                                            ChessContext.Core.WhiteCanCastleQueenSide = false;

                                        else if (fromSquare.Piece == Pieces.Rook && fromSquare.File == 7)
                                            ChessContext.Core.WhiteCanCastleKingSide = false;

                                    }
                                }
                                #endregion  

                                lastMove = toSquare;
                                toSquare.SetPiece(fromSquare.Piece, fromSquare.PieceColor);
                                fromSquare.clearSquare();
                                addMoveAnnotation();
                                ChessContext.Core.ChangeTurn();
                                return;
                            }
                            else
                            {
                                //Invalidate square selection if the move is not allowed
                                //TODO: Display a toast message
                                return;
                            }
                        }
                    }

                    // Handle Capture
                    if (toSquare.Piece != Pieces.None)
                    {
                        if (fromSquare.Piece != Pieces.None)
                        {
                            if (new MoveValidation(fromSquare, toSquare,
                                fromSquare.Piece == Pieces.Pawn).Validate)
                            {
                                // Avoid capture of same piece color
                                if (fromSquare.PieceColor != toSquare.PieceColor)
                                {
                                    if (handlePassant)
                                    {
                                        if (lastMove.PieceColor != fromSquare.PieceColor)
                                            lastMove.SetPiece(Pieces.None, PieceColor.None);
                                        else
                                        {
                                            MoveValidation.GhostSquare.clearSquare();
                                            return;
                                        }
                                    }

                                    if (fromSquare.Piece != Pieces.Pawn)
                                    {
                                        if (toSquare.Piece == Pieces.GhostPawn)
                                            toSquare.clearSquare();
                                    }

                                    toSquare.SetPiece(fromSquare.Piece, fromSquare.PieceColor);
                                    fromSquare.clearSquare();
                                    addMoveAnnotation();
                                    ChessContext.Core.ChangeTurn();
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            toSquare.BackColor = Color.LightGreen;
            toSquare.IsSelected = true;

            fromSquare = toSquare;
        }

        private static void addMoveAnnotation()
        {
            if (ChessContext.Core.WhosPlaying == PieceColor.White)
                ChessContext.Core.MoveList.Add(new Annotattion(fromSquare, toSquare));

            if (ChessContext.Core.WhosPlaying == PieceColor.Black)
                ChessContext.Core.MoveList2.Add(new Annotattion(fromSquare, toSquare));
        }

        bool handlePassant
        {
            get
            {
                return ChessContext.Core.IsPassantActive &&
                       fromSquare.Piece == Pieces.Pawn && toSquare.Piece == Pieces.GhostPawn;
            }
        }

        void clearSquare()
        {
            BackgroundImage = null;
            Piece = Pieces.None;
            PieceColor = PieceColor.None;
        }
    }
}

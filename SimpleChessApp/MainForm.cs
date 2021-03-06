﻿using SimpleChessApp.Game;
using System;
using System.Drawing;
using System.Windows.Forms;
using static SimpleChessApp.Game.ChessContext;

namespace SimpleChessApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            label1.Text = "";
            #region Stupid Region
            // This is stupid to add one event for each item but menustrip sucks :D
            // and this is the only way
            knightToolStripMenuItem1.Click += Item_Click;
            queenToolStripMenuItem1.Click += Item_Click;
            kingToolStripMenuItem1.Click += Item_Click;
            bishopToolStripMenuItem1.Click += Item_Click;
            rookToolStripMenuItem1.Click += Item_Click;
            restartToolStripMenuItem.Click += Item_Click;
            clearBoardToolStripMenuItem.Click += Item_Click;
            promotionToolStripMenuItem.Click += Item_Click;
            castlingToolStripMenuItem.Click += Item_Click;
            passantToolStripMenuItem.Click += Item_Click;
            checkToolStripMenuItem.Click += Item_Click; 
            discoveredCheckToolStripMenuItem.Click += Item_Click;
            checkOnCastlingToolStripMenuItem.Click += Item_Click;

            passantToolStripMenuItem.Tag = DebugItems.Passant;
            castlingToolStripMenuItem.Tag = DebugItems.Castling;
            promotionToolStripMenuItem.Tag = DebugItems.Promotion;
            checkToolStripMenuItem.Tag = DebugItems.Check;
            discoveredCheckToolStripMenuItem.Tag = DebugItems.DiscoverCheck;
            checkOnCastlingToolStripMenuItem.Tag = DebugItems.CheckOnCastling;

            restartToolStripMenuItem.Tag = GameControl.Restart;
            clearBoardToolStripMenuItem.Tag = GameControl.ClearBoard;

            knightToolStripMenuItem1.Tag = Pieces.Knight;
            queenToolStripMenuItem1.Tag = Pieces.Queen;
            kingToolStripMenuItem1.Tag = Pieces.King;
            bishopToolStripMenuItem1.Tag = Pieces.Bishop;
            rookToolStripMenuItem1.Tag = Pieces.Rook;
            #endregion
            Square.Action += Square_Action;

            // #252526 #2D2D30
            var c = ColorTranslator.FromHtml("#2D2D30");
            BackColor = c;
            ForeColor = Color.WhiteSmoke;
            menuStrip1.BackColor = c;
            menuStrip1.ForeColor = Color.WhiteSmoke;

        }

        private void Square_Action(object sender, EventArgs e)
        {
            label1.Text = sender.ToString();
            fillListBox();
        }

        private void fillListBox()
        {
            listBox1.Items.Clear();
            foreach (var item in Core[0].ChessBoard.WhitePieces.Values)
                listBox1.Items.Add(item);
            listBox1.DisplayMember = "SpecialName";

            listBox2.Items.Clear();
            foreach (var item in Core[0].ChessBoard.BlackPieces.Values)
                listBox2.Items.Add(item);
            listBox2.DisplayMember = "SpecialName";

            //if (Core[0].ChessBoard.From != null)
            //    if (Core[0].ChessBoard.From.Piece.Color == PieceColor.White)
            //        listBox1.SelectedItem = Core[0].ChessBoard.From.Piece;
            //    else
            //        listBox2.SelectedItem = Core[0].ChessBoard.From.Piece;
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var x = sender as ToolStripMenuItem;
            handleDebug(x, Core[0]);
            handleDebug(x, Core[1]);
        }

        private void handleDebug(ToolStripMenuItem x, ChessCore w)
        {
            if (x.Tag is GameControl)
            {
                var z = (GameControl)x.Tag;
                if (z == GameControl.ClearBoard) w.ChessBoard.ClearBoard();
                if (z == GameControl.Restart) w.RestartGame();
            }

            if (x.Tag is DebugItems)
            {
                var z = (DebugItems)x.Tag;
                if (z == DebugItems.Passant) w.TestPassant();
                if (z == DebugItems.Castling) w.TestCastling();
                if (z == DebugItems.Promotion) w.TestPromotion();
                if (z == DebugItems.Check) w.TestCheck();
                if (z == DebugItems.DiscoverCheck) w.TestDiscoverCheck();
                if (z == DebugItems.CheckOnCastling) w.CheckOnCastling();
            }

            if (x.Tag is Pieces) w.TestSinglePiece((Pieces)x.Tag);

            fillListBox();
        }

        protected override void OnLoad(EventArgs e)
        {
            designBoard1.SetBoard(ImageLayout.Center, false, false, true);
            designBoard2.SetBoard(ImageLayout.Stretch, true, true, false);
            Core.Add(new ChessCore(designBoard1.Board));
            Core.Add(new ChessCore(designBoard1.Board));

            fillListBox();
        }

        #region Stupid Enums
        enum GameControl
        {
            Restart,
            ClearBoard
        }

        enum DebugItems
        {
            Passant,
            Castling,
            Promotion,
            Check,
            DiscoverCheck,
            CheckOnCastling
        }
        #endregion

        private void checkOnCastlingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
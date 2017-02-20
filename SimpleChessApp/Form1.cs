﻿using SimpleChessApp.Chess;
using System;
using System.Windows.Forms;
using static SimpleChessApp.Chess.ChessContext;

namespace SimpleChessApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
            label2.Text = "";
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

            passantToolStripMenuItem.Tag = DebugItems.Passant;
            castlingToolStripMenuItem.Tag = DebugItems.Castling;
            promotionToolStripMenuItem.Tag = DebugItems.Promotion;

            restartToolStripMenuItem.Tag = GameControl.Restart;
            clearBoardToolStripMenuItem.Tag = GameControl.ClearBoard;

            knightToolStripMenuItem1.Tag = Pieces.Knight;
            queenToolStripMenuItem1.Tag = Pieces.Queen;
            kingToolStripMenuItem1.Tag = Pieces.King;
            bishopToolStripMenuItem1.Tag = Pieces.Bishop;
            rookToolStripMenuItem1.Tag = Pieces.Rook;
            #endregion

            Square.Action += Square_Action;
        }

        private void Square_Action(object sender, EventArgs e)
        {
            label1.Text = sender.ToString();
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var x = sender as ToolStripMenuItem;
            handleDebug(x, Core[0]);
            handleDebug(x, Core[1]);
        }

        private static void handleDebug(ToolStripMenuItem x, ChessCore w)
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
            }

            if (x.Tag is Pieces) w.TestSinglePiece((Pieces)x.Tag);
        }

        protected override void OnLoad(EventArgs e)
        {
            var ChessBoard1 = new Board(panel1, false, false, true);
            var ChessBoard2 = new Board(panel2, true, true, false);

            Core.Add(new ChessCore(ChessBoard1));
            Core.Add(new ChessCore(ChessBoard2));

            bindingSource1.DataSource = Core[0].ChessBoard.WhitePieces.Values;
            listBox1.DisplayMember = "SpecialName";
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
            Promotion
        }
        #endregion
    }
}
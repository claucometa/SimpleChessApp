using SimpleChessApp.Chess;
using System;
using System.Text;
using System.Windows.Forms;
using static SimpleChessApp.ChessContext;

namespace SimpleChessApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Core.NextTurn += Core_NextTurn;
            Core.ActionChanged += Core_ActionChanged;
            label1.Text = "None";

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
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var x = sender as ToolStripMenuItem;

            if (x.Tag is GameControl)
            {
                var z = (GameControl)x.Tag;
                if (z == GameControl.ClearBoard) Core.ChessBoard.ClearBoard();
                if (z == GameControl.Restart) Core.RestartGame();
            }

            if (x.Tag is DebugItems)
            {
                var z = (DebugItems)x.Tag;
                if (z == DebugItems.Passant) Core.TestPassant();
                if (z == DebugItems.Castling) Core.TestCastling();
                if (z == DebugItems.Promotion) Core.TestPromotion();
            }

            if (x.Tag is Pieces)
            {
                Core.TestSinglePiece((Pieces)x.Tag);
            }
        }

        private void Core_NextTurn(object sender, EventArgs e)
        {
            describe();
        }

        private void Core_ActionChanged(object sender, ActionEventArgs e)
        {
            var x = (Square)sender;
            numericUpDown1.Value = x.File;
            numericUpDown2.Value = x.Rank;
            label1.Text = e.Action.ToString();
        }

        protected override void OnLoad(EventArgs e)
        {
            Core.BuildBoard(panel1);

            listBox1.DataSource = Core.Turns.Moves;

            Core.Turns.Moves.ListChanged += Moves_ListChanged;

            listBox3.DataSource = Core.ChessBoard.WhitePieces;
            listBox4.DataSource = Core.ChessBoard.BlackPieces;
            listBox2.DataSource = Core.ChessBoard.BlackCaptured;
            listBox5.DataSource = Core.ChessBoard.WhiteCaptured;
            listBox3.DisplayMember = "SpecialName";
            listBox4.DisplayMember = "SpecialName";
            listBox2.DisplayMember = "SpecialName";
            listBox5.DisplayMember = "SpecialName";

            describe();
        }

        private void Moves_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            if (e.ListChangedType == System.ComponentModel.ListChangedType.ItemAdded)
            {
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        }

        private void describe()
        {
            var x = new StringBuilder();
            var turno = Core.WhosPlaying == PieceColor.Black ? "Black's turn" : "White's turn";
            if (!Core.DisableTurn) x.AppendLine($"{turno}");
            x.AppendLine($"Turn enabled: {!Core.DisableTurn}");
            x.AppendLine($"White castling king side: {Core.WhiteCanCastleKingSide}");
            x.AppendLine($"White castling queen side: {Core.WhiteCanCastleQueenSide}");
            x.AppendLine($"Black castling king side: {Core.BlackCanCastleKingSide}");
            x.AppendLine($"Black castling queen side: {Core.BlackCanCastleQueenSide}");
            x.AppendLine($"Check Squares:  {Core.checks.MoveList.Count}");
            textBox1.Text = x.ToString();

            if (!Core.DisableTurn)
            {
                radioButton1.Checked = Core.WhosPlaying == PieceColor.Black;
                radioButton2.Checked = Core.WhosPlaying == PieceColor.White;
            }
            else
            {
                radioButton2.Checked = false;
                radioButton1.Checked = false;
            }
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
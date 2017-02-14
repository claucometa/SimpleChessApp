using SimpleChessApp.Chess;
using System;
using System.Text;
using System.Windows.Forms;
using static SimpleChessApp.Chess.ChessContext;
using static SimpleChessApp.Chess.ChessCore;

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

            #region SinglePiece test
            knightToolStripMenuItem.Tag = Pieces.Knight;
            queenToolStripMenuItem.Tag = Pieces.Queen;
            kingToolStripMenuItem.Tag = Pieces.King;
            bishopToolStripMenuItem.Tag = Pieces.Bishop;
            rookToolStripMenuItem.Tag = Pieces.Rook;
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
            #endregion  
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

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Core.TestSinglePiece((Pieces)e.ClickedItem.Tag);
        }

        protected override void OnLoad(EventArgs e)
        {
            Core.BuildBoard();
            panel1.Controls.Add(Core.ChessBoard);            
            listBox1.DataSource = Core.MoveList;
            listBox2.DataSource = Core.MoveList2;
            describe();
        }
        
        private void describe()
        {
            var x = new StringBuilder();
            var turno = Core.WhosPlaying == PieceColor.Black ? "Black's turn" : "White's turn";
            if (!Core.HasNoTurns) x.AppendLine($"{turno}");
            x.AppendLine($"Turn enabled: {!Core.HasNoTurns}");
            x.AppendLine($"Passant enabled: {Core.IsPassantActive}");
            x.AppendLine($"White castling king side: {Core.WhiteCanCastleKingSide}");
            x.AppendLine($"White castling queen side: {Core.WhiteCanCastleQueenSide}");
            x.AppendLine($"Black castling king side: {Core.BlackCanCastleKingSide}");
            x.AppendLine($"Black castling queen side: {Core.BlackCanCastleQueenSide}");
            textBox1.Text = x.ToString();

            if (!Core.HasNoTurns)
            {
                radioButton1.Checked = Core.WhosPlaying == PieceColor.Black;
                radioButton2.Checked = Core.WhosPlaying == PieceColor.White;
            }
            else
            {
                radioButton2.Checked = false;
                radioButton1.Checked = false;
            }

            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox2.SelectedIndex = listBox2.Items.Count - 1;

            if (Core.WhosPlaying == PieceColor.White)
                listBox1.ClearSelected();

            if (Core.WhosPlaying == PieceColor.Black)
                listBox2.ClearSelected();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Core.RestartGame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Core.TestPassant();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button3, 0, 0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Core.TestCastling();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Core.ChessBoard.ClearBoard();
        }
    }
}
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleChessApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Chess.Square.CliquedSquare += Square_ClickMe;

            #region SinglePiece test
            knightToolStripMenuItem.Tag = Chess.Pieces.Knight;
            queenToolStripMenuItem.Tag = Chess.Pieces.Queen;
            kingToolStripMenuItem.Tag = Chess.Pieces.King;
            bishopToolStripMenuItem.Tag = Chess.Pieces.Bishop;
            rookToolStripMenuItem.Tag = Chess.Pieces.Rook;
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
            #endregion  
        }

        private void Square_ClickMe(object sender, EventArgs e)
        {
            var x = (Chess.Square)sender;
            numericUpDown1.Value = x.File;
            numericUpDown2.Value = x.Rank;
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Chess.ChessContext.Core.TestSinglePiece((Chess.Pieces)e.ClickedItem.Tag);
        }

        protected override void OnLoad(EventArgs e)
        {
            Chess.ChessContext.Core.BuildBoard();
            panel1.Controls.Add(Chess.ChessContext.Core.ChessBoard);
            Chess.ChessContext.Core.GameStatus += Core_GameStatus;
            listBox1.DataSource = Chess.ChessContext.Core.MoveList;
            listBox2.DataSource = Chess.ChessContext.Core.MoveList2;
            describe();
        }

        private void Core_GameStatus(object sender, EventArgs e)
        {
            describe();
        }

        private void describe()
        {
            var x = new StringBuilder();
            var turno = Chess.ChessContext.Core.WhosPlaying == Chess.PieceColor.Black ? "Black's turn" : "White's turn";
            if (!Chess.ChessContext.Core.HasNoTurns) x.AppendLine($"{turno}");
            x.AppendLine($"Turn enabled: {!Chess.ChessContext.Core.HasNoTurns}");
            x.AppendLine($"Passant enabled: {Chess.ChessContext.Core.IsPassantActive}");
            x.AppendLine($"White castling king side: {Chess.ChessContext.Core.WhiteCanCastleKingSide}");
            x.AppendLine($"White castling queen side: {Chess.ChessContext.Core.WhiteCanCastleQueenSide}");
            x.AppendLine($"Black castling king side: {Chess.ChessContext.Core.BlackCanCastleKingSide}");
            x.AppendLine($"Black castling queen side: {Chess.ChessContext.Core.BlackCanCastleQueenSide}");
            textBox1.Text = x.ToString();

            if (!Chess.ChessContext.Core.HasNoTurns)
            {
                radioButton1.Checked = Chess.ChessContext.Core.WhosPlaying == Chess.PieceColor.Black;
                radioButton2.Checked = Chess.ChessContext.Core.WhosPlaying == Chess.PieceColor.White;
            }
            else
            {
                radioButton2.Checked = false;
                radioButton1.Checked = false;
            }

            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox2.SelectedIndex = listBox2.Items.Count - 1;

            if (Chess.ChessContext.Core.WhosPlaying == Chess.PieceColor.Black)
                listBox2.ClearSelected();

            if (Chess.ChessContext.Core.WhosPlaying == Chess.PieceColor.White)
                listBox1.ClearSelected();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Chess.ChessContext.Core.RestartGame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Chess.ChessContext.Core.TestPassant();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button3, 0, 0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Chess.ChessContext.Core.TestCastling();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Chess.ChessContext.Core.ChessBoard.ClearBoard();
        }
    }
}
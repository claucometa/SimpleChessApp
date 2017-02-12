using System;
using System.Text;
using System.Windows.Forms;

namespace SimpleChessApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            #region SinglePiece test
            knightToolStripMenuItem.Tag = Chess.Pieces.Knight;
            queenToolStripMenuItem.Tag = Chess.Pieces.Queen;
            kingToolStripMenuItem.Tag = Chess.Pieces.King;
            bishopToolStripMenuItem.Tag = Chess.Pieces.Bishop;
            rookToolStripMenuItem.Tag = Chess.Pieces.Rook;
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
            #endregion  
        }

        private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Chess.ChessContext.Core.TestSinglePiece((Chess.Pieces)e.ClickedItem.Tag);
        }

        protected override void OnLoad(EventArgs e)
        {
            panel1.Controls.Add(Chess.ChessContext.Core.ChessBoard);
            Chess.ChessContext.Core.GameStatus += Core_GameStatus;
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
    }
}
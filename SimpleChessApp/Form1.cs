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
            #region Fill Todo List
            dataGridView1.DataSource = TodoItem.Items;
            dataGridView1.RowTemplate.Height = 21;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            #endregion
        }

        protected override void OnLoad(EventArgs e)
        {
            panel1.Controls.Add(Chess.ChessContext.Core.ChessBoard);
            Chess.ChessContext.Core.GameStatus += Core_GameStatus;
            describe();
        }

        private void Core_GameStatus(object sender, System.EventArgs e)
        {
            describe();
        }

        private void describe()
        {
            var x = new StringBuilder();
            var turno = Chess.ChessContext.Core.IsBlackPlaying ? "Pretas jogam" : "Brancas jogam";
            x.AppendLine($"Turno: {turno}");
            x.AppendLine($"Passante ativo: {Chess.ChessContext.Core.IsPassantActive}");
            textBox1.Text = x.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Chess.ChessContext.Core.RestartGame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Chess.ChessContext.Core.PassantTest();
        }
    }
}
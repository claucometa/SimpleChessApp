using System.Collections.Generic;
using System.Windows.Forms;

namespace SimpleChessApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
       

            #region TODO LIST
            var x = new List<TodoItem>();
            // Implemented
            x.Add(new TodoItem(false, null, "--------- DONE ---------"));
            x.Add(new TodoItem(true, Difficulty.Easy, "1 Selection"));
            x.Add(new TodoItem(true, Difficulty.Easy, "2 Move with Click && Click"));
            x.Add(new TodoItem(true, Difficulty.Easy, "3 General Capture"));
            x.Add(new TodoItem(true, Difficulty.Easy, "3.1 Avoid capturing of same color"));
            x.Add(new TodoItem(true, Difficulty.Easy, "4 Pawn movement"));
            // Not implemented
            x.Add(new TodoItem(false, null, ""));
            x.Add(new TodoItem(false, null, "--------- TO DO LIST ---------"));
            x.Add(new TodoItem(false, null, "1 Game Flow"));
            x.Add(new TodoItem(false, Difficulty.Easy, "1.1 Switch players"));
            x.Add(new TodoItem(false, Difficulty.Easy, "1.2 Reset board"));
            x.Add(new TodoItem(false, null, "2 Movement"));
            x.Add(new TodoItem(false, Difficulty.Hard, "2.1 Movement rules for each piece"));
            x.Add(new TodoItem(false, Difficulty.Hard, "2.2 Handle move interception"));
            x.Add(new TodoItem(false, null, "2.3 Special moves"));
            x.Add(new TodoItem(false, Difficulty.Medium, "2.3.1 King castle"));
            x.Add(new TodoItem(false, Difficulty.Hard, "2.3.2 Check"));
            x.Add(new TodoItem(false, null, "3. End Game"));
            x.Add(new TodoItem(false, Difficulty.Hard, "3.1 Stale Mate"));
            x.Add(new TodoItem(false, Difficulty.Hard, "3.2 Check Mate"));
            x.Add(new TodoItem(false, Difficulty.Easy, "3.3 Draw by repetition"));
            x.Add(new TodoItem(false, Difficulty.Easy, "4 Pawn promotion"));
            x.Add(new TodoItem(false, Difficulty.Medium, "5 En Passant"));
            x.Add(new TodoItem(false, Difficulty.Easy, "6 Chess annotation"));
            #endregion

            dataGridView1.DataSource = x;
            dataGridView1.RowTemplate.Height = 21;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}

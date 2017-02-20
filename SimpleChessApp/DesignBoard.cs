using SimpleChessApp.Chess;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp
{
    public partial class DesignBoard : UserControl
    {
        public Board Board;

        public DesignBoard()
        {
            InitializeComponent();

            for (int i = 0; i < 8; i++)
            {
                tableLayoutPanel1.Controls.Add(
                    new NiceLabel() { Text = "abcdefgh"[i].ToString() }, i, 0);

                tableLayoutPanel2.Controls.Add(
                    new NiceLabel() { Text = (8 - i).ToString() }, 0, i);
            }
        }

        public void SetBoard(bool flipped = false, bool allMoves = false, bool selected = false)
        {
            Board = new Board(centerPanel, flipped, allMoves, selected);
        }
    }
}

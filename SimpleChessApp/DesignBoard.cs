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

            // #252526 #2D2D30
            var c = ColorTranslator.FromHtml("#252526");
            BackColor = c;

            for (int i = 0; i < 8; i++)
            {
                tableLayoutPanel1.Controls.Add(
                    new NiceLabel() { Text = (8 - i).ToString() }, 0, i);

                tableLayoutPanel2.Controls.Add(
                    new NiceLabel() { TextAlign = ContentAlignment.TopRight, Text = "abcdefgh"[i].ToString() }, i, 0);

            }
        }

        public void SetBoard(ImageLayout pawnSize = ImageLayout.Center, bool flipped = false, bool allMoves = false, bool selected = false)
        {
            Board = new Board(centerPanel, pawnSize, flipped, allMoves, selected);
        }
    }
}

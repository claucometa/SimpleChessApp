using System.Windows.Forms;

namespace SimpleChessApp
{
    public partial class FrmGame : Form
    {
        public FrmGame()
        {
            InitializeComponent();
            label1.Text = Properties.Resources.implemented;
            label2.Text = Properties.Resources.todolist;
        }
    }
}

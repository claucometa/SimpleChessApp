using System.Drawing;
using System.Windows.Forms;

namespace SimpleChessApp
{
    public class NiceLabel : Label
    {
        public NiceLabel()
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            ForeColor = Color.WhiteSmoke;
            TextAlign = ContentAlignment.MiddleCenter;
            Margin = new Padding(0);
            Font = new Font(new FontFamily("Arial"), 8, FontStyle.Bold);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleChessApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var x = new StringBuilder();
            x.AppendLine("Implementations");
            x.AppendLine("--------------------------------------");
            x.AppendLine("1. Selection");
            x.AppendLine("2. Move with Click && Click");
            x.AppendLine("3. Capture");
            x.AppendLine("3.1 Avoid capturing of same color");
            x.AppendLine("");
            x.AppendLine("");
            x.AppendLine("TODO LIST");
            x.AppendLine("--------------------------------------");
            x.AppendLine("1. Switch players");
            x.AppendLine("2. Movement rules for each piece");
            x.AppendLine("3. Handle move interception");
            x.AppendLine("4. King castle");
            x.AppendLine("5. Check");
            x.AppendLine("6. Stale Mate");
            x.AppendLine("7. Check Mate");
            x.AppendLine("8. Pawn promotion");

            label1.Text = x.ToString();
        }
    }
}

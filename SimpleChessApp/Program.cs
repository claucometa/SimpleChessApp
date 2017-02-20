using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace SimpleChessApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            BindingList<KeyValuePair<int, string>> z = new BindingList<KeyValuePair<int, string>>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QPerfMon
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                if (args.Length == 0)
                {
                    Application.Run(new MainForm(new string[] { @".\Processor\% Processor Time\_Total" }));
                }
                else
                {
                    Application.Run(new MainForm(args));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

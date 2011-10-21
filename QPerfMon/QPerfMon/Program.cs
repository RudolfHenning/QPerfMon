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
            if (Properties.Settings.Default.NewVersion)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.NewVersion = false;
                Properties.Settings.Default.Save();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                if (args.Length == 0)
                {
                    Application.Run(new MainForm(new string[] { @"<xml><machine>.</machine><category>Processor</category><counter>% Processor Time</counter><instance>_Total</instance><scale>1</scale><color>red</color></xml>" }));
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

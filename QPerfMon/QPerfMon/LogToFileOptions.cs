using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace QPerfMon
{
    public partial class LogToFileOptions : Form
    {
        public LogToFileOptions()
        {
            InitializeComponent();
        }

        public string LoggingDirectory { get; set; }
        public string LoggingFileName { get; set; }
        public bool LoggingAppendDateTime { get; set; }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (! Directory.Exists(txtLoggingDirectory.Text))
            {
                MessageBox.Show("No or invalid directory specified!", "Directory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLoggingDirectory.Focus();
            }
            else if (txtLoggingFileName.Text.Length == 0)
            {
                MessageBox.Show("No file name specified!", "File name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLoggingDirectory.Focus();
            }
            else
            {
                LoggingDirectory = txtLoggingDirectory.Text;
                LoggingFileName = txtLoggingFileName.Text;
                LoggingAppendDateTime = chkLoggingAppendDateTime.Checked;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cmdSelectColor_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = txtLoggingDirectory.Text;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtLoggingDirectory.Text = fbd.SelectedPath;
            }
        }

        private void LogToFileOptions_Load(object sender, EventArgs e)
        {
            txtLoggingDirectory.Text = LoggingDirectory;
            txtLoggingFileName.Text = LoggingFileName;
            chkLoggingAppendDateTime.Checked = LoggingAppendDateTime;
        }


    }
}

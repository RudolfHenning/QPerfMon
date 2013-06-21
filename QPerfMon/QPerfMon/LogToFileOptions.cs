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

        #region Properties
        public string LoggingDirectory { get; set; }
        public string LoggingFileName { get; set; }
        public bool LoggingAppendDateTime { get; set; }
        public long LoggingMinimumDiskSpaceLimitMB { get; set; }
        public long LoggingCreateNewFileEveryMB { get; set; }
        public int LoggingSampleRate { get; set; }
        public int LoggingDecimalDigits { get; set; }
        public string ValueSeparator { get; set; }
        #endregion

        #region Form events
        private void LogToFileOptions_Load(object sender, EventArgs e)
        {
            txtLoggingDirectory.Text = LoggingDirectory;
            txtLoggingFileName.Text = LoggingFileName;
            chkLoggingAppendDateTime.Checked = LoggingAppendDateTime;
            numericUpDownMinimumDiskSpaceLimitMB.Value = LoggingMinimumDiskSpaceLimitMB;
            numericUpDownCreateNewFileEveryMB.Value = LoggingCreateNewFileEveryMB;
            numericUpDownSampleRate.Value = LoggingSampleRate;
            numericUpDownDecimals.Value = LoggingDecimalDigits;
            txtValueSeparator.Text = ValueSeparator;
        } 
        #endregion

        #region Button events
        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtLoggingDirectory.Text))
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
                LoggingMinimumDiskSpaceLimitMB = (long)numericUpDownMinimumDiskSpaceLimitMB.Value;
                LoggingCreateNewFileEveryMB = (long)numericUpDownCreateNewFileEveryMB.Value;
                LoggingSampleRate = (int)numericUpDownSampleRate.Value;
                LoggingDecimalDigits = (int)numericUpDownDecimals.Value;
                ValueSeparator = txtValueSeparator.Text;

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
        #endregion

    }
}

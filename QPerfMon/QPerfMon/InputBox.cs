using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QPerfMon
{
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();
        }

        public string WindowTitle { get; set; }
        public string Prompt { get; set; }
        public string SelectedValue { get; set; }

        private void InputBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            Text = WindowTitle;
            lblPromptMessage.Text = Prompt;
            txtValue.Text = SelectedValue;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            SelectedValue = txtValue.Text;
        }
    }
}
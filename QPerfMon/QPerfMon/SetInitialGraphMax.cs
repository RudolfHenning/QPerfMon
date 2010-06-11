using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QPerfMon
{
    public partial class SetInitialGraphMax : Form
    {
        public SetInitialGraphMax()
        {
            InitializeComponent();
        }

        public int InitialMaximum { get; set; }

        private void SetInitialGraphMax_Load(object sender, EventArgs e)
        {
            numericUpDownMax.Value = InitialMaximum; 
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            InitialMaximum = (int)numericUpDownMax.Value; 
        }
    }
}

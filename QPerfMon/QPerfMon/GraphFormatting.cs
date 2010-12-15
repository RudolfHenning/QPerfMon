using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QPerfMon
{
    public partial class GraphFormatting : Form
    {
        public GraphFormatting()
        {
            InitializeComponent();
        }

        public Color GraphBackgroundColor { get; set; }
        public Color GridColor { get; set; }
        public Font LabelFont { get; set; }
        public Color LabelForeColor { get; set; }

        private void GraphFormatting_Load(object sender, EventArgs e)
        {
            picBackgroundColor.BackColor = GraphBackgroundColor;
            picGridColor.BackColor = GridColor;
            txtFont.Font = LabelFont;
            txtFont.Text = LabelFont.Name;
            picLabelForeColor.BackColor = LabelForeColor;
        }

        private void cmdSelectBackgroundColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = picBackgroundColor.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                picBackgroundColor.BackColor = colorDialog1.Color;
            }
        }

        private void cmdSelectGridColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = picGridColor.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                picGridColor.BackColor = colorDialog1.Color;
            }
        }

        private void cmdSelectLabelForeColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = picLabelForeColor.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                picLabelForeColor.BackColor = colorDialog1.Color;
            }
        }

        private void cmdSelectLabelFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = txtFont.Font;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFont.Font = fontDialog1.Font;
                txtFont.Text = fontDialog1.Font.Name;
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            GraphBackgroundColor = picBackgroundColor.BackColor;
            GridColor = picGridColor.BackColor;
            LabelFont = txtFont.Font;
            LabelForeColor = picLabelForeColor.BackColor;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

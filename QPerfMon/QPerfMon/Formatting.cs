using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HenIT.Windows.Controls.C2DPushGraph.Graphing;

namespace QPerfMon
{
    public partial class Formatting : Form
    {
        public Formatting()
        {
            InitializeComponent();
        }

        private double scale = 1;
        public double SelectedScale
        {
            get { return scale; }
            set { scale = value; }
        }

        private Color color;
        public Color SelectedColor
        {
            get { return color; }
            set { color = value; }
        }

        private LinePlotStyle plotStyle;
        public LinePlotStyle PlotStyle
        {
            get { return plotStyle; }
            set { plotStyle = value; }
        }

        private void Formatting_Load(object sender, EventArgs e)
        {
            pictureBoxColor.BackColor = color;

            cboScale.Items.Add("1000000");
            cboScale.Items.Add("100000");
            cboScale.Items.Add("10000");
            cboScale.Items.Add("1000");
            cboScale.Items.Add("100");
            cboScale.Items.Add("10");
            cboScale.Items.Add("1");
            cboScale.Items.Add("0.1");
            cboScale.Items.Add("0.01");
            cboScale.Items.Add("0.001");
            cboScale.Items.Add("0.0001");
            cboScale.Items.Add("0.00001");
            cboScale.Items.Add("0.000001");
            cboScale.Items.Add("0.0000001");

            for (int i = 0; i < cboScale.Items.Count ; i++)
            {
                if (double.Parse(cboScale.Items[i].ToString()) == scale)
                {
                    cboScale.SelectedIndex = i;
                    break;
                }   
            }
            switch (plotStyle)
            {
                case LinePlotStyle.None:
                    cboPlotStyle.SelectedIndex = 0;
                    break;
                case LinePlotStyle.Dots:
                    cboPlotStyle.SelectedIndex = 1;
                    break;
                case LinePlotStyle.Cross:
                    cboPlotStyle.SelectedIndex = 2;
                    break;
                case LinePlotStyle.Ex:
                    cboPlotStyle.SelectedIndex = 3;
                    break;
                default:
                    cboPlotStyle.SelectedIndex = 0;
                    break;
            }
        }

        //private void AddListColor(Color color)
        //{
        //    ListViewItem lvi = new ListViewItem(">");
        //    lvi.UseItemStyleForSubItems = false;
        //    ListViewItem.ListViewSubItem lvis = new ListViewItem.ListViewSubItem();
        //    lvis.Text = "";
        //    lvis.ForeColor = color;
        //    lvis.BackColor = color;
        //    lvi.SubItems.Add(lvis);            
        //    lvwColors.Items.Add(lvi);
        //}

        private void lvwColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckOKEnabled();
        }

        private void CheckOKEnabled()
        {
            cmdOK.Enabled = (cboScale.SelectedIndex > -1);
        }

        private void lvwScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckOKEnabled();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (cboScale.SelectedIndex > -1)
            {
                color = pictureBoxColor.BackColor;
                scale = double.Parse(cboScale.Items[cboScale.SelectedIndex].ToString());
                plotStyle = (LinePlotStyle)cboPlotStyle.SelectedIndex;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cmdSelectColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = pictureBoxColor.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBoxColor.BackColor = colorDialog1.Color;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HenIT.Windows.Controls.Graphing;

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

        private System.Drawing.Drawing2D.DashStyle dashStyle;
        public System.Drawing.Drawing2D.DashStyle DashStyle
        {
            get { return dashStyle; }
            set { dashStyle = value; }
        }

        public bool MultiItemFormat { get; set; }

        private void Formatting_Load(object sender, EventArgs e)
        {
            pictureBoxColor.BackColor = color;

            cboScale.Items.Add("100000000");
            cboScale.Items.Add("10000000");
            cboScale.Items.Add("1000000");
            cboScale.Items.Add("100000");
            cboScale.Items.Add("10000");
            cboScale.Items.Add("1000");
            cboScale.Items.Add("100");
            cboScale.Items.Add("10");
            cboScale.Items.Add("1");
            //To address different cultural formatting issues this has to be added in code.
            cboScale.Items.Add(String.Format("{0:F1}", 0.1));
            cboScale.Items.Add(String.Format("{0:F2}", 0.01));
            cboScale.Items.Add(String.Format("{0:F3}", 0.001));
            cboScale.Items.Add(String.Format("{0:F4}", 0.0001));
            cboScale.Items.Add(String.Format("{0:F5}", 0.00001));
            cboScale.Items.Add(String.Format("{0:F6}", 0.000001));
            cboScale.Items.Add(String.Format("{0:F7}", 0.0000001));
            cboScale.Items.Add(String.Format("{0:F8}", 0.00000001));
            cboScale.Items.Add(String.Format("{0:F9}", 0.000000001));
  
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
                case LinePlotStyle.Box:
                    cboPlotStyle.SelectedIndex = 4;
                    break;
                case LinePlotStyle.Triangle:
                    cboPlotStyle.SelectedIndex = 5;
                    break;
                default:
                    cboPlotStyle.SelectedIndex = 0;
                    break;
            }
            switch (dashStyle)
            {
                case System.Drawing.Drawing2D.DashStyle.Dash:
                    cboDashStyle.SelectedIndex = 1;
                    break;
                case System.Drawing.Drawing2D.DashStyle.Dot:
                    cboDashStyle.SelectedIndex = 2;
                    break;                
                case System.Drawing.Drawing2D.DashStyle.DashDot:
                    cboDashStyle.SelectedIndex = 3;
                    break;
                case System.Drawing.Drawing2D.DashStyle.DashDotDot:
                    cboDashStyle.SelectedIndex = 4;
                    break;                
                default:
                    cboDashStyle.SelectedIndex = 0;
                    break;
            }
            if (MultiItemFormat)
            {
                pictureBoxColor.Enabled = false;
                cmdSelectColor.Enabled = false;
            }
        }

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
                dashStyle = (System.Drawing.Drawing2D.DashStyle)cboDashStyle.SelectedIndex;
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


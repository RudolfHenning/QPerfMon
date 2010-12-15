using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace HenIT.Windows.Controls.Graphing
{
    internal class FlowLine : ILine
    {
        public FlowLine(string nameId, Color color, double scale)
        {
            this.nameId = nameId;
            this.color = color;
            this.scale = scale;
        }

        #region Properties
        private string nameId;
        public string NameId
        {
            get
            {
                return nameId;
            }
            set
            {
                nameId = value;
            }
        }
        private List<ValueTimeInstance> magnitudeList = new List<ValueTimeInstance>();
        public List<ValueTimeInstance> MagnitudeList 
        {
            get { return magnitudeList; }
        }
        private Color color;
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
        private LinePlotStyle plotStyle = LinePlotStyle.None;
        public LinePlotStyle PlotStyle
        {
            get
            {
                return plotStyle;
            }
            set
            {
                plotStyle = value;
            }
        }
        private double scale;
        public double Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }
        private uint thickness = 2;
        public uint Thickness
        {
            get
            {
                return thickness;
            }
            set
            {
                thickness = value;
            }
        }
        private bool visible = true;
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        } 
        #endregion

        #region Public methods
        public void Clear()
        {
            magnitudeList = null;
            magnitudeList = new List<ValueTimeInstance>();
        }
        public double GetMaxValue()
        {
            double max = 0;
            if (MagnitudeList.Count > 0)
            {
                max = MagnitudeList[0].Magnitude;
                for (int i = 1; i < MagnitudeList.Count; i++)
                {
                    if (MagnitudeList[i].Magnitude > max)
                        max = MagnitudeList[i].Magnitude;
                }
            }
            return max;
        }
        public double GetMinValue()
        {
            double min = 0;
            if (MagnitudeList.Count > 0)
            {
                min = MagnitudeList[0].Magnitude;
                for (int i = 1; i < MagnitudeList.Count; i++)
                {
                    if (MagnitudeList[i].Magnitude < min)
                        min = MagnitudeList[i].Magnitude;
                }
            }
            return min;
        }         
        #endregion
    }
}

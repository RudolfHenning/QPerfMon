using System;
using System.Drawing;
using System.Collections.Generic;
namespace HenIT.Windows.Controls.Graphing
{
    public interface ILine
    {
        #region Properties
        List<ValueTimeInstance> MagnitudeList { get; }
        Color Color { get; set; }
        LinePlotStyle PlotStyle { get; set; }
        double Scale { get; set; }
        uint Thickness { get; set; }
        bool Visible { get; set; }
        string NameId { get; set; }
        #endregion

        void Clear();        
        double GetMaxValue();
        double GetMinValue();
        
    }
}

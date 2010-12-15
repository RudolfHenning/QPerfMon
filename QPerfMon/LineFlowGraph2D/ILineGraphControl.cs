using System;
using System.Drawing;
using System.Windows.Forms;
namespace HenIT.Windows.Controls.Graphing
{
    interface ILineGraphControl
    {
        #region Properties
        bool AutoAdjustPeek { get; set; }
        bool AutoGridSize { get; set; }
        Color GridColor { get; set; }
        ushort GridSize { get; set; }
        bool HighQuality { get; set; }
        int LineCount { get; }
        ushort LineInterval { get; set; }
        string MaxLabel { get; set; }
        double MaxPeekMagnitude { get; set; }
        double MaxPeekMagnitudePreAutoScale { get; set; }
        string MinLabel { get; set; }
        double MinPeekMagnitude { get; set; }
        bool ShowGrid { get; set; }
        bool ShowLabels { get; set; }
        Color TextColor { get; set; }
        #endregion

        event EventHandler AutoScaleEvent;

        ILine AddLine(string nameID, Color clr, double scale);
        void ClearAllLines();
        double GetCurrentMaxOnGraph();
        ILine GetLine(string nameID);        
        bool LineExists(string nameID);
        bool Push(double magnitude, string nameID);
        bool RemoveLine(string nameID);
        void SetSelectedLine(string nameID);
        void UpdateGraph();
    }
}

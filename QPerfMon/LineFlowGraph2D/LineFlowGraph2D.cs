using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace HenIT.Windows.Controls.Graphing
{
    public class LineFlowGraph2D : Control, ILineGraphControl
    {
        #region Private vars
        private List<ILine> lines = new List<ILine>();
        private int maxCoords = -1;
        private bool isMaxLabelSet;
        private bool isMinLabelSet;
        private ILine selectedLine = null;
        private int offsetX = 0;
        private int moveOffset = 0;
        private int totalUpdateCount = 0;
        private System.ComponentModel.IContainer components = null;
        private int oldestXOffset = 0;
        private DateTime lastClickTime = DateTime.Now;
        private DateTime clickSelectedTime = DateTime.Now;
        private bool clickedSelectedValueTime = false;
        #endregion

        public LineFlowGraph2D()
        {
            InitializeComponent();
            InitializeStyles();
        }

        #region Properties
        private bool autoAdjustPeek;
        public bool AutoAdjustPeek
        {
            get
            {
                return autoAdjustPeek;
            }
            set
            {
                if (autoAdjustPeek != value)
                {
                    autoAdjustPeek = value;
                    Refresh();
                }
            }
        }
        private bool autoGridSize;
        public bool AutoGridSize
        {
            get
            {
                return autoGridSize;
            }
            set
            {
                autoGridSize = value;
                Refresh();
            }
        }
        private Color gridColor = Color.Green;
        public Color GridColor
        {
            get { return gridColor; }
            set
            {
                if (gridColor != value)
                {
                    gridColor = value;
                    Refresh();
                }
            }
        }
        private ushort gridSize = 15;
        public ushort GridSize
        {
            get
            {
                return gridSize;
            }
            set
            {
                if (gridSize != value)
                {
                    gridSize = value;
                    Refresh();
                }
            }
        }
        public bool highQuality = true;
        public bool HighQuality
        {
            get
            {
                return highQuality;
            }
            set
            {
                if (value != highQuality)
                {
                    highQuality = value;
                    Refresh(); // Force redraw
                }
            }
        }

        public int LineCount
        {
            get { return lines.Count; }
        }
        private ushort lineInterval = 5;
        public ushort LineInterval
        {
            get { return lineInterval; }
            set
            {
                if (lineInterval != value)
                {
                    lineInterval = value;
                    maxCoords = -1; // Recalculate
                    Refresh();
                }
            }
        }
        private string maxLabel = "Max";
        public string MaxLabel
        {
            get
            {
                return maxLabel;
            }
            set
            {
                isMaxLabelSet = true;
                if (string.Compare(maxLabel, value) != 0)
                {
                    maxLabel = value;
                    maxCoords = -1; // Recalculate
                    Refresh();
                }
            }
        }
        private double maxPeek = 100;
        public double MaxPeekMagnitude
        {
            get { return maxPeek; }
            set
            {
                maxPeek = value;
                maxPeekPreAutoScale = value;
                RefreshLabels();
            }

        }
        private double maxPeekPreAutoScale = 100;
        public double MaxPeekMagnitudePreAutoScale
        {
            set
            {
                maxPeekPreAutoScale = value;
                RefreshLabels();
            }
            get { return maxPeekPreAutoScale; }
        }
        private string minLabel = "Min";
        public string MinLabel
        {
            get { return minLabel; }
            set
            {
                isMinLabelSet = true;

                if (string.Compare(minLabel, value) != 0)
                {
                    minLabel = value;
                    maxCoords = -1; // Recalculate
                    Refresh();
                }
            }

        }
        private double minPeek = 0;
        public double MinPeekMagnitude
        {
            get { return minPeek; }
            set
            {
                minPeek = value;
                RefreshLabels();
            }
        }
        private bool showGrid = true;
        public bool ShowGrid
        {
            get
            {
                return showGrid;
            }
            set
            {
                if (showGrid != value)
                {
                    showGrid = value;
                    Refresh();
                }
            }
        }
        private bool showLabels = true;
        public bool ShowLabels
        {
            get { return showLabels; }
            set
            {
                if (showLabels != value)
                {
                    showLabels = value;

                    /* We're going to need to recalculate our maximum 
                     * coordinates since our graphable width changed */
                    maxCoords = -1;
                    Refresh();
                }
            }
        }
        private Color textColor = Color.Yellow;
        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                if (textColor != value)
                {
                    textColor = value;
                    Refresh();
                }
            }
        }
        private Font textFont = new Font(new FontFamily("Verdana"), 10, FontStyle.Bold);
        public Font TextFont
        {
            get
            {
                return textFont;
            }
            set
            {
                if (textFont != value)
                {
                    textFont = value;
                    Refresh();
                }
            }
        }
        private bool allowClick = false;
        public bool AllowClick
        {
            get
            {
                return allowClick;
            }
            set
            {
                allowClick = value;
            }
        }
        #endregion

        #region Events
        public event EventHandler AutoScaleEvent;
        private void RaiseAutoScaleEvent()
        {
            if (AutoScaleEvent != null)
            {
                AutoScaleEvent(this, null);
            }
        }
        #endregion

        #region Line handling
        public ILine AddLine(string nameId, Color color, double scale)
        {
            if (LineExists(nameId))
            {
                return GetLine(nameId);
            }

            ILine line = new FlowLine(nameId, color, scale);
            line.Color = color;
            line.Scale = scale;

            lines.Add(line);
            return line;
        }
        public ILine GetLine(string nameId)
        {
            foreach (ILine line in lines)
            {
                if (string.Compare(nameId, line.NameId, true) == 0)
                {
                    return line;
                }
            }
            return null;
        }
        public bool LineExists(string nameId)
        {
            return GetLine(nameId) != null;
        }
        public bool RemoveLine(string nameId)
        {
            ILine line = GetLine(nameId);
            if (line == null)
            {
                return false;
            }

            return lines.Remove(line);
        }
        public void ClearAllLines()
        {
            lines.Clear();
        }
        public void SetSelectedLine(string nameId)
        {
            if (nameId.Length > 0)
                selectedLine = GetLine(nameId);
            else
                selectedLine = null;
        }
        public bool Push(double magnitude, string nameID)
        {
            ILine line = GetLine(nameID);
            if (line == null)
            {
                return false;
            }
            /* Now add the magnitude (push point) to the array of push points, but
               first restrict it to the peek bounds */

            if (!autoAdjustPeek && (magnitude * line.Scale > maxPeek))
            {
                magnitude = maxPeek;
            }
            else if (autoAdjustPeek && (magnitude * line.Scale > maxPeek))
            {
                RaiseAutoScaleEvent();
                maxPeek = magnitude * line.Scale;
                maxLabel = maxPeek.ToString("0");

                RefreshLabels();
            }
            else if (!autoAdjustPeek && (magnitude * line.Scale < minPeek))
            {
                magnitude = minPeek;
            }
            else if (autoAdjustPeek && (magnitude * line.Scale < minPeek))
            {
                minPeek = magnitude;
                RefreshLabels();
            }
            else if (autoAdjustPeek && (maxPeek * line.Scale > maxPeekPreAutoScale * line.Scale))
            {
                CheckMaxPeek();
                RefreshLabels();
            }

            magnitude -= minPeek; //assuming this to be 0

            line.MagnitudeList.Add(new ValueTimeInstance() { Magnitude = magnitude, Time = DateTime.Now });
            return true;
        }
        private ILine GetLongestLine()
        {
            int greatestMCount = 0;
            ILine longestLine = null;
            for (int i = 0; i < lines.Count; i++)
            {
                if (greatestMCount < lines[i].MagnitudeList.Count)
                {
                    greatestMCount = lines[i].MagnitudeList.Count;
                    longestLine = lines[i];
                }
            }
            return longestLine;
        }
        #endregion

        #region Updating graph
        //use to to automatically set the 'Max' value of the graph from available values
        private void CheckMaxPeek()
        {
            double globalMaxDataPoint = 0;
            foreach (ILine line in lines)
            {
                if (line.Visible)
                {
                    foreach (ValueTimeInstance datapoint in line.MagnitudeList)
                    {
                        if (datapoint.Magnitude * line.Scale > globalMaxDataPoint)
                            globalMaxDataPoint = datapoint.Magnitude * line.Scale;
                    }
                }
            }
            if (maxPeek > globalMaxDataPoint)
            {
                if (globalMaxDataPoint > maxPeekPreAutoScale)
                {
                    maxPeek = globalMaxDataPoint;
                }
                else
                    maxPeek = maxPeekPreAutoScale;
                maxLabel = maxPeek.ToString("0");
            }
        }
        public double GetCurrentMaxOnGraph()
        {
            double globalMaxDataPoint = 0;
            foreach (ILine line in lines)
            {
                if (line.Visible)
                {
                    foreach (ValueTimeInstance datapoint in line.MagnitudeList)
                    {
                        double scaledValue = datapoint.Magnitude * line.Scale;
                        if (scaledValue > globalMaxDataPoint)
                            globalMaxDataPoint = scaledValue;
                    }
                }
            }
            return globalMaxDataPoint;
        }

        public void UpdateGraph()
        {
            moveOffset = ((totalUpdateCount * lineInterval)) % gridSize;

            //System.Diagnostics.Trace.WriteLine(string.Format("moveOffset gridSize lineInterval {0} {1} {2}", moveOffset, gridSize, lineInterval));

            CullAndEqualizeMagnitudeCounts();
            this.Refresh();
            if (totalUpdateCount < int.MaxValue)
                totalUpdateCount++;
            else
                totalUpdateCount = 0;
        }
        private void CalculateMaxPushPoints()
        {
            /* Calculate the maximum of push points (magnitudes) that can be 
             * drawn for the graphable display width: */

            maxCoords = ((Width - offsetX) / lineInterval) + 2
                               + (((Width - offsetX) % lineInterval) != 0 ? 1 : 0);

            if (maxCoords <= 0)
            {
                maxCoords = 1;
            }
        }
        private void CullAndEqualizeMagnitudeCounts()
        {
            if (maxCoords == -1)
            {
                /* Maximum push points not yet calculated */
                CalculateMaxPushPoints();
            }

            ILine longestLine = GetLongestLine();
            if (longestLine == null)
                return;
            int greatestMCount = GetLongestLine().MagnitudeList.Count;

            if (greatestMCount == 0)
            {
                return; // No magnitudes
            }

            foreach (ILine line in lines)
            {
                /* If the line has less push points than the line with the greatest
                number of push points, new push points are appended with
                the same magnitude as the previous push point. If no push points
                exist for the line, one is added with the least magnitude possible. */

                if (line.MagnitudeList.Count == 0)
                {
                    line.MagnitudeList.Add(new ValueTimeInstance() { Magnitude = minPeek });
                }

                while (line.MagnitudeList.Count < greatestMCount)
                {
                    line.MagnitudeList.Add(
                        line.MagnitudeList[line.MagnitudeList.Count - 1]);
                }

                int cullsRequired = (line.MagnitudeList.Count - maxCoords) + 1;
                if (cullsRequired > 0)
                {
                    line.MagnitudeList.RemoveRange(0, cullsRequired);
                }
            }
        }
        #endregion

        #region Private methods
        private void RefreshLabels()
        {
            /* Within this function we ensure our labels are up to date
             * if the user isn't using custom labels. It is called whenever
             * the graph's range changes. */
            if (!isMinLabelSet)
            {
                /* Use the minimum magnitude as the label since the
                 * user has not yet assigned a custom label: */

                minLabel = minPeek.ToString();
            }
            if (!isMaxLabelSet)
            {
                /* Use the maximum magnitude as the label since the
                 * user has not yet assigned a custom label: */

                maxLabel = maxPeek.ToString();
            }
        }
        #endregion

        #region overrides
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

        protected override void OnSizeChanged(EventArgs e)
        {
            /* We're going to need to recalculate our maximum 
             * coordinates since our graphable width changed */
            maxCoords = -1;
            Refresh();

            base.OnSizeChanged(e);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            SmoothingMode prevSmoothingMode = g.SmoothingMode;
            g.SmoothingMode = (highQuality ? SmoothingMode.HighQuality : SmoothingMode.Default);

            /* Reset our offset so we don't continually shift to the right: */

            offsetX = 0;

            if (showLabels)
            {
                DrawLabels(ref g);
            }

            if (offsetX != 0)
            {
                /* This is to avoid crossing the left grid boundary when 
                 * working with lines with great thickness */

                g.Clip = new Region(new Rectangle(0, 0, Width - offsetX - 1, Height));
            }

            if (showGrid)
            {
                DrawGrid(ref g);
            }
            g.ResetClip();
            DrawLines(ref g);
            if (allowClick)
            {
                DrawClickSelectetion(ref g);
            }
            

            g.SmoothingMode = prevSmoothingMode;
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                DataClickTest(e);
            base.OnMouseClick(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataClickTest(e);
            }
            base.OnMouseMove(e);
        }
        private void DataClickTest(MouseEventArgs e)
        {
            if (lastClickTime.AddMilliseconds(100) < DateTime.Now)
            {
                lastClickTime = DateTime.Now;
                clickedSelectedValueTime = false;

                if (e.X >= 0 && e.X <= Width &&
                    e.Y >= 0 && e.Y <= Height &&
                    e.X > oldestXOffset && e
                    .X < Width - offsetX)
                {
#if DEBUG
                    System.Diagnostics.Trace.WriteLine(string.Format("Mouse X Y oldestXOffset {0} {1} {2}", e.X, e.Y, oldestXOffset));
#endif
                    ILine line = GetLongestLine();
                    if (line != null)
                    {
                        int posX = line.MagnitudeList.Count - (((Width - offsetX) - e.X) / lineInterval) - 1;
                        if (posX >= 0 && posX < line.MagnitudeList.Count)
                        {
                            clickedSelectedValueTime = true;
                            clickSelectedTime = line.MagnitudeList[posX].Time;
                            this.Invalidate();
#if DEBUG
                            System.Diagnostics.Trace.WriteLine(string.Format("posX {0} {1}", posX, clickSelectedTime));
#endif
                        }
                    }
                }
            }
        }
        #endregion

        #region Drawing
        private void InitializeStyles()
        {
            BackColor = Color.Black;

            /* Enable double buffering and similiar techniques to 
             * eliminate flicker */

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;

            SetStyle(ControlStyles.ResizeRedraw, true);
        }
        protected void DrawLabels(ref Graphics g)
        {
            SizeF maxSize = g.MeasureString(maxLabel, textFont);
            SizeF minSize = g.MeasureString(minLabel, textFont);

            float textWidth = ((maxSize.Width > minSize.Width)
                            ? maxSize.Width
                            : minSize.Width) + 6;

            using (SolidBrush textBrush = new SolidBrush(textColor))
            {
                /* Draw the labels (max: Top) (min: Bottom) */
                g.DrawString(maxLabel, textFont, textBrush,
                              Width - textWidth + ((textWidth - maxSize.Width) / 2f),
                              2);

                g.DrawString(minLabel, textFont, textBrush,
                               Width - textWidth + ((textWidth - minSize.Width) / 2f),
                              Height - minSize.Height - 2);
            }
#if DEBUG
            //System.Diagnostics.Trace.WriteLine(string.Format("min max {0} {1}", minLabel, maxLabel));
#endif

            /* Draw the bordering line */

            using (Pen borderPen = new Pen(gridColor, 1))
            {
                g.DrawLine(borderPen, Width - textWidth, 0, Width - textWidth, Height);
            }

            /* Update the offset so we don't draw the graph over the labels */
            offsetX = (int)textWidth;// +6;
        }
        protected void DrawGrid(ref Graphics g)
        {
            int currentWidth = Width;
            using (Pen gridPen = new Pen(gridColor, 1))
            {
                if (autoGridSize)
                {
                    gridSize = (ushort)(Height / 10);
                }

                for (int n = Height - 1; n >= 0; n -= gridSize)
                {
                    g.DrawLine(gridPen, 0, n, currentWidth - offsetX, n);
                }

                for (int n = currentWidth - moveOffset - offsetX; n > 0; n -= gridSize)
                {
                    g.DrawLine(gridPen, n, 0, n, Height);
                }
            } //using (Pen gridPen)
        }
        protected void DrawLines(ref Graphics g)
        {
            ILine longestLine = GetLongestLine();

            foreach (ILine line in lines)
            {
                if (line.MagnitudeList.Count == 0)
                {
                    /* No push points to draw */
                    continue;
                }

                //draw all 'lines' except the 'selected' one that is drawn last
                if (selectedLine == null || (line.NameId != selectedLine.NameId))
                    DrawLine(ref g, line);
            }
            //now draw selected line last
            if (selectedLine != null)
            {
                DrawLine(ref g, selectedLine);
            }
            if (longestLine != null)
                DrawTimes(ref g, longestLine);
        }
        private void DrawLine(ref Graphics g, ILine line)
        {
            if (!line.Visible || line.MagnitudeList.Count == 0)
                return;
            /* Now prepare to draw the line */
            int currentWidth = Width - offsetX;

            using (Pen linePen = new Pen(line.Color, line.Thickness))
            {
#if DEBUG
                //System.Diagnostics.Trace.WriteLine(string.Format("line value {0} {1}", line.NameId, line.MagnitudeList[line.MagnitudeList.Count - 1].Magnitude));
#endif

                Point lastPoint = new Point();
                //start from newest
                lastPoint.X = currentWidth;
                double scaledHeight = line.MagnitudeList[line.MagnitudeList.Count - 1].Magnitude * line.Scale;
                if (scaledHeight >= int.MaxValue)
                    scaledHeight = 0;
                lastPoint.Y = Height - (int)((scaledHeight * Height) / (maxPeek - minPeek));

                for (int n = 0; n < line.MagnitudeList.Count; ++n)
                {
                    /*Get line index since we are drawing from right to left and newest to oldest*/
                    int index = line.MagnitudeList.Count - n - 1;
                    /* Draw a line */
                    int newX = currentWidth - (n * lineInterval);
                    scaledHeight = line.MagnitudeList[index].Magnitude * line.Scale;
                    if (scaledHeight >= int.MaxValue)
                        scaledHeight = 0;
                    int newY = Height - (int)((scaledHeight * Height) / (maxPeek - minPeek));

                    //lines at the top are not visible. Shift it down a little
                    if (newY < 1)
                        newY = 1;

                    g.DrawLine(linePen, lastPoint.X, lastPoint.Y, newX, newY);
                    if (line.PlotStyle != LinePlotStyle.None)
                        DrawLineMarker(ref g, line.Color, newX, newY, line.PlotStyle);

                    lastPoint.X = newX;
                    lastPoint.Y = newY;
                }
            } //using (Pen linePen)
        }
        private void DrawLineMarker(ref Graphics g, Color lineColor, int x, int y, LinePlotStyle style)
        {
            using (Pen plotPen = new Pen(lineColor, 2))
            {
                switch (style)
                {
                    case LinePlotStyle.Dots:
                        g.DrawEllipse(plotPen, x - 2, y - 2, 4, 4);
                        break;
                    case LinePlotStyle.Cross:
                        g.DrawLine(plotPen, x, y - 3, x, y + 3);
                        g.DrawLine(plotPen, x - 3, y, x + 3, y);
                        break;
                    case LinePlotStyle.Ex:
                        g.DrawLine(plotPen, x - 3, y - 3, x + 3, y + 3);
                        g.DrawLine(plotPen, x - 3, y + 3, x + 3, y - 3);
                        break;
                    case LinePlotStyle.Box:
                        g.DrawLine(plotPen, x - 3, y - 3, x + 3, y - 3);
                        g.DrawLine(plotPen, x + 3, y - 3, x + 3, y + 3);
                        g.DrawLine(plotPen, x + 3, y + 3, x - 3, y + 3);
                        g.DrawLine(plotPen, x - 3, y + 3, x - 3, y - 3);
                        break;
                    case LinePlotStyle.Triangle:
                        g.DrawLine(plotPen, x, y - 3, x + 3, y + 3);
                        g.DrawLine(plotPen, x + 3, y + 3, x - 3, y + 3);
                        g.DrawLine(plotPen, x - 3, y + 3, x, y - 3);
                        break;
                    default:
                        break;
                }
            }
        }
        private void DrawTimes(ref Graphics g, ILine longestLine)
        {
            float newestTimeX;
            int oldestTimeX;
            string newestTime;
            string oldestTime;
            float newestTimeY;
            float oldestTimeY;
            int currentWidth = Width;

            oldestTimeX = currentWidth - offsetX - (longestLine.MagnitudeList.Count * lineInterval);
            if (oldestTimeX < 0)
                oldestTimeX = 0;
            oldestXOffset = oldestTimeX;

            if (longestLine != null && longestLine.MagnitudeList.Count > 0)
            {
                using (SolidBrush textBrush = new SolidBrush(textColor))
                {
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;

                    newestTime = longestLine.MagnitudeList[longestLine.MagnitudeList.Count - 1].Time.ToString("HH:mm:ss");
                    SizeF newestSize = g.MeasureString(newestTime, textFont);
                    newestTimeX = currentWidth - offsetX;
                    newestTimeY = (Height - newestSize.Width) / 2;
                    g.DrawString(newestTime, textFont, textBrush, newestTimeX, newestTimeY, stringFormat);

                    if (longestLine.MagnitudeList.Count > 5)
                    {
                        oldestTime = longestLine.MagnitudeList[0].Time.ToString("HH:mm:ss");
                        SizeF oldestSize = g.MeasureString(oldestTime, textFont);
                        oldestTimeY = (Height - oldestSize.Width) / 2;
                        g.DrawString(oldestTime, textFont, textBrush, oldestTimeX, oldestTimeY, stringFormat);
                    }
                    //Draw line at the right
                    using (Pen linePen = new Pen(textBrush))
                    {
                        g.DrawLine(linePen, currentWidth - offsetX, 0, currentWidth - offsetX, Height);
                    }
                }

            }
        }
        private void DrawClickSelectetion(ref Graphics g)
        {
            if (clickedSelectedValueTime)
            {
                ILine line = GetLongestLine();
                int posX = -1;
                string clickSelectedTimeStr = "";
                int currentWidth = Width;
                if (line != null)
                {
                    for (int i = line.MagnitudeList.Count - 1; i >= 0; i--)
                    {
                        if (clickSelectedTime >= line.MagnitudeList[i].Time)
                        {
                            clickSelectedTimeStr = line.MagnitudeList[i].Time.ToString("HH:mm:ss");
                            posX = i;
                            break;
                        }
                    }
                    if (posX == -1)
                    {
                        //probably moved out of scope
                        clickedSelectedValueTime = false;
                    }
                    else
                    {
                        int clickedSelectedX = currentWidth - offsetX - (line.MagnitudeList.Count - 1 - posX) * lineInterval;
                        SizeF clickSelectedTimeSize = g.MeasureString(clickSelectedTimeStr, textFont);

                        using (Pen linePen = new Pen(textColor))
                        {
                            g.DrawLine(linePen, clickedSelectedX, clickSelectedTimeSize.Height, clickedSelectedX, Height);
                            if (clickedSelectedX <= 0)
                                clickedSelectedValueTime = false;
                        }
#if DEBUG
                        System.Diagnostics.Trace.WriteLine(string.Format("clickedSelectedX {0} ", clickedSelectedX));
#endif
                        using (SolidBrush textBrush = new SolidBrush(textColor))
                        {
                            if (clickedSelectedX > (clickSelectedTimeSize.Width / 2))
                            {
                                clickedSelectedX = clickedSelectedX - (int)(clickSelectedTimeSize.Width / 2);
                                if (clickedSelectedX + clickSelectedTimeSize.Width > currentWidth - offsetX)
                                {
                                    clickedSelectedX = currentWidth - offsetX - (int)clickSelectedTimeSize.Width;
                                }
                            }
                            else
                                clickedSelectedX = 0;
                            g.DrawString(clickSelectedTimeStr, textFont, textBrush, clickedSelectedX, 1);
                        }
                    }
                }
            }
        }
        #endregion
    }
}

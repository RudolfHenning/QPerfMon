// ===================================================================
// C2DPushGraph Control
// -------------------------------------------------------------------
// Author: Stuart D. Konen
// E-mail: skonen _|a.t|_ gmail.com
// Date of Release: December 2nd, 2006 
// ===================================================================
// Enhancements made by Rudolf Henning 
// Date March 2010



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Text;


namespace HenIT.Windows.Controls.C2DPushGraph
{
    namespace Graphing
    {
        public class C2DPushGraph : Control
        {
            public event EventHandler AutoScaleEvent;
            private void RaiseAutoScaleEvent()
            {
                if (AutoScaleEvent != null)
                {
                    AutoScaleEvent(this, null);
                }
            }

            // ===================================================================
            // PUBLIC LINEHANDLE CLASS 
            // (Provides public method access to line data members)
            // ===================================================================

            public class LineHandle
            {
                private Line m_Line = null;
                private C2DPushGraph m_Owner = null;

                // ===================================================================

                public LineHandle(ref Object line, C2DPushGraph owner)
                {
                    /* A small hack to get around the compiler error CS0051: */
                    if (string.Compare(line.GetType().Name, "Line") != 0)
                    {
                        throw new System.ArithmeticException(
                            "LineHandle: First Parameter must be " +
                            "type of 'Line' cast to base 'Object'");
                    }

                    m_Line = (Line)line;
                    m_Owner = owner;
                }

                // ===================================================================

                /// <summary> 
                /// Clears any currently displayed magnitudes.
                /// </summary>

                public void Clear()
                {
                    m_Line.m_MagnitudeList.Clear();
                    m_Owner.UpdateGraph();                    
                }
                
                // ===================================================================

                /// <summary> 
                /// Sets or gets the line's current color.
                /// </summary>

                public Color Color
                {
                    set
                    {
                        if (m_Line.m_Color != value)
                        {
                            m_Line.m_Color = value;
                            m_Owner.Refresh();
                        }
                    }
                    get { return m_Line.m_Color; }

                }

                // ===================================================================

                /// <summary> 
                /// Sets or gets the line's thickness in pixels. NOTE: It is advisable
                /// to set HighQuality to false if using a thickness greater than
                /// 2 pixels as the antialiasing creates imperfections.
                /// </summary>

                public uint Thickness
                {
                    set
                    {
                        if (m_Line.m_Thickness != value)
                        {
                            m_Line.m_Thickness = value;
                            m_Owner.Refresh();
                        }
                    }
                    get { return m_Line.m_Thickness; }
                }

                // ===================================================================

                /// <summary> 
                /// Gets or sets a value indicating whether the line is visible.
                /// </summary>

                public bool Visible
                {
                    set
                    {
                        if (m_Line.m_bVisible != value)
                        {
                            m_Line.m_bVisible = value;
                            m_Owner.Refresh();
                        }
                    }
                    get { return m_Line.m_bVisible; }
                }

                // ===================================================================

                /// <summary> 
                /// Gets or sets a value indicating whether this line's magnitudes are
                /// displayed in a bar graph style.
                /// </summary>

                public bool ShowAsBar
                {
                    set
                    {
                        if (m_Line.m_bShowAsBar != value)
                        {
                            m_Line.m_bShowAsBar = value;
                            m_Owner.Refresh();
                        }
                    }
                    get { return m_Line.m_bShowAsBar; }
                }

                public double GetMinValue()
                {
                    if (m_Line == null)
                        return 0;
                    else
                        return m_Line.GetMinValue();
                }
                public double GetMaxValue()
                {
                    if (m_Line == null)
                        return 0;
                    else
                        return m_Line.GetMaxValue();
                }

                public double Scale
                {
                    set
                    {
                        if (m_Line.Scale != value)
                        {
                            m_Line.Scale = value;
                            m_Owner.Refresh();
                        }
                    }
                    get { return m_Line.Scale; }

                }
            }

            // ===================================================================
            // PRIVATE LINE CLASS (Contains Line Data Members)
            // ===================================================================

            private class Line
            {
                public List<double> m_MagnitudeList = new List<double>();
                public Color  m_Color = Color.Green;
                public string m_NameID = "";
                public int    m_NumID = -1;
                public uint   m_Thickness = 1;
                public bool   m_bShowAsBar = false;
                public bool   m_bVisible = true;
                public double scale = 1;
                public double Scale
                {
                    get { return scale; }
                    set { scale = value; }
                }

                // ===================================================================

                public Line(string name)
                {
                    m_NameID = name;
                }

                // ===================================================================

                public Line(int num)
                {
                    m_NumID = num;
                }

                public double GetMinValue()
                {
                    double min = 0;
                    if (m_MagnitudeList.Count > 0)
                    {
                        min = m_MagnitudeList[0];
                        for (int i = 1; i < m_MagnitudeList.Count; i++)
                        {
                            if (m_MagnitudeList[i] < min)
                                min = m_MagnitudeList[i];
                        }

                    }
                    return min;
                }
                public double GetMaxValue()
                {
                    double max = 0;
                    if (m_MagnitudeList.Count > 0)
                    {
                        max = m_MagnitudeList[0];
                        for (int i = 1; i < m_MagnitudeList.Count; i++)
                        {
                            if (m_MagnitudeList[i] > max)
                                max = m_MagnitudeList[i];
                        }

                    }
                    return max;
                }
            }


            // ===================================================================
            // MAIN CONTROL CLASS
            // ===================================================================

            private Color  m_TextColor = Color.Yellow;
            private Color  m_GridColor = Color.Green;
            private string m_MaxLabel = "Max";
            private string m_MinLabel = "Minimum";
            private bool   m_bHighQuality = true;
            private bool   m_bAutoScale = false;
            private bool   m_AutoGridSize = false;
            private bool   m_bMinLabelSet = false;
            private bool   m_bMaxLabelSet = false;
            private bool   m_bShowMinMax = true;
            private bool   m_bShowGrid = true;
            private int    m_MoveOffset = 0;
            private int    m_MaxCoords = -1;
            private int    m_LineInterval = 5;
            private double m_MaxPeek = 100;
            private double m_MaxPeekPreAutoScale = 100;
            private double m_MinPeek = 0;
            private int    m_GridSize = 15;
            private int    m_OffsetX = 0;
            private Line selectedLine = null;


            private List<Line> m_Lines = new List<Line>();
            private System.ComponentModel.IContainer components = null;
            
            // ===================================================================

            public C2DPushGraph()
            {                
                InitializeComponent();
                InitializeStyles();
            }

            // ===================================================================

            public C2DPushGraph(Form Parent)
            {
                Parent.Controls.Add(this);

                InitializeComponent();
                InitializeStyles();
            }

            // ===================================================================

            public C2DPushGraph(Form parent, Rectangle rectPos)
            {
                parent.Controls.Add(this);

                Location = rectPos.Location;
                Height = rectPos.Height;
                Width = rectPos.Width;

                InitializeComponent();
                InitializeStyles();
            }

            // ===================================================================
                   
            /// <summary> 
            /// Clean up any resources being used.
            /// </summary>
            /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

            protected override void Dispose(bool disposing)
            {                
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            #region Component Designer generated code

            // ===================================================================

            /// <summary> 
            /// Required method for Designer support - do not modify 
            /// the contents of this method with the code editor.
            /// </summary>

            private void InitializeComponent()
            {
                components = new System.ComponentModel.Container();
            }

            #endregion

            // ===================================================================

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

            // ===================================================================
            
            public bool AutoGridSize
                {
                    get { return m_AutoGridSize; }
                    set
                    {
                        m_AutoGridSize = value;
                        Refresh();
                    }
                }

            /// <summary> 
            /// Gets or sets the color of any text displayed in the graph (labels).
            /// </summary>

            public Color TextColor
            {
                set
                {
                    if (m_TextColor != value)
                    {
                        m_TextColor = value;
                        Refresh();
                    }
                }
                get { return m_TextColor; }
            }

            // ===================================================================

            /// <summary> 
            /// Gets or sets the graph's grid color.
            /// </summary>

            public Color GridColor
            {
                set
                {
                    if (m_GridColor != value)
                    {
                        m_GridColor = value;
                        Refresh();
                    }
                }
                get { return m_GridColor; }
            }

            // ===================================================================

            /// <summary> 
            /// Gets or sets the number of pixels between each displayed magnitude.        
            /// </summary>

            public ushort LineInterval
            {
                set
                {
                    if ((ushort)m_LineInterval != value)
                    {
                        m_LineInterval = (int)value;
                        m_MaxCoords = -1; // Recalculate
                        Refresh();
                    }
                }
                get { return (ushort)m_LineInterval; }
            }

            // ===================================================================

            /// <summary> 
            /// Gets or sets the string to display as the graph's 'maximum label'.
            /// </summary>

            public string MaxLabel
            {
                set
                {
                    m_bMaxLabelSet = true;

                    if (string.Compare(m_MaxLabel, value) != 0)
                    {
                        m_MaxLabel = value;
                        m_MaxCoords = -1; // Recalculate
                        Refresh();
                    }
                }
                get { return m_MaxLabel; }
            }

            // ===================================================================

            /// <summary> 
            /// Gets or sets the string to display as the graph's 'minimum label'.
            /// </summary>

            public string MinLabel
            {
                set
                {
                    m_bMinLabelSet = true;

                    if (string.Compare(m_MinLabel, value) != 0)
                    {
                        m_MinLabel = value;
                        m_MaxCoords = -1; // Recalculate
                        Refresh();
                    }
                }
                get { return m_MinLabel; }
            }

            // ===================================================================

            /// <summary> 
            /// Gets or sets the width/height (in pixels) of each square in
            /// the graph's grid.
            /// </summary>

            public ushort GridSize
            {
                set
                {
                    if (m_GridSize != (int)value)
                    {
                        m_GridSize = (int)value;
                        Refresh();

                    }
                }
                get { return (ushort)m_GridSize; }
            }

            // ===================================================================

            /// <summary> 
            /// Gets or sets the maximum peek magnitude of the graph, which should be
            /// the largest value you could potentially push to the graph. NOTE: If you
            /// have set AutoScale to true, this value will automatically adjust to
            /// the highest magnitude pushed to the graph.
            /// </summary>

            public double MaxPeekMagnitude
            {
                set 
                { 
                    m_MaxPeek = value;
                    m_MaxPeekPreAutoScale = value;
                    RefreshLabels(); 
                }
                get { return m_MaxPeek; }
            }

            public double MaxPeekMagnitudePreAutoScale
            {
                set
                {
                    m_MaxPeekPreAutoScale = value;
                    RefreshLabels();
                }
                get { return m_MaxPeekPreAutoScale; }
            }

            // ===================================================================

            /// <summary> 
            /// Gets or sets the minimum magnitude of the graph, which should be
            /// the smallest value you could potentially push to the graph.
            /// NOTE: If you have set AutoScale to true, this value will 
            /// automatically adjust to the lowest magnitude pushed to the graph.
            /// </summary>

            public double MinPeekMagnitude
            {
                set 
                { 
                    m_MinPeek = value; 
                    RefreshLabels(); 
                }
                get { return m_MinPeek; }
            }


            // ===================================================================

            /// <summary> 
            /// Gets or sets the value indicating whether the graph automatically
            /// adjusts MinPeekMagnitude and MaxPeekMagnitude to the lowest and highest
            /// values pushed to the graph.
            /// </summary>

            public bool AutoAdjustPeek
            {
                set
                {
                    if (m_bAutoScale != value)
                    {
                        m_bAutoScale = value;
                        Refresh();
                    }
                }
                get { return m_bAutoScale; }
            }

            // ===================================================================

            /// <summary> 
            /// Gets or sets the value indicating whether the graph is rendered in
            /// 'high quality' mode (with antialiasing). It is suggested that this property
            /// be set to false if you intend to display your graph using bar graph 
            /// styles, line thickness greater than two, or if maximum performance 
            /// is absolutely crucial.
            /// </summary>

            public bool HighQuality
            {
                set
                {
                    if (value != m_bHighQuality)
                    {
                        m_bHighQuality = value;
                        Refresh(); // Force redraw
                    }
                }
                get { return m_bHighQuality; }
            }

            // ===================================================================

            /// <summary> 
            /// Gets or sets the value indicating whether the mimimum and maximum labels
            /// are displayed.
            /// </summary>

            public bool ShowLabels
            {
                set
                {
                    if (m_bShowMinMax != value)
                    {
                        m_bShowMinMax = value;

                        /* We're going to need to recalculate our maximum 
                         * coordinates since our graphable width changed */
                        m_MaxCoords = -1;

                        Refresh();
                    }
                }
                get { return m_bShowMinMax; }
            }

            // ===================================================================

            /// <summary> 
            /// Gets or sets the value indicating whether the graph's grid is 
            /// displayed.
            /// </summary>

            public bool ShowGrid
            {
                set
                {
                    if (m_bShowGrid != value)
                    {
                        m_bShowGrid = value;
                        Refresh();
                    }
                }
                get { return m_bShowGrid; }
            }

            /// <summary>
            /// Get the number of lines
            /// </summary>
            public int LineCount
            {
                get { return m_Lines.Count; }
            }

            // ===================================================================

            protected override void OnSizeChanged(EventArgs e)
            {
                /* We're going to need to recalculate our maximum 
                 * coordinates since our graphable width changed */
                m_MaxCoords = -1;

                Refresh();

                base.OnSizeChanged(e); 
            }

            // ===================================================================

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;// CreateGraphics();

                SmoothingMode prevSmoothingMode = g.SmoothingMode;
                g.SmoothingMode = (m_bHighQuality ? SmoothingMode.HighQuality
                                                  : SmoothingMode.Default);

                /* Reset our offset so we don't continually shift to the right: */

                m_OffsetX = 0;

                if (m_bShowMinMax)
                {
                    DrawLabels(ref g);
                }

                if (m_bShowGrid)
                {
                    DrawGrid(ref g);
                }

                if (m_OffsetX != 0)
                {
                    /* This is to avoid crossing the left grid boundary when 
                     * working with lines with great thickness */

                    g.Clip = new Region(
                        new Rectangle(m_OffsetX, 0, Width - m_OffsetX, Height));
                }

                DrawLines(ref g);
                g.ResetClip();

                g.SmoothingMode = prevSmoothingMode;
            }

            // ===================================================================

            /// <summary> 
            /// This function is to be called after you have pushed new magnitude(s)
            /// to the graph's lines and want the control re-rendered to take the
            /// changes into account.
            /// </summary>

            public void UpdateGraph()
            {
                int greatestMCount = 0;
                foreach (Line line in m_Lines)
                {
                    if (greatestMCount < line.m_MagnitudeList.Count)
                    {
                        greatestMCount = line.m_MagnitudeList.Count;
                    }
                }

                if (greatestMCount >= m_MaxCoords)
                {
                    m_MoveOffset =
                        (m_MoveOffset - (((greatestMCount - m_MaxCoords) + 1) * m_LineInterval))
                        % m_GridSize;
                }

                CullAndEqualizeMagnitudeCounts();
                this.Refresh();
            }

            // ===================================================================
            
            protected void CalculateMaxPushPoints()
            {
                /* Calculate the maximum of push points (magnitudes) that can be 
                 * drawn for the graphable display width: */

                m_MaxCoords = ((Width - m_OffsetX) / m_LineInterval) + 2
                                   + (((Width - m_OffsetX) % m_LineInterval) != 0 ? 1 : 0);

                if (m_MaxCoords <= 0)
                {
                    m_MaxCoords = 1;
                }
            }

            // ===================================================================

            protected void DrawLabels(ref Graphics g)
            {
                SizeF maxSize = g.MeasureString(m_MaxLabel, Font);
                SizeF minSize = g.MeasureString(m_MinLabel, Font);

                int textWidth = (int)((maxSize.Width > minSize.Width)
                                ? maxSize.Width
                                : minSize.Width) + 6;

                using (SolidBrush textBrush = new SolidBrush(m_TextColor))
                {

                    /* Draw the labels (max: Top) (min: Bottom) */

                    g.DrawString(m_MaxLabel, Font, textBrush,
                                  textWidth / 2 - (maxSize.Width / 2),
                                  2);

                    g.DrawString(m_MinLabel, Font, textBrush,
                                  textWidth / 2 - (minSize.Width / 2),
                                  Height - minSize.Height - 2);
                }

                /* Draw the bordering line */

                using (Pen borderPen = new Pen(m_GridColor, 1))
                {
                    g.DrawLine(borderPen, textWidth + 6, 0, textWidth + 6, Height);
                }

                /* Update the offset so we don't draw the graph over the labels */
                m_OffsetX = textWidth + 6;
            }

            // ===================================================================

            protected void RefreshLabels()
            {
                /* Within this function we ensure our labels are up to date
                 * if the user isn't using custom labels. It is called whenever
                 * the graph's range changes. */

                if (!m_bMinLabelSet)
                {
                    /* Use the minimum magnitude as the label since the
                     * user has not yet assigned a custom label: */

                    m_MinLabel = m_MinPeek.ToString();
                }

                if (!m_bMaxLabelSet)
                {
                    /* Use the maximum magnitude as the label since the
                     * user has not yet assigned a custom label: */

                    m_MaxLabel = m_MaxPeek.ToString();
                }
            }

            // ===================================================================

            protected void DrawGrid(ref Graphics g)
            {
                using (Pen gridPen = new Pen(m_GridColor, 1))
                {
                    int gridSize;

                    if (m_AutoGridSize)
                    {
                        gridSize = Height / 10;
                    }
                    else
                    {
                        gridSize = m_GridSize;
                    }

                    for (int n = Height - 1; n >= 0; n -= gridSize)
                    {
                        g.DrawLine(gridPen, m_OffsetX, n, Width, n);
                    }

                    for (int n = m_OffsetX + m_MoveOffset; n < Width; n += gridSize)
                    {
                        if (n < m_OffsetX)
                        {
                            continue;
                        }

                        g.DrawLine(gridPen, n, 0, n, Height);
                    }
                } //using (Pen gridPen
            }

            // ===================================================================

            private void CullAndEqualizeMagnitudeCounts()
            {
                if (m_MaxCoords == -1)
                {
                    /* Maximum push points not yet calculated */
                    CalculateMaxPushPoints();
                }

                int greatestMCount = 0;
                foreach (Line line in m_Lines)
                {
                    if (greatestMCount < line.m_MagnitudeList.Count)
                    {
                        greatestMCount = line.m_MagnitudeList.Count;
                    }
                }

                if (greatestMCount == 0)
                {
                    return; // No magnitudes
                }

                foreach (Line line in m_Lines)
                {
                    /* If the line has less push points than the line with the greatest
                    number of push points, new push points are appended with
                    the same magnitude as the previous push point. If no push points
                    exist for the line, one is added with the least magnitude possible. */

                    if (line.m_MagnitudeList.Count == 0)
                    {
                        line.m_MagnitudeList.Add(m_MinPeek);
                    }

                    while (line.m_MagnitudeList.Count < greatestMCount)
                    {
                        line.m_MagnitudeList.Add(
                            line.m_MagnitudeList[line.m_MagnitudeList.Count - 1]);
                    }

                    int cullsRequired = (line.m_MagnitudeList.Count - m_MaxCoords) + 1;
                    if (cullsRequired > 0)
                    {
                        line.m_MagnitudeList.RemoveRange(0, cullsRequired);
                    }
                }
            }

            // ===================================================================

            public void SetSelectedLine(string nameID)
            {
                if (nameID.Length > 0)
                    selectedLine = GetLine(nameID);
                else
                    selectedLine = null;
            }

            protected void DrawLines(ref Graphics g)
            {
                foreach (Line line in m_Lines)
                {
                    if (line.m_MagnitudeList.Count == 0)
                    {
                        //TODO: This may not be nescessary, so look into it.
                        /* No push points to draw */
                        continue;
                        //return;
                    }

                    //draw all 'lines' except the 'selected' one that is drawn last
                    if (selectedLine == null || (line.m_NameID != selectedLine.m_NameID))
                        DrawLine(ref g, line);
                }
                //now draw selected line last
                if (selectedLine != null && selectedLine.m_MagnitudeList.Count > 0)
                {
                    DrawLine(ref g, selectedLine);
                }
            }

            // ===================================================================

            private void DrawLine(ref Graphics g, Line line)
            {
                if (!line.m_bVisible)
                    return;
                /* Now prepare to draw the line or bar */

                using (Pen linePen = new Pen(line.m_Color, line.m_Thickness))
                {

                    Point lastPoint = new Point();
                    lastPoint.X = m_OffsetX;
                    double scaledHeight = line.m_MagnitudeList[0] * line.scale;
                    if (scaledHeight >= int.MaxValue)
                        scaledHeight = 0;
                    lastPoint.Y = Height - (int)((scaledHeight *
                       Height) / (m_MaxPeek - m_MinPeek));

                    for (int n = 0; n < line.m_MagnitudeList.Count; ++n)
                    {
                        if (line.m_bShowAsBar)
                        {
                            /* The line is set to be shown as a bar graph, so
                            first we get the bars rectangle, then draw the bar */

                            Rectangle barRect = new Rectangle();

                            // Weird hack because BarRect.Location.* causes error
                            Point p = barRect.Location;
                            p.X = m_OffsetX + (n * m_LineInterval) + 1;
                            scaledHeight = line.m_MagnitudeList[n] * line.scale;
                            if (scaledHeight >= int.MaxValue)
                                scaledHeight = 0;
                            p.Y = Height - (int)((scaledHeight * Height) /
                                                (m_MaxPeek - m_MinPeek));
                            barRect.Location = p;

                            barRect.Width = m_LineInterval - 1;
                            barRect.Height = Height;

                            DrawBar(barRect, line, ref g);
                        }
                        else
                        {
                            /* Draw a line */

                            int newX = m_OffsetX + (n * m_LineInterval);
                            scaledHeight = line.m_MagnitudeList[n] * line.scale;
                            if (scaledHeight >= int.MaxValue)
                                scaledHeight = 0;
                            int newY = Height - (int)((scaledHeight * Height) /
                                                (m_MaxPeek - m_MinPeek));

                            //lines at the top are not visible. Shift it down a little
                            if (newY < 1)
                                newY = 1;

                            g.DrawLine(linePen, lastPoint.X, lastPoint.Y, newX, newY);

                            lastPoint.X = newX;
                            lastPoint.Y = newY;
                        }
                    }

                    //playing with the idea to display the last value label
                    //double lastVal = line.m_MagnitudeList[line.m_MagnitudeList.Count - 1];
                    //string lastValStr = lastVal.ToString("0");
                    //int textWidth = lastValStr.Length;
                    //SizeF lastValSize = g.MeasureString(lastValStr, Font);
                    //using (SolidBrush textBrush = new SolidBrush(m_TextColor)) //line.m_Color
                    //{
                    //    int newY = Height - (int)(((lastVal * line.scale) * Height) /
                    //                            (m_MaxPeek - m_MinPeek));

                    //    g.DrawString(lastValStr, Font, textBrush,
                    //                  m_OffsetX,
                    //                  newY);
                    //}

                } //using (Pen linePen
            }
            
            // ===================================================================

            private void DrawBar(Rectangle rect, Line line, ref Graphics g)
            {
                using (SolidBrush barBrush = new SolidBrush(line.m_Color))
                {
                    g.FillRectangle(barBrush, rect);
                }
            }

            // ===================================================================

            /// <summary> 
            /// Returns a new line handle (LineHandle object) to the line 
            /// with the matching numerical ID. Returns NULL if a line with a 
            /// matching ID is not found.
            /// </summary>
            /// <param name="numID">
            /// The numerical ID of the line you wish to get a handle to.
            /// </param>

            public LineHandle GetLineHandle(int numID)
            {
                Object line = (Object)GetLine(numID);
                return (line != null ? new LineHandle(ref line, this) : null);
            }

            // ===================================================================

            /// <summary> 
            /// Returns a new line handle (LineHandle object) to the line 
            /// with the matching name (case insensitive). Returns NULL if a 
            /// line with a matching name is not found.
            /// </summary>
            /// <param name="nameID">
            /// The case insensitive name of the line you wish to get a handle to.
            /// </param>

            public LineHandle GetLineHandle(string nameID)
            {
                Object line = (Object)GetLine(nameID);
                return (line != null ? new LineHandle(ref line, this) : null);
            }

            // ===================================================================

            private Line GetLine(int numID)
            {
                foreach (Line line in m_Lines)
                {
                    if (numID == line.m_NumID)
                    {
                        return line;
                    }
                }
                return null;
            }

            // ===================================================================

            private Line GetLine(string nameID)
            {
                foreach (Line line in m_Lines)
                {
                    if (string.Compare(nameID, line.m_NameID, true) == 0)
                    {
                        return line;
                    }
                }
                return null;
            }

            // ===================================================================

            /// <summary> 
            /// Returns true if a line exists with an identification number mathing 
            /// the passed value. Returns false if no match is found.
            /// </summary>
            /// <param name="numID">
            /// The case numerical ID of the line you wish to check the existence
            /// of.
            /// </param>
            
            public bool LineExists(int numID)
            {
                return GetLine(numID) != null;
            }

            // ===================================================================

            /// <summary> 
            /// Returns true if a line exists with a name that case insensitively
            /// matches the passes name. Returns false if no match is found.
            /// </summary>
            /// <param name="nameID">
            /// The case insensitive name of the line you wish to check the existence
            /// of.
            /// </param>

            public bool LineExists(string nameID)
            {
                return GetLine(nameID) != null;
            }

            // ===================================================================

            /// <summary> 
            /// Adds a new line using the passed name as an identifier and sets
            /// the line's initial color to the passed color. If successful, returns
            /// a handle to the new line.
            /// </summary>
            /// <param name="nameID">
            /// A case insensitive name for the line you wish to create.
            /// </param>
            /// <param name="clr">
            /// The line's initial color.
            /// </param>

            public LineHandle AddLine(string nameID, Color clr, double scale)
            {
                if (LineExists(nameID))
                {
                    return GetLineHandle(nameID);// null;
                }

                Line line = new Line(nameID);
                line.m_NumID = m_Lines.Count;
                line.m_Color = clr;
                line.Scale = scale;

                m_Lines.Add(line);

                Object objLine = (Object)line;
                return (new LineHandle(ref objLine, this));
            }

            // ===================================================================
            // I strongly suggest that you use this method when performance is critical

            /// <summary> 
            /// Adds a new line using the passed numeric ID as an identifier and sets
            /// the line's initial color to the passed color. If successful, returns
            /// a handle to the new line.
            /// </summary>
            /// <param name="numID">
            /// A unique numerical for the line you wish to create.
            /// </param>
            /// <param name="clr">
            /// The line's initial color.
            /// </param>

            public LineHandle AddLine(int numID, Color clr, double scale)
            {
                if (LineExists(numID))
                {
                    return null;
                }

                Line line = new Line(numID);
                line.m_Color = clr;
                line.Scale = scale;

                m_Lines.Add(line);
                Object objLine = (Object)line;
                return (new LineHandle(ref objLine, this));
            }

            // ===================================================================
            
            /// <summary> 
            /// Removes a line by its name.
            /// </summary>
            /// <param name="nameID">
            /// The line's case-insensitive name.
            /// </param>  

            public bool RemoveLine(string nameID)
            {
                Line line = GetLine(nameID);
                if (line == null)
                {
                    return false;
                }

                return m_Lines.Remove(line);
            }

            // ===================================================================
            
            /// <summary> 
            /// Removes a line by its numerical ID.
            /// </summary>
            /// <param name="numID">
            /// The line's numerical ID.
            /// </param>  

            public bool RemoveLine(int numID)
            {
                Line line = GetLine(numID);
                if (line == null)
                {
                    return false;
                }

                return m_Lines.Remove(line);
            }

            public void ClearAllLines()
            {
                m_Lines.Clear();
            }

            // ===================================================================       

            /// <summary> 
            /// Pushes a new magnitude (point) to the line with the passed name.
            /// </summary>
            /// <param name="magnitude">
            /// The magnitude of the new point.
            /// </param>  
            /// <param name="nameID">
            /// The line's case-insensitive name.
            /// </param>  

            public bool Push(double magnitude, string nameID)
            {
                Line line = GetLine(nameID);
                if (line == null)
                {
                    return false;
                }

                return PushDirect(magnitude, line); 
            }

            // ===================================================================       

            /// <summary> 
            /// Pushes a new magnitude (point) to the line with the passed 
            /// numerical ID.
            /// </summary>
            /// <param name="magnitude">
            /// The magnitude of the new point.
            /// </param>  
            /// <param name="numID">
            /// The line's numerical ID.
            /// </param>  

            public bool Push(double magnitude, int numID)
            {
                Line line = GetLine(numID);
                if (line == null)
                {
                    return false;
                }

                return PushDirect(magnitude, line);
            }
            
            // ===================================================================       

            private bool PushDirect(double magnitude, Line line)
            {
                /* Now add the magnitude (push point) to the array of push points, but
                   first restrict it to the peek bounds */

                if (!m_bAutoScale && (magnitude * line.Scale > m_MaxPeek))
                {
                    magnitude = m_MaxPeek;
                }
                else if (m_bAutoScale && (magnitude * line.Scale > m_MaxPeek))
                {
                    RaiseAutoScaleEvent();
                    m_MaxPeek = magnitude * line.Scale;
                    m_MaxLabel = m_MaxPeek.ToString("0");

                    RefreshLabels();
                }
                else if (!m_bAutoScale && (magnitude * line.Scale < m_MinPeek))
                {
                    magnitude = m_MinPeek;
                }
                else if (m_bAutoScale && (magnitude * line.Scale < m_MinPeek))
                {
                    m_MinPeek = magnitude;
                    RefreshLabels();
                }
                else if (m_bAutoScale && (m_MaxPeek * line.Scale > m_MaxPeekPreAutoScale * line.Scale))
                {
                    Checkm_MaxPeek();
                    RefreshLabels();
                }

                magnitude -= m_MinPeek; //assuming this to be 0

                line.m_MagnitudeList.Add(magnitude);
                return true;
            }

            public void CheckForRescale()
            {
                Checkm_MaxPeek();
                m_MaxCoords = -1;

                Refresh();
                UpdateGraph();
            }

            public double GetCurrentMaxOnGraph()
            {
                double globalMaxDataPoint = 0;
                foreach (Line line in m_Lines)
                {
                    if (line.m_bVisible)
                    {
                        foreach (int datapoint in line.m_MagnitudeList)
                        {
                            if (datapoint * line.Scale > globalMaxDataPoint)
                                globalMaxDataPoint = datapoint * line.Scale;
                        }
                    }
                }
                return globalMaxDataPoint;
            }

            //use to to automatically set the 'Max' value of the graph from available values
            private void Checkm_MaxPeek()
            {
                double globalMaxDataPoint = 0;
                foreach (Line line in m_Lines)
                {
                    if (line.m_bVisible)
                    {
                        foreach (int datapoint in line.m_MagnitudeList)
                        {
                            if (datapoint * line.Scale > globalMaxDataPoint)
                                globalMaxDataPoint = datapoint * line.Scale;
                        }
                    }
                }
                if (m_MaxPeek > globalMaxDataPoint)
                {
                    if (globalMaxDataPoint > m_MaxPeekPreAutoScale)
                    {
                        m_MaxPeek = globalMaxDataPoint;
                    }
                    else
                        m_MaxPeek = m_MaxPeekPreAutoScale;
                    m_MaxLabel = m_MaxPeek.ToString("0");
                }
            }
        }
    }
}

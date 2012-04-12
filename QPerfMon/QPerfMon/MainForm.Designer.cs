namespace QPerfMon
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Loading...");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lineFlowGraph2DControl = new HenIT.Windows.Controls.Graphing.LineFlowGraph2D();
            this.contextMenuStripGraph = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.frequencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.halfSecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twoSecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fiveSecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenSecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thirtySecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sixtySecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.setTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphFormatOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maximuminitialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snapToDesktopSidesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.addPerformanceCounterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.loadSetToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentSetToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rememberSizePositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.logDataToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startLoggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvwCounters = new QPerfMon.ListViewR();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderScale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripLvw = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.visibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formattingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.disableCounterOnErrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastErrorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addClonePerformanceCounterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCloneAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.loadSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveSelectionToNewWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testAddCloneCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textAddCloneAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialogQPerf = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialogQPerf = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSelection = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStripGraph.SuspendLayout();
            this.contextMenuStripLvw.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lineFlowGraph2DControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvwCounters);
            this.splitContainer1.Size = new System.Drawing.Size(586, 422);
            this.splitContainer1.SplitterDistance = 312;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // lineFlowGraph2DControl
            // 
            this.lineFlowGraph2DControl.AllowClick = true;
            this.lineFlowGraph2DControl.AutoAdjustPeek = true;
            this.lineFlowGraph2DControl.AutoGridSize = true;
            this.lineFlowGraph2DControl.BackColor = System.Drawing.Color.Black;
            this.lineFlowGraph2DControl.ContextMenuStrip = this.contextMenuStripGraph;
            this.lineFlowGraph2DControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lineFlowGraph2DControl.GridColor = System.Drawing.Color.DarkGreen;
            this.lineFlowGraph2DControl.GridSize = ((ushort)(31));
            this.lineFlowGraph2DControl.HighQuality = true;
            this.lineFlowGraph2DControl.LineInterval = ((ushort)(5));
            this.lineFlowGraph2DControl.Location = new System.Drawing.Point(0, 0);
            this.lineFlowGraph2DControl.MaxLabel = "100";
            this.lineFlowGraph2DControl.MaxPeekMagnitude = 100D;
            this.lineFlowGraph2DControl.MaxPeekMagnitudePreAutoScale = 100D;
            this.lineFlowGraph2DControl.MinLabel = "0";
            this.lineFlowGraph2DControl.MinPeekMagnitude = 0D;
            this.lineFlowGraph2DControl.Name = "lineFlowGraph2DControl";
            this.lineFlowGraph2DControl.ShowGrid = true;
            this.lineFlowGraph2DControl.ShowLabels = true;
            this.lineFlowGraph2DControl.Size = new System.Drawing.Size(586, 312);
            this.lineFlowGraph2DControl.TabIndex = 1;
            this.lineFlowGraph2DControl.TextColor = System.Drawing.Color.Yellow;
            this.lineFlowGraph2DControl.TextFont = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            // 
            // contextMenuStripGraph
            // 
            this.contextMenuStripGraph.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frequencyToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.toolStripMenuItem2,
            this.setTitleToolStripMenuItem,
            this.graphFormatOptionsToolStripMenuItem,
            this.maximuminitialToolStripMenuItem,
            this.snapToDesktopSidesToolStripMenuItem,
            this.alwaysOnTopToolStripMenuItem,
            this.toolStripMenuItem5,
            this.addPerformanceCounterToolStripMenuItem,
            this.toolStripMenuItem6,
            this.loadSetToolStripMenuItem1,
            this.saveCurrentSetToolStripMenuItem1,
            this.rememberSizePositionToolStripMenuItem,
            this.toolStripMenuItem4,
            this.logDataToFileToolStripMenuItem,
            this.startLoggingToolStripMenuItem});
            this.contextMenuStripGraph.Name = "contextMenuStripGraph";
            this.contextMenuStripGraph.Size = new System.Drawing.Size(321, 362);
            // 
            // frequencyToolStripMenuItem
            // 
            this.frequencyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.halfSecondsToolStripMenuItem,
            this.secondToolStripMenuItem,
            this.twoSecondsToolStripMenuItem,
            this.fiveSecondsToolStripMenuItem,
            this.tenSecondsToolStripMenuItem,
            this.thirtySecondsToolStripMenuItem,
            this.sixtySecondsToolStripMenuItem});
            this.frequencyToolStripMenuItem.Name = "frequencyToolStripMenuItem";
            this.frequencyToolStripMenuItem.Size = new System.Drawing.Size(320, 24);
            this.frequencyToolStripMenuItem.Text = "Frequency";
            // 
            // halfSecondsToolStripMenuItem
            // 
            this.halfSecondsToolStripMenuItem.Name = "halfSecondsToolStripMenuItem";
            this.halfSecondsToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.halfSecondsToolStripMenuItem.Text = "0.5 Seconds";
            this.halfSecondsToolStripMenuItem.Click += new System.EventHandler(this.halfSecondsToolStripMenuItem_Click);
            // 
            // secondToolStripMenuItem
            // 
            this.secondToolStripMenuItem.Checked = true;
            this.secondToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.secondToolStripMenuItem.Name = "secondToolStripMenuItem";
            this.secondToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.secondToolStripMenuItem.Text = "1 Second";
            this.secondToolStripMenuItem.Click += new System.EventHandler(this.secondToolStripMenuItem_Click);
            // 
            // twoSecondsToolStripMenuItem
            // 
            this.twoSecondsToolStripMenuItem.Name = "twoSecondsToolStripMenuItem";
            this.twoSecondsToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.twoSecondsToolStripMenuItem.Text = "2 Seconds";
            this.twoSecondsToolStripMenuItem.Click += new System.EventHandler(this.twoSecondsToolStripMenuItem_Click);
            // 
            // fiveSecondsToolStripMenuItem
            // 
            this.fiveSecondsToolStripMenuItem.Name = "fiveSecondsToolStripMenuItem";
            this.fiveSecondsToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.fiveSecondsToolStripMenuItem.Text = "5 Seconds";
            this.fiveSecondsToolStripMenuItem.Click += new System.EventHandler(this.fiveSecondsToolStripMenuItem_Click);
            // 
            // tenSecondsToolStripMenuItem
            // 
            this.tenSecondsToolStripMenuItem.Name = "tenSecondsToolStripMenuItem";
            this.tenSecondsToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.tenSecondsToolStripMenuItem.Text = "10 Seconds";
            this.tenSecondsToolStripMenuItem.Click += new System.EventHandler(this.tenSecondsToolStripMenuItem_Click);
            // 
            // thirtySecondsToolStripMenuItem
            // 
            this.thirtySecondsToolStripMenuItem.Name = "thirtySecondsToolStripMenuItem";
            this.thirtySecondsToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.thirtySecondsToolStripMenuItem.Text = "30 Seconds";
            this.thirtySecondsToolStripMenuItem.Click += new System.EventHandler(this.thirtySecondsToolStripMenuItem_Click);
            // 
            // sixtySecondsToolStripMenuItem
            // 
            this.sixtySecondsToolStripMenuItem.Name = "sixtySecondsToolStripMenuItem";
            this.sixtySecondsToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.sixtySecondsToolStripMenuItem.Text = "60 Seconds";
            this.sixtySecondsToolStripMenuItem.Click += new System.EventHandler(this.sixtySecondsToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.CheckOnClick = true;
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(320, 24);
            this.pauseToolStripMenuItem.Text = "Pause";
            this.pauseToolStripMenuItem.CheckedChanged += new System.EventHandler(this.pauseToolStripMenuItem_CheckedChanged);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(317, 6);
            // 
            // setTitleToolStripMenuItem
            // 
            this.setTitleToolStripMenuItem.Name = "setTitleToolStripMenuItem";
            this.setTitleToolStripMenuItem.Size = new System.Drawing.Size(320, 24);
            this.setTitleToolStripMenuItem.Text = "Set Title";
            this.setTitleToolStripMenuItem.Click += new System.EventHandler(this.setTitleToolStripMenuItem_Click);
            // 
            // graphFormatOptionsToolStripMenuItem
            // 
            this.graphFormatOptionsToolStripMenuItem.Name = "graphFormatOptionsToolStripMenuItem";
            this.graphFormatOptionsToolStripMenuItem.Size = new System.Drawing.Size(320, 24);
            this.graphFormatOptionsToolStripMenuItem.Text = "Graph format options";
            this.graphFormatOptionsToolStripMenuItem.Click += new System.EventHandler(this.graphFormatOptionsToolStripMenuItem_Click);
            // 
            // maximuminitialToolStripMenuItem
            // 
            this.maximuminitialToolStripMenuItem.Name = "maximuminitialToolStripMenuItem";
            this.maximuminitialToolStripMenuItem.Size = new System.Drawing.Size(320, 24);
            this.maximuminitialToolStripMenuItem.Text = "Maximum (initial)";
            this.maximuminitialToolStripMenuItem.Click += new System.EventHandler(this.maximuminitialToolStripMenuItem_Click);
            // 
            // snapToDesktopSidesToolStripMenuItem
            // 
            this.snapToDesktopSidesToolStripMenuItem.CheckOnClick = true;
            this.snapToDesktopSidesToolStripMenuItem.Name = "snapToDesktopSidesToolStripMenuItem";
            this.snapToDesktopSidesToolStripMenuItem.Size = new System.Drawing.Size(320, 24);
            this.snapToDesktopSidesToolStripMenuItem.Text = "Snap to desktop sides";
            this.snapToDesktopSidesToolStripMenuItem.CheckedChanged += new System.EventHandler(this.snapToDesktopSidesToolStripMenuItem_CheckedChanged);
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(320, 24);
            this.alwaysOnTopToolStripMenuItem.Text = "Always on top";
            this.alwaysOnTopToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_CheckStateChanged);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(317, 6);
            // 
            // addPerformanceCounterToolStripMenuItem
            // 
            this.addPerformanceCounterToolStripMenuItem.Image = global::QPerfMon.Properties.Resources.add;
            this.addPerformanceCounterToolStripMenuItem.Name = "addPerformanceCounterToolStripMenuItem";
            this.addPerformanceCounterToolStripMenuItem.Size = new System.Drawing.Size(320, 24);
            this.addPerformanceCounterToolStripMenuItem.Text = "Add performance counter";
            this.addPerformanceCounterToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(317, 6);
            // 
            // loadSetToolStripMenuItem1
            // 
            this.loadSetToolStripMenuItem1.Name = "loadSetToolStripMenuItem1";
            this.loadSetToolStripMenuItem1.Size = new System.Drawing.Size(320, 24);
            this.loadSetToolStripMenuItem1.Text = "Load set";
            this.loadSetToolStripMenuItem1.Click += new System.EventHandler(this.loadSetToolStripMenuItem_Click);
            // 
            // saveCurrentSetToolStripMenuItem1
            // 
            this.saveCurrentSetToolStripMenuItem1.Name = "saveCurrentSetToolStripMenuItem1";
            this.saveCurrentSetToolStripMenuItem1.Size = new System.Drawing.Size(320, 24);
            this.saveCurrentSetToolStripMenuItem1.Text = "Save current set";
            this.saveCurrentSetToolStripMenuItem1.Click += new System.EventHandler(this.saveCurrentSetToolStripMenuItem_Click);
            // 
            // rememberSizePositionToolStripMenuItem
            // 
            this.rememberSizePositionToolStripMenuItem.Name = "rememberSizePositionToolStripMenuItem";
            this.rememberSizePositionToolStripMenuItem.Size = new System.Drawing.Size(320, 24);
            this.rememberSizePositionToolStripMenuItem.Text = "Remember Size/Position (on Save/Load)";
            this.rememberSizePositionToolStripMenuItem.Click += new System.EventHandler(this.rememberSizePositionToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(317, 6);
            // 
            // logDataToFileToolStripMenuItem
            // 
            this.logDataToFileToolStripMenuItem.Name = "logDataToFileToolStripMenuItem";
            this.logDataToFileToolStripMenuItem.Size = new System.Drawing.Size(320, 24);
            this.logDataToFileToolStripMenuItem.Text = "Log data to file options";
            this.logDataToFileToolStripMenuItem.Click += new System.EventHandler(this.logDataToFileToolStripMenuItem_Click);
            // 
            // startLoggingToolStripMenuItem
            // 
            this.startLoggingToolStripMenuItem.Enabled = false;
            this.startLoggingToolStripMenuItem.Name = "startLoggingToolStripMenuItem";
            this.startLoggingToolStripMenuItem.Size = new System.Drawing.Size(320, 24);
            this.startLoggingToolStripMenuItem.Text = "Start logging";
            this.startLoggingToolStripMenuItem.Click += new System.EventHandler(this.startLoggingToolStripMenuItem_Click);
            // 
            // lvwCounters
            // 
            this.lvwCounters.CheckBoxes = true;
            this.lvwCounters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeaderScale,
            this.columnHeader3});
            this.lvwCounters.ContextMenuStrip = this.contextMenuStripLvw;
            this.lvwCounters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwCounters.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            this.lvwCounters.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lvwCounters.Location = new System.Drawing.Point(0, 0);
            this.lvwCounters.Name = "lvwCounters";
            this.lvwCounters.Size = new System.Drawing.Size(586, 102);
            this.lvwCounters.TabIndex = 0;
            this.lvwCounters.UseCompatibleStateImageBehavior = false;
            this.lvwCounters.View = System.Windows.Forms.View.Details;
            this.lvwCounters.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwCounters_ItemChecked);
            this.lvwCounters.SelectedIndexChanged += new System.EventHandler(this.lvwCounters_SelectedIndexChanged);
            this.lvwCounters.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvwCounters_KeyUp);
            this.lvwCounters.Resize += new System.EventHandler(this.lvwCounters_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Performance counter";
            this.columnHeader1.Width = 368;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Color";
            // 
            // columnHeaderScale
            // 
            this.columnHeaderScale.Text = "Scale";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Last value";
            this.columnHeader3.Width = 65;
            // 
            // contextMenuStripLvw
            // 
            this.contextMenuStripLvw.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visibleToolStripMenuItem,
            this.formattingToolStripMenuItem,
            this.toolStripSeparator1,
            this.disableCounterOnErrorToolStripMenuItem,
            this.lastErrorToolStripMenuItem1,
            this.toolStripMenuItem1,
            this.addToolStripMenuItem,
            this.addClonePerformanceCounterToolStripMenuItem,
            this.addCloneAllToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.toolStripMenuItem3,
            this.loadSetToolStripMenuItem,
            this.saveCurrentSetToolStripMenuItem,
            this.moveSelectionToNewWindowToolStripMenuItem,
            this.testAddToolStripMenuItem,
            this.testAddCloneCategoryToolStripMenuItem,
            this.textAddCloneAllToolStripMenuItem});
            this.contextMenuStripLvw.Name = "contextMenuStrip1";
            this.contextMenuStripLvw.Size = new System.Drawing.Size(274, 358);
            // 
            // visibleToolStripMenuItem
            // 
            this.visibleToolStripMenuItem.Checked = true;
            this.visibleToolStripMenuItem.CheckOnClick = true;
            this.visibleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.visibleToolStripMenuItem.Name = "visibleToolStripMenuItem";
            this.visibleToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.visibleToolStripMenuItem.Text = "Visible";
            this.visibleToolStripMenuItem.CheckedChanged += new System.EventHandler(this.visibleToolStripMenuItem_CheckedChanged);
            // 
            // formattingToolStripMenuItem
            // 
            this.formattingToolStripMenuItem.Name = "formattingToolStripMenuItem";
            this.formattingToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.formattingToolStripMenuItem.Text = "Formatting";
            this.formattingToolStripMenuItem.Click += new System.EventHandler(this.formattingToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(270, 6);
            // 
            // disableCounterOnErrorToolStripMenuItem
            // 
            this.disableCounterOnErrorToolStripMenuItem.CheckOnClick = true;
            this.disableCounterOnErrorToolStripMenuItem.Name = "disableCounterOnErrorToolStripMenuItem";
            this.disableCounterOnErrorToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.disableCounterOnErrorToolStripMenuItem.Text = "Disable counter on error";
            this.disableCounterOnErrorToolStripMenuItem.CheckedChanged += new System.EventHandler(this.disableCounterOnErrorToolStripMenuItem_CheckedChanged);
            // 
            // lastErrorToolStripMenuItem1
            // 
            this.lastErrorToolStripMenuItem1.Name = "lastErrorToolStripMenuItem1";
            this.lastErrorToolStripMenuItem1.Size = new System.Drawing.Size(273, 24);
            this.lastErrorToolStripMenuItem1.Text = "Last Error";
            this.lastErrorToolStripMenuItem1.Visible = false;
            this.lastErrorToolStripMenuItem1.Click += new System.EventHandler(this.lastErrorToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(270, 6);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Image = global::QPerfMon.Properties.Resources.add;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.addToolStripMenuItem.Text = "Add performance counter";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // addClonePerformanceCounterToolStripMenuItem
            // 
            this.addClonePerformanceCounterToolStripMenuItem.Enabled = false;
            this.addClonePerformanceCounterToolStripMenuItem.Image = global::QPerfMon.Properties.Resources.AddEx;
            this.addClonePerformanceCounterToolStripMenuItem.Name = "addClonePerformanceCounterToolStripMenuItem";
            this.addClonePerformanceCounterToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.addClonePerformanceCounterToolStripMenuItem.Text = "Add clone category";
            this.addClonePerformanceCounterToolStripMenuItem.Click += new System.EventHandler(this.addClonePerformanceCounterToolStripMenuItem_Click);
            // 
            // addCloneAllToolStripMenuItem
            // 
            this.addCloneAllToolStripMenuItem.Enabled = false;
            this.addCloneAllToolStripMenuItem.Image = global::QPerfMon.Properties.Resources.AddEx;
            this.addCloneAllToolStripMenuItem.Name = "addCloneAllToolStripMenuItem";
            this.addCloneAllToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.addCloneAllToolStripMenuItem.Text = "Add clone all";
            this.addCloneAllToolStripMenuItem.Click += new System.EventHandler(this.addCloneAllToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Enabled = false;
            this.removeToolStripMenuItem.Image = global::QPerfMon.Properties.Resources.stop;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.removeToolStripMenuItem.Text = "Remove performance counter(s)";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(270, 6);
            // 
            // loadSetToolStripMenuItem
            // 
            this.loadSetToolStripMenuItem.Name = "loadSetToolStripMenuItem";
            this.loadSetToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.loadSetToolStripMenuItem.Text = "Load set";
            this.loadSetToolStripMenuItem.Click += new System.EventHandler(this.loadSetToolStripMenuItem_Click);
            // 
            // saveCurrentSetToolStripMenuItem
            // 
            this.saveCurrentSetToolStripMenuItem.Name = "saveCurrentSetToolStripMenuItem";
            this.saveCurrentSetToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.saveCurrentSetToolStripMenuItem.Text = "Save current set";
            this.saveCurrentSetToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentSetToolStripMenuItem_Click);
            // 
            // moveSelectionToNewWindowToolStripMenuItem
            // 
            this.moveSelectionToNewWindowToolStripMenuItem.Enabled = false;
            this.moveSelectionToNewWindowToolStripMenuItem.Name = "moveSelectionToNewWindowToolStripMenuItem";
            this.moveSelectionToNewWindowToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.moveSelectionToNewWindowToolStripMenuItem.Text = "Move selection to new window";
            this.moveSelectionToNewWindowToolStripMenuItem.Click += new System.EventHandler(this.moveSelectionToNewWindowToolStripMenuItem_Click);
            // 
            // testAddToolStripMenuItem
            // 
            this.testAddToolStripMenuItem.Name = "testAddToolStripMenuItem";
            this.testAddToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.testAddToolStripMenuItem.Text = "Test add";
            this.testAddToolStripMenuItem.Visible = false;
            this.testAddToolStripMenuItem.Click += new System.EventHandler(this.testAddToolStripMenuItem_Click);
            // 
            // testAddCloneCategoryToolStripMenuItem
            // 
            this.testAddCloneCategoryToolStripMenuItem.Name = "testAddCloneCategoryToolStripMenuItem";
            this.testAddCloneCategoryToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.testAddCloneCategoryToolStripMenuItem.Text = "Test add clone category";
            this.testAddCloneCategoryToolStripMenuItem.Visible = false;
            this.testAddCloneCategoryToolStripMenuItem.Click += new System.EventHandler(this.testAddCloneCategoryToolStripMenuItem_Click);
            // 
            // textAddCloneAllToolStripMenuItem
            // 
            this.textAddCloneAllToolStripMenuItem.Name = "textAddCloneAllToolStripMenuItem";
            this.textAddCloneAllToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
            this.textAddCloneAllToolStripMenuItem.Text = "Text add clone all";
            this.textAddCloneAllToolStripMenuItem.Visible = false;
            this.textAddCloneAllToolStripMenuItem.Click += new System.EventHandler(this.textAddCloneAllToolStripMenuItem_Click);
            // 
            // saveFileDialogQPerf
            // 
            this.saveFileDialogQPerf.DefaultExt = "qpmset";
            this.saveFileDialogQPerf.Filter = "Quick Perfmon Files|*.qpmset";
            // 
            // openFileDialogQPerf
            // 
            this.openFileDialogQPerf.DefaultExt = "qpmset";
            this.openFileDialogQPerf.Filter = "Quick Perfmon Files|*.qpmset";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelVersion,
            this.toolStripStatusLabelSelection});
            this.statusStrip1.Location = new System.Drawing.Point(0, 422);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(586, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelVersion
            // 
            this.toolStripStatusLabelVersion.AutoSize = false;
            this.toolStripStatusLabelVersion.Name = "toolStripStatusLabelVersion";
            this.toolStripStatusLabelVersion.Size = new System.Drawing.Size(150, 17);
            this.toolStripStatusLabelVersion.Text = "Version";
            this.toolStripStatusLabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelSelection
            // 
            this.toolStripStatusLabelSelection.AutoSize = false;
            this.toolStripStatusLabelSelection.Name = "toolStripStatusLabelSelection";
            this.toolStripStatusLabelSelection.Size = new System.Drawing.Size(421, 17);
            this.toolStripStatusLabelSelection.Spring = true;
            this.toolStripStatusLabelSelection.Text = ".";
            this.toolStripStatusLabelSelection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 444);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "MainForm";
            this.Text = "Quick performance counter viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStripGraph.ResumeLayout(false);
            this.contextMenuStripLvw.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeaderScale;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLvw;
        private System.Windows.Forms.ToolStripMenuItem visibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formattingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripGraph;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frequencyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem halfSecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twoSecondsToolStripMenuItem;
        private ListViewR lvwCounters;
        private System.Windows.Forms.ToolStripMenuItem fiveSecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tenSecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thirtySecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastErrorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem setTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentSetToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogQPerf;
        private System.Windows.Forms.OpenFileDialog openFileDialogQPerf;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem maximuminitialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveSelectionToNewWindowToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSelection;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem logDataToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startLoggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sixtySecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snapToDesktopSidesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addClonePerformanceCounterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPerformanceCounterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem addCloneAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSetToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentSetToolStripMenuItem1;
        private HenIT.Windows.Controls.Graphing.LineFlowGraph2D lineFlowGraph2DControl;
        private System.Windows.Forms.ToolStripMenuItem graphFormatOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelVersion;
        private System.Windows.Forms.ToolStripMenuItem disableCounterOnErrorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testAddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testAddCloneCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textAddCloneAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rememberSizePositionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
    }
}


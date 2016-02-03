using HenIT.Windows.Controls;
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
            this.allInOneContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addPerformanceCounterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removePerformanceCountersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formattingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.moreFormattingOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetAllColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visibleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.moveSelectedToNewWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.copyDefinitionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteFromDefinitionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.applicationSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphFormatOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maximumInitialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rememberWindowsSizeLocationSaveLoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snapToDesktopSidesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disablePerformanceCountersOnErrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frequencyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.halfSecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twoSecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fiveSecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenSecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thirtySecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sixtySecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggingOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startLoggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.loadSetFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentSetToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.lvwCounters = new HenIT.Windows.Controls.DragAndDropListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderScale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.allInOneContextMenuStrip.SuspendLayout();
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
            this.lineFlowGraph2DControl.ContextMenuStrip = this.allInOneContextMenuStrip;
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
            this.lineFlowGraph2DControl.DoubleClick += new System.EventHandler(this.c2DPushGraphControl_DoubleClick);
            // 
            // allInOneContextMenuStrip
            // 
            this.allInOneContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPerformanceCounterToolStripMenuItem1,
            this.addTemplateToolStripMenuItem,
            this.removePerformanceCountersToolStripMenuItem,
            this.formattingToolStripMenuItem1,
            this.moreFormattingOptionsToolStripMenuItem,
            this.visibleToolStripMenuItem1,
            this.moveSelectedToNewWindowToolStripMenuItem,
            this.toolStripSeparator2,
            this.copyDefinitionsToolStripMenuItem,
            this.pasteFromDefinitionsToolStripMenuItem,
            this.toolStripSeparator3,
            this.applicationSettingsToolStripMenuItem,
            this.frequencyToolStripMenuItem1,
            this.pauseAllToolStripMenuItem,
            this.loggingToolStripMenuItem,
            this.toolStripSeparator4,
            this.loadSetFromFileToolStripMenuItem,
            this.saveCurrentSetToolStripMenuItem2});
            this.allInOneContextMenuStrip.Name = "allInOneContextMenuStrip";
            this.allInOneContextMenuStrip.Size = new System.Drawing.Size(248, 352);
            // 
            // addPerformanceCounterToolStripMenuItem1
            // 
            this.addPerformanceCounterToolStripMenuItem1.Image = global::QPerfMon.Properties.Resources.add;
            this.addPerformanceCounterToolStripMenuItem1.Name = "addPerformanceCounterToolStripMenuItem1";
            this.addPerformanceCounterToolStripMenuItem1.Size = new System.Drawing.Size(247, 22);
            this.addPerformanceCounterToolStripMenuItem1.Text = "Add Performance Counter";
            this.addPerformanceCounterToolStripMenuItem1.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // addTemplateToolStripMenuItem
            // 
            this.addTemplateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cloneCategoryToolStripMenuItem,
            this.cloneAllToolStripMenuItem});
            this.addTemplateToolStripMenuItem.Name = "addTemplateToolStripMenuItem";
            this.addTemplateToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.addTemplateToolStripMenuItem.Text = "Add Template";
            // 
            // cloneCategoryToolStripMenuItem
            // 
            this.cloneCategoryToolStripMenuItem.Enabled = false;
            this.cloneCategoryToolStripMenuItem.Name = "cloneCategoryToolStripMenuItem";
            this.cloneCategoryToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.cloneCategoryToolStripMenuItem.Text = "Clone Category";
            this.cloneCategoryToolStripMenuItem.Click += new System.EventHandler(this.addClonePerformanceCounterToolStripMenuItem_Click);
            // 
            // cloneAllToolStripMenuItem
            // 
            this.cloneAllToolStripMenuItem.Enabled = false;
            this.cloneAllToolStripMenuItem.Name = "cloneAllToolStripMenuItem";
            this.cloneAllToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.cloneAllToolStripMenuItem.Text = "Clone All";
            this.cloneAllToolStripMenuItem.Click += new System.EventHandler(this.addCloneAllToolStripMenuItem_Click);
            // 
            // removePerformanceCountersToolStripMenuItem
            // 
            this.removePerformanceCountersToolStripMenuItem.Enabled = false;
            this.removePerformanceCountersToolStripMenuItem.Image = global::QPerfMon.Properties.Resources.stop;
            this.removePerformanceCountersToolStripMenuItem.Name = "removePerformanceCountersToolStripMenuItem";
            this.removePerformanceCountersToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.removePerformanceCountersToolStripMenuItem.Text = "Remove Performance Counter(s)";
            this.removePerformanceCountersToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // formattingToolStripMenuItem1
            // 
            this.formattingToolStripMenuItem1.Enabled = false;
            this.formattingToolStripMenuItem1.Name = "formattingToolStripMenuItem1";
            this.formattingToolStripMenuItem1.Size = new System.Drawing.Size(247, 22);
            this.formattingToolStripMenuItem1.Text = "Formatting";
            this.formattingToolStripMenuItem1.Click += new System.EventHandler(this.formattingToolStripMenuItem_Click);
            // 
            // moreFormattingOptionsToolStripMenuItem
            // 
            this.moreFormattingOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetAllColorsToolStripMenuItem});
            this.moreFormattingOptionsToolStripMenuItem.Name = "moreFormattingOptionsToolStripMenuItem";
            this.moreFormattingOptionsToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.moreFormattingOptionsToolStripMenuItem.Text = "More Formatting Options";
            // 
            // resetAllColorsToolStripMenuItem
            // 
            this.resetAllColorsToolStripMenuItem.Name = "resetAllColorsToolStripMenuItem";
            this.resetAllColorsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.resetAllColorsToolStripMenuItem.Text = "Reset All Colors";
            this.resetAllColorsToolStripMenuItem.Click += new System.EventHandler(this.resetAllColorsToolStripMenuItem_Click);
            // 
            // visibleToolStripMenuItem1
            // 
            this.visibleToolStripMenuItem1.Checked = true;
            this.visibleToolStripMenuItem1.CheckOnClick = true;
            this.visibleToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.visibleToolStripMenuItem1.Enabled = false;
            this.visibleToolStripMenuItem1.Name = "visibleToolStripMenuItem1";
            this.visibleToolStripMenuItem1.Size = new System.Drawing.Size(247, 22);
            this.visibleToolStripMenuItem1.Text = "Visible";
            this.visibleToolStripMenuItem1.CheckedChanged += new System.EventHandler(this.visibleToolStripMenuItem_CheckedChanged);
            // 
            // moveSelectedToNewWindowToolStripMenuItem
            // 
            this.moveSelectedToNewWindowToolStripMenuItem.Enabled = false;
            this.moveSelectedToNewWindowToolStripMenuItem.Name = "moveSelectedToNewWindowToolStripMenuItem";
            this.moveSelectedToNewWindowToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.moveSelectedToNewWindowToolStripMenuItem.Text = "Move Selected To New Window";
            this.moveSelectedToNewWindowToolStripMenuItem.Click += new System.EventHandler(this.moveSelectionToNewWindowToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(244, 6);
            // 
            // copyDefinitionsToolStripMenuItem
            // 
            this.copyDefinitionsToolStripMenuItem.Enabled = false;
            this.copyDefinitionsToolStripMenuItem.Name = "copyDefinitionsToolStripMenuItem";
            this.copyDefinitionsToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.copyDefinitionsToolStripMenuItem.Text = "Copy Definition(s)";
            this.copyDefinitionsToolStripMenuItem.Click += new System.EventHandler(this.copyDefinitionToolStripMenuItem_Click);
            // 
            // pasteFromDefinitionsToolStripMenuItem
            // 
            this.pasteFromDefinitionsToolStripMenuItem.Name = "pasteFromDefinitionsToolStripMenuItem";
            this.pasteFromDefinitionsToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.pasteFromDefinitionsToolStripMenuItem.Text = "Paste From Definition(s)";
            this.pasteFromDefinitionsToolStripMenuItem.Click += new System.EventHandler(this.pasteFromDefnitionToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(244, 6);
            // 
            // applicationSettingsToolStripMenuItem
            // 
            this.applicationSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwaysOnTopToolStripMenuItem,
            this.graphFormatOptionsToolStripMenuItem,
            this.maximumInitialToolStripMenuItem,
            this.rememberWindowsSizeLocationSaveLoadToolStripMenuItem,
            this.setTitleToolStripMenuItem,
            this.snapToDesktopSidesToolStripMenuItem,
            this.disablePerformanceCountersOnErrorToolStripMenuItem});
            this.applicationSettingsToolStripMenuItem.Name = "applicationSettingsToolStripMenuItem";
            this.applicationSettingsToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.applicationSettingsToolStripMenuItem.Text = "Application Settings";
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.alwaysOnTopToolStripMenuItem.Text = "Always On Top";
            this.alwaysOnTopToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_CheckStateChanged);
            // 
            // graphFormatOptionsToolStripMenuItem
            // 
            this.graphFormatOptionsToolStripMenuItem.Name = "graphFormatOptionsToolStripMenuItem";
            this.graphFormatOptionsToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.graphFormatOptionsToolStripMenuItem.Text = "Graph Format Options";
            this.graphFormatOptionsToolStripMenuItem.Click += new System.EventHandler(this.graphFormatOptionsToolStripMenuItem_Click);
            // 
            // maximumInitialToolStripMenuItem
            // 
            this.maximumInitialToolStripMenuItem.Name = "maximumInitialToolStripMenuItem";
            this.maximumInitialToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.maximumInitialToolStripMenuItem.Text = "Maximum (Initial)";
            this.maximumInitialToolStripMenuItem.Click += new System.EventHandler(this.maximuminitialToolStripMenuItem_Click);
            // 
            // rememberWindowsSizeLocationSaveLoadToolStripMenuItem
            // 
            this.rememberWindowsSizeLocationSaveLoadToolStripMenuItem.Name = "rememberWindowsSizeLocationSaveLoadToolStripMenuItem";
            this.rememberWindowsSizeLocationSaveLoadToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.rememberWindowsSizeLocationSaveLoadToolStripMenuItem.Text = "Remember Windows Size/Location (Save/Load)";
            this.rememberWindowsSizeLocationSaveLoadToolStripMenuItem.Click += new System.EventHandler(this.rememberWindowsSizeLocationSaveLoadToolStripMenuItem_Click);
            // 
            // setTitleToolStripMenuItem
            // 
            this.setTitleToolStripMenuItem.Name = "setTitleToolStripMenuItem";
            this.setTitleToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.setTitleToolStripMenuItem.Text = "Set Title";
            this.setTitleToolStripMenuItem.Click += new System.EventHandler(this.setTitleToolStripMenuItem_Click);
            // 
            // snapToDesktopSidesToolStripMenuItem
            // 
            this.snapToDesktopSidesToolStripMenuItem.CheckOnClick = true;
            this.snapToDesktopSidesToolStripMenuItem.Name = "snapToDesktopSidesToolStripMenuItem";
            this.snapToDesktopSidesToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.snapToDesktopSidesToolStripMenuItem.Text = "Snap To Desktop Sides";
            this.snapToDesktopSidesToolStripMenuItem.CheckedChanged += new System.EventHandler(this.snapToDesktopSidesToolStripMenuItem_CheckedChanged);
            // 
            // disablePerformanceCountersOnErrorToolStripMenuItem
            // 
            this.disablePerformanceCountersOnErrorToolStripMenuItem.CheckOnClick = true;
            this.disablePerformanceCountersOnErrorToolStripMenuItem.Name = "disablePerformanceCountersOnErrorToolStripMenuItem";
            this.disablePerformanceCountersOnErrorToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.disablePerformanceCountersOnErrorToolStripMenuItem.Text = "Disable Performance Counters On Error";
            this.disablePerformanceCountersOnErrorToolStripMenuItem.CheckedChanged += new System.EventHandler(this.disableCounterOnErrorToolStripMenuItem_CheckedChanged);
            // 
            // frequencyToolStripMenuItem1
            // 
            this.frequencyToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.halfSecondsToolStripMenuItem,
            this.secondToolStripMenuItem,
            this.twoSecondsToolStripMenuItem,
            this.fiveSecondsToolStripMenuItem,
            this.tenSecondsToolStripMenuItem,
            this.thirtySecondsToolStripMenuItem,
            this.sixtySecondsToolStripMenuItem});
            this.frequencyToolStripMenuItem1.Image = global::QPerfMon.Properties.Resources.Old2New;
            this.frequencyToolStripMenuItem1.Name = "frequencyToolStripMenuItem1";
            this.frequencyToolStripMenuItem1.Size = new System.Drawing.Size(247, 22);
            this.frequencyToolStripMenuItem1.Text = "Frequency";
            // 
            // halfSecondsToolStripMenuItem
            // 
            this.halfSecondsToolStripMenuItem.Name = "halfSecondsToolStripMenuItem";
            this.halfSecondsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.halfSecondsToolStripMenuItem.Text = "0.5 Seconds";
            this.halfSecondsToolStripMenuItem.Click += new System.EventHandler(this.halfSecondsToolStripMenuItem_Click);
            // 
            // secondToolStripMenuItem
            // 
            this.secondToolStripMenuItem.Checked = true;
            this.secondToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.secondToolStripMenuItem.Name = "secondToolStripMenuItem";
            this.secondToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.secondToolStripMenuItem.Text = "1 Second";
            this.secondToolStripMenuItem.Click += new System.EventHandler(this.secondToolStripMenuItem_Click);
            // 
            // twoSecondsToolStripMenuItem
            // 
            this.twoSecondsToolStripMenuItem.Name = "twoSecondsToolStripMenuItem";
            this.twoSecondsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.twoSecondsToolStripMenuItem.Text = "2 Seconds";
            this.twoSecondsToolStripMenuItem.Click += new System.EventHandler(this.twoSecondsToolStripMenuItem_Click);
            // 
            // fiveSecondsToolStripMenuItem
            // 
            this.fiveSecondsToolStripMenuItem.Name = "fiveSecondsToolStripMenuItem";
            this.fiveSecondsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.fiveSecondsToolStripMenuItem.Text = "5 Seconds";
            this.fiveSecondsToolStripMenuItem.Click += new System.EventHandler(this.fiveSecondsToolStripMenuItem_Click);
            // 
            // tenSecondsToolStripMenuItem
            // 
            this.tenSecondsToolStripMenuItem.Name = "tenSecondsToolStripMenuItem";
            this.tenSecondsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.tenSecondsToolStripMenuItem.Text = "10 Seconds";
            this.tenSecondsToolStripMenuItem.Click += new System.EventHandler(this.tenSecondsToolStripMenuItem_Click);
            // 
            // thirtySecondsToolStripMenuItem
            // 
            this.thirtySecondsToolStripMenuItem.Name = "thirtySecondsToolStripMenuItem";
            this.thirtySecondsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.thirtySecondsToolStripMenuItem.Text = "30 Seconds";
            this.thirtySecondsToolStripMenuItem.Click += new System.EventHandler(this.thirtySecondsToolStripMenuItem_Click);
            // 
            // sixtySecondsToolStripMenuItem
            // 
            this.sixtySecondsToolStripMenuItem.Name = "sixtySecondsToolStripMenuItem";
            this.sixtySecondsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.sixtySecondsToolStripMenuItem.Text = "60 Seconds";
            this.sixtySecondsToolStripMenuItem.Click += new System.EventHandler(this.sixtySecondsToolStripMenuItem_Click);
            // 
            // pauseAllToolStripMenuItem
            // 
            this.pauseAllToolStripMenuItem.CheckOnClick = true;
            this.pauseAllToolStripMenuItem.Name = "pauseAllToolStripMenuItem";
            this.pauseAllToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.pauseAllToolStripMenuItem.Text = "Pause All";
            this.pauseAllToolStripMenuItem.CheckedChanged += new System.EventHandler(this.pauseToolStripMenuItem_CheckedChanged);
            // 
            // loggingToolStripMenuItem
            // 
            this.loggingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loggingOptionsToolStripMenuItem,
            this.startLoggingToolStripMenuItem});
            this.loggingToolStripMenuItem.Image = global::QPerfMon.Properties.Resources.doc_edit;
            this.loggingToolStripMenuItem.Name = "loggingToolStripMenuItem";
            this.loggingToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.loggingToolStripMenuItem.Text = "Logging";
            // 
            // loggingOptionsToolStripMenuItem
            // 
            this.loggingOptionsToolStripMenuItem.Name = "loggingOptionsToolStripMenuItem";
            this.loggingOptionsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.loggingOptionsToolStripMenuItem.Text = "Logging Options";
            this.loggingOptionsToolStripMenuItem.Click += new System.EventHandler(this.logDataToFileToolStripMenuItem_Click);
            // 
            // startLoggingToolStripMenuItem
            // 
            this.startLoggingToolStripMenuItem.Enabled = false;
            this.startLoggingToolStripMenuItem.Name = "startLoggingToolStripMenuItem";
            this.startLoggingToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.startLoggingToolStripMenuItem.Text = "Start Logging";
            this.startLoggingToolStripMenuItem.Click += new System.EventHandler(this.startLoggingToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(244, 6);
            // 
            // loadSetFromFileToolStripMenuItem
            // 
            this.loadSetFromFileToolStripMenuItem.Image = global::QPerfMon.Properties.Resources.folder_doc;
            this.loadSetFromFileToolStripMenuItem.Name = "loadSetFromFileToolStripMenuItem";
            this.loadSetFromFileToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.loadSetFromFileToolStripMenuItem.Text = "Load Set From File";
            this.loadSetFromFileToolStripMenuItem.Click += new System.EventHandler(this.loadSetToolStripMenuItem_Click);
            // 
            // saveCurrentSetToolStripMenuItem2
            // 
            this.saveCurrentSetToolStripMenuItem2.Image = global::QPerfMon.Properties.Resources.save;
            this.saveCurrentSetToolStripMenuItem2.Name = "saveCurrentSetToolStripMenuItem2";
            this.saveCurrentSetToolStripMenuItem2.Size = new System.Drawing.Size(247, 22);
            this.saveCurrentSetToolStripMenuItem2.Text = "Save Current Set";
            this.saveCurrentSetToolStripMenuItem2.Click += new System.EventHandler(this.saveCurrentSetToolStripMenuItem_Click);
            // 
            // lvwCounters
            // 
            this.lvwCounters.AllowDrop = true;
            this.lvwCounters.CheckBoxes = true;
            this.lvwCounters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeaderScale,
            this.columnHeader3});
            this.lvwCounters.ContextMenuStrip = this.allInOneContextMenuStrip;
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
            this.lvwCounters.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwCounters_DragDrop);
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
            this.allInOneContextMenuStrip.ResumeLayout(false);
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
        private DragAndDropListView lvwCounters;
        private System.Windows.Forms.SaveFileDialog saveFileDialogQPerf;
        private System.Windows.Forms.OpenFileDialog openFileDialogQPerf;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSelection;
        private System.Windows.Forms.ToolTip toolTip1;
        private HenIT.Windows.Controls.Graphing.LineFlowGraph2D lineFlowGraph2DControl;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelVersion;
        private System.Windows.Forms.ContextMenuStrip allInOneContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addPerformanceCounterToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloneCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloneAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removePerformanceCountersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formattingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem visibleToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem moveSelectedToNewWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem copyDefinitionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteFromDefinitionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem loggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggingOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startLoggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem loadSetFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentSetToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem frequencyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pauseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem halfSecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twoSecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fiveSecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tenSecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thirtySecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sixtySecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphFormatOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maximumInitialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rememberWindowsSizeLocationSaveLoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snapToDesktopSidesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disablePerformanceCountersOnErrorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moreFormattingOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetAllColorsToolStripMenuItem;
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using HenIT.Utilities;
using HenIT.Windows.Controls.Graphing;
using System.Linq;

namespace QPerfMon
{
    public partial class MainForm : FadeSnapForm
    {
        #region Private vars
        private List<PCMonInstance> pcMonInstances = new List<PCMonInstance>();
        private List<Color> lineColors = new List<Color>();
        private System.Threading.Timer timer;
        private System.Threading.TimerCallback timerCallback;
        private int initialMaxValue = 100;
        private uint defaultLineThickness = 2;
        private string displayTitle = "";
        private string defaultTitle = "Quick performance counter viewer";
        private bool initializing = false;
        private bool paused = true;
        private QPerfMonFile initialPerfMonFile;

        private bool loggingEnabled = false;
        private string loggingOutputFilePath;
        private string loggingOutputFilePathBase;
        private int loggingOutputFileNewFileCounter;
        private int loggingSampleRateCounter = 1;
        #endregion

        #region Constructors
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string[] args)
        {
            initializing = true;
            InitializeComponent();            

            List<string> counters = CommandLineUtils.GetDefault(args);            

            lineColors.Add(Color.LightBlue);
            lineColors.Add(Color.Red);
            lineColors.Add(Color.LightGreen);
            lineColors.Add(Color.Yellow);
            lineColors.Add(Color.RoyalBlue);
            lineColors.Add(Color.Orange);
            lineColors.Add(Color.BlueViolet);
            lineColors.Add(Color.White);

            try
            {
                if (CommandLineUtils.IsCommand(args, "-h", "/h", "-?", "/?"))
                    ShowCommandLineHelp("");
                initialMaxValue = int.Parse(CommandLineUtils.GetCommand(args, initialMaxValue.ToString(), new string[] { "-max:", "/max:" }));
                defaultLineThickness = uint.Parse(CommandLineUtils.GetCommand(args, defaultLineThickness.ToString(), new string[] { "-lt:", "/lt:" }));
                displayTitle = CommandLineUtils.GetCommand(args, "", new string[] { "-title:", "/title:" });

                if (counters.Count == 0)
                    counters.Add(@"<xml><machine>.</machine><category>Processor</category><counter>% Processor Time</counter><instance>_Total</instance></xml>");
                if (counters[0].EndsWith(".qpmset"))
                {
                    openFileDialogQPerf.FileName = counters[0];
                    saveFileDialogQPerf.FileName = counters[0];
                    initialPerfMonFile = SerializationUtils.DeserializeXMLFile<QPerfMonFile>(counters[0]);
                }
                else
                {
                    initialPerfMonFile = new QPerfMonFile();
                    initialPerfMonFile.Title = displayTitle;
                    initialPerfMonFile.InitialMaxValue = initialMaxValue;
                    foreach (string counter in counters)
                    {
                        initialPerfMonFile.CounterDefinitionList.Add(counter);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        } 
        #endregion

        #region ShowCommandLineHelp
        private void ShowCommandLineHelp(string warning)
        {
            MessageBox.Show(warning + "The following command line parameters are supported\r\n" +
                "QPerfMon.exe [Quick perfmon set file] [Machine\\Category\\Counter\\Instance\\[Scale] [-max:X] [-lt:Y] [-title:Z]]\r\n" +
                "Where\r\n" +
                "\tQuick perfmon set file: Path to .qpmset file\r\n" +
                "\tPerformance counter: \r\n" +
                "\t\tMachine: name of the machine\r\n" +
                "\t\tCategory: performance category\r\n" +
                "\t\tCounter: name of performance counter\r\n" +
                "\t\tInstance: instance of performance counter\r\n\t\t\t(blank for none)\r\n" +
                "\t\tScale: scale of performance counter\r\n\t\t\t(default=1)\r\n" +
                "\tmax: Initial maximum Y axis value of graph grid\r\n" +
                "\tlt: default line thickness (default=1)\r\n" +
                "\ttitle: give a title to the window\r\n" +
                "\r\nExample QPerfMon.exe \".\\Processor\\% Processor Time\\_Total\\1\" -max:100 -title:Test",
                "Command line parameters", 
                MessageBoxButtons.OK, warning.Length == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Exclamation);
        } 
        #endregion

        #region Form events
        private void MainForm_Load(object sender, EventArgs e)
        {
            rememberSizePositionToolStripMenuItem.Checked = Properties.Settings.Default.RememberSizeLocationOnSaveLoad;
            snapToDesktopSidesToolStripMenuItem.Checked = Properties.Settings.Default.MainWindowSnap;
            alwaysOnTopToolStripMenuItem.Checked = Properties.Settings.Default.AlwaysOnTop;
            lineFlowGraph2DControl.BackColor = Properties.Settings.Default.GraphBackgroundColor;
            lineFlowGraph2DControl.GridColor = Properties.Settings.Default.GridColor;
            lineFlowGraph2DControl.TextColor = Properties.Settings.Default.GraphTextColor;
            lineFlowGraph2DControl.TextFont = Properties.Settings.Default.GraphTextFont;
            toolStripStatusLabelVersion.Text = string.Format("Version {0}", Application.ProductVersion.ToString());
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            LoadCounters(initialPerfMonFile);

            timerCallback = new System.Threading.TimerCallback(onTimerTick);
            timer = new System.Threading.Timer(timerCallback, null, 0, 1000);
            lvwCounters_Resize(null, null);
            IsLoggingSetUp();
            initializing = false;
            paused = false;
            UpdateTitleText();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            paused = true; //stop any collection still in progress
            timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            Properties.Settings.Default.GraphBackgroundColor = lineFlowGraph2DControl.BackColor;
            Properties.Settings.Default.GridColor = lineFlowGraph2DControl.GridColor;
            Properties.Settings.Default.GraphTextColor = lineFlowGraph2DControl.TextColor;
            Properties.Settings.Default.GraphTextFont =lineFlowGraph2DControl.TextFont;
            Properties.Settings.Default.Save();
        }
        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            try
            {
                lvwCounters.EndUpdate();
            }
            catch { }
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                LoadListView();
            }
            else if (e.KeyCode == Keys.F8)
            {
                c2DPushGraphControl_DoubleClick(sender, e);
            }
        }
        private void c2DPushGraphControl_DoubleClick(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
        }
        #endregion

        #region Listview events
        private void lvwCounters_Resize(object sender, EventArgs e)
        {
            int rest = lvwCounters.Columns[1].Width + lvwCounters.Columns[2].Width + lvwCounters.Columns[3].Width;
            if (lvwCounters.ClientSize.Width > rest)
                lvwCounters.Columns[0].Width = lvwCounters.ClientSize.Width - rest;
        }
        private void lvwCounters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!initializing)
            {
                try
                {
                    foreach (ListViewItem lvi in lvwCounters.Items)
                    {
                        PCMonInstance pcmi = (PCMonInstance)lvi.Tag;
                        pcmi.Selected = lvi.Selected;
                        ILine currentLine = lineFlowGraph2DControl.GetLine(lvi.Text);
                        if (currentLine != null)
                        {
                            if (lvi.Selected)
                            {
                                //c2DPushGraphControl.GetLineHandle(lvi.Text).Thickness = defaultLineThickness + 2;
                                currentLine.Thickness = defaultLineThickness + 2;
                            }
                            else
                            {
                                //c2DPushGraphControl.GetLineHandle(lvi.Text).Thickness = defaultLineThickness;
                                currentLine.Thickness = defaultLineThickness;
                            }
                        }
                    }
                    copyDefinitionToolStripMenuItem.Enabled = false;
                    //push only the first selected one to top
                    if (lvwCounters.SelectedItems.Count > 0)
                    {
                        lineFlowGraph2DControl.SetSelectedLine(lvwCounters.SelectedItems[0].Text);
                        
                        removeToolStripMenuItem.Enabled = (lvwCounters.Items.Count > 1) && (lvwCounters.Items.Count > lvwCounters.SelectedItems.Count);
                        moveSelectionToNewWindowToolStripMenuItem.Enabled = (lvwCounters.Items.Count > lvwCounters.SelectedItems.Count);
                        copyDefinitionToolStripMenuItem.Enabled = true;

                        if (lvwCounters.SelectedItems[0].Index > 0)
                            moveUpToolStripMenuItem.Enabled = true;
                        if (lvwCounters.SelectedItems[lvwCounters.SelectedItems.Count-1].Index < lvwCounters.Items.Count-1)
                            moveDownToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        lineFlowGraph2DControl.SetSelectedLine("");
                        removeToolStripMenuItem.Enabled = false;
                        moveUpToolStripMenuItem.Enabled = false;
                        moveDownToolStripMenuItem.Enabled = false;
                        moveSelectionToNewWindowToolStripMenuItem.Enabled = false;
                    }

                    if (lvwCounters.SelectedItems.Count == 1)
                    {
                        visibleToolStripMenuItem.Enabled = true;
                        formattingToolStripMenuItem.Enabled = true;
                        addClonePerformanceCounterToolStripMenuItem.Enabled = true;
                        addCloneAllToolStripMenuItem.Enabled = true;
                        toolStripSeparator1.Visible = lvwCounters.SelectedItems[0].ForeColor == Color.Red;
                        lastErrorToolStripMenuItem1.Visible = lvwCounters.SelectedItems[0].ForeColor == Color.Red;
                    }
                    else
                    {
                        visibleToolStripMenuItem.Enabled = false;
                        formattingToolStripMenuItem.Enabled = false;
                        addClonePerformanceCounterToolStripMenuItem.Enabled = false;
                        addCloneAllToolStripMenuItem.Enabled = false;
                        toolStripSeparator1.Visible = false;
                        lastErrorToolStripMenuItem1.Visible = false;
                    }

                    

                    UpdateStatusBarText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void lvwCounters_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!initializing)
            {
                ILine line = lineFlowGraph2DControl.GetLine(e.Item.Text);
                //HenIT.Windows.Controls.C2DPushGraph.Graphing.C2DPushGraph.LineHandle lh = c2DPushGraphControl.GetLineHandle(e.Item.Text);
                line.Visible = e.Item.Checked;
                lineFlowGraph2DControl.UpdateGraph();
                if (lvwCounters.CheckedItems.Count == 0)
                    e.Item.Checked = true;
            }
        }
        #endregion

        #region Splitter events
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            try
            {
                lvwCounters.EndUpdate();
            }
            catch { }
        }
        #endregion

        #region Timer event
        private void onTimerTick(object obj)
        {
            if (!paused && !initializing)
            {
                if (this != null && !this.Disposing)
                {
                    try
                    {
                        this.Invoke((MethodInvoker)delegate()
                            {
                                try
                                {
                                    lvwCounters.BeginUpdate();
                                    int[] newvalues = new int[lvwCounters.Items.Count];
                                    for (int i = 0; i < pcMonInstances.Count; i++)
                                    {
                                        if (paused)
                                            break;
                                        PCMonInstance pcMonInstance = pcMonInstances[i];
                                        ListViewItem currentLvi = null;
                                        currentLvi = (from ListViewItem l in lvwCounters.Items
                                                      where l.Tag is PCMonInstance &&
                                                        ((PCMonInstance)l.Tag).CounterDefinition() == pcMonInstance.CounterDefinition()
                                                      select l).FirstOrDefault();

                                        float pcValue = 0;
                                        try
                                        {
                                            //lazy load if needed
                                            if (pcMonInstance.PCInstance == null)
                                            {
                                                pcMonInstance.CreatePCInstance();
                                            }

                                            pcValue = (pcMonInstance.PCInstance.NextValue());
                                            //Get right list view item to update
                                            if (currentLvi != null)
                                                if (currentLvi.ForeColor != SystemColors.WindowText)
                                                    currentLvi.ForeColor = SystemColors.WindowText;
                                            pcMonInstance.LastError = "";

                                        }
                                        catch (Exception ex)
                                        {
                                            if (currentLvi != null)
                                                currentLvi.ForeColor = Color.Red;
                                            pcMonInstance.LastError = ex.Message;
                                            if (Properties.Settings.Default.DisableCounterOnError)
                                            {
                                                if (currentLvi != null)
                                                {
                                                    currentLvi.Checked = false;
                                                    currentLvi.SubItems[3].Text = "Err";
                                                }
                                            }
                                        }
                                        finally
                                        {
                                            pcMonInstance.LastValue = pcValue;
                                            string pcValueStr;
                                            if (pcValue > 999)
                                                pcValueStr = string.Format("{0:F1}", pcValue);
                                            else
                                                pcValueStr = string.Format("{0:F3}", pcValue);
                                            if (currentLvi != null)
                                                if (currentLvi.SubItems[3].Text != pcValueStr)
                                                    currentLvi.SubItems[3].Text = pcValueStr;

                                            lineFlowGraph2DControl.Push(pcValue, pcMonInstance.Name);
                                        }
                                    }
                                    LogToFile();
                                    lineFlowGraph2DControl.UpdateGraph();
                                }
                                catch (Exception ex)
                                {
                                    paused = true;
                                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                finally
                                {
                                    lvwCounters.EndUpdate();
                                }
                            });
                    }
                    catch (System.ObjectDisposedException) { }
                    catch (System.InvalidOperationException) { }
                }
            }
        }
        private void SetPollingFrequency(ToolStripMenuItem tsmi)
        {
            halfSecondsToolStripMenuItem.Checked = (halfSecondsToolStripMenuItem == tsmi);
            secondToolStripMenuItem.Checked = (secondToolStripMenuItem == tsmi);
            twoSecondsToolStripMenuItem.Checked = (twoSecondsToolStripMenuItem == tsmi);
            fiveSecondsToolStripMenuItem.Checked = (fiveSecondsToolStripMenuItem == tsmi);
            tenSecondsToolStripMenuItem.Checked = (tenSecondsToolStripMenuItem == tsmi);
            thirtySecondsToolStripMenuItem.Checked = (thirtySecondsToolStripMenuItem == tsmi);
            sixtySecondsToolStripMenuItem.Checked = (sixtySecondsToolStripMenuItem == tsmi);

            long period = 1000;
            if (halfSecondsToolStripMenuItem.Checked)
                period = 500;
            else if (twoSecondsToolStripMenuItem.Checked)
                period = 2000;
            else if (fiveSecondsToolStripMenuItem.Checked)
                period = 5000;
            else if (tenSecondsToolStripMenuItem.Checked)
                period = 10000;
            else if (thirtySecondsToolStripMenuItem.Checked)
                period = 30000;
            else if (sixtySecondsToolStripMenuItem.Checked)
                period = 60000;
            timer.Change(0, period);
        }
        #endregion

        #region Context menu events
        private void visibleToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (lvwCounters.SelectedItems.Count == 1)
            {
                lvwCounters.SelectedItems[0].Checked = !lvwCounters.SelectedItems[0].Checked;
            }
        }
        private void formattingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwCounters.SelectedItems.Count ==1)
            {
                Formatting formatting = new Formatting();
                ILine line = lineFlowGraph2DControl.GetLine(lvwCounters.SelectedItems[0].Text);
                formatting.SelectedScale = line.Scale;
                formatting.SelectedColor = line.Color;
                formatting.PlotStyle = line.PlotStyle;
                if (formatting.ShowDialog() == DialogResult.OK)
                {
                    lvwCounters.SelectedItems[0].SubItems[2].Text = formatting.SelectedScale.ToString();
                    lvwCounters.SelectedItems[0].SubItems[1].ForeColor = formatting.SelectedColor;
                    lvwCounters.SelectedItems[0].SubItems[1].BackColor = formatting.SelectedColor;

                    line.Color = formatting.SelectedColor;
                    line.Scale = formatting.SelectedScale;
                    line.PlotStyle = formatting.PlotStyle;
                    PCMonInstance pcMonInstance = (PCMonInstance)lvwCounters.SelectedItems[0].Tag;
                    pcMonInstance.Scale = formatting.SelectedScale;
                    pcMonInstance.PlotStyle = (int)formatting.PlotStyle;
                    pcMonInstance.PlotColor = formatting.SelectedColor;
                }
            }
        }
        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwCounters.SelectedItems.Count > 0 && lvwCounters.SelectedItems[0].Index > 0)
            {
                bool oldPause = paused;
                StartStopLogging(true);
                paused = true;
                initializing = true;

                ListViewItem itemAbove = lvwCounters.Items[lvwCounters.SelectedItems[0].Index - 1];


                initializing = false;
                paused = oldPause;
            }
        }
        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AddNewCounter("", "", "", "");
            AddNewCounters("", "", "");
        }
        private void addClonePerformanceCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwCounters.SelectedItems.Count > 0)
            {
                PCMonInstance pcmi = (PCMonInstance)lvwCounters.SelectedItems[0].Tag;
                AddNewCounters(pcmi.Machine, pcmi.Category, "");
            }
            else
                AddNewCounters("", "", "");
        }
        private void addCloneAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwCounters.SelectedItems.Count > 0)
            {
                PCMonInstance pcmi = (PCMonInstance)lvwCounters.SelectedItems[0].Tag;
                AddNewCounters(pcmi.Machine, pcmi.Category, pcmi.Counter);
            }
            else
                AddNewCounters("", "", "");
        }
        private void AddNewCounter(string initialMachine, string initialCategory, string initialCounter, string initialInstance)
        {
            AddCounter addCounter = new AddCounter();
            addCounter.InitialMachine = initialMachine;
            addCounter.InitialCategory = initialCategory;
            addCounter.InitialCounter = initialCounter;
            addCounter.InitialInstance = initialInstance;
            int colorIndex = lvwCounters.Items.Count % lineColors.Count;
            addCounter.InitialColor = lineColors[colorIndex];
            foreach(ListViewItem lvi in lvwCounters.Items)
            {
                addCounter.ExistingCounters.Add(lvi.Text);
            }

            if (addCounter.ShowDialog() == DialogResult.OK)
            {
                StartStopLogging(true);
                bool oldPause = paused;
                paused = true;
                initializing = true;
                try
                {
                    pcMonInstances.Add(addCounter.SelectedPCMonInstance);                    
                    ListViewItem lvi = new ListViewItem(addCounter.SelectedPCMonInstance.Name);
                    lvi.UseItemStyleForSubItems = false;
                    ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem();
                    sub.Text = "###";
                    sub.ForeColor = addCounter.InitialColor;
                    sub.BackColor = addCounter.InitialColor;
                    lvi.SubItems.Add(sub);
                    lvi.SubItems.Add(addCounter.SelectedPCMonInstance.Scale.ToString());
                    lvi.SubItems.Add("");
                    lvi.Checked = true;
                    lvi.Tag = addCounter.SelectedPCMonInstance;
                    lvwCounters.Items.Add(lvi);

                    ILine line = lineFlowGraph2DControl.AddLine(addCounter.SelectedPCMonInstance.Name, addCounter.InitialColor, addCounter.SelectedPCMonInstance.Scale);

                    line.Thickness = defaultLineThickness;
                    line.PlotStyle = (LinePlotStyle)addCounter.SelectedPCMonInstance.PlotStyle;
                    UpdateStatusBarText();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                initializing = false;
                paused = oldPause;
            }
        }
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwCounters.Items.Count > 1)
            {
                if (MessageBox.Show("Are you sure you want to remove this performance counter(s)?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        StartStopLogging(true);
                        foreach (ListViewItem lvi in lvwCounters.SelectedItems)
                        {
                            PCMonInstance removeItem = (PCMonInstance)lvi.Tag;
                            lineFlowGraph2DControl.RemoveLine(removeItem.Name);
                            pcMonInstances.Remove(removeItem);
                        }
                        lineFlowGraph2DControl.SetSelectedLine("");
                        LoadListView();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    UpdateStatusBarText();
                    lineFlowGraph2DControl.UpdateGraph();
                    copyDefinitionToolStripMenuItem.Enabled = false;
                }
            }
        }
        private void copyDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<CounterDefinitionList>");
            foreach (ListViewItem lvi in lvwCounters.SelectedItems)
            {
                PCMonInstance item = (PCMonInstance)lvi.Tag;
                string xmlDef = item.GetCounterDefinitionXml();
                sb.AppendLine(string.Format("<string>{0}</string>", 
                    xmlDef.Replace("<", "&lt;").Replace(">", "&gt;")));
                
            }
            sb.AppendLine("</CounterDefinitionList>");
            Clipboard.SetText(sb.ToString());
        }
        private void pasteFromDefnitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool oldPause = paused;
            try
            {
                StartStopLogging(true);                
                paused = true;
                initializing = true;

                string definitionXml = Clipboard.GetText(TextDataFormat.UnicodeText).Trim(' ','\r','\n');
                if (definitionXml.Length > 0)
                {
                    if (definitionXml.StartsWith("<CounterDefinitionList>") && definitionXml.EndsWith("</CounterDefinitionList>"))
                    {
                        List<PCMonInstance> importCounters = PCMonInstance.GetCountersFromCounterDefinitionList(definitionXml);

                        foreach (PCMonInstance pcmi in importCounters)
                        {
                            bool duplicate = false;
                            foreach (ListViewItem i in lvwCounters.Items)
                            {
                                if (i.Tag is PCMonInstance)
                                {
                                    PCMonInstance item = (PCMonInstance)i.Tag;
                                    if (item.CounterDefinition() == pcmi.CounterDefinition())
                                    {
                                        duplicate = true;
                                        MessageBox.Show(string.Format("You cannot add a duplicate performance counter!\r\nThe counter {0} already exists!", pcmi.CounterDefinition()),
                                            "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                }
                            }

                            if (!duplicate)
                            {
                                pcMonInstances.Add(pcmi);
                                ListViewItem lvi = new ListViewItem(pcmi.Name);
                                lvi.UseItemStyleForSubItems = false;
                                ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem();
                                sub.Text = "###";
                                sub.ForeColor = pcmi.PlotColor;
                                sub.BackColor = pcmi.PlotColor;
                                lvi.SubItems.Add(sub);
                                lvi.SubItems.Add(pcmi.Scale.ToString());
                                lvi.SubItems.Add("");
                                lvi.Checked = true;
                                lvi.Tag = pcmi;
                                lvwCounters.Items.Add(lvi);

                                ILine line = lineFlowGraph2DControl.AddLine(pcmi.Name, pcmi.PlotColor, pcmi.Scale);

                                line.Thickness = defaultLineThickness;
                                line.PlotStyle = (LinePlotStyle)pcmi.PlotStyle;
                                UpdateStatusBarText();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("The current content of the clipboard does not contain a valid performance counter definition (set)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                initializing = false;
                paused = oldPause;
            }
        }
        private void loadSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogQPerf.ShowDialog() == DialogResult.OK)
                {
                    StartStopLogging(true);
                    saveFileDialogQPerf.FileName = openFileDialogQPerf.FileName;
                    QPerfMonFile qPerfMonFile = SerializationUtils.DeserializeXMLFile<QPerfMonFile>(openFileDialogQPerf.FileName);
                    //toolStripStatusLabelSelection.Text = qPerfMonFile.Version;
                    LoadCounters(qPerfMonFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveCurrentSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialogQPerf.ShowDialog() == DialogResult.OK)
                {
                    openFileDialogQPerf.FileName = saveFileDialogQPerf.FileName;
                    QPerfMonFile qPerfMonFile = new QPerfMonFile();
                    qPerfMonFile.Title = displayTitle;
                    qPerfMonFile.InitialMaxValue = initialMaxValue;
                    qPerfMonFile.Version = Application.ProductVersion.ToString();

                    //to preserve the order of visible items the definitions from the ListView are used
                    foreach (ListViewItem lvi in lvwCounters.Items)
                    {
                        if (lvi.Tag is PCMonInstance)
                        {
                            PCMonInstance pcmi = (PCMonInstance)lvi.Tag;
                            string key = pcmi.KeyToXml();
                            qPerfMonFile.CounterDefinitionList.Add(key);
                        }
                    }

                    //foreach (PCMonInstance pcmi in pcMonInstances)
                    //{
                        
                    //    string key = pcmi.KeyToXml();
                    //    qPerfMonFile.CounterDefinitionList.Add(key);
                    //}
                    qPerfMonFile.MainWindowSize = this.Size;
                    qPerfMonFile.MainWindowLocation = this.Location;
                    qPerfMonFile.RememberMainWindowSizeLocation = rememberSizePositionToolStripMenuItem.Checked;                    
                    SerializationUtils.SerializeXMLToFile(saveFileDialogQPerf.FileName, qPerfMonFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lastErrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwCounters.SelectedItems.Count == 1)
            {
                PCMonInstance pcMonInstance = (PCMonInstance)lvwCounters.SelectedItems[0].Tag;
                if (pcMonInstance.LastError.Length > 0)
                {
                    MessageBox.Show(pcMonInstance.LastError, "Error details", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
        }
        private void moveSelectionToNewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder parameters = new StringBuilder();
            foreach (ListViewItem lvi in lvwCounters.SelectedItems)
            {
                lvi.Checked = false;
                PCMonInstance pcmi = (PCMonInstance)lvi.Tag;
                string key = pcmi.KeyToXml();
                parameters.Append("\"");
                parameters.Append(key);
                parameters.Append("\" ");
            }
            parameters.Append(String.Format(" -max:{0}", initialMaxValue));
            parameters.Append(" \"-title:New window\"");            

            try
            {
                StartStopLogging(true);
                ProcessStartInfo psi = new ProcessStartInfo(System.Reflection.Assembly.GetExecutingAssembly().Location, parameters.ToString());
                Process.Start(psi);

                foreach (ListViewItem lvi in lvwCounters.SelectedItems)
                {
                    PCMonInstance removeItem = (PCMonInstance)lvi.Tag;
                    lineFlowGraph2DControl.RemoveLine(removeItem.Name);
                    pcMonInstances.Remove(removeItem);
                }
                LoadListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.DoEvents();
            lineFlowGraph2DControl.UpdateGraph();
        }        
        private void halfSecondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPollingFrequency(halfSecondsToolStripMenuItem);
        }
        private void secondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPollingFrequency(secondToolStripMenuItem);
        }
        private void twoSecondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPollingFrequency(twoSecondsToolStripMenuItem);
        }
        private void fiveSecondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPollingFrequency(fiveSecondsToolStripMenuItem);
        }
        private void tenSecondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPollingFrequency(tenSecondsToolStripMenuItem);
        }
        private void thirtySecondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPollingFrequency(thirtySecondsToolStripMenuItem);
        }
        private void sixtySecondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPollingFrequency(sixtySecondsToolStripMenuItem);
        }
        private void pauseToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            paused = pauseToolStripMenuItem.Checked;
            UpdateTitleText();
        }
        private void setTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBox inputBox = new InputBox();
            inputBox.WindowTitle = "Set window title";
            inputBox.Prompt = "Change the Title for the window";
            inputBox.SelectedValue = displayTitle;
            if (inputBox.ShowDialog() == DialogResult.OK)
            {
                displayTitle = inputBox.SelectedValue;
            }
            if (displayTitle.Length > 0)
            {
                this.Text = defaultTitle + " - " + displayTitle;
            }
            else
            {
                this.Text = defaultTitle;
            }
        }
        private void graphFormatOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphFormatting graphFormatting = new GraphFormatting();
            graphFormatting.GraphBackgroundColor = lineFlowGraph2DControl.BackColor;
            graphFormatting.GridColor = lineFlowGraph2DControl.GridColor;
            graphFormatting.LabelFont = lineFlowGraph2DControl.TextFont;
            graphFormatting.LabelForeColor = lineFlowGraph2DControl.TextColor;
            if (graphFormatting.ShowDialog() == DialogResult.OK)
            {
                lineFlowGraph2DControl.BackColor = graphFormatting.GraphBackgroundColor;
                lineFlowGraph2DControl.GridColor = graphFormatting.GridColor;
                lineFlowGraph2DControl.TextFont = graphFormatting.LabelFont;
                lineFlowGraph2DControl.TextColor = graphFormatting.LabelForeColor;
                Properties.Settings.Default.GraphBackgroundColor = lineFlowGraph2DControl.BackColor;
                Properties.Settings.Default.GridColor = lineFlowGraph2DControl.GridColor;
                Properties.Settings.Default.GraphTextColor = lineFlowGraph2DControl.TextColor;
                Properties.Settings.Default.GraphTextFont = lineFlowGraph2DControl.TextFont;
                Properties.Settings.Default.Save();
                lineFlowGraph2DControl.UpdateGraph();
            }
        }
        private void maximuminitialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetInitialGraphMax setInitialGraphMax = new SetInitialGraphMax();
            setInitialGraphMax.InitialMaximum = initialMaxValue;
            if (setInitialGraphMax.ShowDialog() == DialogResult.OK)
            {
                initialMaxValue = setInitialGraphMax.InitialMaximum;
                lineFlowGraph2DControl.MaxPeekMagnitudePreAutoScale = setInitialGraphMax.InitialMaximum;

                if (initialMaxValue > lineFlowGraph2DControl.GetCurrentMaxOnGraph())
                {
                    lineFlowGraph2DControl.MaxPeekMagnitude = setInitialGraphMax.InitialMaximum;
                    lineFlowGraph2DControl.MaxLabel = setInitialGraphMax.InitialMaximum.ToString();
                }

                lineFlowGraph2DControl.UpdateGraph();
            }
        }
        private void logDataToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loggingEnabled)
            {
                if (MessageBox.Show("Logging is active! Do you want to stop logging and change logging settings?", "Logging active", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                else
                {
                    StartStopLogging(true);
                }
            }
            LogToFileOptions logToFileOptions = new LogToFileOptions();
            logToFileOptions.LoggingDirectory = Properties.Settings.Default.LoggingDirectory;
            logToFileOptions.LoggingFileName = Properties.Settings.Default.LoggingFileName;
            logToFileOptions.LoggingAppendDateTime = Properties.Settings.Default.LoggingAppendDateTime;
            logToFileOptions.LoggingMinimumDiskSpaceLimitMB = Properties.Settings.Default.LoggingMinimumDiskSpaceLimitMB;
            logToFileOptions.LoggingCreateNewFileEveryMB = Properties.Settings.Default.LoggingCreateNewFileEveryMB;
            logToFileOptions.LoggingSampleRate = Properties.Settings.Default.LoggingSampleRate;
            logToFileOptions.LoggingDecimalDigits = Properties.Settings.Default.LoggingDecimalDigits;
            if (logToFileOptions.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.LoggingDirectory = logToFileOptions.LoggingDirectory;
                Properties.Settings.Default.LoggingFileName = logToFileOptions.LoggingFileName;
                Properties.Settings.Default.LoggingAppendDateTime = logToFileOptions.LoggingAppendDateTime;
                Properties.Settings.Default.LoggingMinimumDiskSpaceLimitMB = logToFileOptions.LoggingMinimumDiskSpaceLimitMB;
                Properties.Settings.Default.LoggingCreateNewFileEveryMB = logToFileOptions.LoggingCreateNewFileEveryMB;
                Properties.Settings.Default.LoggingSampleRate = logToFileOptions.LoggingSampleRate;
                Properties.Settings.Default.LoggingDecimalDigits = logToFileOptions.LoggingDecimalDigits;
                Properties.Settings.Default.Save();
                IsLoggingSetUp();
            }
        }
        private void startLoggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartStopLogging(false);
        }
        private void snapToDesktopSidesToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MainWindowSnap = snapToDesktopSidesToolStripMenuItem.Checked;
            this.SnappingEnabled = Properties.Settings.Default.MainWindowSnap;
        }
        private void disableCounterOnErrorToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DisableCounterOnError = disableCounterOnErrorToolStripMenuItem.Checked;
        }
        #endregion        

        #region Loading
        private void LoadCounters(QPerfMonFile qPerfMonFile)
        {
            bool oldPause = paused;
            paused = true;
            initializing = true;
            try
            {
                pcMonInstances = new List<PCMonInstance>();
                lvwCounters.Items.Clear();
                lineFlowGraph2DControl.ClearAllLines();
                displayTitle = qPerfMonFile.Title;
                lineFlowGraph2DControl.MaxLabel = qPerfMonFile.InitialMaxValue.ToString();
                lineFlowGraph2DControl.MaxPeekMagnitude = qPerfMonFile.InitialMaxValue;
                initialMaxValue = qPerfMonFile.InitialMaxValue;

                foreach (string counterDefinition in qPerfMonFile.CounterDefinitionList)
                {
                    bool unique = true;
                    try
                    {
                        toolStripStatusLabelSelection.Text = string.Format("Loading {0}", counterDefinition);
                        Application.DoEvents();
                        PCMonInstance pcMonInstance = new PCMonInstance(counterDefinition);

                        foreach (PCMonInstance existing in pcMonInstances)
                        {
                            if (existing.Equals(pcMonInstance))
                            {
                                unique = false;
                                break;
                            }
                        }
                        if (unique)
                        {
                            pcMonInstance.CreatePCInstance();
                            pcMonInstances.Add(pcMonInstance);
                        }
                    }
                    catch (Exception innerEx)
                    {
                        MessageBox.Show(string.Format("Error parsing {0}\r\n{1}", counterDefinition, innerEx.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                ILine line;
                for (int i = 0; i < pcMonInstances.Count; i++)
                {
                    int colorIndex = i % lineColors.Count;
                    if (pcMonInstances[i].LoadColorError)
                        pcMonInstances[i].PlotColor = lineColors[colorIndex];
                    line = lineFlowGraph2DControl.AddLine(pcMonInstances[i].Name, pcMonInstances[i].PlotColor, pcMonInstances[i].Scale);
                    line.PlotStyle = (LinePlotStyle)pcMonInstances[i].PlotStyle;
                    line.Thickness = defaultLineThickness;
                }

                try
                {
                    if (qPerfMonFile.MainWindowSize != null && qPerfMonFile.MainWindowLocation != null && qPerfMonFile.RememberMainWindowSizeLocation && qPerfMonFile.MainWindowSize != new Size(0, 0))
                    {
                        this.Size = qPerfMonFile.MainWindowSize;
                        this.Location = qPerfMonFile.MainWindowLocation;                        
                    }
                    rememberSizePositionToolStripMenuItem.Checked = qPerfMonFile.RememberMainWindowSizeLocation;
                    Properties.Settings.Default.RememberSizeLocationOnSaveLoad = rememberSizePositionToolStripMenuItem.Checked;
                }
                catch { }

                LoadListView(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            initializing = false;
            paused = oldPause;
            UpdateTitleText();
            lineFlowGraph2DControl.UpdateGraph();
        }
        private void LoadListView()
        {
            LoadListView(false);
        }
        private void LoadListView(bool withInitializing)
        {
            bool oldPause = paused;
            paused = true;
            initializing = true;
            try
            {
                lvwCounters.Items.Clear();
                List<ListViewItem> lvitems = new List<ListViewItem>();
                foreach (PCMonInstance pcMonInstance in pcMonInstances)
                {
                    toolStripStatusLabelSelection.Text = string.Format("Loading {0}", pcMonInstance.Name);
                    Application.DoEvents();
                    int colorIndex = lvwCounters.Items.Count % lineColors.Count;
                    string scale = "1";
                    if (pcMonInstance.Scale < 1)
                        scale = pcMonInstance.Scale.ToString("0.########");
                    else
                        scale = pcMonInstance.Scale.ToString("0");

                    ListViewItem lvi = new ListViewItem(pcMonInstance.Name);
                    lvi.UseItemStyleForSubItems = false;
                    ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem();
                    sub.Text = "###";
                    sub.ForeColor = pcMonInstance.PlotColor; 
                    sub.BackColor = pcMonInstance.PlotColor;
                    lvi.SubItems.Add(sub);
                    lvi.SubItems.Add(scale);
                    lvi.SubItems.Add(pcMonInstance.LastValue.ToString("0.00"));
                    lvi.Checked = true;
                    lvi.Tag = pcMonInstance;
                    lvi.Selected = pcMonInstance.Selected;
                    lvitems.Add(lvi);                    
                }
                lvwCounters.Items.AddRange(lvitems.ToArray());
                lineFlowGraph2DControl.SetSelectedLine("");
                UpdateStatusBarText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            initializing = withInitializing;
            paused = oldPause;
        }
        private void UpdateStatusBarText()
        {
            try
            {
                StringBuilder footerText = new StringBuilder();
                if (loggingEnabled)
                {

                    footerText.Append("* ");
                }
                
                if (lvwCounters.SelectedItems.Count == 0)
                {
                    
                    footerText.Append(string.Format("{0} counter(s)", lvwCounters.Items.Count));
                }
                else if (lvwCounters.SelectedItems.Count == 1)
                {
                    footerText.Append(string.Format("{0} counter(s), Selected: {1}", lvwCounters.Items.Count, lvwCounters.SelectedItems[0].Text));
                }
                else
                {
                    footerText.Append(string.Format("{0} counter(s), {1} selected", lvwCounters.Items.Count, lvwCounters.SelectedItems.Count));
                }
                toolStripStatusLabelSelection.Text = footerText.ToString();
                
                toolStripStatusLabelSelection.ToolTipText = toolStripStatusLabelSelection.Text;
                toolTip1.SetToolTip(statusStrip1, toolStripStatusLabelSelection.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateTitleText()
        {
            if (displayTitle.Length > 0)
            {
                this.Text = defaultTitle + " - " + displayTitle;
            }
            else
            {
                this.Text = defaultTitle;
            }
            if (paused)
                this.Text += " (paused)";
        }
        #endregion              

        #region Logging to File
        private void IsLoggingSetUp()
        {
            bool isSetUp = false;
            if ((Properties.Settings.Default.LoggingDirectory.Length > 0)
                && (Properties.Settings.Default.LoggingFileName.Length > 0)
                && pcMonInstances.Count > 0)
            {
                if (System.IO.Directory.Exists(Properties.Settings.Default.LoggingDirectory))
                    isSetUp = true;
            }
            startLoggingToolStripMenuItem.Enabled = isSetUp;
        }
        private void StartStopLogging(bool forceStop)
        {
            if (forceStop)
                loggingEnabled = true;
            if (loggingEnabled)
            {
                loggingEnabled = false;
                startLoggingToolStripMenuItem.Text = "Start logging";
            }
            else
            {
                string filename = Properties.Settings.Default.LoggingFileName;
                if (Properties.Settings.Default.LoggingAppendDateTime)
                    filename += DateTime.Now.ToString("yyyyMMddHHmmss");
                filename += ".csv";
                loggingOutputFilePath = System.IO.Path.Combine(Properties.Settings.Default.LoggingDirectory, filename);
                loggingOutputFilePathBase = loggingOutputFilePath;
                loggingOutputFileNewFileCounter = 0;
                loggingSampleRateCounter = 1;
                CreateNewLoggingFile();
            }
            UpdateStatusBarText();
        }
        private void CreateNewLoggingFile()
        {
            try
            {
                if (System.IO.File.Exists(loggingOutputFilePath))
                {
                    if (MessageBox.Show(string.Format("The file '{0}' already exists! Should it be overwritten?", loggingOutputFilePath), "File exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                    else
                    {
                        System.IO.File.Delete(loggingOutputFilePath);
                    }
                }

                StringBuilder header = new StringBuilder();
                header.Append("Time");
                foreach (PCMonInstance pcmi in pcMonInstances)
                {
                    header.Append("," + pcmi.Name.Replace(",", ""));
                }
                header.Append("\r\n");
                System.IO.File.WriteAllText(loggingOutputFilePath, header.ToString());
                loggingEnabled = true;
                startLoggingToolStripMenuItem.Text = "Stop logging";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CreateNewLoggingFileName()
        {
            while (System.IO.File.Exists(loggingOutputFilePath) && loggingOutputFileNewFileCounter < Properties.Settings.Default.LoggingCreateNewFileMaxCounter)
            {
                loggingOutputFileNewFileCounter++;
                loggingOutputFilePath = loggingOutputFilePathBase.Replace(".csv", "-" + loggingOutputFileNewFileCounter.ToString() + ".csv");
            }
        }
        private void LogToFile()
        {
            if (loggingEnabled)
            {
                try
                {
                    string loggingDecimalDigitsFormatting = "{0:F" + Properties.Settings.Default.LoggingDecimalDigits.ToString() + "}";
                    loggingSampleRateCounter++;
                    if (loggingSampleRateCounter > Properties.Settings.Default.LoggingSampleRate)
                    {
                        loggingSampleRateCounter = 1;

                        //Checking disk space
                        if (!loggingOutputFilePath.StartsWith("\\") && loggingOutputFilePath.Length > 0)
                        {
                            System.IO.DriveInfo di = new System.IO.DriveInfo(loggingOutputFilePath.Substring(0, 1));
                            long availableMB = (di.AvailableFreeSpace / 1048576);
                            if (availableMB < Properties.Settings.Default.LoggingMinimumDiskSpaceLimitMB)
                            {
                                StartStopLogging(true);
                                MessageBox.Show("Disk space running low! Logging stopped to avoid system crash.", "Low disk space", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        //if something or someone deletes the file... nasty of them!
                        if (!System.IO.File.Exists(loggingOutputFilePath))
                        {
                            CreateNewLoggingFile();
                        }
                        //Check if new file must be created
                        if (Properties.Settings.Default.LoggingCreateNewFileEveryMB > 0)
                        {
                            System.IO.FileInfo fi = new System.IO.FileInfo(loggingOutputFilePath);
                            if ((fi.Length / 1048576) >= Properties.Settings.Default.LoggingCreateNewFileEveryMB)
                            {
                                CreateNewLoggingFileName();
                                CreateNewLoggingFile();
                            }
                        }

                        StringBuilder lineTest = new StringBuilder();
                        lineTest.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        foreach (PCMonInstance pcmi in pcMonInstances)
                        {
                            lineTest.Append("," + string.Format(loggingDecimalDigitsFormatting, pcmi.LastValue));
                        }
                        lineTest.AppendLine();
                        System.IO.File.AppendAllText(loggingOutputFilePath, lineTest.ToString());
                    }
                }
                catch (Exception ex)
                {
                    StartStopLogging(true);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        } 
        #endregion               

        private void testAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewCounters("", "", "");
            //AddCounters addcounters = new AddCounters();
            //if (addcounters.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    StartStopLogging(true);
            //    bool oldPause = paused;
            //    paused = true;
            //    initializing = true;
            //    try
            //    {

            //        foreach (PCMonInstance pcmi in addcounters.SelectedPCMonInstances)
            //        {
            //            pcmi.PlotColor = lineColors[(lvwCounters.Items.Count + 1) % lineColors.Count];

            //            pcMonInstances.Add(pcmi);
            //            ListViewItem lvi = new ListViewItem(pcmi.Name);
            //            lvi.UseItemStyleForSubItems = false;
            //            ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem();
            //            sub.Text = "###";
            //            sub.ForeColor = pcmi.PlotColor;
            //            sub.BackColor = pcmi.PlotColor;
            //            lvi.SubItems.Add(sub);
            //            lvi.SubItems.Add(pcmi.Scale.ToString());
            //            lvi.SubItems.Add("");
            //            lvi.Checked = true;
            //            lvi.Tag = pcmi;
            //            lvwCounters.Items.Add(lvi);

            //            ILine line = lineFlowGraph2DControl.AddLine(pcmi.Name, pcmi.PlotColor, pcmi.Scale);

            //            line.Thickness = defaultLineThickness;
            //            line.PlotStyle = (LinePlotStyle)pcmi.PlotStyle;
            //            UpdateStatusBarText();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    initializing = false;
            //    paused = oldPause;
            //}
        }

        private void testAddCloneCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwCounters.SelectedItems.Count > 0)
            {
                PCMonInstance pcmi = (PCMonInstance)lvwCounters.SelectedItems[0].Tag;
                AddNewCounters(pcmi.Machine, pcmi.Category, "");
            }
            else
                AddNewCounters("", "", "");
        }

        private void AddNewCounters(string initialMachine, string initialCategory, string initialCounter)
        {
            AddCounters addcounters = new AddCounters();
            addcounters.InitialMachine = initialMachine;
            foreach (ListViewItem lvi in lvwCounters.Items)
            {
                addcounters.ExistingCounters.Add(lvi.Text);
            }
            if (initialCategory.Length > 0) 
                addcounters.InitialCategory = initialCategory;
            if (initialCounter.Length > 0)
                addcounters.InitialCounter = initialCounter;
            if (addcounters.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StartStopLogging(true);
                bool oldPause = paused;
                paused = true;
                initializing = true;
                try
                {

                    foreach (PCMonInstance pcmi in addcounters.SelectedPCMonInstances)
                    {
                        pcmi.PlotColor = GetNextLineColor();

                        pcMonInstances.Add(pcmi);
                        ListViewItem lvi = new ListViewItem(pcmi.Name);
                        lvi.UseItemStyleForSubItems = false;
                        ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem();
                        sub.Text = "###";
                        sub.ForeColor = pcmi.PlotColor;
                        sub.BackColor = pcmi.PlotColor;
                        lvi.SubItems.Add(sub);
                        lvi.SubItems.Add(pcmi.Scale.ToString());
                        lvi.SubItems.Add("");
                        lvi.Checked = true;
                        lvi.Tag = pcmi;
                        lvwCounters.Items.Add(lvi);

                        ILine line = lineFlowGraph2DControl.AddLine(pcmi.Name, pcmi.PlotColor, pcmi.Scale);

                        line.Thickness = defaultLineThickness;
                        line.PlotStyle = (LinePlotStyle)pcmi.PlotStyle;
                        UpdateStatusBarText();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                initializing = false;
                paused = oldPause;
            }
        }

        private void textAddCloneAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwCounters.SelectedItems.Count > 0)
            {
                PCMonInstance pcmi = (PCMonInstance)lvwCounters.SelectedItems[0].Tag;
                AddNewCounters(pcmi.Machine, pcmi.Category, pcmi.Counter);
            }
            else
                AddNewCounter("", "", "", "");
        }
        
        private Color GetNextLineColor()
        {
            Color nextColor = lineColors[(lvwCounters.Items.Count + 1) % lineColors.Count];
            if (lvwCounters.Items.Count > 0)
            {
                PCMonInstance lastItem = (PCMonInstance)lvwCounters.Items[lvwCounters.Items.Count - 1].Tag;
                if (nextColor.ToArgb() == lastItem.PlotColor.ToArgb())
                {
                    nextColor = lineColors[(lvwCounters.Items.Count + 2) % lineColors.Count];
                }
            }
            return nextColor;
        }

        private void lvwCounters_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                removeToolStripMenuItem_Click(sender, e);
            }
        }

        private void alwaysOnTopToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AlwaysOnTop = alwaysOnTopToolStripMenuItem.Checked;
            this.TopMost = alwaysOnTopToolStripMenuItem.Checked;
        }

        private void rememberSizePositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rememberSizePositionToolStripMenuItem.Checked = !rememberSizePositionToolStripMenuItem.Checked;
            Properties.Settings.Default.RememberSizeLocationOnSaveLoad = rememberSizePositionToolStripMenuItem.Checked;
        }






    }
}

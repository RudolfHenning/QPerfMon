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
            snapToDesktopSidesToolStripMenuItem.Checked = Properties.Settings.Default.MainWindowSnap;
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
                        if (lvi.Selected)
                        {
                            //c2DPushGraphControl.GetLineHandle(lvi.Text).Thickness = defaultLineThickness + 2;
                            lineFlowGraph2DControl.GetLine(lvi.Text).Thickness = defaultLineThickness + 2;
                        }
                        else
                        {
                            //c2DPushGraphControl.GetLineHandle(lvi.Text).Thickness = defaultLineThickness;
                            lineFlowGraph2DControl.GetLine(lvi.Text).Thickness = defaultLineThickness ;
                        }
                    }
                    //push only the first selected one to top
                    if (lvwCounters.SelectedItems.Count > 0)
                    {
                        lineFlowGraph2DControl.SetSelectedLine(lvwCounters.SelectedItems[0].Text);
                        
                        removeToolStripMenuItem.Enabled = (lvwCounters.Items.Count > 1) && (lvwCounters.Items.Count > lvwCounters.SelectedItems.Count);
                        moveSelectionToNewWindowToolStripMenuItem.Enabled = (lvwCounters.Items.Count > lvwCounters.SelectedItems.Count);
                    }
                    else
                    {
                        lineFlowGraph2DControl.SetSelectedLine("");
                        removeToolStripMenuItem.Enabled = false;
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
            LoadListView(false);
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
                                lvwCounters.BeginUpdate();
                                try
                                {
                                    int[] newvalues = new int[lvwCounters.Items.Count];
                                    for (int i = 0; i < pcMonInstances.Count; i++)
                                    {
                                        if (paused)
                                            break;
                                        PCMonInstance pcMonInstance = pcMonInstances[i];
                                        float pcValue = 0;
                                        try
                                        {
                                            //lazy load if needed
                                            if (pcMonInstance.PCInstance == null)
                                            {
                                                pcMonInstance.CreatePCInstance();
                                            }

                                            pcValue = (pcMonInstance.PCInstance.NextValue());
                                            if (lvwCounters.Items[i].ForeColor != SystemColors.WindowText)
                                                lvwCounters.Items[i].ForeColor = SystemColors.WindowText;
                                            pcMonInstance.LastError = "";

                                        }
                                        catch (Exception ex)
                                        {
                                            lvwCounters.Items[i].ForeColor = Color.Red;
                                            pcMonInstance.LastError = ex.Message;
                                            if (Properties.Settings.Default.DisableCounterOnError)
                                            {
                                                lvwCounters.Items[i].Checked = false;
                                                lvwCounters.Items[i].SubItems[3].Text = "Err";
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
                                            if (lvwCounters.Items[i].SubItems[3].Text != pcValueStr)
                                                lvwCounters.Items[i].SubItems[3].Text = pcValueStr;

                                            lineFlowGraph2DControl.Push(pcValue, pcMonInstance.Name);
                                        }

                                        //try
                                        //{
                                        //    //lazy load if needed
                                        //    if (pcMonInstance.PCInstance == null)
                                        //    {
                                        //        pcMonInstance.CreatePCInstance();
                                        //    }

                                        //    float pcValue = 0;
                                        //    try
                                        //    {
                                        //        pcValue = (pcMonInstance.PCInstance.NextValue());
                                        //        if (lvwCounters.Items[i].ForeColor != SystemColors.WindowText)
                                        //            lvwCounters.Items[i].ForeColor = SystemColors.WindowText;
                                        //        pcMonInstance.LastError = "";
                                        //    }
                                        //    catch(Exception ex)
                                        //    {
                                        //        lvwCounters.Items[i].ForeColor = Color.Red;
                                        //        pcMonInstance.LastError = ex.Message;
                                        //        if (Properties.Settings.Default.DisableCounterOnError)
                                        //        {
                                        //            lvwCounters.Items[i].Checked = false;
                                        //            lvwCounters.Items[i].SubItems[3].Text = "Err";
                                        //        }
                                        //    }
                                        //    pcMonInstance.LastValue = pcValue;
                                        //    string pcValueStr;
                                        //    if (pcValue > 999)
                                        //        pcValueStr = string.Format("{0:F1}", pcValue);
                                        //    else
                                        //        pcValueStr = string.Format("{0:F3}", pcValue);
                                        //    if (lvwCounters.Items[i].SubItems[3].Text != pcValueStr)
                                        //        lvwCounters.Items[i].SubItems[3].Text = pcValueStr;

                                        //    lineFlowGraph2DControl.Push(pcValue, pcMonInstance.Name); 

                                        //}
                                        //catch (Exception ex) //disable counter
                                        //{
                                        //    lineFlowGraph2DControl.Push(0, pcMonInstances[i].Name);
                                        //    lvwCounters.Items[i].ForeColor = Color.Red;
                                        //    lvwCounters.Items[i].Checked = false;
                                        //    lvwCounters.Items[i].SubItems[3].Text = "Err";
                                        //    pcMonInstance.LastError = ex.Message;
                                        //}
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
                //HenIT.Windows.Controls.C2DPushGraph.Graphing.C2DPushGraph.LineHandle lh = c2DPushGraphControl.GetLineHandle(lvwCounters.SelectedItems[0].Text);
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
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewCounter("", "", "", "");
        }
        private void addClonePerformanceCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwCounters.SelectedItems.Count > 0)
            {
                PCMonInstance pcmi = (PCMonInstance)lvwCounters.SelectedItems[0].Tag;
                AddNewCounter(pcmi.Machine, pcmi.Category, "", "");
            }
            else
                AddNewCounter("", "", "", "");
        }
        private void addCloneAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwCounters.SelectedItems.Count > 0)
            {
                PCMonInstance pcmi = (PCMonInstance)lvwCounters.SelectedItems[0].Tag;
                AddNewCounter(pcmi.Machine, pcmi.Category, pcmi.Counter, pcmi.Instance);
            }
            else
                AddNewCounter("", "", "", "");
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
                }
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
                    
                    foreach (PCMonInstance pcmi in pcMonInstances)
                    {
                        
                        string key = pcmi.KeyToXml();// .Name;
                        //if (pcmi.Scale < 1)
                        //    key += "\\" + pcmi.Scale.ToString("0.########");
                        //else
                        //    key += "\\" + pcmi.Scale.ToString("0");
                        //key += "\\" + pcmi.PlotStyle;
                        //key += "\\" + pcmi.PlotColor;
                        qPerfMonFile.CounterDefinitionList.Add(key);
                    }
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
                string key = pcmi.Name;
                if (pcmi.Scale < 1)
                    key += "\\" + pcmi.Scale.ToString("0.########");
                else
                    key += "\\" + pcmi.Scale.ToString("0");
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
                //C2DPushGraph.LineHandle m_LineHandle;
                for (int i = 0; i < pcMonInstances.Count; i++)
                {
                    int colorIndex = i % lineColors.Count;
                    if (pcMonInstances[i].LoadColorError)
                        pcMonInstances[i].PlotColor = lineColors[colorIndex];
                    line = lineFlowGraph2DControl.AddLine(pcMonInstances[i].Name, pcMonInstances[i].PlotColor, pcMonInstances[i].Scale);
                    line.PlotStyle = (LinePlotStyle)pcMonInstances[i].PlotStyle;
                    line.Thickness = defaultLineThickness;
                }
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

    }
}

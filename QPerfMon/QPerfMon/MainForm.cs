using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using HenIT.Windows.Controls.C2DPushGraph.Graphing;
using HenIT.Utilities;

namespace QPerfMon
{
    public partial class MainForm : Form
    {
        #region Private vars
        private List<PCMonInstance> pcMonInstances = new List<PCMonInstance>();
        private List<Color> lineColors = new List<Color>();
        private System.Threading.Timer timer;
        private System.Threading.TimerCallback timerCallback;
        private System.Threading.Mutex ctrlMutex;
        private int initialMaxValue = 100;
        private uint defaultLineThickness = 2;
        private string displayTitle = "";
        private bool initializing = false;
        private bool paused = false; 
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
            if (counters.Count == 0)
                counters.Add(@".\Processor\% Processor Time\_Total\1");

            ctrlMutex = new System.Threading.Mutex();
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

                lvwCounters.Items.Clear();
                foreach (string arg in counters)
                {
                    string[] parts = arg.Split('\\');
                    if (parts.Length < 4)
                    {
                        ShowCommandLineHelp("Invalid parameter (performance counter) specified!\r\n" + arg + "\r\n\r\n");
                    }
                    else
                    {
                        string key;
                        string scale = "1";
                        key = parts[0].Trim() + "\\" + parts[1].Trim() + "\\" + parts[2].Trim() + "\\";
                        if (parts[3].Trim().Length > 0)
                            key += parts[3].Trim();

                        PCMonInstance pcMonInstance = new PCMonInstance(key);
                        pcMonInstance.Machine = parts[0].Trim();
                        pcMonInstance.Category = parts[1].Trim();
                        pcMonInstance.Counter = parts[2].Trim();
                        pcMonInstance.Instance = parts[3].Trim().Length == 0 ? null : parts[3].Trim();
                        if (parts.Length > 4)
                        {
                            scale = parts[4];
                            pcMonInstance.Scale = double.Parse(parts[4]);
                        }
                        pcMonInstance.PCInstance = new PerformanceCounter(pcMonInstance.Category, pcMonInstance.Counter, pcMonInstance.Instance, pcMonInstance.Machine);
                        pcMonInstances.Add(pcMonInstance);

                        int colorIndex = lvwCounters.Items.Count % lineColors.Count;
                        ListViewItem lvi = new ListViewItem(key);
                        lvi.UseItemStyleForSubItems = false;
                        ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem();
                        sub.Text = "###";
                        sub.ForeColor = lineColors[colorIndex];
                        sub.BackColor = lineColors[colorIndex];
                        lvi.SubItems.Add(sub);
                        lvi.SubItems.Add(scale);
                        lvi.SubItems.Add("");
                        lvi.Checked = true;
                        lvi.Tag = pcMonInstance;
                        lvwCounters.Items.Add(lvi);
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
                "QPerfMon.exe <Machine\\Category\\Counter\\Instance\\[Scale]> [-max:X] [-lt:Y] [-title:Z]\r\n" +
                "Where\r\n" +
                "\tPerformance counter:\r\n" +
                "\t\tMachine: name of the machine\r\n" +
                "\t\tCategory: performance category\r\n" +
                "\t\tCounter: name of performance counter\r\n" +
                "\t\tInstance: instance of performance counter\r\n\t\t\t(blank for none)\r\n" +
                "\t\tScale: scale of performance counter\r\n\t\t\t(default=1)\r\n" +
                "\tmax: Initial maximum Y axis value of graph grid\r\n" +
                "\tlt: default line thickness (default=1)\r\n" +
                "\ttitle: give a title to the window\r\n" +
                "\r\nExample QPerfMon.exe \".\\Processor\\% Processor Time\\_Total\\1\" -max:100 -title:Test",
                "Command line parameters", MessageBoxButtons.OK, warning.Length == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Exclamation);
        } 
        #endregion

        #region Form events
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (displayTitle.Length > 0)
            {
                this.Text += " - " + displayTitle;
            }
            c2DPushGraphControl.MaxLabel = initialMaxValue.ToString();
            c2DPushGraphControl.MaxPeekMagnitude = initialMaxValue;
            c2DPushGraphControl.GridSize = (ushort)(c2DPushGraphControl.Height / 10);

            C2DPushGraph.LineHandle m_LineHandle;

            for (int i = 0; i < pcMonInstances.Count; i++)
            {
                int colorIndex = i % lineColors.Count;
                m_LineHandle = c2DPushGraphControl.AddLine(pcMonInstances[i].Name, lineColors[colorIndex], pcMonInstances[i].Scale);
                m_LineHandle.Thickness = defaultLineThickness;
            }

            timerCallback = new System.Threading.TimerCallback(onTimerTick);
            timer = new System.Threading.Timer(timerCallback, null, 0, 1000);
            lvwCounters_Resize(null, null);
            initializing = false;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }
        #endregion

        #region Listview events
        private void lvwCounters_Resize(object sender, EventArgs e)
        {
            lvwCounters.Columns[0].Width = lvwCounters.ClientSize.Width - lvwCounters.Columns[1].Width - lvwCounters.Columns[2].Width - lvwCounters.Columns[3].Width;
        }
        private void lvwCounters_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvwCounters.Items)
            {
                if (lvi.Selected)
                    c2DPushGraphControl.GetLineHandle(lvi.Text).Thickness = defaultLineThickness + 2;
                else
                    c2DPushGraphControl.GetLineHandle(lvi.Text).Thickness = defaultLineThickness;
            }
            //push only the first selected one to top
            if (lvwCounters.SelectedItems.Count > 0)
            {
                c2DPushGraphControl.SetSelectedLine(lvwCounters.SelectedItems[0].Text);
            }
            else
                c2DPushGraphControl.SetSelectedLine("");
        }
        private void lvwCounters_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!initializing)
            {
                HenIT.Windows.Controls.C2DPushGraph.Graphing.C2DPushGraph.LineHandle lh = c2DPushGraphControl.GetLineHandle(e.Item.Text);
                lh.Visible = e.Item.Checked;
                c2DPushGraphControl.UpdateGraph();
                if (lvwCounters.CheckedItems.Count == 0)
                    e.Item.Checked = true;
            }
        }

        #endregion

        #region Timer event
        private void onTimerTick(object obj)
        {
            if (!paused)
            {
                ctrlMutex.WaitOne();

                this.Invoke((MethodInvoker)delegate()
                    {
                        lvwCounters.BeginUpdate();
                        int[] newvalues = new int[lvwCounters.Items.Count];
                        for (int i = 0; i < pcMonInstances.Count; i++)
                        {
                            PCMonInstance pcMonInstance = pcMonInstances[i];
                            float pcValue = (pcMonInstance.PCInstance.NextValue());
                            string pcValueStr = pcValue.ToString("0.00");
                            if (lvwCounters.Items[i].SubItems[3].Text != pcValueStr)
                                lvwCounters.Items[i].SubItems[3].Text = pcValueStr;
                            c2DPushGraphControl.Push(pcValue, pcMonInstance.Name);
                        }

                        c2DPushGraphControl.UpdateGraph();
                        lvwCounters.EndUpdate();
                    });
                ctrlMutex.ReleaseMutex();
            }
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
                formatting.SelectedScale = double.Parse(lvwCounters.SelectedItems[0].SubItems[2].Text);
                formatting.SelectedColor = lvwCounters.SelectedItems[0].SubItems[1].ForeColor;
                if (formatting.ShowDialog() == DialogResult.OK)
                {
                    lvwCounters.SelectedItems[0].SubItems[2].Text = formatting.SelectedScale.ToString();
                    lvwCounters.SelectedItems[0].SubItems[1].ForeColor = formatting.SelectedColor;
                    lvwCounters.SelectedItems[0].SubItems[1].BackColor = formatting.SelectedColor;

                    HenIT.Windows.Controls.C2DPushGraph.Graphing.C2DPushGraph.LineHandle lh = c2DPushGraphControl.GetLineHandle(lvwCounters.SelectedItems[0].Text);
                    lh.Color = formatting.SelectedColor;
                    lh.Scale = formatting.SelectedScale;
                    PCMonInstance pcMonInstance = (PCMonInstance)lvwCounters.SelectedItems[0].Tag;
                    pcMonInstance.Scale = formatting.SelectedScale;
                }
            }
        }
        private void pauseToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            paused = pauseToolStripMenuItem.Checked;
        }
        #endregion
    }
}

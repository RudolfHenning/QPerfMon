using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace QPerfMon
{
    public partial class AddCounters : Form
    {
        public AddCounters()
        {
            InitializeComponent();
            InitialMachine = ".";
            InitialCategory = "Processor";
            InitialCounter = "% Processor Time";
            ExistingCounters = new List<string>();
        }

        public string InitialMachine { get; set; }
        public string InitialCategory { get; set; }
        public string InitialCounter { get; set; }
        public List<PCMonInstance> SelectedPCMonInstances { get; private set; }
        public List<string> ExistingCounters { get; set; }

        #region Form events
        private void AddCounters_Load(object sender, EventArgs e)
        {
            cboScale.Items.Add("1000000");
            cboScale.Items.Add("100000");
            cboScale.Items.Add("10000");
            cboScale.Items.Add("1000");
            cboScale.Items.Add("100");
            cboScale.Items.Add("10");
            cboScale.Items.Add("1");
            //To address different cultural formatting issues this has to be added in code.
            cboScale.Items.Add(String.Format("{0:F1}", 0.1));
            cboScale.Items.Add(String.Format("{0:F2}", 0.01));
            cboScale.Items.Add(String.Format("{0:F3}", 0.001));
            cboScale.Items.Add(String.Format("{0:F4}", 0.0001));
            cboScale.Items.Add(String.Format("{0:F5}", 0.00001));
            cboScale.Items.Add(String.Format("{0:F6}", 0.000001));
            cboScale.Items.Add(String.Format("{0:F7}", 0.0000001));
            cboScale.SelectedIndex = 6;
            if (InitialMachine == null || InitialMachine == "")
                InitialMachine = System.Environment.MachineName;
            txtComputer.Text = InitialMachine;
        }
        private void AddCounters_Shown(object sender, EventArgs e)
        {
            backgroundWorkerLoadCategories.RunWorkerAsync();
        }
        #endregion

        #region List view events
        private void lvwCategory_Resize(object sender, EventArgs e)
        {
            timerResize.Enabled = false;
            timerResize.Enabled = true;
        }
        private void lvwCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwCategory.SelectedItems.Count > 0)
            {
                try
                {
                    PerformanceCounter[] pcs;
                    List<ListViewItem> lvwInstances = new List<ListViewItem>();
                    List<ListViewItem> lvwCounters = new List<ListViewItem>();
                    lvwCounter.Items.Clear();
                    lvwInstance.Items.Clear();

                    PerformanceCounterCategory pcCat = new PerformanceCounterCategory(lvwCategory.SelectedItems[0].Text, txtComputer.Text);
                    string instanceName = "";
                    if (pcCat.CategoryType == PerformanceCounterCategoryType.MultiInstance)
                    {
                        string[] instances = pcCat.GetInstanceNames();
                        foreach (string instanceNameItem in (from s in instances
                                                             orderby s
                                                             select s))
                        {
                            lvwInstances.Add(new ListViewItem(instanceNameItem));
                        }
                        lvwInstance.Items.AddRange(lvwInstances.ToArray());
                        if (instances.Length > 0)
                            instanceName = pcCat.GetInstanceNames()[0];
                        pcs = pcCat.GetCounters(instanceName);
                    }
                    else
                        pcs = pcCat.GetCounters();
                    foreach (PerformanceCounter pc in (from p in pcs
                                                       orderby p.CounterName
                                                       select p))
                    {
                        lvwCounters.Add(new ListViewItem(pc.CounterName));
                    }
                    lvwCounter.Items.AddRange(lvwCounters.ToArray());
                    if (InitialCounter != null && InitialCounter.Length > 0)
                    {
                        for (int i = 0; i < lvwCounter.Items.Count; i++)
                        {
                            if (lvwCounter.Items[i].Text.ToLower() == InitialCounter.ToLower())
                            {
                                lvwCounter.Items[i].Selected = true;
                                lvwCounter.Items[i].EnsureVisible();
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            CheckForValidCounter();
        }
        private void lvwCounter_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckForValidCounter();
        }
        private void lvwInstance_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckForValidCounter();
        }
        #endregion

        #region Button events
        private void cmdLoadCategories_Click(object sender, EventArgs e)
        {
            backgroundWorkerLoadCategories.RunWorkerAsync();
        }
        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (CheckForValidCounter())
            {
                SelectedPCMonInstances = new List<PCMonInstance>();
                if (lvwInstance.Items.Count > 0)
                {
                    foreach (ListViewItem lviCounter in lvwCounter.SelectedItems)
                    {
                        foreach (ListViewItem lviInst in lvwInstance.SelectedItems)
                        {
                            PCMonInstance newPCMonInstance = new PCMonInstance();
                            newPCMonInstance.Machine = txtComputer.Text;
                            newPCMonInstance.Category = lvwCategory.SelectedItems[0].Text;
                            newPCMonInstance.Counter = lviCounter.Text;
                            newPCMonInstance.Instance = lviInst.Text;
                            newPCMonInstance.Scale = double.Parse(cboScale.SelectedItem.ToString());
                            newPCMonInstance.SetName();
                            SelectedPCMonInstances.Add(newPCMonInstance);
                        }
                    }
                }
                else
                {
                    foreach (ListViewItem lviCounter in lvwCounter.SelectedItems)
                    {
                        PCMonInstance newPCMonInstance = new PCMonInstance();
                        newPCMonInstance.Machine = txtComputer.Text;
                        newPCMonInstance.Category = lvwCategory.SelectedItems[0].Text;
                        newPCMonInstance.Counter = lviCounter.Text;
                        newPCMonInstance.Instance = "";
                        newPCMonInstance.Scale = double.Parse(cboScale.SelectedItem.ToString());
                        newPCMonInstance.SetName();
                        SelectedPCMonInstances.Add(newPCMonInstance);
                    }
                }
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }
        #endregion

        #region Timer and background events
        private void timerResize_Tick(object sender, EventArgs e)
        {
            timerResize.Enabled = false;
            columnHeaderCategory.Width = lvwCategory.ClientSize.Width;
            columnHeaderCounter.Width = lvwCounter.ClientSize.Width;
            columnHeaderInstance.Width = lvwInstance.ClientSize.Width;
        }
        private void backgroundWorkerLoadCategories_DoWork(object sender, DoWorkEventArgs e)
        {
            string machineName = txtComputer.Text;
            if (machineName == "" || machineName == ".")
                machineName = System.Environment.MachineName;
            this.Invoke((MethodInvoker)delegate
            {
                LoadCategories(machineName);
                CheckForValidCounter();
            });
        } 
        #endregion

        #region Private methods
        private void LoadCategories(string machineName)
        {
            Cursor.Current = Cursors.WaitCursor;
            lvwCategory.Items.Clear();
            try
            {
                List<ListViewItem> cats = new List<ListViewItem>();
                foreach (PerformanceCounterCategory performanceCounterCategory in (from c in PerformanceCounterCategory.GetCategories(machineName)
                                                                                   orderby c.CategoryName
                                                                                   select c))
                {
                    cats.Add(new ListViewItem(performanceCounterCategory.CategoryName));
                }
                lvwCategory.Items.AddRange(cats.ToArray());

                if (InitialCategory != null && InitialCategory.Length > 0)
                {
                    for (int i = 0; i < lvwCategory.Items.Count; i++)
                    {
                        if (InitialCategory.Length > 0 && lvwCategory.Items[i].Text.ToLower() == InitialCategory.ToLower())
                        {
                            lvwCategory.Items[i].Selected = true;
                            lvwCategory.Items[i].EnsureVisible();
                            break;
                        }
                    }
                }
                else
                    for (int i = 0; i < lvwCategory.Items.Count; i++)
                    {
                        if (lvwCategory.Items[i].Text == "Processor")
                        {
                            lvwCategory.Items[i].Selected = true;
                            lvwCategory.Items[i].EnsureVisible();
                            break;
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }
        private bool CheckForValidCounter()
        {
            bool result = true;
            lblWarning.Text = "";
            if (txtComputer.Text.Length == 0)
            {
                result = false;
                lblWarning.Text = "Specify computer!";
            }
            else if (lvwCategory.SelectedItems.Count == 0)
            {
                result = false;
                lblWarning.Text = "Specify category!";
            }
            else if (lvwCounter.SelectedItems.Count == 0)
            {
                result = false;
                lblWarning.Text = "Specify counter!";
            }
            else if (lvwInstance.Items.Count > 0 && lvwInstance.SelectedItems.Count == 0)
            {
                result = false;
                lblWarning.Text = "Specify instance!";
            }
            else
            {
                if (ExistingCounters.Count > 0)
                {
                    foreach (ListViewItem lviCounter in lvwCounter.SelectedItems)
                    {
                        string key = txtComputer.Text + "\\" + lvwCategory.SelectedItems[0].Text + "\\" + lviCounter.Text + "\\";
                        if (lvwInstance.Items.Count > 0)
                        {
                            foreach (ListViewItem lviInstance in lvwInstance.SelectedItems)
                            {
                                key = txtComputer.Text + "\\" + lvwCategory.SelectedItems[0].Text + "\\" + lviCounter.Text + "\\";
                                if (lviInstance.Text.Contains("\\"))
                                    key += "\"" + lviInstance.Text + "\"";
                                else
                                    key += lviInstance.Text;
                                if (ExistingCounters.Contains(key))
                                {
                                    result = false;
                                    lblWarning.Text = "Duplicate!";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (ExistingCounters.Contains(key))
                            {
                                result = false;
                                lblWarning.Text = "Duplicate!";
                            }
                        }
                        if (!result)
                            break;
                        //if (lvwInstance.Text.Length > 0)
                        //{
                        //    if (cboInstance.Text.Contains("\\"))
                        //        key += "\"" + cboInstance.Text + "\"";
                        //    else
                        //        key += cboInstance.Text;
                        //}
                        //if (ExistingCounters.Count > 0 && ExistingCounters.Contains(key))
                        //{
                        //    result = false;
                        //    lblWarning.Text = "Duplicate!";
                        //}
                    }
                }
            }
            cmdOK.Enabled = result;
            return result;
        }        
        #endregion
        
    }
}

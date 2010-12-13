﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace QPerfMon
{
    public partial class AddCounter : Form
    {
        public AddCounter()
        {
            InitializeComponent();
        }

        public PCMonInstance SelectedPCMonInstance { get; set; }
        public string InitialMachine { get; set; }
        public string InitialCategory { get; set; }
        public string InitialCounter { get; set; }
        public string InitialInstance { get; set; }
        public Color InitialColor { get; set; }

        #region Form events
        private void AddCounter_Load(object sender, EventArgs e)
        {
            txtComputer.Text = InitialMachine;
            if (txtComputer.Text.Length > 0)
            {
                backgroundWorkerLoadMachineDetails.RunWorkerAsync();
            }
            cboScale.SelectedIndex = 6;
            cboPlotStyle.SelectedIndex = 0;
            pictureBoxColor.BackColor = InitialColor;
        } 
        #endregion

        #region Button events
        private void cmdLoadCategories_Click(object sender, EventArgs e)
        {
            LoadCategories(txtComputer.Text);
        }
        private void cmdSelectColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = pictureBoxColor.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBoxColor.BackColor = colorDialog1.Color;
            }
        }
        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (txtComputer.Text.Length == 0)
            {
                MessageBox.Show("You must specify the computer!", "Computer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtComputer.Focus();
            }
            else if (cboCategory.SelectedIndex == -1)
            {
                MessageBox.Show("You must specify the category!", "Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboCategory.Focus();
            }
            else if (cboCounter.SelectedIndex == -1)
            {
                MessageBox.Show("You must specify the counter!", "Counter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboCounter.Focus();
            }
            else if (cboInstance.Items.Count > 0 && cboInstance.SelectedIndex == -1)
            {
                MessageBox.Show("You must specify the instance!", "Instance", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboInstance.Focus();
            }
            else if (cboScale.SelectedIndex == -1)
            {
                MessageBox.Show("You must specify the scale!", "Scale", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboScale.Focus();
            }
            else
            {
                try
                {
                    string key = txtComputer.Text + "\\" + cboCategory.Text + "\\" + cboCounter.Text + "\\";
                    if (cboInstance.Text.Length > 0)
                    {
                        if (cboInstance.Text.Contains("\\"))
                            key += "\"" + cboInstance.Text + "\"";
                        else
                            key += cboInstance.Text;
                    }
                    key += "\\" + cboScale.SelectedItem.ToString();
                    key += "\\" + cboPlotStyle.SelectedIndex.ToString();
                    SelectedPCMonInstance = new PCMonInstance(key);
                    SelectedPCMonInstance.Scale = double.Parse(cboScale.SelectedItem.ToString());
                    SelectedPCMonInstance.PlotStyle = cboPlotStyle.SelectedIndex;
                    InitialColor = pictureBoxColor.BackColor;
                    SelectedPCMonInstance.CreatePCInstance();

                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        } 
        #endregion

        #region Load data
        private void LoadCategories(string machineName)
        {
            cboCategory.Items.Clear();
            try
            {
                foreach (PerformanceCounterCategory performanceCounterCategory in PerformanceCounterCategory.GetCategories(machineName))
                {
                    cboCategory.Items.Add(performanceCounterCategory.CategoryName);
                }
                if (InitialCategory != null && InitialCategory.Length > 0)
                {
                    for (int i = 0; i < cboCategory.Items.Count; i++)
                    {
                        if (cboCategory.Items[i].ToString().ToLower() == InitialCategory.ToLower())
                        {
                            cboCategory.SelectedIndex = i;
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
        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.Text.Length > 0)
            {
                try
                {
                    PerformanceCounter[] pcs;
                    cboCounter.Items.Clear();
                    cboCounter.Text = "";
                    cboInstance.Items.Clear();
                    cboInstance.Text = "";

                    PerformanceCounterCategory pcCat = new PerformanceCounterCategory(cboCategory.Text, txtComputer.Text);
                    string instanceName = "";
                    if (pcCat.CategoryType == PerformanceCounterCategoryType.MultiInstance)
                    {
                        string[] instances = pcCat.GetInstanceNames();
                        foreach (string instanceNameItem in instances)
                        {
                            cboInstance.Items.Add(instanceNameItem);
                        }
                        if (instances.Length > 0)
                            instanceName = pcCat.GetInstanceNames()[0];
                        pcs = pcCat.GetCounters(instanceName);
                    }
                    else
                        pcs = pcCat.GetCounters();
                    foreach (PerformanceCounter pc in pcs)
                    {
                        cboCounter.Items.Add(pc.CounterName);
                    }
                    if (InitialCounter != null && InitialCounter.Length > 0)
                    {
                        for (int i = 0; i < cboCounter.Items.Count; i++)
                        {
                            if (cboCounter.Items[i].ToString().ToLower() == InitialCounter.ToLower())
                            {
                                cboCounter.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    if (InitialInstance != null && InitialInstance.Length > 0)
                    {
                        for (int i = 0; i < cboInstance.Items.Count; i++)
                        {
                            if (cboInstance.Items[i].ToString().ToLower() == InitialInstance.ToLower())
                            {
                                cboInstance.SelectedIndex = i;
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
        }
        private void txtComputer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadCategories(txtComputer.Text);
            }
        }
        private void backgroundWorkerLoadMachineDetails_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                System.Threading.Thread.Sleep(100);
                LoadCategories(txtComputer.Text);
            }
            );
        } 
        #endregion

    }
}

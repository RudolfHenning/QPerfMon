using System;
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



        private void cmdLoadCategories_Click(object sender, EventArgs e)
        {
            LoadCategories(txtComputer.Text);
        }

        private void LoadCategories(string machineName)
        {
            cboCategory.Items.Clear();
            try
            {
                foreach (PerformanceCounterCategory performanceCounterCategory in PerformanceCounterCategory.GetCategories(machineName))
                {
                    cboCategory.Items.Add(performanceCounterCategory.CategoryName);
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
            else
            {

                string key = txtComputer.Text + "\\" + cboCategory.Text + "\\" + cboCounter.Text + "\\";
                if (cboInstance.Text.Length > 0)
                    key += cboInstance.Text;
                SelectedPCMonInstance = new PCMonInstance(key);
                SelectedPCMonInstance.Machine = txtComputer.Text;
                SelectedPCMonInstance.Category = cboCategory.Text;
                SelectedPCMonInstance.Counter = cboCounter.Text;
                
                if (cboInstance.Text.Length > 0)
                    SelectedPCMonInstance.Instance = cboInstance.Text;

                SelectedPCMonInstance.Scale = 1;

                SelectedPCMonInstance.PCInstance = new PerformanceCounter(
                        SelectedPCMonInstance.Category, 
                        SelectedPCMonInstance.Counter,  
                        SelectedPCMonInstance.Instance, 
                        SelectedPCMonInstance.Machine);   

                DialogResult = DialogResult.OK;
                Close();
            }
        }        
    }
}

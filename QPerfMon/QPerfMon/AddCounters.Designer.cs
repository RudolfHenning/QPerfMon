namespace QPerfMon
{
    partial class AddCounters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCounters));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdLoadCategories = new System.Windows.Forms.Button();
            this.txtComputer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.timerResize = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorkerLoadCategories = new System.ComponentModel.BackgroundWorker();
            this.lblWarning = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboScale = new System.Windows.Forms.ComboBox();
            this.lvwCategory = new QPerfMon.ListViewR();
            this.columnHeaderCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwCounter = new QPerfMon.ListViewR();
            this.columnHeaderCounter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwInstance = new QPerfMon.ListViewR();
            this.columnHeaderInstance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(353, 345);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 6;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(272, 345);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 5;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdLoadCategories
            // 
            this.cmdLoadCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLoadCategories.Location = new System.Drawing.Point(368, 12);
            this.cmdLoadCategories.Name = "cmdLoadCategories";
            this.cmdLoadCategories.Size = new System.Drawing.Size(60, 23);
            this.cmdLoadCategories.TabIndex = 2;
            this.cmdLoadCategories.Text = "Load";
            this.cmdLoadCategories.UseVisualStyleBackColor = true;
            this.cmdLoadCategories.Click += new System.EventHandler(this.cmdLoadCategories_Click);
            // 
            // txtComputer
            // 
            this.txtComputer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComputer.Location = new System.Drawing.Point(70, 14);
            this.txtComputer.Name = "txtComputer";
            this.txtComputer.Size = new System.Drawing.Size(292, 20);
            this.txtComputer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Computer";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 40);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvwInstance);
            this.splitContainer1.Size = new System.Drawing.Size(416, 274);
            this.splitContainer1.SplitterDistance = 137;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lvwCategory);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lvwCounter);
            this.splitContainer2.Size = new System.Drawing.Size(416, 137);
            this.splitContainer2.SplitterDistance = 208;
            this.splitContainer2.TabIndex = 0;
            // 
            // timerResize
            // 
            this.timerResize.Tick += new System.EventHandler(this.timerResize_Tick);
            // 
            // backgroundWorkerLoadCategories
            // 
            this.backgroundWorkerLoadCategories.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerLoadCategories_DoWork);
            // 
            // lblWarning
            // 
            this.lblWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblWarning.AutoSize = true;
            this.lblWarning.ForeColor = System.Drawing.Color.Firebrick;
            this.lblWarning.Location = new System.Drawing.Point(14, 350);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(10, 13);
            this.lblWarning.TabIndex = 6;
            this.lblWarning.Text = ".";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 323);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Scale";
            // 
            // cboScale
            // 
            this.cboScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScale.FormattingEnabled = true;
            this.cboScale.Location = new System.Drawing.Point(52, 320);
            this.cboScale.Name = "cboScale";
            this.cboScale.Size = new System.Drawing.Size(376, 21);
            this.cboScale.TabIndex = 4;
            // 
            // lvwCategory
            // 
            this.lvwCategory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderCategory});
            this.lvwCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwCategory.FullRowSelect = true;
            this.lvwCategory.HideSelection = false;
            this.lvwCategory.Location = new System.Drawing.Point(0, 0);
            this.lvwCategory.MultiSelect = false;
            this.lvwCategory.Name = "lvwCategory";
            this.lvwCategory.Size = new System.Drawing.Size(208, 137);
            this.lvwCategory.TabIndex = 0;
            this.lvwCategory.UseCompatibleStateImageBehavior = false;
            this.lvwCategory.View = System.Windows.Forms.View.Details;
            this.lvwCategory.SelectedIndexChanged += new System.EventHandler(this.lvwCategory_SelectedIndexChanged);
            this.lvwCategory.Resize += new System.EventHandler(this.lvwCategory_Resize);
            // 
            // columnHeaderCategory
            // 
            this.columnHeaderCategory.Text = "Category";
            this.columnHeaderCategory.Width = 204;
            // 
            // lvwCounter
            // 
            this.lvwCounter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderCounter});
            this.lvwCounter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwCounter.FullRowSelect = true;
            this.lvwCounter.HideSelection = false;
            this.lvwCounter.Location = new System.Drawing.Point(0, 0);
            this.lvwCounter.Name = "lvwCounter";
            this.lvwCounter.Size = new System.Drawing.Size(204, 137);
            this.lvwCounter.TabIndex = 0;
            this.lvwCounter.UseCompatibleStateImageBehavior = false;
            this.lvwCounter.View = System.Windows.Forms.View.Details;
            this.lvwCounter.SelectedIndexChanged += new System.EventHandler(this.lvwCounter_SelectedIndexChanged);
            this.lvwCounter.Resize += new System.EventHandler(this.lvwCategory_Resize);
            // 
            // columnHeaderCounter
            // 
            this.columnHeaderCounter.Text = "Counter";
            this.columnHeaderCounter.Width = 200;
            // 
            // lvwInstance
            // 
            this.lvwInstance.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderInstance});
            this.lvwInstance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwInstance.FullRowSelect = true;
            this.lvwInstance.HideSelection = false;
            this.lvwInstance.Location = new System.Drawing.Point(0, 0);
            this.lvwInstance.Name = "lvwInstance";
            this.lvwInstance.Size = new System.Drawing.Size(416, 133);
            this.lvwInstance.TabIndex = 0;
            this.lvwInstance.UseCompatibleStateImageBehavior = false;
            this.lvwInstance.View = System.Windows.Forms.View.Details;
            this.lvwInstance.SelectedIndexChanged += new System.EventHandler(this.lvwInstance_SelectedIndexChanged);
            this.lvwInstance.Resize += new System.EventHandler(this.lvwCategory_Resize);
            // 
            // columnHeaderInstance
            // 
            this.columnHeaderInstance.Text = "Instance";
            this.columnHeaderInstance.Width = 412;
            // 
            // AddCounters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 380);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboScale);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cmdLoadCategories);
            this.Controls.Add(this.txtComputer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddCounters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddCounters";
            this.Load += new System.EventHandler(this.AddCounters_Load);
            this.Shown += new System.EventHandler(this.AddCounters_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdLoadCategories;
        private System.Windows.Forms.TextBox txtComputer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ListViewR lvwCategory;
        private System.Windows.Forms.ColumnHeader columnHeaderCategory;
        private ListViewR lvwCounter;
        private System.Windows.Forms.ColumnHeader columnHeaderCounter;
        private ListViewR lvwInstance;
        private System.Windows.Forms.ColumnHeader columnHeaderInstance;
        private System.Windows.Forms.Timer timerResize;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoadCategories;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboScale;
    }
}
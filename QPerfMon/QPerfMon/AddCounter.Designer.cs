namespace QPerfMon
{
    partial class AddCounter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCounter));
            this.label1 = new System.Windows.Forms.Label();
            this.txtComputer = new System.Windows.Forms.TextBox();
            this.cmdLoadCategories = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.cboCounter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboInstance = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.backgroundWorkerLoadMachineDetails = new System.ComponentModel.BackgroundWorker();
            this.cboScale = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboPlotStyle = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdSelectColor = new System.Windows.Forms.Button();
            this.pictureBoxColor = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.lblWarning = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColor)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Computer";
            // 
            // txtComputer
            // 
            this.txtComputer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComputer.Location = new System.Drawing.Point(99, 10);
            this.txtComputer.Name = "txtComputer";
            this.txtComputer.Size = new System.Drawing.Size(200, 20);
            this.txtComputer.TabIndex = 1;
            this.txtComputer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtComputer_KeyUp);
            // 
            // cmdLoadCategories
            // 
            this.cmdLoadCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLoadCategories.Location = new System.Drawing.Point(305, 8);
            this.cmdLoadCategories.Name = "cmdLoadCategories";
            this.cmdLoadCategories.Size = new System.Drawing.Size(60, 23);
            this.cmdLoadCategories.TabIndex = 2;
            this.cmdLoadCategories.Text = "Load";
            this.cmdLoadCategories.UseVisualStyleBackColor = true;
            this.cmdLoadCategories.Click += new System.EventHandler(this.cmdLoadCategories_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Category";
            // 
            // cboCategory
            // 
            this.cboCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(99, 47);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(265, 21);
            this.cboCategory.Sorted = true;
            this.cboCategory.TabIndex = 4;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.cboCategory_SelectedIndexChanged);
            // 
            // cboCounter
            // 
            this.cboCounter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCounter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCounter.FormattingEnabled = true;
            this.cboCounter.Location = new System.Drawing.Point(99, 74);
            this.cboCounter.Name = "cboCounter";
            this.cboCounter.Size = new System.Drawing.Size(265, 21);
            this.cboCounter.Sorted = true;
            this.cboCounter.TabIndex = 6;
            this.cboCounter.SelectedIndexChanged += new System.EventHandler(this.cboCounter_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Counter";
            // 
            // cboInstance
            // 
            this.cboInstance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboInstance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInstance.FormattingEnabled = true;
            this.cboInstance.Location = new System.Drawing.Point(99, 101);
            this.cboInstance.Name = "cboInstance";
            this.cboInstance.Size = new System.Drawing.Size(265, 21);
            this.cboInstance.Sorted = true;
            this.cboInstance.TabIndex = 8;
            this.cboInstance.SelectedIndexChanged += new System.EventHandler(this.cboInstance_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Instance";
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(209, 249);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 11;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(290, 249);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // backgroundWorkerLoadMachineDetails
            // 
            this.backgroundWorkerLoadMachineDetails.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerLoadMachineDetails_DoWork);
            // 
            // cboScale
            // 
            this.cboScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScale.FormattingEnabled = true;
            this.cboScale.Location = new System.Drawing.Point(102, 52);
            this.cboScale.Name = "cboScale";
            this.cboScale.Size = new System.Drawing.Size(245, 21);
            this.cboScale.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Scale";
            // 
            // cboPlotStyle
            // 
            this.cboPlotStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlotStyle.FormattingEnabled = true;
            this.cboPlotStyle.Items.AddRange(new object[] {
            "None",
            "Dot (o)",
            "Cross (+)",
            "Ex (X)",
            "Box(□)",
            "Triangle(∆)"});
            this.cboPlotStyle.Location = new System.Drawing.Point(102, 79);
            this.cboPlotStyle.Name = "cboPlotStyle";
            this.cboPlotStyle.Size = new System.Drawing.Size(245, 21);
            this.cboPlotStyle.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Point style";
            // 
            // cmdSelectColor
            // 
            this.cmdSelectColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelectColor.Location = new System.Drawing.Point(310, 23);
            this.cmdSelectColor.Name = "cmdSelectColor";
            this.cmdSelectColor.Size = new System.Drawing.Size(37, 23);
            this.cmdSelectColor.TabIndex = 1;
            this.cmdSelectColor.Text = "- - -";
            this.cmdSelectColor.UseVisualStyleBackColor = true;
            this.cmdSelectColor.Click += new System.EventHandler(this.cmdSelectColor_Click);
            // 
            // pictureBoxColor
            // 
            this.pictureBoxColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxColor.Location = new System.Drawing.Point(101, 25);
            this.pictureBoxColor.Name = "pictureBoxColor";
            this.pictureBoxColor.Size = new System.Drawing.Size(203, 21);
            this.pictureBoxColor.TabIndex = 17;
            this.pictureBoxColor.TabStop = false;
            this.pictureBoxColor.Click += new System.EventHandler(this.cmdSelectColor_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Color";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmdSelectColor);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pictureBoxColor);
            this.groupBox1.Controls.Add(this.cboScale);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cboPlotStyle);
            this.groupBox1.Location = new System.Drawing.Point(12, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 112);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Formatting";
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Location = new System.Drawing.Point(13, 254);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(10, 13);
            this.lblWarning.TabIndex = 12;
            this.lblWarning.Text = ".";
            // 
            // AddCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(376, 284);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cboInstance);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboCounter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdLoadCategories);
            this.Controls.Add(this.txtComputer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCounter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Performance Counter";
            this.Load += new System.EventHandler(this.AddCounter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtComputer;
        private System.Windows.Forms.Button cmdLoadCategories;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.ComboBox cboCounter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboInstance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoadMachineDetails;
        private System.Windows.Forms.ComboBox cboScale;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboPlotStyle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdSelectColor;
        private System.Windows.Forms.PictureBox pictureBoxColor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label lblWarning;
    }
}
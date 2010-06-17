namespace QPerfMon
{
    partial class LogToFileOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogToFileOptions));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLoggingDirectory = new System.Windows.Forms.TextBox();
            this.cmdSelectColor = new System.Windows.Forms.Button();
            this.chkLoggingAppendDateTime = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLoggingFileName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(326, 127);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(245, 127);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 6;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Output directory";
            // 
            // txtLoggingDirectory
            // 
            this.txtLoggingDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoggingDirectory.Location = new System.Drawing.Point(37, 29);
            this.txtLoggingDirectory.Name = "txtLoggingDirectory";
            this.txtLoggingDirectory.Size = new System.Drawing.Size(321, 20);
            this.txtLoggingDirectory.TabIndex = 1;
            // 
            // cmdSelectColor
            // 
            this.cmdSelectColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelectColor.Location = new System.Drawing.Point(364, 27);
            this.cmdSelectColor.Name = "cmdSelectColor";
            this.cmdSelectColor.Size = new System.Drawing.Size(37, 23);
            this.cmdSelectColor.TabIndex = 2;
            this.cmdSelectColor.Text = "- - -";
            this.cmdSelectColor.UseVisualStyleBackColor = true;
            this.cmdSelectColor.Click += new System.EventHandler(this.cmdSelectColor_Click);
            // 
            // chkLoggingAppendDateTime
            // 
            this.chkLoggingAppendDateTime.AutoSize = true;
            this.chkLoggingAppendDateTime.Location = new System.Drawing.Point(15, 96);
            this.chkLoggingAppendDateTime.Name = "chkLoggingAppendDateTime";
            this.chkLoggingAppendDateTime.Size = new System.Drawing.Size(122, 19);
            this.chkLoggingAppendDateTime.TabIndex = 5;
            this.chkLoggingAppendDateTime.Text = "Append date/time";
            this.chkLoggingAppendDateTime.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(384, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "File name (do not specify extension - .csv will be added automatically)";
            // 
            // txtLoggingFileName
            // 
            this.txtLoggingFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoggingFileName.Location = new System.Drawing.Point(37, 70);
            this.txtLoggingFileName.Name = "txtLoggingFileName";
            this.txtLoggingFileName.Size = new System.Drawing.Size(321, 20);
            this.txtLoggingFileName.TabIndex = 4;
            // 
            // LogToFileOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 162);
            this.Controls.Add(this.txtLoggingFileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkLoggingAppendDateTime);
            this.Controls.Add(this.cmdSelectColor);
            this.Controls.Add(this.txtLoggingDirectory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogToFileOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logging to file options";
            this.Load += new System.EventHandler(this.LogToFileOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLoggingDirectory;
        private System.Windows.Forms.Button cmdSelectColor;
        private System.Windows.Forms.CheckBox chkLoggingAppendDateTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLoggingFileName;
    }
}
namespace QPerfMon
{
    partial class GraphFormatting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphFormatting));
            this.cmdSelectBackgroundColor = new System.Windows.Forms.Button();
            this.picBackgroundColor = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdSelectGridColor = new System.Windows.Forms.Button();
            this.picGridColor = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdSelectLabelFont = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdSelectLabelForeColor = new System.Windows.Forms.Button();
            this.picLabelForeColor = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFont = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picBackgroundColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGridColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLabelForeColor)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSelectBackgroundColor
            // 
            this.cmdSelectBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelectBackgroundColor.Location = new System.Drawing.Point(301, 10);
            this.cmdSelectBackgroundColor.Name = "cmdSelectBackgroundColor";
            this.cmdSelectBackgroundColor.Size = new System.Drawing.Size(37, 23);
            this.cmdSelectBackgroundColor.TabIndex = 1;
            this.cmdSelectBackgroundColor.Text = "- - -";
            this.cmdSelectBackgroundColor.UseVisualStyleBackColor = true;
            this.cmdSelectBackgroundColor.Click += new System.EventHandler(this.cmdSelectBackgroundColor_Click);
            // 
            // picBackgroundColor
            // 
            this.picBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBackgroundColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBackgroundColor.Location = new System.Drawing.Point(166, 12);
            this.picBackgroundColor.Name = "picBackgroundColor";
            this.picBackgroundColor.Size = new System.Drawing.Size(129, 21);
            this.picBackgroundColor.TabIndex = 9;
            this.picBackgroundColor.TabStop = false;
            this.picBackgroundColor.Click += new System.EventHandler(this.cmdSelectBackgroundColor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Graph background color";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(263, 143);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 10;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(182, 143);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 9;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdSelectGridColor
            // 
            this.cmdSelectGridColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelectGridColor.Location = new System.Drawing.Point(301, 39);
            this.cmdSelectGridColor.Name = "cmdSelectGridColor";
            this.cmdSelectGridColor.Size = new System.Drawing.Size(37, 23);
            this.cmdSelectGridColor.TabIndex = 3;
            this.cmdSelectGridColor.Text = "- - -";
            this.cmdSelectGridColor.UseVisualStyleBackColor = true;
            this.cmdSelectGridColor.Click += new System.EventHandler(this.cmdSelectGridColor_Click);
            // 
            // picGridColor
            // 
            this.picGridColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picGridColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picGridColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picGridColor.Location = new System.Drawing.Point(166, 41);
            this.picGridColor.Name = "picGridColor";
            this.picGridColor.Size = new System.Drawing.Size(129, 21);
            this.picGridColor.TabIndex = 13;
            this.picGridColor.TabStop = false;
            this.picGridColor.Click += new System.EventHandler(this.cmdSelectGridColor_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Grid color";
            // 
            // cmdSelectLabelFont
            // 
            this.cmdSelectLabelFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelectLabelFont.Location = new System.Drawing.Point(301, 97);
            this.cmdSelectLabelFont.Name = "cmdSelectLabelFont";
            this.cmdSelectLabelFont.Size = new System.Drawing.Size(37, 23);
            this.cmdSelectLabelFont.TabIndex = 8;
            this.cmdSelectLabelFont.Text = "- - -";
            this.cmdSelectLabelFont.UseVisualStyleBackColor = true;
            this.cmdSelectLabelFont.Click += new System.EventHandler(this.cmdSelectLabelFont_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Label font";
            // 
            // cmdSelectLabelForeColor
            // 
            this.cmdSelectLabelForeColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelectLabelForeColor.Location = new System.Drawing.Point(301, 68);
            this.cmdSelectLabelForeColor.Name = "cmdSelectLabelForeColor";
            this.cmdSelectLabelForeColor.Size = new System.Drawing.Size(37, 23);
            this.cmdSelectLabelForeColor.TabIndex = 5;
            this.cmdSelectLabelForeColor.Text = "- - -";
            this.cmdSelectLabelForeColor.UseVisualStyleBackColor = true;
            this.cmdSelectLabelForeColor.Click += new System.EventHandler(this.cmdSelectLabelForeColor_Click);
            // 
            // picLabelForeColor
            // 
            this.picLabelForeColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picLabelForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLabelForeColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLabelForeColor.Location = new System.Drawing.Point(166, 70);
            this.picLabelForeColor.Name = "picLabelForeColor";
            this.picLabelForeColor.Size = new System.Drawing.Size(129, 21);
            this.picLabelForeColor.TabIndex = 19;
            this.picLabelForeColor.TabStop = false;
            this.picLabelForeColor.Click += new System.EventHandler(this.cmdSelectLabelForeColor_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Label forecolor";
            // 
            // txtFont
            // 
            this.txtFont.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtFont.Location = new System.Drawing.Point(166, 99);
            this.txtFont.Name = "txtFont";
            this.txtFont.ReadOnly = true;
            this.txtFont.Size = new System.Drawing.Size(129, 20);
            this.txtFont.TabIndex = 7;
            this.txtFont.Click += new System.EventHandler(this.cmdSelectLabelFont_Click);
            // 
            // GraphFormatting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 178);
            this.Controls.Add(this.txtFont);
            this.Controls.Add(this.cmdSelectLabelForeColor);
            this.Controls.Add(this.picLabelForeColor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdSelectLabelFont);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdSelectGridColor);
            this.Controls.Add(this.picGridColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdSelectBackgroundColor);
            this.Controls.Add(this.picBackgroundColor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GraphFormatting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Graph formatting options";
            this.Load += new System.EventHandler(this.GraphFormatting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBackgroundColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGridColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLabelForeColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSelectBackgroundColor;
        private System.Windows.Forms.PictureBox picBackgroundColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdSelectGridColor;
        private System.Windows.Forms.PictureBox picGridColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdSelectLabelFont;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdSelectLabelForeColor;
        private System.Windows.Forms.PictureBox picLabelForeColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFont;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
    }
}
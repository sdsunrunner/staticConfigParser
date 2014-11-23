namespace BinData
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BinData = new System.Windows.Forms.Button();
            this.OutPut = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BinData
            // 
            this.BinData.Location = new System.Drawing.Point(35, 12);
            this.BinData.Name = "BinData";
            this.BinData.Size = new System.Drawing.Size(89, 35);
            this.BinData.TabIndex = 0;
            this.BinData.Text = "BinData";
            this.BinData.UseVisualStyleBackColor = true;
            this.BinData.Click += new System.EventHandler(this.BinData_Click);
            // 
            // OutPut
            // 
            this.OutPut.BackColor = System.Drawing.SystemColors.Window;
            this.OutPut.Location = new System.Drawing.Point(35, 53);
            this.OutPut.Multiline = true;
            this.OutPut.Name = "OutPut";
            this.OutPut.ReadOnly = true;
            this.OutPut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutPut.Size = new System.Drawing.Size(393, 549);
            this.OutPut.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 614);
            this.Controls.Add(this.OutPut);
            this.Controls.Add(this.BinData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BinData;
        private System.Windows.Forms.TextBox OutPut;
    }
}


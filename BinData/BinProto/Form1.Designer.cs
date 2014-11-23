namespace BinProto
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
            this.BinProto = new System.Windows.Forms.Button();
            this.OutPut = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BinProto
            // 
            this.BinProto.Location = new System.Drawing.Point(28, 28);
            this.BinProto.Name = "BinProto";
            this.BinProto.Size = new System.Drawing.Size(101, 31);
            this.BinProto.TabIndex = 0;
            this.BinProto.Text = "BinProto";
            this.BinProto.UseVisualStyleBackColor = true;
            this.BinProto.Click += new System.EventHandler(this.BinProto_Click);
            // 
            // OutPut
            // 
            this.OutPut.BackColor = System.Drawing.Color.White;
            this.OutPut.ForeColor = System.Drawing.SystemColors.WindowText;
            this.OutPut.Location = new System.Drawing.Point(28, 65);
            this.OutPut.Multiline = true;
            this.OutPut.Name = "OutPut";
            this.OutPut.ReadOnly = true;
            this.OutPut.Size = new System.Drawing.Size(356, 514);
            this.OutPut.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 591);
            this.Controls.Add(this.OutPut);
            this.Controls.Add(this.BinProto);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BinProto;
        private System.Windows.Forms.TextBox OutPut;
    }
}


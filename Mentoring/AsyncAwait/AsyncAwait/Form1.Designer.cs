namespace AsyncAwait
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
            this.addNewCtrlBtn = new System.Windows.Forms.Button();
            this.mainPnl = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // addNewCtrlBtn
            // 
            this.addNewCtrlBtn.Location = new System.Drawing.Point(12, 639);
            this.addNewCtrlBtn.Name = "addNewCtrlBtn";
            this.addNewCtrlBtn.Size = new System.Drawing.Size(75, 23);
            this.addNewCtrlBtn.TabIndex = 5;
            this.addNewCtrlBtn.Text = "Add";
            this.addNewCtrlBtn.UseVisualStyleBackColor = true;
            this.addNewCtrlBtn.Click += new System.EventHandler(this.addNewCtrlBtn_Click);
            // 
            // mainPnl
            // 
            this.mainPnl.AutoScroll = true;
            this.mainPnl.BackColor = System.Drawing.SystemColors.ControlDark;
            this.mainPnl.Location = new System.Drawing.Point(12, 12);
            this.mainPnl.Name = "mainPnl";
            this.mainPnl.Size = new System.Drawing.Size(1097, 621);
            this.mainPnl.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 674);
            this.Controls.Add(this.mainPnl);
            this.Controls.Add(this.addNewCtrlBtn);
            this.Name = "Form1";
            this.Text = "AsyncAwaitTask";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addNewCtrlBtn;
        private System.Windows.Forms.Panel mainPnl;
    }
}


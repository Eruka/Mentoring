namespace NotePadWinForms
{
    partial class NotePad
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
            this.SourseTextBox = new System.Windows.Forms.RichTextBox();
            this.ResultTextBox = new System.Windows.Forms.RichTextBox();
            this.TransformBtn = new System.Windows.Forms.Button();
            this.PluginsCmbBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // SourseTextBox
            // 
            this.SourseTextBox.Location = new System.Drawing.Point(21, 21);
            this.SourseTextBox.Name = "SourseTextBox";
            this.SourseTextBox.Size = new System.Drawing.Size(241, 60);
            this.SourseTextBox.TabIndex = 0;
            this.SourseTextBox.Text = "";
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.Location = new System.Drawing.Point(21, 128);
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.Size = new System.Drawing.Size(241, 60);
            this.ResultTextBox.TabIndex = 1;
            this.ResultTextBox.Text = "";
            // 
            // TransformBtn
            // 
            this.TransformBtn.Location = new System.Drawing.Point(21, 87);
            this.TransformBtn.Name = "TransformBtn";
            this.TransformBtn.Size = new System.Drawing.Size(75, 23);
            this.TransformBtn.TabIndex = 2;
            this.TransformBtn.Text = "Transform";
            this.TransformBtn.UseVisualStyleBackColor = true;
            this.TransformBtn.Click += new System.EventHandler(this.TransformBtn_Click);
            // 
            // PluginsCmbBox
            // 
            this.PluginsCmbBox.FormattingEnabled = true;
            this.PluginsCmbBox.Location = new System.Drawing.Point(286, 23);
            this.PluginsCmbBox.Name = "PluginsCmbBox";
            this.PluginsCmbBox.Size = new System.Drawing.Size(176, 21);
            this.PluginsCmbBox.TabIndex = 3;
            this.PluginsCmbBox.SelectedValueChanged += new System.EventHandler(this.PluginsCmbBox_SelectedValueChanged);
            // 
            // NotePad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 439);
            this.Controls.Add(this.PluginsCmbBox);
            this.Controls.Add(this.TransformBtn);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.SourseTextBox);
            this.Name = "NotePad";
            this.Text = "NotePad";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox SourseTextBox;
        private System.Windows.Forms.RichTextBox ResultTextBox;
        private System.Windows.Forms.Button TransformBtn;
        private System.Windows.Forms.ComboBox PluginsCmbBox;
    }
}


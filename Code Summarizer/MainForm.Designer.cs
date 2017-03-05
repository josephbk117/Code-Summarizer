namespace Code_Summarizer
{
    partial class MainForm
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
            this.folderPathTextBox = new System.Windows.Forms.TextBox();
            this.OpenFolderPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // folderPathTextBox
            // 
            this.folderPathTextBox.Location = new System.Drawing.Point(13, 13);
            this.folderPathTextBox.Name = "folderPathTextBox";
            this.folderPathTextBox.Size = new System.Drawing.Size(384, 20);
            this.folderPathTextBox.TabIndex = 0;
            // 
            // OpenFolderPanel
            // 
            this.OpenFolderPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.OpenFolderPanel.Location = new System.Drawing.Point(404, 13);
            this.OpenFolderPanel.Name = "OpenFolderPanel";
            this.OpenFolderPanel.Size = new System.Drawing.Size(76, 20);
            this.OpenFolderPanel.TabIndex = 1;
            this.OpenFolderPanel.Click += new System.EventHandler(this.OpenFolderPanel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 292);
            this.Controls.Add(this.OpenFolderPanel);
            this.Controls.Add(this.folderPathTextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox folderPathTextBox;
        private System.Windows.Forms.Panel OpenFolderPanel;
    }
}


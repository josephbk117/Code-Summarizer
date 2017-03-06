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
            this.titleLabel = new System.Windows.Forms.Label();
            this.closePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // folderPathTextBox
            // 
            this.folderPathTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.folderPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.folderPathTextBox.ForeColor = System.Drawing.Color.White;
            this.folderPathTextBox.Location = new System.Drawing.Point(55, 80);
            this.folderPathTextBox.Name = "folderPathTextBox";
            this.folderPathTextBox.Size = new System.Drawing.Size(417, 13);
            this.folderPathTextBox.TabIndex = 0;
            // 
            // OpenFolderPanel
            // 
            this.OpenFolderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.OpenFolderPanel.Location = new System.Drawing.Point(478, 69);
            this.OpenFolderPanel.Name = "OpenFolderPanel";
            this.OpenFolderPanel.Size = new System.Drawing.Size(118, 36);
            this.OpenFolderPanel.TabIndex = 1;
            this.OpenFolderPanel.Click += new System.EventHandler(this.OpenFolderPanel_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(250, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(136, 21);
            this.titleLabel.TabIndex = 2;
            this.titleLabel.Text = "Code Summarizer";
            // 
            // closePanel
            // 
            this.closePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.closePanel.Location = new System.Drawing.Point(598, 2);
            this.closePanel.Name = "closePanel";
            this.closePanel.Size = new System.Drawing.Size(41, 37);
            this.closePanel.TabIndex = 3;
            this.closePanel.Click += new System.EventHandler(this.ClosePanel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Code_Summarizer.Properties.Resources.GUIScriptSummary7;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(640, 400);
            this.Controls.Add(this.closePanel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.OpenFolderPanel);
            this.Controls.Add(this.folderPathTextBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox folderPathTextBox;
        private System.Windows.Forms.Panel OpenFolderPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel closePanel;
    }
}


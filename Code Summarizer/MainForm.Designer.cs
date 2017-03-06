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
            this.closePanel = new System.Windows.Forms.Panel();
            this.dragPanel = new System.Windows.Forms.Panel();
            this.outputFolderPathTextBox = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.dragPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderPathTextBox
            // 
            this.folderPathTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.folderPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.folderPathTextBox.Font = new System.Drawing.Font("Roboto Condensed", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.folderPathTextBox.ForeColor = System.Drawing.Color.White;
            this.folderPathTextBox.Location = new System.Drawing.Point(55, 78);
            this.folderPathTextBox.Name = "folderPathTextBox";
            this.folderPathTextBox.Size = new System.Drawing.Size(417, 19);
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
            // closePanel
            // 
            this.closePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.closePanel.Location = new System.Drawing.Point(598, 2);
            this.closePanel.Name = "closePanel";
            this.closePanel.Size = new System.Drawing.Size(39, 33);
            this.closePanel.TabIndex = 3;
            this.closePanel.Click += new System.EventHandler(this.ClosePanel_Click);
            this.closePanel.MouseEnter += new System.EventHandler(this.ClosePanel_MouseEnter);
            this.closePanel.MouseLeave += new System.EventHandler(this.ClosePanel_MouseLeave);
            // 
            // dragPanel
            // 
            this.dragPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dragPanel.Controls.Add(this.titleLabel);
            this.dragPanel.Location = new System.Drawing.Point(41, 0);
            this.dragPanel.Name = "dragPanel";
            this.dragPanel.Size = new System.Drawing.Size(554, 35);
            this.dragPanel.TabIndex = 4;
            this.dragPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragPanel_MouseDown);
            this.dragPanel.MouseEnter += new System.EventHandler(this.dragPanel_MouseEnter);
            this.dragPanel.MouseLeave += new System.EventHandler(this.dragPanel_MouseLeave);
            this.dragPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragPanel_MouseMove);
            // 
            // outputFolderPathTextBox
            // 
            this.outputFolderPathTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.outputFolderPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputFolderPathTextBox.Font = new System.Drawing.Font("Roboto Condensed", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputFolderPathTextBox.ForeColor = System.Drawing.Color.White;
            this.outputFolderPathTextBox.Location = new System.Drawing.Point(55, 128);
            this.outputFolderPathTextBox.Name = "outputFolderPathTextBox";
            this.outputFolderPathTextBox.Size = new System.Drawing.Size(417, 19);
            this.outputFolderPathTextBox.TabIndex = 5;
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Orbitron", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(197, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(162, 19);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Code Summarizer";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Code_Summarizer.Properties.Resources.GUIScriptSummary8;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(640, 400);
            this.Controls.Add(this.outputFolderPathTextBox);
            this.Controls.Add(this.dragPanel);
            this.Controls.Add(this.closePanel);
            this.Controls.Add(this.OpenFolderPanel);
            this.Controls.Add(this.folderPathTextBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(50, 50);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dragPanel.ResumeLayout(false);
            this.dragPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox folderPathTextBox;
        private System.Windows.Forms.Panel OpenFolderPanel;
        private System.Windows.Forms.Panel closePanel;
        private System.Windows.Forms.Panel dragPanel;
        private System.Windows.Forms.TextBox outputFolderPathTextBox;
        private System.Windows.Forms.Label titleLabel;
    }
}


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
            this.openFolderPathTextBox = new System.Windows.Forms.TextBox();
            this.outputFolderPathTextBox = new System.Windows.Forms.TextBox();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.outputFolderButton = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.summaryGenerateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFolderPathTextBox
            // 
            this.openFolderPathTextBox.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openFolderPathTextBox.Location = new System.Drawing.Point(12, 34);
            this.openFolderPathTextBox.Name = "openFolderPathTextBox";
            this.openFolderPathTextBox.Size = new System.Drawing.Size(461, 27);
            this.openFolderPathTextBox.TabIndex = 0;
            // 
            // outputFolderPathTextBox
            // 
            this.outputFolderPathTextBox.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F);
            this.outputFolderPathTextBox.Location = new System.Drawing.Point(12, 94);
            this.outputFolderPathTextBox.Name = "outputFolderPathTextBox";
            this.outputFolderPathTextBox.Size = new System.Drawing.Size(461, 27);
            this.outputFolderPathTextBox.TabIndex = 1;
            // 
            // openFolderButton
            // 
            this.openFolderButton.Font = new System.Drawing.Font("Roboto Condensed", 9.75F, System.Drawing.FontStyle.Bold);
            this.openFolderButton.Location = new System.Drawing.Point(482, 34);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(187, 23);
            this.openFolderButton.TabIndex = 2;
            this.openFolderButton.Text = "Open Folder";
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.OpenFolderButton_Click);
            // 
            // outputFolderButton
            // 
            this.outputFolderButton.Font = new System.Drawing.Font("Roboto Condensed", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputFolderButton.Location = new System.Drawing.Point(482, 94);
            this.outputFolderButton.Name = "outputFolderButton";
            this.outputFolderButton.Size = new System.Drawing.Size(187, 23);
            this.outputFolderButton.TabIndex = 3;
            this.outputFolderButton.Text = "Output Folder";
            this.outputFolderButton.UseVisualStyleBackColor = true;
            this.outputFolderButton.Click += new System.EventHandler(this.OutputFolderButton_Click);
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(14, 67);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(655, 21);
            this.comboBox.TabIndex = 4;
            // 
            // summaryGenerateButton
            // 
            this.summaryGenerateButton.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.summaryGenerateButton.Location = new System.Drawing.Point(232, 127);
            this.summaryGenerateButton.Name = "summaryGenerateButton";
            this.summaryGenerateButton.Size = new System.Drawing.Size(205, 44);
            this.summaryGenerateButton.TabIndex = 5;
            this.summaryGenerateButton.Text = "Generate Code Summary";
            this.summaryGenerateButton.UseVisualStyleBackColor = true;
            this.summaryGenerateButton.Click += new System.EventHandler(this.SummaryGenerateButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(680, 183);
            this.Controls.Add(this.summaryGenerateButton);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.outputFolderButton);
            this.Controls.Add(this.openFolderButton);
            this.Controls.Add(this.outputFolderPathTextBox);
            this.Controls.Add(this.openFolderPathTextBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(50, 50);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox openFolderPathTextBox;
        private System.Windows.Forms.TextBox outputFolderPathTextBox;
        private System.Windows.Forms.Button openFolderButton;
        private System.Windows.Forms.Button outputFolderButton;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Button summaryGenerateButton;
    }
}


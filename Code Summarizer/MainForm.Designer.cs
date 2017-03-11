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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openFolderPathTextBox = new System.Windows.Forms.TextBox();
            this.outputFolderPathTextBox = new System.Windows.Forms.TextBox();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.outputFolderButton = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.summaryGenerateButton = new System.Windows.Forms.Button();
            this.acessSpecifierPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dataTypeSpecifierPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.identifierSpecifierPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.acessSpecifierPanel.SuspendLayout();
            this.dataTypeSpecifierPanel.SuspendLayout();
            this.identifierSpecifierPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFolderPathTextBox
            // 
            this.openFolderPathTextBox.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openFolderPathTextBox.Location = new System.Drawing.Point(11, 20);
            this.openFolderPathTextBox.Name = "openFolderPathTextBox";
            this.openFolderPathTextBox.Size = new System.Drawing.Size(461, 27);
            this.openFolderPathTextBox.TabIndex = 0;
            // 
            // outputFolderPathTextBox
            // 
            this.outputFolderPathTextBox.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F);
            this.outputFolderPathTextBox.Location = new System.Drawing.Point(11, 85);
            this.outputFolderPathTextBox.Name = "outputFolderPathTextBox";
            this.outputFolderPathTextBox.Size = new System.Drawing.Size(461, 27);
            this.outputFolderPathTextBox.TabIndex = 1;
            // 
            // openFolderButton
            // 
            this.openFolderButton.BackColor = System.Drawing.Color.White;
            this.openFolderButton.Font = new System.Drawing.Font("Orbitron", 11.25F, System.Drawing.FontStyle.Bold);
            this.openFolderButton.Location = new System.Drawing.Point(481, 20);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(187, 27);
            this.openFolderButton.TabIndex = 2;
            this.openFolderButton.Text = "Open Folder";
            this.openFolderButton.UseVisualStyleBackColor = false;
            this.openFolderButton.Click += new System.EventHandler(this.OpenFolderButton_Click);
            // 
            // outputFolderButton
            // 
            this.outputFolderButton.BackColor = System.Drawing.Color.White;
            this.outputFolderButton.Font = new System.Drawing.Font("Orbitron", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputFolderButton.Location = new System.Drawing.Point(481, 85);
            this.outputFolderButton.Name = "outputFolderButton";
            this.outputFolderButton.Size = new System.Drawing.Size(187, 27);
            this.outputFolderButton.TabIndex = 3;
            this.outputFolderButton.Text = "Output Folder";
            this.outputFolderButton.UseVisualStyleBackColor = false;
            this.outputFolderButton.Click += new System.EventHandler(this.OutputFolderButton_Click);
            // 
            // comboBox
            // 
            this.comboBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(11, 53);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(657, 26);
            this.comboBox.TabIndex = 4;
            // 
            // summaryGenerateButton
            // 
            this.summaryGenerateButton.BackColor = System.Drawing.Color.White;
            this.summaryGenerateButton.Font = new System.Drawing.Font("Orbitron", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.summaryGenerateButton.Location = new System.Drawing.Point(160, 206);
            this.summaryGenerateButton.Name = "summaryGenerateButton";
            this.summaryGenerateButton.Size = new System.Drawing.Size(312, 44);
            this.summaryGenerateButton.TabIndex = 5;
            this.summaryGenerateButton.Text = "Generate Code Summary";
            this.summaryGenerateButton.UseCompatibleTextRendering = true;
            this.summaryGenerateButton.UseVisualStyleBackColor = false;
            this.summaryGenerateButton.Click += new System.EventHandler(this.SummaryGenerateButton_Click);
            // 
            // acessSpecifierPanel
            // 
            this.acessSpecifierPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.acessSpecifierPanel.Controls.Add(this.label1);
            this.acessSpecifierPanel.Location = new System.Drawing.Point(11, 118);
            this.acessSpecifierPanel.Name = "acessSpecifierPanel";
            this.acessSpecifierPanel.Size = new System.Drawing.Size(197, 59);
            this.acessSpecifierPanel.TabIndex = 6;
            this.acessSpecifierPanel.Click += new System.EventHandler(this.AcessSpecifierPanel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Access Specifier Colour";
            // 
            // dataTypeSpecifierPanel
            // 
            this.dataTypeSpecifierPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.dataTypeSpecifierPanel.Controls.Add(this.label2);
            this.dataTypeSpecifierPanel.Location = new System.Drawing.Point(241, 118);
            this.dataTypeSpecifierPanel.Name = "dataTypeSpecifierPanel";
            this.dataTypeSpecifierPanel.Size = new System.Drawing.Size(197, 59);
            this.dataTypeSpecifierPanel.TabIndex = 7;
            this.dataTypeSpecifierPanel.Click += new System.EventHandler(this.DataTypeSpecifierPanel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Data Type Specifier Colour";
            // 
            // identifierSpecifierPanel
            // 
            this.identifierSpecifierPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.identifierSpecifierPanel.Controls.Add(this.label3);
            this.identifierSpecifierPanel.Location = new System.Drawing.Point(471, 118);
            this.identifierSpecifierPanel.Name = "identifierSpecifierPanel";
            this.identifierSpecifierPanel.Size = new System.Drawing.Size(197, 59);
            this.identifierSpecifierPanel.TabIndex = 7;
            this.identifierSpecifierPanel.Click += new System.EventHandler(this.IdentifierSpecifierPanel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(15, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Identifier Specifier Colour";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(680, 262);
            this.Controls.Add(this.identifierSpecifierPanel);
            this.Controls.Add(this.dataTypeSpecifierPanel);
            this.Controls.Add(this.acessSpecifierPanel);
            this.Controls.Add(this.summaryGenerateButton);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.outputFolderButton);
            this.Controls.Add(this.openFolderButton);
            this.Controls.Add(this.outputFolderPathTextBox);
            this.Controls.Add(this.openFolderPathTextBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(50, 50);
            this.Name = "MainForm";
            this.Text = "Code Summarizer";
            this.acessSpecifierPanel.ResumeLayout(false);
            this.acessSpecifierPanel.PerformLayout();
            this.dataTypeSpecifierPanel.ResumeLayout(false);
            this.dataTypeSpecifierPanel.PerformLayout();
            this.identifierSpecifierPanel.ResumeLayout(false);
            this.identifierSpecifierPanel.PerformLayout();
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
        private System.Windows.Forms.Panel acessSpecifierPanel;
        private System.Windows.Forms.Panel dataTypeSpecifierPanel;
        private System.Windows.Forms.Panel identifierSpecifierPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}


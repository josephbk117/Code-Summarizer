using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using CodeSummarizer;
using System.Drawing;

namespace CodeSummarizerWpf
{

    public partial class MainWindow : Window
    {
        BackgroundWorker bgw;
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        List<string> csFiles = new List<string>();
        string[] templates;

        partial class ThreadObjectHelper
        {
            public string _template { get; }
            public string[] _files { get; }
            public string _outputFolderPath { get; }
            public System.Drawing.Color _acessSpecifierColour { get; }
            public System.Drawing.Color _dataTypeColour { get; }
            public System.Drawing.Color _identifierColour { get; }


            public ThreadObjectHelper(string template, string[] files, string outputFolderPath,
                System.Drawing.Color acess, System.Drawing.Color data, System.Drawing.Color identifier)
            {
                this._template = template;
                this._files = files;
                this._outputFolderPath = outputFolderPath;
                this._acessSpecifierColour = acess;
                this._dataTypeColour = data;
                this._identifierColour = identifier;
            }
        }

        System.Drawing.Color acessSpecifierColour, dataTypeColour, identifierColour;
        
        public MainWindow()
        {
            InitializeComponent();
            bgw = new BackgroundWorker()
            {
                WorkerReportsProgress = true
            };
            
            bgw.DoWork += BackgroundWorker_DoWork;
            bgw.ProgressChanged += BackgroundWorker_ProgressChanged;
            bgw.RunWorkerCompleted += BackgroundWorker_OnWorkCompleted;
            InitTemplates();           
        }

        private void BackgroundWorker_OnWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Value = 0;
            if (System.Windows.MessageBox.Show("Output At : " + outputFolderTextBox.Text + "\nDo you want to open it?", "Documentation Sucessfully Generated", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(outputFolderTextBox.Text + "/index.html");
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > 100)
                return;
            progressBar.Value = e.ProgressPercentage;
        }
        //TODO : Make summarizer send completion data , pass Bg worker as argument
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ThreadObjectHelper toh = (ThreadObjectHelper)e.Argument;

            Summarizer.Summarize(toh._files.ToList(),toh._outputFolderPath,bgw,ColorTranslator.ToHtml(toh._identifierColour), ColorTranslator.ToHtml(toh._dataTypeColour));           

        }
        #region Open folder Label Cosmetics

        private void OpenFolder_Label_OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            openFolderLabel.BorderBrush.Opacity = 0.45d;

        }

        private void OpenFolder_Label_OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            openFolderLabel.BorderBrush.Opacity = 1d;
            openFolderLabel.Background.Opacity = 1d;
        }

        private void OpenFolder_Label_OnLeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            openFolderLabel.BorderBrush.Opacity = 0.45d;
            openFolderLabel.Background.Opacity = 0.45d;
        }


        private void OutputFolder_Label_OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            outputFolderLabel.BorderBrush.Opacity = 0.45d;
        }

        private void OutputFolder_Label_OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            outputFolderLabel.BorderBrush.Opacity = 1d;
            outputFolderLabel.Background.Opacity = 1d;
        }

        private void OutputFolder_Label_OnLeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            outputFolderLabel.BorderBrush.Opacity = 0.45d;
            outputFolderLabel.Background.Opacity = 0.45d;
        }


        private void GenerateCodeSummaryBtn_Label_OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            generateCodeSummaryBtn.BorderBrush.Opacity = 0.45d;
        }

        private void GenerateCodeSummaryBtn_Label_OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            generateCodeSummaryBtn.BorderBrush.Opacity = 1d;
            generateCodeSummaryBtn.Background.Opacity = 1d;
        }

        private void GenerateCodeSummaryBtn_OnLeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            generateCodeSummaryBtn.BorderBrush.Opacity = 0.45d;
            generateCodeSummaryBtn.Background.Opacity = 0.45d;

        }
        #endregion
        
        private void InitTemplates()
        {
            templates = Directory.GetFiles(Environment.CurrentDirectory + "/Res/Templates/Class Templates/", "*.html");
            for (int i = 0; i < templates.Length; i++)
            {
                string templateName = templates[i].Substring(templates[i].LastIndexOf("/") + 1).Replace(".html", "");
                ComboBoxItem item = new ComboBoxItem();
                item.Content = templateName;
                item.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 90, 190));
                item.Padding = new Thickness(0, 5, 0, 5);
                comboBox.Items.Add(item);
            }
        }

        private void OpenFolder_Label_OnLeftMouseBtnUp(object sender, MouseButtonEventArgs e)
        {
            if (fileList.Items.Count > 0)
            {
                fileList.Items.Clear();
            }

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                openFolderTextBox.Text = fbd.SelectedPath;
                csFiles = Directory.GetFiles(fbd.SelectedPath, "*.cs").ToList<string>();

                foreach (string file in csFiles)
                {
                    fileList.Items.Add(file);
                }
            }
        }
        private void OutputFolder_Label_OnLeftMouseBtnUp(object sender, MouseButtonEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                outputFolderTextBox.Text = fbd.SelectedPath;
                statusBarOutputPath.Content = "Output Path : " + fbd.SelectedPath;
            }
        }

        private void GenerateCodeSummaryBtn_OnLeftMouseBtnUp(object sender, MouseButtonEventArgs e)
        {
            if (csFiles.Count > 0)
            {
                bgw.RunWorkerAsync(new ThreadObjectHelper(templates[comboBox.SelectedIndex], csFiles.ToArray(),
                    outputFolderTextBox.Text, acessSpecifierColour, dataTypeColour, identifierColour));
            }
            else
                System.Windows.MessageBox.Show("No Files are there");
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            browser.Source = new Uri(templates[comboBox.SelectedIndex]);
        }

        private void TextBox_HintTextRemove_OnClick(object sender, MouseButtonEventArgs e)
        {
            ((System.Windows.Controls.TextBox)(sender)).Text = "";
        }

        private void fileList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start((string)fileList.SelectedValue);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
        private void ColourSpecify_OnClick(object sender, MouseButtonEventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (sender.Equals(acessSpecifierPanel))
                {
                    acessSpecifierColour = cd.Color;
                }
                else if (sender.Equals(dataTypeSpecifierPanel))
                {
                    dataTypeColour = cd.Color;
                }
                else
                    identifierColour = cd.Color;
                ((System.Windows.Shapes.Rectangle)(sender)).Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, cd.Color.R, cd.Color.G, cd.Color.B));
            }
        }
    }
}

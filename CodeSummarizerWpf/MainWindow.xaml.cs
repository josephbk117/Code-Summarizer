using Code_Summarizer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            browser.Source = new Uri(templates[0]);
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

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ThreadObjectHelper toh = (ThreadObjectHelper)e.Argument;

            for (int i = 0; i < csFiles.Count; i++)
            {
                ScriptFileInfo SFI = new ScriptFileInfo(toh._files[i]);
                SFI.Analyze();

                HtmlPageWriter hpw = new HtmlPageWriter(toh._template)
                {
                    AcessSpecifierColour = "rgb(" + toh._acessSpecifierColour.R + "," + toh._acessSpecifierColour.G + "," + toh._acessSpecifierColour.B + ")",
                    DataTypeSpecifierColour = "rgb(" + toh._dataTypeColour.R + "," + toh._dataTypeColour.G + "," + toh._dataTypeColour.B + ")",
                    IdentifierSpecifierColour = "rgb(" + toh._identifierColour.R + "," + toh._identifierColour.G + "," + toh._identifierColour.B + ")"
                };
                hpw.SetContent(SFI.GetNamespace(), SFI.GetClassName(), SFI.GetDerievedClass(), SFI.GetMemberFunctions(), SFI.GetMemberVariables(), SFI.GetDependencies(), SFI.GetTodos(), SFI._pathName, SFI.GetFileAcsessDate());
                Directory.CreateDirectory(toh._outputFolderPath + "/pages/");
                string classDocOutputPath = toh._outputFolderPath + "/pages/" + SFI.GetClassName() + " Doc.html";
                hpw.OutputWebPage(classDocOutputPath);
                HtmlNavigationManager.AddClass(classDocOutputPath);
                bgw.ReportProgress((int)(((float)i / csFiles.Count) * 100f));
            }
            HtmlNavigationManager.WriteNavigationPage(toh._outputFolderPath + "/index.html");

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
        /// <summary>
        /// Initialize the templates
        /// </summary>
        private void InitTemplates()
        {
            templates = Directory.GetFiles(Environment.CurrentDirectory + "/Res/Templates/", "*.html");
            for (int i = 0; i < templates.Length; i++)
            {
                string templateName = templates[i].Substring(templates[i].LastIndexOf("/") + 1).Replace(".html", "");
                comboBox.Items.Add(templateName);
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
            bgw.RunWorkerAsync(new ThreadObjectHelper(templates[comboBox.SelectedIndex], csFiles.ToArray(),
                outputFolderTextBox.Text, acessSpecifierColour, dataTypeColour, identifierColour));
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
                ((Rectangle)(sender)).Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, cd.Color.R, cd.Color.G, cd.Color.B));
            }
        }
    }
}

using Code_Summarizer;
using System;
using System.Collections.Generic;
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
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        List<string> csFiles = new List<string>();
        ScriptFileInfo SFI;
        string[] templates;
        HtmlPageWriter hpw;

        System.Drawing.Color acessSpecifierColour, dataTypeColour, identifierColour;

        public MainWindow()
        {
            InitializeComponent();
            InitTemplates();
            browser.Source = new Uri(templates[0]);
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
            templates = Directory.GetFiles(Environment.CurrentDirectory + "/Res/Templates/", "*.html");
            for (int i = 0; i < templates.Length; i++)
            {
                string templateName = templates[i].Substring(templates[i].LastIndexOf("/") + 1).Replace(".html", "");
                comboBox.Items.Add(templateName);
            }
        }

        private void OpenFolder_Label_OnLeftMouseBtnUp(object sender, MouseButtonEventArgs e)
        {
            if(fileList.Items.Count > 0)
            {
                fileList.Items.Clear();
            }

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                openFolderTextBox.Text = fbd.SelectedPath;
                csFiles = Directory.GetFiles(fbd.SelectedPath, "*.cs").ToList<string>();

                foreach( string file in csFiles)
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
            }
        }
        private void GenerateClassSummary(string filePath)
        {
            SFI = new ScriptFileInfo(filePath);
            SFI.Analyze();

            hpw = new HtmlPageWriter(templates[comboBox.SelectedIndex])
            {
                AcessSpecifierColour = FormattedColor(System.Windows.Media.Color.FromArgb(255, acessSpecifierColour.R, acessSpecifierColour.G, acessSpecifierColour.B)),
                DataTypeSpecifierColour = FormattedColor(System.Windows.Media.Color.FromArgb(255, dataTypeColour.R, dataTypeColour.G, dataTypeColour.B)),
                IdentifierSpecifierColour = FormattedColor(System.Windows.Media.Color.FromArgb(255, identifierColour.R, identifierColour.G, identifierColour.B))
            };
            hpw.SetContent(SFI.GetNamespace(), SFI.GetClassName(), SFI.GetDerievedClass(), SFI.GetMemberFunctions(), SFI.GetMemberVariables(), SFI.GetDependencies(), SFI.GetTodos(), SFI._pathName, SFI.GetFileAcsessDate());
            Directory.CreateDirectory(outputFolderTextBox.Text + "/pages/");
            string classDocOutputPath = outputFolderTextBox.Text + "/pages/" + SFI.GetClassName() + " Doc.html";
            hpw.OutputWebPage(classDocOutputPath);
            HtmlNavigationManager.AddClass(classDocOutputPath);
        }
        private void GenerateCodeSummaryBtn_OnLeftMouseBtnUp(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < csFiles.Count; i++)
            {
                GenerateClassSummary(csFiles[i]);
            }
            HtmlNavigationManager.WriteNavigationPage(outputFolderTextBox.Text + "/index.html");
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            browser.Source = new Uri(templates[comboBox.SelectedIndex]);
        }

        private void TextBox_HintTextRemove_OnClick(object sender, MouseButtonEventArgs e)
        {
            ((System.Windows.Controls.TextBox)(sender)).Text = "";
        }

        private string FormattedColor(Color color)
        {
            return "rgb(" + color.R + "," + color.G + "," + color.B + ")";
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

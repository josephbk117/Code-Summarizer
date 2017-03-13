using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Code_Summarizer
{
    public partial class MainForm : Form
    {
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        List<string> csFiles = new List<string>();
        ScriptFileInfo SFI;
        string[] templates;
        HtmlPageWriter hpw;

        public MainForm()
        {
            InitializeComponent();
            InitTemplates();
        }

        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                openFolderPathTextBox.Text = fbd.SelectedPath;
                csFiles = Directory.GetFiles(fbd.SelectedPath, "*.cs").ToList<string>();
                openFolderPathTextBox.Text = fbd.SelectedPath;
            }
        }

        private void GenerateClassSummary(string filePath)
        {
            SFI = new ScriptFileInfo(filePath);
            SFI.Analyze();

            hpw = new HtmlPageWriter(templates[comboBox.SelectedIndex])
            {
                AcessSpecifierColour = FormattedColor(acessSpecifierPanel.BackColor),
                DataTypeSpecifierColour = FormattedColor(dataTypeSpecifierPanel.BackColor),
                IdentifierSpecifierColour = FormattedColor(identifierSpecifierPanel.BackColor)
            };
            hpw.SetContent(SFI.GetNamespace(), SFI.GetClassName(), SFI.GetDerievedClass(), SFI.GetMemberFunctions(), SFI.GetMemberVariables(), SFI.GetDependencies(), SFI.GetTodos(), SFI._pathName, SFI.GetFileAcsessDate());
            Directory.CreateDirectory(outputFolderPathTextBox.Text + "/pages/");
            string classDocOutputPath = outputFolderPathTextBox.Text + "/pages/" + SFI.GetClassName() + " Doc.html";
            hpw.OutputWebPage(classDocOutputPath);            
            HtmlNavigationManager.AddClass(classDocOutputPath);
        }

        private void InitTemplates()
        {
            templates = Directory.GetFiles(Environment.CurrentDirectory + "/Res/Templates/", "*.html");
            for (int i = 0; i < templates.Length; i++)
            {
                string templateName = templates[i].Substring(templates[i].LastIndexOf("/") + 1).Replace(".html", "");
                comboBox.Items.Add(templateName);
            }
        }

        private void OutputFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                outputFolderPathTextBox.Text = fbd.SelectedPath;
            }
        }

        private void SummaryGenerateButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < csFiles.Count; i++)
            {
                GenerateClassSummary(csFiles[i]);
            }
            HtmlNavigationManager.WriteNavigationPage(outputFolderPathTextBox.Text + "/index.html");
        }

        private void AcessSpecifierPanel_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                acessSpecifierPanel.BackColor = cd.Color;
            }
        }

        private void DataTypeSpecifierPanel_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                dataTypeSpecifierPanel.BackColor = cd.Color;
            }
        }

        private void IdentifierSpecifierPanel_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                identifierSpecifierPanel.BackColor = cd.Color;
            }
        }
        private string FormattedColor(System.Drawing.Color color)
        {
            return "rgb(" + color.R + "," + color.G + "," + color.B + ")";
        }
    }
}

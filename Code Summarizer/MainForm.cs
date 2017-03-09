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

            HtmlPageWriter hpw = new HtmlPageWriter(templates[comboBox.SelectedIndex]);
            hpw.SetContent(SFI.GetNamespace(), SFI.GetClassName(), SFI.GetDerievedClass(), SFI.GetMemberFunctions(), SFI.GetMemberVariables(), SFI.GetDependencies(), SFI.GetTodos(), SFI._pathName, SFI.GetFileAcsessDate());
            hpw.OutputWebPage(outputFolderPathTextBox.Text+"/"+SFI.GetClassName() + " Doc.html");
            
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
        }        
    }
}

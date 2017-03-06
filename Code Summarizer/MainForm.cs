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

        public MainForm()
        {
            InitializeComponent();
        }

        private void OpenFolderPanel_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                csFiles = Directory.GetFiles(fbd.SelectedPath, "*.cs").ToList<string>();
                SFI = new ScriptFileInfo(csFiles[0]);
                folderPathTextBox.Text = fbd.SelectedPath;
                SFI.Analyze();

                HtmlPageWriter hpw = new HtmlPageWriter("template.html");
                hpw.SetContent(SFI.GetNamespace(), SFI.GetClassName(), SFI.GetDerievedClass(), SFI.GetMemberFunctions(), SFI.GetMemberVariables(), SFI.GetDependencies(), SFI.GetTodos(), SFI._pathName);
                hpw.OutputWebPage("Output.html");
            }
        }

        private void ClosePanel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                csFiles = Directory.GetFiles(fbd.SelectedPath,"*cs").ToList<string>();
                SFI = new ScriptFileInfo(csFiles[0]);
                folderPathTextBox.Text = fbd.SelectedPath;
                SFI.Analyze();
                Console.WriteLine("Class name : " + SFI.getClassName());
                foreach (string s in SFI.getDependencies())
                {
                    Console.WriteLine("Dependencies = " + s);
                }
                foreach (string s in SFI.getMemberFunctions())
                {
                    Console.WriteLine("Functions : " + s);
                }

                HtmlPageWriter hpw = new HtmlPageWriter("template.html");
                hpw.setContent(SFI.getClassName(), SFI.getDerievedClass(),SFI.getMemberFunctions(), SFI.getMemberVariables(),SFI.getDependencies(),SFI._pathName);
                hpw.outputWebPage("Output.html");
            }
        }
    }
}

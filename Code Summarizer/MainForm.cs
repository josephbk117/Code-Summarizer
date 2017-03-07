using System;
using System.Collections.Generic;
using System.Drawing;
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
        Point mouseOffset;
        public MainForm()
        {
            InitializeComponent();
            mouseOffset = new Point(0, 0);
        }

        private void OpenFolderPanel_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                folderPathTextBox.Text = fbd.SelectedPath;
                csFiles = Directory.GetFiles(fbd.SelectedPath, "*.cs").ToList<string>();
                SFI = new ScriptFileInfo(csFiles[0]);
                folderPathTextBox.Text = fbd.SelectedPath;
                SFI.Analyze();

                HtmlPageWriter hpw = new HtmlPageWriter(Environment.CurrentDirectory+"/Res/Templates/CoolBlue.html");
                hpw.SetContent(SFI.GetNamespace(), SFI.GetClassName(), SFI.GetDerievedClass(), SFI.GetMemberFunctions(), SFI.GetMemberVariables(), SFI.GetDependencies(), SFI.GetTodos(), SFI._pathName,SFI.GetFileAcsessDate());
                hpw.OutputWebPage("Output.html");
            }
        }

        private void ClosePanel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DragPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseOffset = new Point(-e.X, -e.Y);
        }

        private void DragPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void DragPanel_MouseLeave(object sender, EventArgs e)
        {
            dragPanel.BackColor = Color.FromArgb(0, 0, 0, 0);
        }

        private void DragPanel_MouseEnter(object sender, EventArgs e)
        {
            dragPanel.BackColor = Color.FromArgb(90, 0, 0, 50);
        }

        private void ClosePanel_MouseEnter(object sender, EventArgs e)
        {
            closePanel.BackColor = Color.FromArgb(90, 0, 0, 50);
        }

        private void ClosePanel_MouseLeave(object sender, EventArgs e)
        {
            closePanel.BackColor = Color.FromArgb(0, 0, 0, 0);
        }
    }
}

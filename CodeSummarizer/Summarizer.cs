using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummarizer
{
    //Takes codeFileData and then prints output accordingly
    public static class Summarizer
    {
        public static void Summarize(List<string> codeFileLocations,string outputFolder, BackgroundWorker bgw,string hexIdentifier,string hexDataType)
        {
            int count = 0;
            List<CodeFileData> cfds = new List<CodeFileData>();
            foreach (string codeLoc in codeFileLocations)
            {
                CodeFileData cfd = new CodeFileData(codeLoc);
                cfds.Add(cfd);
                bgw.ReportProgress((int)(((float)count++ / codeFileLocations.Count) * 50f));
            }
            ProjectInfo.Analyze(cfds);
            bgw.ReportProgress(75);
            foreach (CodeFileData cf in cfds)
            {
                OutputWebPage ow = new OutputWebPage("Cool Blue.html", outputFolder, cf, hexIdentifier ,  hexDataType);
                ow.GenerateHtmlContent();
            }
            bgw.ReportProgress(100);
        }
    }
}

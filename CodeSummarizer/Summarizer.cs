using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace CodeSummarizer
{
    //Takes codeFileData and then prints output accordingly
    public static class Summarizer
    {
        public static void Summarize(List<string> codeFileLocations, string outputFolder, BackgroundWorker bgw, string hexIdentifier, string hexDataType, string templateName)
        {
            int count = 0;
            List<CodeFileData> cfds = new List<CodeFileData>();
            foreach (string codeLoc in codeFileLocations)
            {
                CodeFileData cfd = new CodeFileData(codeLoc);
                cfds.Add(cfd);
                bgw.ReportProgress((int)(((float)count++ / codeFileLocations.Count) * 75f));
            }
            ProjectInfo.Analyze(cfds);
            bgw.ReportProgress(85);

            List<string> classes = new List<string>();
            List<string> enums = new List<string>();
            List<string> interfaces = new List<string>();

            foreach (CodeFileData cf in cfds)
            {
                foreach (ClassData cd in cf.classes)
                {
                    classes.Add(cd.ClassName);
                }
                foreach (EnumData ed in cf.enums)
                {
                    enums.Add(ed.EnumName);
                }
                foreach (InterfaceData ifs in cf.interfaces)
                {
                    interfaces.Add(ifs.InterfaceName);
                }
                OutputWebPage ow = new OutputWebPage(templateName + ".html", outputFolder, cf, hexIdentifier, hexDataType);
                ow.GenerateHtmlContent();
            }
            GenerateIndexPage(outputFolder, classes, enums, interfaces);

            bgw.ReportProgress(100);
        }

        private static void GenerateIndexPage(string outputFolder, List<string> classes, List<string> enums, List<string> interfaces)
        {
            string indexTemplate = "Res/IndexTemplates/indexTemplate.html";

            string indexTemplateContent = File.ReadAllText(indexTemplate);

            string classContent = "<ol>";
            foreach (string cs in classes)
            {
                classContent += $"<li><a href = \"{cs}_Class_Doc.html\">{cs}</a></li>";
            }
            classContent += "</ol>";

            string enumContent = "<ol>";
            foreach (string es in enums)
            {
                enumContent += $"<li><a href = \"{es}_Enum_Doc.html\">{es}</a></li>";
            }
            enumContent += "</ol>";

            string infContent = "<ol>";
            foreach (string ifs in interfaces)
            {
                infContent += $"<li><a href = \"{ifs}_Interface_Doc.html\">{ifs}</a></li>";
            }
            infContent += "</ol>";

            indexTemplateContent = indexTemplateContent.Replace("#CLASSES#", classContent);
            indexTemplateContent = indexTemplateContent.Replace("#INTERFACENAME#", infContent);
            indexTemplateContent = indexTemplateContent.Replace("#ENUMS#", enumContent);

            File.WriteAllText(outputFolder + "/index.html", indexTemplateContent);
        }
    }
}

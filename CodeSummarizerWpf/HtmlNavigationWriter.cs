using System;
using System.Collections.Generic;
using System.IO;

namespace Code_Summarizer
{
    class HtmlNavigationWriter
    {
        private const string CLASSES = "#CLASSES#";
        private string _templateFilePath = "";
        private string _outputFilePath = "";
        private string _htmlContent = "";
        private List<string> _classDocOutputPath;
        public HtmlNavigationWriter(List<string> classes, string pathOfTemplate, string pathOfoutputFile)
        {
            this._outputFilePath = pathOfoutputFile;
            this._templateFilePath = pathOfTemplate;
            this._classDocOutputPath = classes;
            try
            {
                using (StreamReader sr = new StreamReader(pathOfTemplate))
                {
                    _htmlContent = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The index template file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Outputs the html page
        /// </summary>
        public void OutputHtmlPage()
        {
            string classList = "<ol>";
            foreach (string classPath in _classDocOutputPath)
            {
                string className = classPath.Substring(classPath.LastIndexOf('/')+1).Replace(" Doc.html","").Trim();
                classList += "<li><a href = \"file:///"+classPath+"\">" + className + "</a></li>";
            }
            classList += "</ol>";
            _htmlContent = _htmlContent.Replace(CLASSES, classList);
            using (StreamWriter sw = new StreamWriter(_outputFilePath))
            {
                sw.WriteLine(_htmlContent);
                sw.Flush();
            }
        }
    }
}

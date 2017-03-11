using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Summarizer
{
    class HtmlNavigationWriter
    {
        private const string CLASSES = "#CLASSES#";
        private string _templateFilePath = "";
        private string _outputFilePath = "";
        private string _htmlContent = "";
        private List<string> _classes;
        public HtmlNavigationWriter(List<string> classes,string pathOfTemplate,string pathOfoutputFile)
        {
            this._outputFilePath = pathOfoutputFile;
            this._templateFilePath = pathOfTemplate;
            this._classes = classes;
            try
            {   
                using (StreamReader sr = new StreamReader(pathOfTemplate))
                {
                    _htmlContent = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        public void OutputHtmlPage()
        {
            string classList = "<ol>";
            foreach (string className in _classes)
            {
                classList += "<li>" + className + "</li>";
            }
            classList += "</ol>";
            _htmlContent = _htmlContent.Replace(CLASSES,classList);
            using (StreamWriter sw = new StreamWriter(_outputFilePath))
            {
                sw.WriteLine(_htmlContent);
                sw.Flush();
            }
        }
    }
}

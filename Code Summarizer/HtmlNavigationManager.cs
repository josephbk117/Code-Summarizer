using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Summarizer
{
    static class HtmlNavigationManager
    {
        private static List<string> classes;

        public static void AddClass(string className)
        {
            if (classes == null)
            {
                classes = new List<string>();
            }
            classes.Add(className);
        }
        public static void WriteNavigationPage(string path)
        {
            HtmlNavigationWriter hn = new HtmlNavigationWriter(classes, "indexTemplate.html", path);
            hn.OutputHtmlPage();
        }
    }
}

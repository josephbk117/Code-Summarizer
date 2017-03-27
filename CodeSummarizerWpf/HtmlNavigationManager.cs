using System.Collections.Generic;

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
            HtmlNavigationWriter hn = new HtmlNavigationWriter(classes, "Res/IndexTemplates/indexTemplate.html", path);
            hn.OutputHtmlPage();
        }
    }
}

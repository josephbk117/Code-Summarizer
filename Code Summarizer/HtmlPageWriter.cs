using System;
using System.IO;
using System.Collections.Generic;


namespace Code_Summarizer
{
    class HtmlPageWriter
    {
        const string NAMESPACE = "#NAMESPACE#";
        const string CLASSNAME = "#CLASSNAME#";
        const string DCLASSNAME = "#DCLASSNAME#";
        const string FILENAME = "#FILENAME#";
        const string FUNCTIONS = "#FUNCTIONS#";
        const string DEPENDENCIES = "#DEPENDENCIES#";
        const string MEMVARS = "#MEMVARS#";
        const string TODOS = "#TODOS#";
        private string _filePath;
        private string _htmlContent = "";

        public HtmlPageWriter(string filePath)
        {
            this._filePath = filePath;
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(_filePath))
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
        public void SetContent(string namespaceValue, string className, string dClassName, List<string> functions, List<string> memVariables, List<string> dependecyList, List<string> todos, string fileName)
        {
            _htmlContent = _htmlContent.Replace(CLASSNAME, className).Replace(FILENAME, fileName).Replace(NAMESPACE, namespaceValue);
            _htmlContent = _htmlContent.Replace(DCLASSNAME, dClassName);

            string classTodos = "<ul style = \"color: rgb(200, 220, 220)\">";
            foreach (string todo in todos)
            {
                classTodos += "<li>" + todo + "</li>";
            }
            classTodos += "</ul>";

            _htmlContent = _htmlContent.Replace(TODOS, classTodos);

            string memberFunctions = "<ol style = \"color: rgb(200, 220, 220)\">";
            foreach (string func in functions)
            {
                Console.WriteLine("Func original vals - " + func);
                string[] components = func.Split(' ');
                components[0] = components[0].Insert(0, "<font color = rgb(70,150,200)>");
                components[0] += " </font>";
                components[1] = components[1].Insert(0, "<font color = rgb(90,200,240)>");
                components[1] += " </font>";
                Console.WriteLine("Last index = " + components[2]);
                components[2] = "<font color = rgb(230,250,255)>" + func.Substring(func.IndexOf(components[2]));//components[2].Insert(0, "<font color = rgb(230,250,255)>");
                components[2] += " </font>";
                string newFunc = components[0] + components[1] + components[2];
                memberFunctions += "<li>" + newFunc + "</li>";
            }
            memberFunctions += "</ol>";
            _htmlContent = _htmlContent.Replace(FUNCTIONS, memberFunctions);

            string dependencies = "<ol style = \"color: rgb(200, 220, 220)\">";
            foreach (string depend in dependecyList)
            {
                dependencies += "<li>" + depend + "</li>";
            }

            dependencies += "</ol>";
            _htmlContent = _htmlContent.Replace(DEPENDENCIES, dependencies);

            string memberVariables = "<ol style = \"color: rgb(200, 220, 220)\">";
            foreach (string vars in memVariables)
            {
                memberVariables += "<li>" + vars + "</li>";
            }
            memberVariables += "</ol>";
            _htmlContent = _htmlContent.Replace(MEMVARS, memberVariables);

        }
        public void OutputWebPage(string outputPath)
        {
            using (StreamWriter sw = new StreamWriter(outputPath))
            {
                sw.WriteLine(_htmlContent);
                sw.Flush();
            }
        }
    }
}

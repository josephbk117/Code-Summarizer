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
        const string LAST_MODIFIED = "#LASTMODIFIED#";
        private string _filePath;
        private string _htmlContent = "";
        public string AcessSpecifierColour { set; get; }
        public string DataTypeSpecifierColour { set; get; }
        public string IdentifierSpecifierColour { set; get; }

        public HtmlPageWriter(string filePath)
        {
            this._filePath = filePath;
            AcessSpecifierColour = "rgb(200, 220, 220)";
            DataTypeSpecifierColour = "rgb(200, 220, 220)";
            IdentifierSpecifierColour = "rgb(200, 220, 220)";
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
        /// <summary>
        /// Sets all the content of the class to the html file
        /// </summary>
        /// <param name="namespaceValue">Namespace of the class</param>
        /// <param name="className">Name of the class</param>
        /// <param name="derivedClassName">Name of class that current class derieves from</param>
        /// <param name="functions">List of memeber functions</param>
        /// <param name="memVariables">List of memeber variables</param>
        /// <param name="dependecyList">List if include statements</param>
        /// <param name="todos">Todo statements in the class</param>
        /// <param name="fileName">Output file name</param>
        /// <param name="lastAcessTime">Last time .cs file was accessed</param>
        public void SetContent(string namespaceValue, string className, string derivedClassName, List<string> functions, List<string> memVariables, List<string> dependecyList, List<string> todos, string fileName, string lastAcessTime)
        {
            SetUpNamespceAndClassName(namespaceValue, className, fileName);
            SetUpDerievedClassName(derivedClassName);
            SetUpLastAcessTime(lastAcessTime);
            SetUpTodos(todos);
            SetUpDependencies(dependecyList);
            SetUpFunctions(functions);
            SetUpMemberVariables(memVariables);

        }

        private void SetUpLastAcessTime(string lastAcessTime)
        {
            _htmlContent = _htmlContent.Replace(LAST_MODIFIED, lastAcessTime);
        }

        private void SetUpDerievedClassName(string derivedClassName)
        {
            _htmlContent = _htmlContent.Replace(DCLASSNAME, derivedClassName);
        }

        private void SetUpNamespceAndClassName(string namespaceValue, string className, string fileName)
        {
            _htmlContent = _htmlContent.Replace(CLASSNAME, className).Replace(FILENAME, fileName).Replace(NAMESPACE, namespaceValue);
        }
        
        private void SetUpMemberVariables(List<string> memVariables)
        {
            string memberVariables = "<ol style = \"color: rgb(200, 220, 220)\">";
            foreach (string vars in memVariables)
            {
                string[] splitted = vars.Split(' ');
                int size = splitted.Length;
                splitted[size - 1] = splitted[size - 1].Insert(0, "<font color = " + IdentifierSpecifierColour + ">") + "</font>";
                splitted[size - 2] = splitted[size - 2].Insert(0, "<font color = " + DataTypeSpecifierColour + ">") + "</font>";
                string sVal = "";
                bool hasAcessSpecifier = false;
                for (int i = 0; i < splitted.Length; i++)
                {
                    //TODO : Possible problem sets first index as private if no acess specified
                    //public static || static public problem fix
                    string newVal = splitted[i];
                    
                    if (newVal == "public" || newVal == "private" || newVal == "protected")
                    {
                        hasAcessSpecifier = true;
                        newVal = newVal.Insert(0, "<font color = " + AcessSpecifierColour + ">") + "</font>";
                    }
                    else if (i < splitted.Length - 2)
                    {
                        newVal = newVal.Insert(0, "<font color = " + IdentifierSpecifierColour + ">") + "</font>";
                    }
                    sVal += newVal + " ";
                }
                if (!hasAcessSpecifier)
                    sVal = sVal.Insert(0, "<font color = " + AcessSpecifierColour + ">private </font>");

                memberVariables += "<li>" + sVal + "</li>";
            }
            memberVariables += "</ol>";
            _htmlContent = _htmlContent.Replace(MEMVARS, memberVariables);
        }
        private void SetUpDependencies(List<string> dependecyList)
        {
            string dependencies = "<ol style = \"color: rgb(200, 220, 220)\">";
            foreach (string depend in dependecyList)
            {
                dependencies += "<li>" + depend + "</li>";
            }

            dependencies += "</ol>";
            _htmlContent = _htmlContent.Replace(DEPENDENCIES, dependencies);
        }

        private void SetUpFunctions(List<string> functions)
        {
            string memberFunctions = "<ol style = \"color: rgb(200, 220, 220)\">";
            foreach (string func in functions)
            {
                string[] components = func.Split(' ');
                components[0] = components[0].Insert(0, "<font color = " + AcessSpecifierColour + ">");
                components[0] += " </font>";
                components[1] = components[1].Insert(0, "<font color = " + DataTypeSpecifierColour + ">");
                components[1] += " </font>";
                components[2] = "<font color = " + IdentifierSpecifierColour + ">" + func.Substring(func.IndexOf(components[2]));
                components[2] += " </font>";
                string newFunc = components[0] + components[1] + components[2];
                memberFunctions += "<li>" + newFunc + "</li>";
            }
            memberFunctions += "</ol>";
            _htmlContent = _htmlContent.Replace(FUNCTIONS, memberFunctions);
        }

        private void SetUpTodos(List<string> todos)
        {
            string classTodos = "<ul style = \"color: rgb(200, 220, 220)+'\">";
            foreach (string todo in todos)
            {
                classTodos += "<li>" + todo + "</li>";
            }
            classTodos += "</ul>";

            _htmlContent = _htmlContent.Replace(TODOS, classTodos);
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

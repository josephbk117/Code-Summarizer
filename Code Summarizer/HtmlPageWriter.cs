﻿using System;
using System.IO;
using System.Collections.Generic;


namespace Code_Summarizer
{
    class HtmlPageWriter
    {
        const string CLASSNAME = "#CLASSNAME#";
        const string DCLASSNAME = "#DCLASSNAME#";
        const string FILENAME = "#FILENAME#";
        const string FUNCTIONS = "#FUNCTIONS#";
        const string DEPENDENCIES = "#DEPENDENCIES#";
        const string MEMVARS = "#MEMVARS#";
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
                    Console.WriteLine(_htmlContent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        public void setContent(string className, string dClassName,List<string> functions, List<string> memVariables,List<string> dependecyList,string fileName)
        {
            _htmlContent = _htmlContent.Replace(CLASSNAME, className).Replace(FILENAME,fileName);
            _htmlContent = _htmlContent.Replace(DCLASSNAME, dClassName);
            string memberFunctions = "<ol style = \"color: rgb(200, 220, 220)\">";
            foreach (string func in functions)
            {
                memberFunctions += "<li>" + func + "</li>";
            }
            memberFunctions += "</ol>";
            _htmlContent = _htmlContent.Replace(FUNCTIONS, memberFunctions);

            string dependencies = "<ol style = \"color: rgb(200, 220, 220)\">";
            foreach (string depend in dependecyList)
            {
                dependencies += "<li>"+ depend +"</li>";
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

            
            Console.WriteLine("--------After replace ----" + _htmlContent);
        }
        public void outputWebPage(string outputPath)
        {
            using (StreamWriter sw = new StreamWriter(outputPath))
            {
                sw.WriteLine(_htmlContent);
                sw.Flush();
            }
        }
    }
}

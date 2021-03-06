﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Code_Summarizer
{
    class ScriptFileInfo
    {
        public string _pathName;
        private string _className;
        private string _derievedClass;
        private string _namespace;
        private string _lastModified;
        private List<string> _memberVariables;
        private List<string> _memberFunctions;
        private List<string> _dependencies;
        private List<string> _todos;

        //To help out with removing function body
        partial class Pair
        {
            private int Value1;
            private int Value2;

            public Pair(int val1, int val2)
            {
                Value1 = val1;
                Value2 = val2;
            }
            public int GetValue1()
            {
                return Value1;
            }
            public int GetValue2()
            {
                return Value2;
            }
        }

        public ScriptFileInfo(string pathName)
        {
            this._pathName = pathName;
            _className = "NULL";
            _derievedClass = "None";
            _namespace = "None";
            _lastModified = "NULL";
            _memberVariables = new List<string>();
            _memberFunctions = new List<string>();
            _dependencies = new List<string>();
            _todos = new List<string>();
        }
        /// <summary>
        /// Get the different pieces of data from the class
        /// </summary>
        public void Analyze()
        {
            String line = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(_pathName))
                {
                    line = sr.ReadToEnd();
                    FileInfo info = new FileInfo(_pathName);
                    _lastModified = info.LastWriteTime.Day + "/" + info.LastWriteTime.Month + "/" +
                        info.LastWriteTime.Year + " - " + info.LastWriteTime.ToLongTimeString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return;
            }
            //Remove regions to allow correct extraction
            line = RemoveRegionsAndHashStatements(line);
            //Extract todos
            line = ExtractTodos(line);
            //Remove Comments
            line = RemoveComments(line);
            //Extract Dependencies
            ExtractUsingStatements(line); //order is important
            //Extract namespace
            line = ExtractNamespace(line);
            //Extract class and derieve class name
            line = ExtractClassAndDerievedClass(line);
            //Remove body of functions
            line = RemoveFunctionBodies(line);
            //First extract functions then only variables
            line = ExtractMemberFunctions(line);
            //Extact variables
            ExtractMemberVariables(line);
        }

        private static string RemoveRegionsAndHashStatements(string line)
        {
            Regex rgx = new Regex(@"#.*");
            foreach (Match match in rgx.Matches(line))
            {
                line = line.Replace(match.Value, "");
            }
            return line;
        }
        private void ExtractMemberVariables(string line)
        {
            bool hasSeenEquals = false;
            string newString = "";
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '=')
                {
                    hasSeenEquals = true;
                }
                else if (line[i] == ';')
                    hasSeenEquals = false;
                if (!hasSeenEquals)
                    newString += line[i];
            }

            string variablePattern = @"(\w+\s+\w+\s+\w+\s+\w+)|(\w+\s+\w+\s+\w+\[\]\s+\w+)|(\w+\s+\w+\[\]\s+\w+)|(\w+\<\w+\>\s+\w+)|(\w+\s+\w+\<\w+\>\s+\w+)|(\w+\s+\w+\s+\w+)|(\w+\s+\w+)";
            Regex variableRegex = new Regex(variablePattern);
            foreach (Match match in variableRegex.Matches(newString))
            {
                if (!_memberVariables.Contains(match.Value))
                    _memberVariables.Add(match.Value);
            }
        }

        private string ExtractMemberFunctions(string line)
        {
            string functionPattern = @"(\w+\s+\w+\[.*\]\s*\w+\s*?\(.*\))|(\w+\[.*\]\s*\w+\s*?\(.*\))|(\w+\s+\w+\s*\<\w+\>\s*\w+\s*?\(.*\))|(\w+\s*\<\w+\>\s*\w+\s*?\(.*\))|(\w+\s+\w+\s+\w+\s*?\(.*\))";
            Regex functionRegex = new Regex(functionPattern);
            foreach (Match match in functionRegex.Matches(line))
            {
                //TODO potential error zone can not support more than one space b/w words
                //TODO : to prevent  non functions to be added check if the coming chars after the ( is the {
                string func = match.Value;

                string temp = func.Substring(0, match.Value.IndexOf('(') - 1);

                int numOfSpaces = temp.Split(' ').Length - 1;
                Console.WriteLine("Function name = " + func + ", spaces = " + numOfSpaces);
                if (numOfSpaces == 1)
                {
                    func = func.Insert(0, "private ");
                }
                _memberFunctions.Add(func);
                line = line.Replace(match.Value, "");
            }
            return line;
        }

        private static string RemoveFunctionBodies(string line)
        {
            Stack<int> bracketPosition = new Stack<int>();
            List<Pair> codeSections = new List<Pair>();
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '{')
                    bracketPosition.Push(i);
                else if (line[i] == '}')
                {
                    if (bracketPosition.Count == 1)
                    {
                        codeSections.Add(new Pair(bracketPosition.Peek(), i));
                    }
                    bracketPosition.Pop();
                }
            }
            string temp = line;
            foreach (Pair pair in codeSections)
            {
                Console.WriteLine(line.Substring(pair.GetValue1(), pair.GetValue2() - pair.GetValue1()));
                temp = temp.Replace(line.Substring(pair.GetValue1(), (pair.GetValue2() + 1) - pair.GetValue1()), "");
            }
            line = temp;
            return line;
        }
        //TODO : Do recursive class searches ...  
        private string ExtractClassAndDerievedClass(string line)
        {
            string classAndDerievedClassPattern = @"(class\s+\w+\s+:\s+\w+)|(class\s+\w+)";
            Regex classAndDerievedClassRegex = new Regex(classAndDerievedClassPattern);
            string completeValue = "";

            completeValue = classAndDerievedClassRegex.Match(line).Value;
            line = line.Replace(completeValue, "");
            //Support both derieved no derivation
            if (completeValue.Contains(":"))
            {
                _className = completeValue.Substring(0, completeValue.IndexOf(":") - 1).Replace("class", "").Trim();
                _derievedClass = completeValue.Substring(completeValue.IndexOf(":") + 1).Trim();
            }
            else
            {
                _className = completeValue.Replace("class", "").Trim();
                _derievedClass = "None";
            }

            int indexOfFirstOpeningBracket = line.IndexOf('{');
            int indexOfLastClosingBracket = line.LastIndexOf('}');
            line = line.Substring(indexOfFirstOpeningBracket + 1, (indexOfLastClosingBracket - 1) - (indexOfFirstOpeningBracket + 1));

            return line;
        }

        private void ExtractUsingStatements(string line)
        {
            string usingPattern = @"using\s+\w+.*";
            Regex usingRegex = new Regex(usingPattern);
            foreach (Match match in usingRegex.Matches(line))
            {
                _dependencies.Add(match.Value.Substring(6).Replace(";", ""));
            }
        }

        private string ExtractNamespace(string line)
        {
            string namespacePattern = @"namespace\s+\w+";
            Regex namespaceRgx = new Regex(namespacePattern);

            bool isThereNamespace = false;
            int matchIndex = 0;
            foreach (Match match in namespaceRgx.Matches(line))
            {
                matchIndex = match.Index;
                _namespace = match.Value.Substring(10);
                isThereNamespace = true;
            }
            if (isThereNamespace)
            {
                int posOfBracket = line.IndexOf('{', matchIndex);
                int posOfLastBracket = line.LastIndexOf('}');
                line = line.Substring(posOfBracket + 1, (posOfLastBracket - 1) - posOfBracket);
            }

            return line;
        }

        private string ExtractTodos(string line)
        {
            Regex rgx = new Regex(@"//TODO.*\r\n");
            foreach (Match match in rgx.Matches(line))
            {
                _todos.Add(match.Value.Substring(7));
                line = line.Replace(match.Value, "");
            }
            return line;
        }

        private string RemoveComments(string line)
        {
            Regex comments = new Regex(@"//.*\r\n");
            foreach (Match match in comments.Matches(line))
            {
                line = line.Replace(match.Value, "");
            }
            //Remove multi line comments
            Stack<int> multilineCmtPos = new Stack<int>();
            for (int i = 0; i < line.Length - 1; i++)
            {
                if (line[i] == '/' && line[i + 1] == '*')
                {
                    multilineCmtPos.Push(i);
                }
                if (multilineCmtPos.Count >= 1)
                {
                    if (line[i] == '*' && line[i + 1] == '/')
                    {
                        int index = multilineCmtPos.Pop();
                        line = line.Remove(index, i - index);
                    }
                }
            }
            return line;
        }

        private string RemoveMultipleSpaces(string text)
        {
            string newText = "";
            int spaceCount = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                    spaceCount++;
                else
                    spaceCount = 0;
                if (spaceCount < 2)
                    newText += text[i];
            }
            return newText;
        }

        public List<string> GetDependencies()
        {
            return _dependencies;
        }
        public List<string> GetMemberFunctions()
        {
            return _memberFunctions;
        }
        public List<string> GetMemberVariables()
        {
            return _memberVariables;
        }
        public List<string> GetTodos()
        {
            if (_todos.Count == 0)
            {
                _todos.Add("No Todos");
            }
            return _todos;
        }
        public string GetClassName()
        {
            return _className;
        }
        public string GetDerievedClass()
        {
            return _derievedClass;
        }
        public string GetNamespace()
        {
            return _namespace;
        }
        public string GetFileAcsessDate()
        {
            return _lastModified;
        }
    }
}
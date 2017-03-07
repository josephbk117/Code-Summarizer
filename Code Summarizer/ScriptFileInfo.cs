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

        private bool enteredMainBody = false;
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
            //Extract todos
            line = ExtractTodos(line);
            //Extract Dependencies
            ExtractUsingStatements(line); //order is important
            //Extract namespace
            line = ExtractNamespace(line);
            //Extract class and derieve class name
            line = ExtractClassAndDerievedClass(line);

            int firstusingStatement = line.IndexOf('{');
            int lastIndexOfBracket = line.LastIndexOf('}') - 1;
            line = line.Substring(firstusingStatement, lastIndexOfBracket - firstusingStatement);
            string[] chunk = line.Split(';');
            for (int i = 0; i < chunk.Length; i++)
            {

                if (!chunk[i].Contains("(") && enteredMainBody == false)
                {

                    chunk[i] = chunk[i].Trim();
                    string pattern = @"(\w+\s+\w+\s+\w+.*\=)|(\w+\s+\w+)";
                    Match result = Regex.Match(chunk[i], pattern);

                    while (result.Success)
                    {
                        Console.WriteLine("Results : " + result.Value);
                        _memberVariables.Add(result.Value.Replace("=", "").Trim());
                        result = result.NextMatch();
                    }
                }

                if (chunk[i].Contains("{"))
                {
                    chunk[i] = chunk[i].Trim();

                    string dataType = "";
                    string functionName = "";

                    if (chunk[i].Contains("(") && chunk[i].Contains(")"))
                    {
                        enteredMainBody = true;

                        chunk[i] = chunk[i].Replace("\r\n", "").Replace("}", "").Replace("{", "").Trim();
                        int firstBlankSpacePos = chunk[i].IndexOf(' ');
                        int firstBracketPos = chunk[i].IndexOf('(');
                        int closingBracketPos = chunk[i].IndexOf(')', firstBlankSpacePos);
                        dataType = chunk[i].Substring(0, firstBlankSpacePos);
                        functionName = chunk[i].Substring(firstBlankSpacePos, closingBracketPos);
                        functionName = functionName.Substring(0, functionName.IndexOf(')') + 1);
                        dataType = dataType.Trim().Replace(" ", "");

                        string combined = dataType + functionName;
                        string tempWithoutParameter = combined.Substring(0, combined.IndexOf('('));
                        int wordCount = tempWithoutParameter.Split(' ').Length - 1;

                        if (wordCount == 1)
                        {
                            combined = combined.Insert(0, "private ");
                        }

                        _memberFunctions.Add(combined);
                    }
                }
            }
        }

        private string ExtractClassAndDerievedClass(string line)
        {
            string classAndDerievedClassPattern = @"(class\s+\w+\s+:\s+\w+)|(class\s+\w+)";
            Regex classAndDerievedClassRgx = new Regex(classAndDerievedClassPattern);
            string completeValue = "";
            foreach (Match match in classAndDerievedClassRgx.Matches(line))
            {
                completeValue = match.Value;
            }
            line = line.Replace(completeValue, "");

            _className = completeValue.Substring(0, completeValue.IndexOf(":") - 1).Replace("class", "").Trim();
            _derievedClass = completeValue.Substring(completeValue.IndexOf(":") + 1).Trim();

            return line;
        }

        private void ExtractUsingStatements(string line)
        {
            string usingPattern = @"using\s+\w+.*";
            Regex usingRegex = new Regex(usingPattern);
            foreach (Match match in usingRegex.Matches(line))
            {
                Console.WriteLine("Regex value = " + match.Value + " at index " + match.Index);
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
                line = line.Substring(posOfBracket - 1, (posOfLastBracket - 1) - posOfBracket);
                Console.WriteLine("After formatting : " + line);
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

            Regex comments = new Regex(@"//.*\r\n");
            foreach (Match match in comments.Matches(line))
            {
                line = line.Replace(match.Value, "");
            }
            return line;
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

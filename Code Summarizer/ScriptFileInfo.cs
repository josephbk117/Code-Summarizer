using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Code_Summarizer
{
    class ScriptFileInfo
    {
        public string _pathName;
        string _className;
        string _derievedClass;
        List<string> _memberVariables;
        List<string> _memberFunctions;
        List<string> _dependencies;
        List<string> _todos;

        bool enteredMainBody = false;
        public ScriptFileInfo(string pathName)
        {
            this._pathName = pathName;
            _className = "NULL";
            _derievedClass = "None";
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return;
            }
            Regex rgx = new Regex(@"//TODO.*\r\n");

            foreach (Match match in rgx.Matches(line))
            {
                Console.WriteLine("Regex value = " + match.Value + " at index " + match.Index);
                _todos.Add(match.Value.Substring(7));
                line = line.Replace(match.Value, "");
            }
            
            int firstusingStatement = line.IndexOf("using");
            int lastIndexOfBracket = line.LastIndexOf('}') - 1;
            line = line.Substring(firstusingStatement, lastIndexOfBracket);
            string[] chunk = line.Split(';');
            for (int i = 0; i < chunk.Length; i++)
            {                
                if (chunk[i].Contains("using"))
                {
                    int indexOfG = chunk[i].IndexOf('g') + 1;
                    chunk[i] = chunk[i].Trim(' ');
                    _dependencies.Add(chunk[i].Substring(indexOfG));
                }

                else if (chunk[i].Contains("{"))
                {
                    chunk[i] = chunk[i].Trim();

                    string dataType = "";
                    string functionName = "";

                    if (chunk[i].Contains("class"))
                    {
                        int endOfClassChar = chunk[i].IndexOf(' ');
                        int classOpeningBracket = chunk[i].IndexOf('{');
                        string classAndDerived = chunk[i].Substring(endOfClassChar, classOpeningBracket).Replace(" ", "").Replace("{", "").Trim();
                        Console.WriteLine("-----*****Class and derieved = " + classAndDerived);

                        int colonPos = classAndDerived.IndexOf(":");
                        try
                        {
                            _className = classAndDerived.Substring(0, colonPos);
                        }
                        catch
                        {
                            _className = classAndDerived;
                        }
                        try
                        {
                            _derievedClass = classAndDerived.Substring(colonPos, Math.Abs(_className.Length - classAndDerived.Length));
                        }
                        catch
                        {
                            _derievedClass = " None";
                        }
                        if (_derievedClass.Length <= 1)
                        {
                            _derievedClass = " None";
                        }
                        chunk[i] = chunk[i].Remove(0, classOpeningBracket + 1);
                        chunk[i + 1] = chunk[i + 1].Insert(0, chunk[i] + "\r\n");
                    }
                    else if (chunk[i].Contains("(") && chunk[i].Contains(")"))
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
                else
                {
                    if (!chunk[i].Contains("(") && enteredMainBody == false)
                    {

                        chunk[i] = chunk[i].Trim();
                        string pattern = @"(public \w+ \w+)|(private \w+ \w+)|(protected \w+ \w+)|(static \w+ \w+)|(const \w+ \w+)|(readonly \w+ \w+)|(\w+ \w+)";
                        Match result = Regex.Match(chunk[i], pattern);

                        while (result.Success)
                        {
                            Console.WriteLine("Results : " + result.Value);
                            _memberVariables.Add(result.Value);
                            result = result.NextMatch();
                        }
                    }
                }
            }
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
        

    }
}

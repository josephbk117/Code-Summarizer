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

        bool enteredMainBody = false;
        public ScriptFileInfo(string pathName)
        {
            this._pathName = pathName;
            _className = "NULL";
            _derievedClass = "None";
            _memberVariables = new List<string>();
            _memberFunctions = new List<string>();
            _dependencies = new List<string>();
        }

        public void Analyze()
        {
            String line = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(_pathName))
                {

                    line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            int firstusingStatement = line.IndexOf("using");
            int lastIndexOfBracket = line.LastIndexOf('}') - 1;
            line = line.Substring(firstusingStatement, lastIndexOfBracket);
            string[] chunk = line.Split(';');
            for (int i = 0; i < chunk.Length; i++)
            {
                Console.WriteLine("Line :" + i + " " + chunk[i]);
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
                        
                        chunk[i] = chunk[i].Replace("\r\n","").Replace("}","").Replace("{","").Trim();
                        int firstBlankSpacePos = chunk[i].IndexOf(' ');
                        int firstBracketPos = chunk[i].IndexOf('(');
                        int closingBracketPos = chunk[i].IndexOf(')', firstBlankSpacePos);
                        dataType = chunk[i].Substring(0, firstBlankSpacePos);
                        functionName = chunk[i].Substring(firstBlankSpacePos, closingBracketPos);
                        _memberFunctions.Add(dataType + " " + functionName);
                    }
                }
                else
                {
                    Console.WriteLine("Chunk before regex = " + chunk[i]);

                    if (!chunk[i].Contains("(") && enteredMainBody == false)
                    {
                        Console.WriteLine("Chunk no brackets : " + chunk[i]);
                        chunk[i] = chunk[i].Trim();
                        string pattern = @"\w+ \w+";
                        Match result = Regex.Match(chunk[i], pattern);

                        while (result.Success)
                        {
                            Console.WriteLine("Rsults : " + result.Value);
                            _memberVariables.Add(result.Value);
                            result = result.NextMatch();
                        }
                    }
                }
            }
        }
        public List<string> getDependencies()
        {
            return _dependencies;
        }
        public List<string> getMemberFunctions()
        {
            return _memberFunctions;
        }
        public List<string> getMemberVariables()
        {
            return _memberVariables;
        }
        public string getClassName()
        {
            return _className;
        }
        public string getDerievedClass()
        {
            return _derievedClass;
        }
        
    }
}

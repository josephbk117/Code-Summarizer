using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CodeSummarizer
{
    public class ClassData : ICodeBaseData
    {
        public string Namespace { set; get; }
        public string ClassName { set; get; }
        public string DerievedClass { set; get; }
        public List<FunctionData> Functions { set; get; }
        public List<VariableData> Variables { set; get; }

        public ClassData()
        {
            Functions = new List<FunctionData>();
            Variables = new List<VariableData>();
        }

        public void Analyze(string dataText)
        {
            dataText = RemoveFunctionBodies(dataText);
            Logger.OutputToLog("After removing function bodies : " + Environment.NewLine + dataText);
            dataText = ExtractFunctions(dataText);
            Logger.OutputToLog("After removing functions : " + Environment.NewLine + dataText);
            ExtractMemberVariables(dataText);
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
                Logger.OutputToLog("Variable to analyze :- " + match.Value);
                VariableData vars = new VariableData(match.Value);
                Variables.Add(vars);
            }
        }

        private string ExtractFunctions(string line)
        {

            string functionPattern = @"(\w+\s+\w+\s*?\(.*\))|(\w+\s+\w+\[.*\]\s*\w+\s*?\(.*\))|(\w+\[.*\]\s*\w+\s*?\(.*\))|(\w+\s+\w+\s*\<\w+\>\s*\w+\s*?\(.*\))|(\w+\s*\<\w+\>\s*\w+\s*?\(.*\))|(\w+\s+\w+\s+\w+\s*?\(.*\))";
            Regex functionRegex = new Regex(functionPattern);
            foreach (Match match in functionRegex.Matches(line))
            {
                string func = match.Value;
                string temp = func.Substring(0, match.Value.IndexOf('(') - 1);

                FunctionData funcData = new FunctionData(match.Value);

                Functions.Add(funcData);
                line = line.Replace(match.Value, "");
            }
            return line;
        }

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

        private static string RemoveFunctionBodies(string line)
        {
            Stack<int> bracketPosition = new Stack<int>();
            List<Pair> codeSections = new List<Pair>();
            bool hasSeenFunction = false;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ')')
                {
                    hasSeenFunction = true;
                }
                if (hasSeenFunction)
                {
                    if (line[i] == '{')
                        bracketPosition.Push(i);
                    else if (line[i] == '}')
                    {
                        if (bracketPosition.Count == 1)
                        {
                            codeSections.Add(new Pair(bracketPosition.Peek(), i));
                            hasSeenFunction = false;
                        }
                        bracketPosition.Pop();
                    }
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
    }
}

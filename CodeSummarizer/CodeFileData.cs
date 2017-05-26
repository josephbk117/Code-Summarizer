using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
//TODO : 
//Segement the code according to scope
//Global > Namespace > Class = Interfaces >!= Enums > Variables = Functions 

namespace CodeSummarizer
{
    public class CodeFileData
    {
        //contains interfaces,enums,classes,dependencies,acess time,modification time,size in MB
        public List<ClassData> classes;
        public List<InterfaceData> interfaces;
        public List<EnumData> enums;
        public List<string> dependencies;
        public List<string> todos;
        long storageSize = 0;
        public string lastModifiedTime = "";
        private string codeFileContent;
        string fileName = "";

        public CodeFileData(string filePath)
        {            
            codeFileContent = File.ReadAllText(filePath);
            storageSize = File.ReadAllBytes(filePath).Length;
            lastModifiedTime = File.GetLastWriteTime(filePath).ToLongDateString() + " " + File.GetLastWriteTime(filePath).ToLongTimeString();
            fileName = filePath;

            //Formatting correclty for evaluation
            codeFileContent = RemoveRegionsAndHashStatements(codeFileContent);
            codeFileContent = ExtractTodos(codeFileContent);
            codeFileContent = RemoveComments(codeFileContent); //By this time there is no regions and comments & todos extracted
            codeFileContent = RemoveExtraSpaces(codeFileContent);

            //Responsibilty to section the code to different parts            
            ExtractUsingStatements(codeFileContent);
            ExtractEnumStatements(codeFileContent);
            ExtractInterfaces(codeFileContent);
            
            Logger.OutputToLog("INITIAL FORMATTED RESULT : " + Environment.NewLine + codeFileContent);
            ExtractClasses(codeFileContent);
                        
        }

        private void LogOutput()
        {
            Logger.OutputToLog(fileName + ", Last Modified : " + lastModifiedTime + Environment.NewLine);
            foreach (string dep in dependencies)
            {
                Logger.OutputToLog("Using : " + dep + Environment.NewLine);
            }
            Logger.OutputToLog("Interfaces used : " + Environment.NewLine);
            foreach (InterfaceData iData in interfaces)
            {
                Logger.OutputToLog("Interface : " + iData.InterfaceName + Environment.NewLine);
                foreach (FunctionData func in iData.Functions)
                {
                    Logger.OutputToLog("    " + func.ToString() + Environment.NewLine);
                }
            }
            Logger.OutputToLog("Enums Used : " + Environment.NewLine);
            foreach (EnumData eData in enums)
            {
                Logger.OutputToLog("Enum : " + eData.EnumName + Environment.NewLine);
                foreach (string eList in eData.EnumValues)
                {
                    Logger.OutputToLog("    " + eList + Environment.NewLine);
                }
            }
            Logger.OutputToLog("Todos ::" + Environment.NewLine);
            foreach (string todo in todos)
            {
                Logger.OutputToLog("    " + todo);
            }

            foreach (ClassData cData in classes)
            {
                Logger.OutputToLog("Class : " + cData.ClassName + " Derieved From " + cData.DerievedClass + Environment.NewLine);
                Logger.OutputToLog("Variables : " + Environment.NewLine);
                foreach (VariableData vars in cData.Variables)
                {
                    Logger.OutputToLog("    " + vars.ToString() + Environment.NewLine);
                }
                Logger.OutputToLog("Functions : " + Environment.NewLine);
                foreach (FunctionData funcs in cData.Functions)
                {
                    Logger.OutputToLog("    " + funcs.ToString() + Environment.NewLine);
                }
            }
        }

        private void ExtractInterfaces(string codeFileContent)
        {
            interfaces = new List<InterfaceData>();
            string usingPattern = @"interface\s+\w+";
            Regex usingRegex = new Regex(usingPattern);
            foreach (Match match in usingRegex.Matches(codeFileContent))
            {
                int indexOfInterface = match.Index;

                string namespaceText = "";
                if (codeFileContent.Contains("namespace "))
                {
                    int indexOfNamespace = codeFileContent.LastIndexOf("namespace ", indexOfInterface);
                    int iDx = indexOfNamespace + 10;
                    while (codeFileContent[iDx] != '{')
                    {
                        namespaceText += codeFileContent[iDx];
                        iDx++;
                    }
                    namespaceText = namespaceText.Trim();
                }
                else
                    namespaceText = "None";

                InterfaceData iData = new InterfaceData()
                {
                    InterfaceName = match.Value.Substring(10),
                    Namespace = namespaceText
                };
                string data = "";

                int fBracket = codeFileContent.IndexOf('{', match.Index);
                int lBracket = codeFileContent.IndexOf('}', fBracket + 1);

                data = codeFileContent.Substring(fBracket, lBracket - fBracket);

                iData.Analyze(data);
                interfaces.Add(iData);
            }
        }

        private void ExtractClasses(string codeFileContent)
        {
            classes = new List<ClassData>();
            string usingPattern = @"class\s+\w+";
            Regex usingRegex = new Regex(usingPattern);

            //Before analyzing class content , assign namespace to the class.
            //Find last instance of namespace before match found index
            foreach (Match match in usingRegex.Matches(codeFileContent))
            {
                int indexOfClass = match.Index;
                //start index fromthere and back wards till front
                string namespaceText = "";
                if (codeFileContent.Contains("namespace "))
                {
                    int indexOfNamespace = codeFileContent.LastIndexOf("namespace ", indexOfClass);
                    int iDx = indexOfNamespace + 10;
                    while (codeFileContent[iDx] != '{')
                    {
                        namespaceText += codeFileContent[iDx];
                        iDx++;
                    }
                    namespaceText = namespaceText.Trim();
                }
                else
                    namespaceText = "None";
                Logger.OutputToLog("Namespace : " + namespaceText + Environment.NewLine);
                ClassData cd = new ClassData()
                {
                    Namespace = namespaceText
                };
                Stack<int> bracketPosition = new Stack<int>();
                bool hasEnteredClassBlock = false;
                string newStartString = codeFileContent.Substring(indexOfClass);
                ExtractClassAndDerievedClass(newStartString, cd);
                int classStartIndex = 0;
                int classEndIndex = 0;
                bool isValidClass = true;
                for (int i = 0; i < newStartString.Length; i++)
                {                    
                    if(!hasEnteredClassBlock)
                    {
                        if(!newStartString.Contains("{") && !newStartString.Contains("}"))
                        {
                            isValidClass = false;
                            break;
                        }
                    }

                    if (newStartString[i] == '{')
                    {
                        bracketPosition.Push(i);
                        if (!hasEnteredClassBlock)
                        {
                            hasEnteredClassBlock = true;
                            classStartIndex = i + 1;
                        }
                    }
                    else if (newStartString[i] == '}')
                    {
                        if (bracketPosition.Count <= 0)
                        {
                            isValidClass = false;
                            break;
                        }
                        bracketPosition.Pop();
                    }

                    if (bracketPosition.Count == 0 && hasEnteredClassBlock)
                    {
                        classEndIndex = i;
                        break;
                    }
                }
                if (isValidClass)
                {
                    try
                    {
                        newStartString = newStartString.Substring(classStartIndex, classEndIndex - classStartIndex);
                        Logger.OutputToLog("Analysis of class Data : " + Environment.NewLine + newStartString);
                        cd.Analyze(newStartString);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error : class start index = " + classStartIndex + ", end index = " + classEndIndex + " , class : " + cd.ClassName);
                        Console.WriteLine(e.StackTrace);
                    }                    
                    classes.Add(cd);
                }
            }
        }

        private void ExtractEnumStatements(string codeFileContent)
        {
            enums = new List<EnumData>();
            string usingPattern = @"enum\s+\w+";
            Regex usingRegex = new Regex(usingPattern);
            foreach (Match match in usingRegex.Matches(codeFileContent))
            {
                int indexOfEnum = match.Index;
                
                string namespaceText = "";
                if (codeFileContent.Contains("namespace "))
                {
                    int indexOfNamespace = codeFileContent.LastIndexOf("namespace ", indexOfEnum);
                    int iDx = indexOfNamespace + 10;
                    while (codeFileContent[iDx] != '{')
                    {
                        namespaceText += codeFileContent[iDx];
                        iDx++;
                    }
                    namespaceText = namespaceText.Trim();
                }
                else
                    namespaceText = "None";

                EnumData enumData = new EnumData()
                {
                    EnumName = match.Value.Substring(5),
                    //Class = classText,
                    Namespace = namespaceText
                };
                string newText = "";
                int i = match.Index + match.Value.Length;
                while (codeFileContent[i] != '}')
                {
                    newText += codeFileContent[i];
                    i++;
                }
                enumData.Analyze(newText);
                enums.Add(enumData);
            }
        }

        private void ExtractUsingStatements(string line)
        {
            dependencies = new List<string>();
            string usingPattern = @"using\s+\w+.*";
            Regex usingRegex = new Regex(usingPattern);
            foreach (Match match in usingRegex.Matches(line))
            {
                Console.WriteLine("extracted : " + match.Value);
                dependencies.Add(match.Value.Substring(6).Replace(";", ""));
            }
            if(dependencies.Count < 1)
            {
                dependencies.Add("None");
            }
        }

        private void ExtractClassAndDerievedClass(string line, ClassData classData)
        {
            string classAndDerievedClassPattern = @"(class\s+\w+\s+:\s+\w+)|(class\s+\w+)";
            Regex classAndDerievedClassRegex = new Regex(classAndDerievedClassPattern);
            string completeValue = "";

            completeValue = classAndDerievedClassRegex.Match(line).Value;
            if (completeValue.Length > 0)
                line = line.Replace(completeValue, "");
            //Support both derieved & no derivation
            if (completeValue.Contains(":"))
            {
                classData.ClassName = completeValue.Substring(0, completeValue.IndexOf(":") - 1).Replace("class", "").Trim();
                classData.DerievedClass = completeValue.Substring(completeValue.IndexOf(":") + 1).Trim();
            }
            else
            {
                classData.ClassName = completeValue.Replace("class", "").Trim();
                classData.DerievedClass = "None";
            }
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
        private static string RemoveRegionsAndHashStatements(string line)
        {
            Regex rgx = new Regex(@"#.*");
            foreach (Match match in rgx.Matches(line))
            {
                line = line.Replace(match.Value, "");
            }
            return line;
        }

        private string ExtractTodos(string line)
        {
            todos = new List<string>();
            Regex rgx = new Regex(@"//TODO.*\r\n");
            foreach (Match match in rgx.Matches(line))
            {
                todos.Add(match.Value.Substring(7));
                line = line.Replace(match.Value, "");
            }
            return line;
        }

        private string RemoveExtraSpaces(string text)
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
    }
}

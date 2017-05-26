using System;
using System.Collections.Generic;

namespace CodeSummarizer
{
    //Contains info about classes and in which other cs files they show up
    //Reads the classes and also checks the other code file datas and see it the class was used
    //Either as memeber variable or function
    //Only one instance of Project Info

    //Go through each code file and get the classes used
    public static class ProjectInfo
    {
        /// <summary>
        /// Contains info about what all classes, enums and interfaces the current class is using
        /// </summary>
        public static Dictionary<ClassData, List<string>> classUseDictionary;
        public static Dictionary<ClassData, List<string>> enumUseDictionary;

        public static void Analyze(List<CodeFileData> codeFiles)
        {
            List<ClassData> classes = CollectClasses(codeFiles);
            List<EnumData> enums = CollectEnums(codeFiles);
            classUseDictionary = new Dictionary<ClassData, List<string>>();
            enumUseDictionary = new Dictionary<ClassData, List<string>>();

            //After collection check which classes used variables or functions of type class
            for (int i = 0; i < classes.Count; i++)
            {
                List<string> usedClasses = new List<string>();
                List<string> usedEnums = new List<string>();
                //Takes care of classes used in this class
                AnalyzeClassUsage(classes, i, ref usedClasses);
                classUseDictionary.Add(classes[i], usedClasses);

                AnalyzeEnumUsage(classes, enums, i, ref usedEnums);
                enumUseDictionary.Add(classes[i], usedEnums);
            }
        }

        private static void AnalyzeEnumUsage(List<ClassData> classes, List<EnumData> enums, int i, ref List<string> usedEnums)
        {
            for (int j = 0; j < enums.Count; j++)
            {
                foreach (VariableData vars in classes[i].Variables)
                {
                    if (vars.VariableType == enums[j].EnumName)
                    {
                        if (!usedEnums.Contains(enums[j].EnumName))
                            usedEnums.Add(enums[j].EnumName);
                    }
                }
                foreach (FunctionData funcs in classes[i].Functions)
                {
                    if (funcs.ReturnType == enums[j].EnumName)
                    {
                        if (!usedEnums.Contains(enums[j].EnumName))
                            usedEnums.Add(enums[j].EnumName);
                    }
                }
            }
        }

        private static void AnalyzeClassUsage(List<ClassData> classes, int i, ref List<string> usedClasses)
        {
            for (int j = 0; j < classes.Count; j++)
            {
                //iterate through the mem vars and functions to see if there are any of current class name                    
                //Look through vars and funcs of this class and check if it uses other classes
                //Also check derieved class and also interfaces and enums

                if (classes[i].DerievedClass == classes[j].ClassName)
                    usedClasses.Add(classes[j].ClassName);

                try
                {

                    foreach (VariableData vars in classes[i].Variables)
                    {
                        if (vars.VariableType == classes[j].ClassName)
                        {
                            if (!usedClasses.Contains(classes[j].ClassName))
                                usedClasses.Add(classes[j].ClassName);
                        }
                    }
                    foreach (FunctionData funcs in classes[i].Functions)
                    {
                        if (funcs.ReturnType == classes[j].ClassName)
                        {
                            if (!usedClasses.Contains(classes[j].ClassName))
                                usedClasses.Add(classes[j].ClassName);
                        }
                    }
                }catch
                {
                    Console.WriteLine("ERROR : variable list or function list not inited, " + classes[i].ClassName);
                }
            }
        }

        private static List<ClassData> CollectClasses(List<CodeFileData> codeFiles)
        {
            List<ClassData> cData = new List<ClassData>();
            for (int i = 0; i < codeFiles.Count; i++)
            {
                for (int j = 0; j < codeFiles[i].classes.Count; j++)
                {
                    cData.Add(codeFiles[i].classes[j]);
                }
            }
            return cData;
        }
        private static List<EnumData> CollectEnums(List<CodeFileData> codeFiles)
        {
            List<EnumData> eData = new List<EnumData>();
            for (int i = 0; i < codeFiles.Count; i++)
            {
                for (int j = 0; j < codeFiles[i].enums.Count; j++)
                {
                    eData.Add(codeFiles[i].enums[j]);
                }
            }
            return eData;
        }
    }
}

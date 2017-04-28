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
        public static Dictionary<ClassData, List<string>> classUseDictionary;

        public static void Analyze(List<CodeFileData> codeFiles)
        {
            List<ClassData> classes = CollectClasses(codeFiles);
            classUseDictionary = new Dictionary<ClassData, List<string>>();

            //After collection check which classes used variables or functions of type class
            for (int i = 0; i < classes.Count; i++)
            {
                List<string> usedClasses = new List<string>();
                for (int j = 0; j < classes.Count; j++)
                {
                    //iterate through the mem vars and functions to see if there are any of current class name                    
                    //Look through vars and funcs of this class and check if it uses other classes
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
                }
                classUseDictionary.Add(classes[i], usedClasses);
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
    }
}

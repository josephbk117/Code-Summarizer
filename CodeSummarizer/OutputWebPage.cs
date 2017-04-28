using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummarizer
{
    public class OutputWebPage
    {
        //takes data from Project Info and replaces Values from templates with actual values
        //Outputs single web page
        string templateLocation;
        string fileLocation;

        CodeFileData codeData;

        #region Replacement Constants
        const string FILENAME = "#FILENAME#";
        const string LAST_MODIFIED = "#LASTMODIFIED#";
        const string NAMESPACE = "#NAMESPACE#";

        const string CLASSNAME = "#CLASSNAME#";
        const string DCLASSNAME = "#DCLASSNAME#";
        const string FUNCTIONS = "#FUNCTIONS#";
        const string DEPENDENCIES = "#DEPENDENCIES#";
        const string MEMVARS = "#MEMVARS#";
        const string TODOS = "#TODOS#";

        const string ENUM_NAME = "#ENUMNAME#";
        const string ENUMS = "#ENUMS#";

        const string INTERFACE_NAME = "#INTERFACENAME#";
        const string INTERFACE_FUNCS = "#INTERFACEFUNCS#";
        #endregion

        string classtemplateContent;
        string enumTemplateContent;
        string interfaceTemplateContent;

        string hexId;
        string hexDat;

        public OutputWebPage(string templateLocation, string outputFolderLocation, CodeFileData codeData, string hexId, string hexDat)
        {
            string classTemplateLoc = "Res/Templates/Class Templates/" + templateLocation;
            string enumTemplateLoc = "Res/Templates/Enum Templates/" + templateLocation;
            string interfaceTemplateLoc = "Res/Templates/Interface Templates/" + templateLocation;

            this.templateLocation = templateLocation;
            this.fileLocation = outputFolderLocation;
            this.codeData = codeData;

            this.hexId = hexId;
            this.hexDat = hexDat;

            classtemplateContent = File.ReadAllText(classTemplateLoc);
            enumTemplateContent = File.ReadAllText(enumTemplateLoc);
            interfaceTemplateContent = File.ReadAllText(interfaceTemplateLoc);

        }

        public void GenerateHtmlContent()
        {
            //All lasses get output
            OutputAllClasses(codeData, classtemplateContent);
            //All enums get output
            OutputAllEnums(codeData, enumTemplateContent);
            //All interfaces get output
            OutputAllInterfaces(codeData, interfaceTemplateContent);
        }

        private void OutputAllInterfaces(CodeFileData codeData, string templateContent)
        {
            foreach (InterfaceData iData in codeData.interfaces)
            {
                string content = templateContent.Replace(INTERFACE_NAME, iData.InterfaceName);
                content = content.Replace(LAST_MODIFIED, codeData.lastModifiedTime);
                content = content.Replace(NAMESPACE, iData.Namespace);
                string iValues = "<ul>";
                foreach (FunctionData fVal in iData.Functions)
                {
                    iValues += "<li>" + fVal.ToString() + "</li>";
                }
                iValues += "</ul>";
                content = content.Replace(INTERFACE_FUNCS, iValues);

                File.WriteAllText(fileLocation + "\\" + iData.InterfaceName + "_Interface_Doc.html", content);
            }
        }

        private void OutputAllEnums(CodeFileData codeData, string templateContent)
        {
            foreach (EnumData eData in codeData.enums)
            {
                string content = templateContent.Replace(ENUM_NAME, eData.EnumName);
                content = content.Replace(LAST_MODIFIED, codeData.lastModifiedTime);
                content = content.Replace(NAMESPACE, eData.Namespace);
                string eValues = "<ul>";
                foreach (string eVal in eData.EnumValues)
                {
                    eValues += "<li>" + eVal + "</li>";
                }
                eValues += "</ul>";
                content = content.Replace(ENUMS, eValues);

                File.WriteAllText(fileLocation + "\\" + eData.EnumName + "_Enum_Doc.html", content);
            }
        }

        private void OutputAllClasses(CodeFileData codeData, string templateContent)
        {
            foreach (ClassData cData in codeData.classes)
            {
                string content = templateContent.Replace(LAST_MODIFIED, codeData.lastModifiedTime);
                //get all class references add to list and pass on to the functions
                List<string> usedClasses = new List<string>();

                foreach (string cName in ProjectInfo.classUseDictionary[cData])
                {
                    usedClasses.Add(cName);
                }

                content = SetDependencies(codeData, content);
                content = SetTodos(codeData, content);
                content = SetClassContent(cData, content, usedClasses);

                File.WriteAllText(fileLocation + "\\" + cData.ClassName + "_Class_Doc.html", content);
            }
        }

        private string SetDependencies(CodeFileData codeData, string content)
        {
            string depends = "<ol>";
            foreach (string dep in codeData.dependencies)
            {
                depends += "<li>" + dep + "</li>";
            }
            depends += "</ol>";
            content = content.Replace(DEPENDENCIES, depends);
            return content;
        }

        private string SetTodos(CodeFileData codeData, string content)
        {
            string todos = "<ol>";
            foreach (string todo in codeData.todos)
            {
                todos += "<li>" + todo + "</li>";
            }
            todos += "</ol>";
            content = content.Replace(TODOS, todos);
            return content;
        }

        public string SetClassContent(ClassData classData, string content, List<string> usedClasses)
        {
            content = content.Replace(CLASSNAME, classData.ClassName);
            content = content.Replace(DCLASSNAME, classData.DerievedClass);
            content = content.Replace(NAMESPACE, classData.Namespace);
            content = SetVariables(classData, content, usedClasses);
            content = SetFunctions(classData, content, usedClasses);
            return content;
        }

        private string SetFunctions(ClassData classData, string content, List<string> usedClasses)
        {
            string funcs = "<ul>";
            foreach (FunctionData fData in classData.Functions)
            {
                if (usedClasses.Contains(fData.ReturnType))
                {
                    funcs += $"<li><a href = \"{fData.ReturnType}_Class_Doc.html\"><font color = \"{hexDat}\">{fData.ReturnType}</font></a> <font color = \"{hexId}\">{fData.FunctionName}</font> {fData.GetHtmlParameterString(hexId, hexDat)}</li>";
                }
                else
                    funcs += $"<li><font color = \"{hexDat}\">{fData.ReturnType}</font>  <font color = \"{hexId}\">{fData.FunctionName}</font> {fData.GetHtmlParameterString(hexId, hexDat)}</li>";
            }
            funcs += "</ul>";
            content = content.Replace(FUNCTIONS, funcs);
            return content;
        }

        private string SetVariables(ClassData classData, string content, List<string> usedClasses)
        {

            string vars = "<ul>";
            foreach (VariableData vData in classData.Variables)
            {
                if (usedClasses.Contains(vData.VariableType))
                {
                    vars += $"<li><a href = \"{vData.VariableType}_Class_Doc.html\" ><font color = \"{hexDat}\">{vData.VariableType}</font></a> <font color = \"{hexId}\">{vData.VariableName}</font></li>";
                }
                else
                    vars += $"<li><font color = \"{hexDat}\">{vData.VariableType}</font></a> <font color = \"{hexId}\">{vData.VariableName}</font></li>";
            }
            vars += "</ul>";
            content = content.Replace(MEMVARS, vars);
            return content;
        }
    }
}

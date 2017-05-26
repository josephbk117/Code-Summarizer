using System;

namespace CodeSummarizer
{
    public class VariableData
    {
        public string VariableName { set; get; }
        public string VariableType { set; get; }        

        public VariableData(string variableData)
        {
            int indexOfLastSpace = variableData.LastIndexOf(' ');
            string varName = variableData.Substring(indexOfLastSpace);
            Logger.OutputToLog(" Var name : " + varName + " , ");
            variableData = variableData.Remove(indexOfLastSpace).Trim();

            indexOfLastSpace = variableData.LastIndexOf(' ') + 1;
            string varType = variableData.Substring(indexOfLastSpace);
            Logger.OutputToLog(" Var type : " + varType + Environment.NewLine);
            this.VariableName = varName;
            this.VariableType = varType;

            if (this.VariableType.Contains(">"))
            {
                this.VariableType = this.VariableType.Replace(">", "&gt");
            }
            if (this.VariableType.Contains("<"))
            {
                this.VariableType = this.VariableType.Replace("<", "&lt");
            }
        }
        public override string ToString()
        {
            return VariableType + " " + VariableName;
        }
        public string ToString(string hexId, string hexDat)
        {
            return $"<font color = \"{ hexDat }\">{VariableType}</font> <font color = \"{hexId}\">{VariableName}</font>";
        }
    }
}

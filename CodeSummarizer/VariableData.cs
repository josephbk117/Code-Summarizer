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
            variableData = variableData.Remove(indexOfLastSpace).Trim();

            indexOfLastSpace = variableData.LastIndexOf(' ') + 1;
            string varType = variableData.Substring(indexOfLastSpace);

            this.VariableName = varName;
            this.VariableType = varType;
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

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CodeSummarizer
{
    public class InterfaceData : ICodeBaseData
    {
        public string InterfaceName { set; get; }
        public List<FunctionData> Functions { set; get; }
        public string Namespace { set; get; }

        public void Analyze(string dataText)
        {
            Functions = new List<FunctionData>();
            ExtractInterfaceFunctions(dataText);
        }

        void ExtractInterfaceFunctions(string text)
        {
            string functionPattern = @"(\w+\s+\w+\s*?\(.*\))";
            Regex functionRegex = new Regex(functionPattern);
            foreach (Match match in functionRegex.Matches(text))
            {
                FunctionData fData = new FunctionData(match.Value);
                Functions.Add(fData);
            }
        }
    }
}

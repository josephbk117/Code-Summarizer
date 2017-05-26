using System.Collections.Generic;

namespace CodeSummarizer
{
    public class FunctionData
    {
        public string ReturnType { set; get; }
        public string FunctionName { set; get; }
        public List<VariableData> FunctionParameters { set; get; }

        public FunctionData(string functionData)
        {
            FunctionParameters = new List<VariableData>();
            int indexOfBracket = functionData.IndexOf('(') + 1; //check if next value is ) , then no params
            string parameterBlock = functionData.Substring(indexOfBracket).Replace(")", "").Trim();
            string[] strParams = parameterBlock.Split(',');
            foreach (string param in strParams)
            {
                string str = param.Trim();
                if (str.Length > 1)
                {
                    FunctionParameters.Add(new VariableData(str));
                }
            }
            string withoutParams = functionData.Remove(indexOfBracket - 1).Trim();
            FunctionName = withoutParams.Substring(withoutParams.LastIndexOf(' ') + 1).Trim();

            string withoutRest = withoutParams.Replace(FunctionName, "").Trim();
            ReturnType = withoutRest.Substring(withoutRest.LastIndexOf(' ') + 1).Trim();
            if(ReturnType.Contains(">"))
            {
                ReturnType = ReturnType.Replace(">", "&gt");
            }
            if (ReturnType.Contains("<"))
            {
                ReturnType = ReturnType.Replace("<", "&lt");
            }

        }
        public override string ToString()
        {
            return ReturnType + " " + FunctionName + " " + GetParameterString();
        }
        public string GetParameterString()
        {
            string paramStr = "( ";
            for (int i = 0; i < FunctionParameters.Count; i++)
            {
                if (i == 0)
                    paramStr += FunctionParameters[i].ToString();
                else
                    paramStr += ", " + FunctionParameters[i].ToString().Trim();
            }
            paramStr += " )";
            return paramStr;
        }
        public string GetHtmlParameterString(string hexId, string hexDat)
        {
            string paramStr = "( ";
            for (int i = 0; i < FunctionParameters.Count; i++)
            {
                if (i == 0)
                    paramStr += FunctionParameters[i].ToString(hexId, hexDat);
                else
                    paramStr += ", " + FunctionParameters[i].ToString(hexId, hexDat).Trim();
            }
            paramStr += " )";
            return paramStr;
        }
    }
}

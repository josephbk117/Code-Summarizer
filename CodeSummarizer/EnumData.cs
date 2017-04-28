using System.Collections.Generic;

namespace CodeSummarizer
{
    public class EnumData : ICodeBaseData
    {
        public string EnumName { set; get; }
        public List<string> EnumValues { set; get; }
        public string Class { set; get; }
        public string Namespace { set; get; }

        public EnumData()
        {
            EnumValues = new List<string>();
        }

        public void Analyze(string dataText)
        {
            ExtractEnumValues(dataText);
        }
        private void ExtractEnumValues(string text)
        {
            text = text.Replace("{", "").Trim().Replace("}", "").Trim();
            string[] enumVals = text.Split(',');
            int eVal = 0;
            for (int i = 0; i < enumVals.Length; i++)
            {
                if (enumVals[i].Contains("="))
                {
                    int eqIndex = enumVals[i].IndexOf('=');
                    string numStr = enumVals[i].Substring(eqIndex + 1).Trim();
                    eVal = int.Parse(numStr);
                    enumVals[i] = enumVals[i].Substring(0, eqIndex).Trim() + " = " + eVal;
                    eVal++;
                }
                else
                {
                    enumVals[i] = enumVals[i] + " = " + eVal;
                    eVal++;
                }
                EnumValues.Add(enumVals[i]);
            }
        }
    }
}

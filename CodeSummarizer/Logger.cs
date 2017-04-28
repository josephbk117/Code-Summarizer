using System.IO;

namespace CodeSummarizer
{
    public static class Logger
    {
        public static void ResetLog()
        {
            if(File.Exists("LogOutput.txt"))
            {
                File.Delete("LogOutput.txt");
            }
        }

        public static void OutputToLog(string data)
        {            
            using (StreamWriter sw = File.AppendText("LogOutput.txt"))
            {
                sw.Write(data);
            }
        }
    }
}

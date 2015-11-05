using System;
using System.Diagnostics;

namespace ConsoleApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(MoveFile("C:\\a.txt", "C:\\b.xt"));
            Console.WriteLine(Echo("Echo"));
            Console.WriteLine("Done");
            Console.ReadLine();
        }


        public static CmdOutput Echo(string message)
        {
            return RunCmdProcess($"/C echo {message}");
        }

        public static CmdOutput MoveFile(string sourcePath, string targetPath)
        {
            return RunCmdProcess($"/C move {sourcePath} {targetPath}");
        }

        private static CmdOutput RunCmdProcess(string arguments)
        {
            Process cmd = new Process();
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.FileName = "CMD.exe";
            cmd.StartInfo.Arguments = arguments;
            cmd.Start();
            return new CmdOutput(cmd.StandardOutput.ReadToEnd(), cmd.StandardError.ReadToEnd());
        }
    }

    public class CmdOutput
    {
        public string StdOut { get; private set; }
        public string StdErr { get; private set; }
        public CmdOutput(string stdOut, string stdErr)
        {
            this.StdOut = stdOut.Trim(TrimChars);
            this.StdErr = stdErr.Trim(TrimChars);
        }

        public override string ToString()
        {
            return $"StdOut: {StdOut}, StdErr: {StdErr}";
        }


        private char[] TrimChars = new char[] {'\r', '\n' };
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Arrow.Framework
{
    public static class ExecuteHelper
    {
        #region 执行Dos命令
        /// <summary>
        /// 执行Dos命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static string ExecuteDOS(string cmd)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine(cmd);
            process.StandardInput.WriteLine("exit");
            return process.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// 执行Dos命令，并返回错误信息
        /// </summary>
        /// <param name="cmd">dos命令</param>
        /// <param name="error">返回的错误信息</param>
        /// <returns></returns>
        public static string ExecuteDOS(string cmd, out string error)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine(cmd);
            process.StandardInput.WriteLine("exit");
            error = process.StandardError.ReadToEnd();
            return process.StandardOutput.ReadToEnd();
        }
        #endregion

    }
}

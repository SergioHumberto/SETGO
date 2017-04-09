using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Data.SqlClient;

namespace WebApplicationTemplate.Test
{
    [TestClass]
    public class AssemblyEvents
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            RunScriptCommands("Initialize", "Initialize.sql");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            RunScriptCommands("Cleanup", "Cleanup.sql");
        }

        /// <summary>
        /// Run Scripts Initialize And Cleanup, receives as parameter name of the folder where is found the index Script and the name from index Script
		/// </summary>
		/// <param name="nameFolder"></param>
		/// <param name="nameScript"></param>  
        private static void RunScriptCommands(string nameFolder, string nameScript)
        {
            String Path2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            XmlDocument document = new XmlDocument();
            document.Load(Path2 + @"../../../Properties.config");
            XmlNodeList elemList = document.GetElementsByTagName("add");

            string conexionString = string.Empty;
            //Get conexionString from properties.config
            foreach (XmlElement nodo in elemList)
            {
                conexionString = nodo.GetAttribute("value");
            }

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conexionString);
            string pathFilePreDeployment = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))); //Nos dirigimos a la raiz de proyecto
            pathFilePreDeployment = pathFilePreDeployment + @"\" + nameFolder;
            //Command for sql
            string arguments = @"sqlcmd -S " + builder.DataSource + " -U " + builder.UserID + " -P " + builder.Password + " -d  " + builder.InitialCatalog + @"  -i  """ + pathFilePreDeployment + @"\" + nameScript + @"""";
            ExecuteCommand(arguments, pathFilePreDeployment); 
        }

        /// <summary>
        /// ExecuteCommand
        /// </summary>
        /// <param name="arguments">Commands SQL</param>
        /// <param name="p_WorkingDirectory">Execution path</param>
        public static void ExecuteCommand(string p_arguments, string p_WorkingDirectory = null)
        {
            string strStandardError, strStandardOutput;
            Process objProcess = new Process();

            #region ProcessInfo
            var objProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + p_arguments)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            if (p_WorkingDirectory != null)
            {
                objProcessInfo.WorkingDirectory = p_WorkingDirectory;   //Execution path.             
            }
            #endregion

            #region Process start
            objProcess.StartInfo = objProcessInfo;
            objProcess.Start();
            objProcess.WaitForExit();
            #endregion

            #region Error read

            strStandardError = objProcess.StandardError.ReadToEnd();
            // something wrong happen while running commands
            if (!string.IsNullOrEmpty(strStandardError))
            {
                throw new Exception(strStandardError);
            }

            strStandardOutput = objProcess.StandardOutput.ReadToEnd();// Error processing comands sql
            // for SQL error messages like:
            // Msg ###, Level ##, State #, "Error message".
            if (!string.IsNullOrEmpty(strStandardOutput) &&
                (strStandardOutput.Contains("Msg") || strStandardOutput.Contains("Level") || strStandardOutput.Contains("State")))
            {
                throw new Exception(strStandardOutput);
            }
            #endregion
        }
    }
}

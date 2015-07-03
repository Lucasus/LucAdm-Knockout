using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace LucAdm.Tests
{
    public class WebPublisher
    {
        public void Publish()
        {
            var projectFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\LucAdm.Web\LucAdm.Web.csproj");
            var arguments = String.Format("{0} /p:DeployOnBuild=true /maxcpucount:4 /p:PublishProfile=Test /p:Configuration=Test /p:VisualStudioVersion=12.0",
                projectFileName);

            var publishProcess = new Process
            {
                StartInfo =
                {
                    FileName = ConfigurationManager.AppSettings.Get("MSBuildPath"),
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
            publishProcess.Start();

            var output = publishProcess.StandardOutput.ReadToEnd();
            publishProcess.WaitForExit();
            if (!output.Contains("Build succeeded"))
            {
                throw new Exception("Build exception: " + output);
            }
        }
    }
}
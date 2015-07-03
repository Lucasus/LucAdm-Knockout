using System;
using System.Configuration;
using System.Diagnostics;

namespace LucAdm.Tests
{
    public sealed class WebServer : IDisposable
    {
        private bool _isStarted;
        private Process _process;

        public void Dispose()
        {
            if (_process != null)
            {
                _process.Dispose();
            }
        }

        public WebServer Start()
        {
            //TODO: Remove everything from website path first

            var configuration = ConfigurationManager.AppSettings.Get("Configuration");

            if (configuration == "Debug")
            {
                new WebPublisher().Publish();

                // Start IIS Express
                if (_isStarted)
                {
                    return this;
                }

                _isStarted = true;

                var applicationPath = ConfigurationManager.AppSettings.Get("WwwRootPath");
                var port = int.Parse(ConfigurationManager.AppSettings.Get("Port"));

                _process = new Process
                {
                    StartInfo =
                    {
                        FileName = ConfigurationManager.AppSettings.Get("IISExpressPath"),
                        Arguments = string.Format("/path:\"{0}\" /port:{1}", applicationPath, port)
                    }
                };
                _process.Start();
            }

            return this;
        }

        public void Stop()
        {
            var configuration = ConfigurationManager.AppSettings.Get("Configuration");

            if (configuration == "Debug")
            {
                if (_process.HasExited == false)
                {
                    _process.Kill();
                }
                _isStarted = false;
            }
        }
    }
}
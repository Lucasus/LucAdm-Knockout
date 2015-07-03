using System;
using System.Configuration;
using System.Diagnostics;

namespace LucAdm.Tests
{
    public sealed class SeleniumServer : IDisposable
    {
        private bool _isStarted;
        private Process _process;

        private string JavaArguments
        {
            get
            {
                return @"-jar " + ConfigurationManager.AppSettings.Get("SeleniumServerPath");
            }
        }

        public void Dispose()
        {
            if (_process != null)
            {
                _process.Dispose();
            }
        }

        public SeleniumServer Start()
        {
            if (_isStarted)
            {
                return this;
            }

            _isStarted = true;

            _process = new Process
            {
                StartInfo =
                {
                    FileName = "java",
                    Arguments = JavaArguments,
                    CreateNoWindow = true
                }
            };
            _process.Start();
            return this;
        }

        public void Stop()
        {
            try
            {
                _process.Kill();
            }
            catch
            {
                // Process already killed - we do nothing
            }
            _isStarted = false;
        }
    }
}
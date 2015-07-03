using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using System;
using System.Drawing.Imaging;

namespace LucAdm.Tests
{
    public sealed class SeleniumFixture : IDisposable
    {
        private readonly SeleniumServer _seleniumServer;
        private readonly UsesDbFixture _usesDbFixture;
        private readonly WebServer _webServer;
        private readonly Browser _browser;

        public SeleniumFixture()
        {
            _usesDbFixture = new UsesDbFixture();
            _webServer = new WebServer().Start();
            _seleniumServer = new SeleniumServer().Start();

            var driver = new EventFiringWebDriver(new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory));

            _browser = new Browser(driver);
            driver.ExceptionThrown += (object sender, WebDriverExceptionEventArgs e) =>
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");
                driver.GetScreenshot().SaveAsFile("SeleniumException-" + timestamp + ".png", ImageFormat.Png);
            };
        }

        public Browser Browser { get { return _browser; } }

        public void Dispose()
        {
            _browser.Quit();
            _seleniumServer.Stop();
            _webServer.Stop();
            _usesDbFixture.Dispose();
        }
    }
};
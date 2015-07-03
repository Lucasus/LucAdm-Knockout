using OpenQA.Selenium;

namespace LucAdm.Tests
{
    public abstract class PageObject
    {
        public abstract string Url { get; }

        public IWebDriver Driver { get; set; }
    }
}
using OpenQA.Selenium;

namespace KnockAdm.Tests
{
    public abstract class PageObject
    {
        public abstract string Url { get; }

        public IWebDriver Driver { get; set; }
    }
}
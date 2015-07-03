using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace LucAdm.Tests
{
    public static class DriverExtensions
    {
        public static IReadOnlyCollection<IWebElement> WaitForList(this IWebDriver driver, string cssSelector, int? expectedCount, double timeout)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeout)).Until(x =>
            {
                return expectedCount == null && driver.ListByCss(cssSelector).Count > 0
                    || expectedCount != null && driver.ListByCss(cssSelector).Count == expectedCount;
            });
            return driver.ListByCss(cssSelector);
        }

        public static IWebElement WaitFor(this IWebDriver driver, string cssSelector, double timeout = 2)
        {
            return WaitForList(driver, cssSelector, 1, timeout).FirstOrDefault();
        }

        public static void WaitUntilHidden(this IWebDriver driver, string cssSelector, double timeout = 2)
        {
            WaitForList(driver, cssSelector, 0, timeout);
        }

        public static IReadOnlyCollection<IWebElement> ListByCss(this ISearchContext driver, string cssSelector)
        {
            return driver.FindElements(By.CssSelector(cssSelector));
        }

        public static IWebElement ElementFor(this ISearchContext driver, string cssSelector)
        {
            return driver.FindElement(By.CssSelector(cssSelector));
        }

        public static void GoToRelativeUrl(this INavigation navigation, string relativeUrl)
        {
            var port = int.Parse(ConfigurationManager.AppSettings.Get("Port"));

            if (!relativeUrl.StartsWith("/"))
            {
                relativeUrl = "/" + relativeUrl;
            }

            navigation.GoToUrl(string.Format("http://localhost:{0}{1}", port, relativeUrl));
        }
    }
}
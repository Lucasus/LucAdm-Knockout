using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace LucAdm.Tests
{
    public static class WebElementExtensions
    {
        public static string Content(this IWebElement element)
        {
            if (element == null)
            {
                return null;
            }
            return element.GetAttribute("innerHTML");
        }

        public static void OverrideValueFor(this IWebDriver driver, string cssSelector, string value, bool waitForExisting = false)
        {
            if (waitForExisting)
            {
                driver.WaitForValue(cssSelector);
            }
            var element = driver.ElementFor(cssSelector);
            element.Clear();
            element.SendKeys(value);
        }

        public static void WaitForValue(this IWebDriver driver, string cssSelector)
        {
            var element = driver.ElementFor(cssSelector);
            new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(x =>
            {
                return !String.IsNullOrEmpty(element.GetAttribute("value"));
            });
        }
    }
}
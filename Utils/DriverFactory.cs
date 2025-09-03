using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Comex.Tests.Automatizacion.Utils
{
    public static class DriverFactory
    {
        public static IWebDriver CreateChromeDriver()
        {
            string userDataDir = @"C:\Users\Pedro Hernadez\AppData\Local\Google\Chrome\User Data";

            var options = new ChromeOptions();
            options.AddArgument($"user-data-dir={userDataDir}");
            options.AddArgument("profile-directory=Default");

            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);

            return new ChromeDriver(options);
        }
    }
}
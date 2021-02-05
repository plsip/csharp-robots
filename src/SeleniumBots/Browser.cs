using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumBots
{
    public class Browser : IDisposable
    {
        public IWebDriver Driver { get; set; }

        public Browser()
        {
            Console.WriteLine("Setting up chrome driver options.");

            ChromeOptions chrome_options = new ChromeOptions();

            chrome_options.AddArgument("--no-sandbox");
            chrome_options.AddArgument("--disable-gpu");
            chrome_options.AddArgument("--headless");
            chrome_options.AddAdditionalCapability("useAutomationExtension", false);
            chrome_options.AddArgument("--single-process");
            chrome_options.AddArgument("--allow-file-access-from-files");
            chrome_options.AddArgument("--disable-web-security");
            chrome_options.AddArgument("--disable-extensions");
            chrome_options.AddArgument("--ignore-certificate-errors");
            chrome_options.AddArgument("--disable-ntp-most-likely-favicons-from-server");
            chrome_options.AddArgument("--disable-ntp-popular-sites");
            chrome_options.AddArgument("--disable-infobars");
            chrome_options.AddArgument("--disable-dev-shm-usage");
            chrome_options.AddArgument("--window-size=1920,1080");

            Console.WriteLine("Finished setting up chrome driver options.");
            Console.WriteLine("Setting up chrome driver.");

            Driver = new ChromeDriver(chrome_options);
            Driver.Manage().Window.Maximize();

            Console.WriteLine("Finished setting up chrome driver.");
        }

        public void Close()
        {
            Driver.Close();
        }

        void IDisposable.Dispose()
        {
            Driver.Close();
        }
    }
}

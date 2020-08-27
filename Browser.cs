using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace example_client
{
    public class Browser
    {
        public void Initialize()
        {
            Console.WriteLine("Setting up chrome options");
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

            Driver = new ChromeDriver(chrome_options);
            Driver.Manage().Window.Maximize();
        }

        public string Title
        {
            get { return Driver.Title; }
        }

        public void Goto(string url)
        {
            Driver.Url = url;
        }

        public void Close()
        {
            Driver.Quit();
        }

        public IWebDriver Driver { get; private set; }
    }
}

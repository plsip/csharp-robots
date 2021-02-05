using RealtorScraper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace RealtorScraper.Selenium
{
    public class WebScraper : IDisposable
    {
        private string baseURL = "https://www.realtor.com/realestateagents/";

        private IWebDriver driver;

        public WebScraper()
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            chromeOptions.AddArgument("--headless");

            string filepath = $@"/mnt/shared/file_manager/chromedriver.exe";
            bool pathExists = File.Exists(filepath);

            Console.WriteLine($"File exists: {pathExists}");

            if (pathExists)
            {
                filepath = $@"/mnt/shared/file_manager";
                driver = new ChromeDriver(filepath, chromeOptions);
            }
        }

        public List<RealtorModel> GetRealtors(string city, string state)
        {
            List<RealtorModel> realtors = new List<RealtorModel>();

            string baseTargetURL = $"{ baseURL }{ city.ToLower() }_{ state.ToLower() }";
            int totalPages = GetTotalPages(baseTargetURL);

            for (int page = 1; page <= totalPages; page++)
            {
                try
                {
                    string targetURL = $"{ baseTargetURL }/pg-{ page }";
                    driver.Navigate().GoToUrl(targetURL);

                    IList<IWebElement> contactInfoElements = driver.FindElements(By.XPath("//a[@id='call_inquiry_cta']"));

                    foreach (var contactInfo in contactInfoElements)
                    {
                        string cityState = contactInfo.GetAttribute("data-agent-address");

                        RealtorModel realtor = new RealtorModel
                        {
                            Name = contactInfo.GetAttribute("data-agent-name"),
                            PhoneNumber = contactInfo.GetAttribute("href").Replace("tel:", ""),
                            City = cityState.Split(',')[0],
                            State = cityState.Split(',')[1].Trim()
                        };

                        realtors.Add(realtor);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return realtors;
        }

        private int GetTotalPages(string url)
        {
            driver.Navigate().GoToUrl(url);

            var paginationElements = driver.FindElements(By.XPath("//a[@class='item ']"));

            // Because the 'next' link is last in list we get the second to last of the page links
            // by flipping the list and skipping the first element (which is the 'next' link)
            var lastPageLink = paginationElements.Reverse().Skip(1).FirstOrDefault();

            int totalPages = Int32.Parse(lastPageLink.Text);
            Console.WriteLine($" Pages: {totalPages}");

            return totalPages;
        }

        public void Dispose()
        {
            driver.Close();
        }
    }
}

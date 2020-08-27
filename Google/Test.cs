using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

using OpenQA.Selenium;


namespace example_client
{
    namespace Google
    {
        [TestFixture]
        public class Test
        {
            readonly Browser browser = new Browser();
            private IWebDriver driver;

            [SetUp]
            public void SetUp()
            {
                browser.Initialize();
            }

            [TearDown]
            public void TearDown()
            {
                browser.Close();
            }

            [Test]
            public void AnyPageTest()
            {
                string inputParameters = Environment.GetEnvironmentVariable("inputParameters");

                Console.WriteLine(inputParameters);

                dynamic parsedInputParameters = JObject.Parse(inputParameters);

                Console.WriteLine(parsedInputParameters);
                Console.WriteLine(parsedInputParameters["testTargetURL"]);
                Console.WriteLine(parsedInputParameters["expectedTitle"]);

                browser.Goto(Convert.ToString(parsedInputParameters["testTargetURL"]));

                driver = browser.Driver;
                
                Double now = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds / 1000L;
                Screenshot image = ((ITakesScreenshot) driver).GetScreenshot();
                image.SaveAsFile($"{Environment.GetEnvironmentVariable("SCREENSHOTS_PATH")}/screenshot_{now}.png");
                
                JObject json = new JObject(
                    new JProperty("timestamp", now));
                string jsonFilePath = $"{Environment.GetEnvironmentVariable("ARTIFACTS_PATH")}/any_page_test_{now}.json";
                System.IO.File.WriteAllText(@"" + jsonFilePath, json.ToString());

                string title = driver.Title;

                Assert.AreEqual(title, Convert.ToString(parsedInputParameters["expectedTitle"]));
                
                Environment.SetEnvironmentVariable("output", $"{{'title':'{title}'}}");
            }
        }
    }
}

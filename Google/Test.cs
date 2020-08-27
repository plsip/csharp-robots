using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;


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

                string targetUrl = Convert.ToString(parsedInputParameters["testTargetURL"]);
                string title = Convert.ToString(parsedInputParameters["expectedTitle"]);
                
                string previousResult = parsedInputParameters["previousResult"];
                if (previousResult != "")
                {
                    Console.WriteLine(parsedInputParameters["previousResult"]);
                    var converter = new ExpandoObjectConverter();
                    dynamic previous = JsonConvert.DeserializeObject<ExpandoObject>(previousResult, converter);
                    targetUrl = previous.targetUrl;
                    title = previous.title;
                }

                browser.Goto(targetUrl);

                driver = browser.Driver;
                
                Double now = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds / 1000L;
                Screenshot image = ((ITakesScreenshot) driver).GetScreenshot();
                image.SaveAsFile($"{Environment.GetEnvironmentVariable("SCREENSHOTS_PATH")}/screenshot_{now}.png");
                
                JObject json = new JObject(
                    new JProperty("timestamp", now));
                string jsonFilePath = $"{Environment.GetEnvironmentVariable("ARTIFACTS_PATH")}/any_page_test_{now}.json";
                System.IO.File.WriteAllText(@"" + jsonFilePath, json.ToString());

                string outputTitle = driver.Title;
                string outputTargetURL = driver.Url;

                Assert.AreEqual(title, title);
                
                Environment.SetEnvironmentVariable("output", $"{{'targetURL':'{outputTargetURL}','title':'{outputTitle}'}}");
            }
        }
    }
}

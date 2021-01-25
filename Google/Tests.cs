using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Dynamic;

namespace example_client
{
    namespace Google
    {
        [TestFixture]
        public class Tests
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
                Console.Write(inputParameters);
                Console.Write(Environment.GetEnvironmentVariables());
            }
        }
    }
}

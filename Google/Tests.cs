using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
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
                Console.Write("start");
                
                IDictionary envs = Environment.GetEnvironmentVariables();
                
                foreach( DictionaryEntry de in envs )
                {
                    Console.WriteLine("Key = {0}, Value = {1}",
                        de.Key, de.Value);
                }

                string inputParameters = Environment.GetEnvironmentVariable("inputParameters");
                Console.Write(inputParameters);
                
                Environment.SetEnvironmentVariable("output", "{'out1':'val1','out2':'val2'}");
            }
        }
    }
}
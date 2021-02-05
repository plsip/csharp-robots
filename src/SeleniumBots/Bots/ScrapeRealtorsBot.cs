using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumBots.Bots
{
    [TestFixture]
    public class ScrapeRealtorsBot
    {
        private Browser browser;

        [SetUp]
        public void SetUp()
        {
            browser = new Browser();
        }

        [TearDown]
        public void TearDown()
        {
            browser.Close();
        }

        [Test]
        public void GetRealtors()
        {
            Console.WriteLine("Getting realtors");
            Assert.True(true);
        }

        [Test]
        public void GetRealtors1()
        {
            Console.WriteLine("Getting realtors");
            Assert.True(true);
        }

        [Test]
        public void GetRealtors2()
        {
            Console.WriteLine("Getting realtors");
            Assert.True(true);
        }
    }
}

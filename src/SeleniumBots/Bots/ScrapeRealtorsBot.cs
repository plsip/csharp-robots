using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumBots.Bots
{
    [TestFixture]
    public class ScrapeRealtorsBot
    {

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
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
            Console.WriteLine("Getting realtors +1");
            Assert.True(true);
        }

        [Test]
        public void GetRealtors2()
        {
            Console.WriteLine("Getting realtors ++");
            Assert.True(true);
        }
    }
}

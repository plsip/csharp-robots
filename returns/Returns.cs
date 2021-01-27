using NUnit.Framework;
using System;

namespace example_client
{
    namespace Google
    {
        [TestFixture]
        public class Returns
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
            public void ReturnsTest()
            {
                //Environment.SetEnvironmentVariable("output", "{'out1':'val1','out2':'val2'}");
                Assert.IsTrue(true);
            }
        }
    }
}

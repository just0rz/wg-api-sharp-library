using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WGSharpAPI.Tools;

namespace WGSharpAPITests
{
    [TestClass]
    public class DateTimeToolsTests
    {
        [TestCategory("Unit test"), TestMethod]
        public void DateFromWGTimestamp()
        {
            long incomingTimestamp = 10;
            var expectedDate = new DateTime(1970, 1, 1, 0, 0, 10, DateTimeKind.Utc);


            var date = incomingTimestamp.DateFromWGTimestamp();

            Assert.AreEqual(date, expectedDate);
        }

        [TestCategory("Unit test"), TestMethod]
        public void DateToWGTimesptamp()
        {
            var incomingDateTime = new DateTime(1970, 1, 1, 0, 0, 10, DateTimeKind.Utc);
            long expectedTimestamp = 10;


            var timestamp = incomingDateTime.DateToWGTimesptamp();

            Assert.AreEqual(timestamp, expectedTimestamp);
        }
    }
}

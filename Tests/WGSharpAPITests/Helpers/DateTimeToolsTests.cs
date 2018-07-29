﻿/*
The MIT License (MIT)

Copyright (c) 2016 Iulian Grosu

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using NUnit.Framework;
using System;
using WGSharpAPI.Tools;

namespace WGSharpAPITests.Helpers
{
    [Category(TestConstants.Category.Dev)]
    public class DateTimeToolsTests
    {
        [Test]
        public void DateFromWGTimestamp()
        {
            long incomingTimestamp = 10;
            var expectedDate = new DateTime(1970, 1, 1, 0, 0, 10, DateTimeKind.Utc);


            var date = incomingTimestamp.DateFromWGTimestamp();

            Assert.AreEqual(date, expectedDate);
        }

        [Test]
        public void DateToWGTimesptamp()
        {
            var incomingDateTime = new DateTime(1970, 1, 1, 0, 0, 10, DateTimeKind.Utc);
            long expectedTimestamp = 10;


            var timestamp = incomingDateTime.DateToWGTimesptamp();

            Assert.AreEqual(timestamp, expectedTimestamp);
        }
    }
}

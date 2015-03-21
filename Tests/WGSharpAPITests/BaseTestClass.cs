/*
The MIT License (MIT)

Copyright (c) 2014 Iulian Grosu

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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using WGSharpAPI;

namespace WGSharpAPITests
{
    public class BaseTestClass
    {
        // you may change this, but don't make it public
        private static readonly string _applicationId = "demo";

        protected WGApplication WGApplication = null;

        // My account details :) - you may modify this
        protected static readonly long accountId = 508637087; // account id
        protected static readonly long createdAt = 1361472166; // unix timestamp
        protected static readonly long kv1sTankId = 5649; // KV-1S tank id

        public BaseTestClass()
        {
            TestClassSetup();
        }

        [ClassInitialize]
        public void TestClassSetup()
        {
            WGApplication = new WGApplication(_applicationId);
        }

        [TestInitialize]
        public void TestSetup()
        {
            // we don't want to kill WG's servers because we're doing real calls to their API
            Thread.Sleep(1000);
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }

        [ClassCleanup]
        public void TestClassCleanup()
        {
        }
    }
}

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
using WGSharpAPI.Enums;
using WGSharpAPI.Tools;

namespace WGSharpAPITests.Accounts
{
    [TestClass]
    public class PlayerInfoTests : BaseTestClass
    {
        [TestCategory("Integration test"), TestMethod]
        public void Account_info_return_playerInfo()
        {
            var createdAtDate = ToolsExtensions.DateFromWGTimestamp(createdAt);
            var result = WGApplication.GetPlayerInfo(accountId);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.Status, "ok");
            Assert.AreEqual(result.Data[0].AccountId, accountId);
            Assert.AreEqual(result.Data[0].CreatedAt, createdAtDate.DateToWGTimesptamp());
        }

        [TestCategory("Integration test"), TestMethod]
        public void Account_info_return_listof_playerInfo()
        {
            var createdAtDate = ToolsExtensions.DateFromWGTimestamp(createdAt);
            var result = WGApplication.GetPlayerInfo(new[] { accountId });

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.Status, "ok");
            Assert.AreEqual(result.Data[0].AccountId, accountId);
            Assert.AreEqual(result.Data[0].CreatedAt, createdAtDate.DateToWGTimesptamp());
        }

        [TestCategory("Integration test"), TestMethod]
        public void Account_info_return_listof_playerInfo_using_all_parameters_except_accessToken()
        {
            var createdAtDate = ToolsExtensions.DateFromWGTimestamp(createdAt);
            var result = WGApplication.GetPlayerInfo(new[] { accountId }, WGLanguageField.EN, null, "account_id,created_at");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.Status, "ok");
            Assert.AreEqual(result.Data[0].AccountId, accountId);
            Assert.AreEqual(result.Data[0].CreatedAt, createdAtDate.DateToWGTimesptamp());
        }
    }
}

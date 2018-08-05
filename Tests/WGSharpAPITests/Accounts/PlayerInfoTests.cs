/*
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
using Moq;
using NUnit.Framework;
using WGSharpAPI.Enums;
using WGSharpAPI.Interfaces;
using WGSharpAPI.Tools;

namespace WGSharpAPITests.Accounts
{
    [Category(TestConstants.Category.Integration)]
    public class PlayerInfoTests : BaseTestClass
    {
        MockRepository _mock;

        [SetUp]
        public void SetUp()
        {
            _mock = new MockRepository(MockBehavior.Default);
        }

        [TearDown]
        public void TearDown()
        {
            _mock = null; 
        }

        [Test]
        public void Account_info_return_playerInfo()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.PlayerData_NoPrivate));
            WGApplication.Request = wgRequestMock.Object;

            var createdAtDate = ToolsExtensions.DateFromWGTimestamp(TestConstants.Just0rzAccount.CreatedAt);
            var result = WGApplication.GetPlayerInfo(TestConstants.Just0rzAccount.AccountId);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual("ok", result.Status);
            Assert.AreEqual(TestConstants.Just0rzAccount.AccountId, result.Data[0].AccountId, "Unexpected accountId.");
            Assert.AreEqual(createdAtDate.DateToWGTimesptamp(), result.Data[0].CreatedAt, "Unexpected createdAt.");
        }

        [Test]
        public void Account_info_return_listof_playerInfo()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.PlayerData_NoPrivate));
            WGApplication.Request = wgRequestMock.Object;

            var createdAtDate = ToolsExtensions.DateFromWGTimestamp(TestConstants.Just0rzAccount.CreatedAt);
            var result = WGApplication.GetPlayerInfo(new[] { TestConstants.Just0rzAccount.AccountId });

            Assert.IsNotNull(result.Data);
            Assert.AreEqual("ok", result.Status);
            Assert.AreEqual(TestConstants.Just0rzAccount.AccountId, result.Data[0].AccountId, "Unexpected accountId.");
            Assert.AreEqual(createdAtDate.DateToWGTimesptamp(), result.Data[0].CreatedAt, "Unexpected createdAt.");
        }

        [Test]
        public void Account_info_return_listof_playerInfo_using_all_parameters_except_accessToken()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.PlayerData_NoPrivate));
            WGApplication.Request = wgRequestMock.Object;

            var createdAtDate = ToolsExtensions.DateFromWGTimestamp(TestConstants.Just0rzAccount.CreatedAt);
            var result = WGApplication.GetPlayerInfo(new[] { TestConstants.Just0rzAccount.AccountId }, WGLanguageField.EN, null, "account_id,created_at");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual("ok", result.Status);
            Assert.AreEqual(TestConstants.Just0rzAccount.AccountId, result.Data[0].AccountId, "Unexpected accountId.");
            Assert.AreEqual(createdAtDate.DateToWGTimesptamp(), result.Data[0].CreatedAt, "Unexpected createdAt.");
        }
    }
}

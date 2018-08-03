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
using System.Collections.Generic;
using WGSharpAPI;
using WGSharpAPI.Entities.PlayerDetails;
using WGSharpAPI.Interfaces;

namespace WGSharpAPITests.Accounts
{
    [Category(TestConstants.Category.Dev)]
    public class SearchPlayersTests : BaseTestClass
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
        public void Account_list_return_1_valid_user()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.SearchPlayerResult_1_valid));
            WGApplication.Request = wgRequestMock.Object;

            var result = WGApplication.SearchPlayers(TestConstants.Just0rzAccount.Username);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual("ok", result.Status);
            Assert.AreEqual(TestConstants.Just0rzAccount.AccountId, result.Data[0].AccountId);
            Assert.AreEqual(TestConstants.Just0rzAccount.Username, result.Data[0].Nickname);
        }

        [Test]
        public void Account_list_return_1_valid_user_using_a_specific_searchType()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.SearchPlayer_1_exact_result));
            WGApplication.Request = wgRequestMock.Object;

            var result = WGApplication.SearchPlayers(TestConstants.Just0rzAccount.Username, WGSharpAPI.Enums.WGSearchType.Exact);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual("ok", result.Status);
            Assert.AreEqual(TestConstants.Just0rzAccount.AccountId, result.Data[0].AccountId);
            Assert.AreEqual(TestConstants.Just0rzAccount.Username, result.Data[0].Nickname);
        }

        [Test]
        public void Account_list_return_1_valid_user_using_a_specific_searchType_and_result_limit()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.SearchPlayer_1_exact_result));
            WGApplication.Request = wgRequestMock.Object;

            var result = WGApplication.SearchPlayers("Just0rz", WGSharpAPI.Enums.WGSearchType.Exact, 1);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual("ok", result.Status);
            Assert.AreEqual(TestConstants.Just0rzAccount.AccountId, result.Data[0].AccountId);
            Assert.AreEqual(TestConstants.Just0rzAccount.Username, result.Data[0].Nickname);
        }

        [Test]
        public void Account_list_return_1_valid_user_with_specified_responseFields()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.SearchPlayer_1_account_id_result_only));
            WGApplication.Request = wgRequestMock.Object;

            var result = WGApplication.SearchPlayers(TestConstants.Just0rzAccount.Username, "account_id", WGSharpAPI.Enums.WGLanguageField.EN, WGSharpAPI.Enums.WGSearchType.Exact, 1);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Status, "ok");
            Assert.AreEqual(TestConstants.Just0rzAccount.AccountId, result.Data[0].AccountId);
            Assert.IsNull(result.Data[0].Nickname);
        }

        [Test]
        public void Account_list_search_less_than_3_chars_startswith()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.SearchPlayer_invalid_2_letters_used));
            WGApplication.Request = wgRequestMock.Object;

            var expectedResult = new WGResponse<List<Player>> { Status = "error", };

            var result = WGApplication.SearchPlayers("Ju");

            Assert.AreEqual(expectedResult.Status, result.Status);
            Assert.IsNull(result.Data);
        }
    }
}

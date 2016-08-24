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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WGSharpAPI;
using WGSharpAPI.Entities.PlayerDetails;

namespace WGSharpAPITests.Accounts
{
    [TestClass]
    public class SearchPlayersTests : BaseTestClass
    {
        [TestCategory("Integration test"), TestMethod]
        public void Account_list_return_1_valid_user()
        {
            var result = WGApplication.SearchPlayers("Just0rz");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual("ok", result.Status);
            Assert.AreEqual(accountId, result.Data[0].AccountId);
            Assert.AreEqual("Just0rz", result.Data[0].Nickname);
        }

        [TestCategory("Integration test"), TestMethod]
        public void Account_list_return_1_valid_user_using_a_specific_searchType()
        {
            var result = WGApplication.SearchPlayers("Just0rz", WGSharpAPI.Enums.WGSearchType.Exact);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual("ok", result.Status);
            Assert.AreEqual(accountId, result.Data[0].AccountId);
            Assert.AreEqual("Just0rz", result.Data[0].Nickname);
        }

        [TestCategory("Integration test"), TestMethod]
        public void Account_list_return_1_valid_user_using_a_specific_searchType_and_result_limit()
        {
            var result = WGApplication.SearchPlayers("Just0rz", WGSharpAPI.Enums.WGSearchType.Exact, 1);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual("ok", result.Status);
            Assert.AreEqual(accountId, result.Data[0].AccountId);
            Assert.AreEqual("Just0rz", result.Data[0].Nickname);
        }

        [TestCategory("Integration test"), TestMethod]
        public void Account_list_return_1_valid_user_with_specified_responseFields()
        {
            var result = WGApplication.SearchPlayers("Just0rz", "account_id", WGSharpAPI.Enums.WGLanguageField.EN, WGSharpAPI.Enums.WGSearchType.Exact, 1);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Status, "ok");
            Assert.AreEqual(result.Data[0].AccountId, accountId);
            Assert.IsNull(result.Data[0].Nickname);
        }

        [TestCategory("Integration test"), TestMethod]
        public void Account_list_search_less_than_3_chars_startswith()
        {
            var expectedResult = new WGResponse<List<Player>> { Status = "error", };

            var result = WGApplication.SearchPlayers("Ju");

            Assert.AreEqual(expectedResult.Status, result.Status);
            Assert.IsNull(result.Data);
        }
    }
}

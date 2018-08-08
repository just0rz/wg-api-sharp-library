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

namespace WGSharpAPITests.Accounts
{
    [Category(TestConstants.Category.Dev)]
    public class PlayerAchievementsTests : BaseTestClass
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
        public void Account_achievements_return_achievements()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.PlayerData_Achievements));
            WGApplication.Request = wgRequestMock.Object;

            var result = WGApplication.GetPlayerAchievements(TestConstants.Just0rzAccount.AccountId);

            if (result.Data != null && result.Data.Count == 0)
                Assert.Inconclusive("The selected player doesn't seem to have ANY achievements. This makes the test useless, you might want to select a different accountId.");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Status, "ok");
            Assert.IsTrue(result.Data.Count > 0);
        }

        [Test]
        public void Account_achievements_for_a_list_of_players_return_achievements()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.PlayerData_Achievements));
            WGApplication.Request = wgRequestMock.Object;

            var result = WGApplication.GetPlayerAchievements(new[] { TestConstants.Just0rzAccount.AccountId });

            if (result.Data != null && result.Data[0] != null && result.Data[0].Achievements.Count == 0)
                Assert.Inconclusive("The selected player doesn't seem to have ANY achievements. This makes the test useless, you might want to select a different accountId.");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Status, "ok");
            Assert.IsTrue(result.Data[0].Achievements.Count > 0);
        }

        [Test]
        public void Account_achievements_specify_all_parameters_return_achievements()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.PlayerData_Achievements));
            WGApplication.Request = wgRequestMock.Object;

            var result = WGApplication.GetPlayerAchievements(new[] { TestConstants.Just0rzAccount.AccountId }, WGLanguageField.EN, null, null);

            if (result.Data != null && result.Data[0] != null && result.Data[0].Achievements.Count == 0)
                Assert.Inconclusive("The selected player doesn't seem to have ANY achievements. This makes the test useless, you might want to select a different accountId.");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Status, "ok");
            Assert.IsTrue(result.Data[0].Achievements.Count > 0);
        }
    }
}

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

namespace WGSharpAPITests.Accounts
{
    [TestClass]
    public class PlayerAchievementsTests : BaseTestClass
    {
        [TestCategory("Integration test"), TestMethod]
        public void Account_achievements_return_achievements()
        {
            var result = WGApplication.GetPlayerAchievements(accountId);

            if (result.Data != null && result.Data[0] != null && result.Data[0].Achievements.Count == 0)
                Assert.Inconclusive("The selected player doesn't seem to have ANY achievements. This makes the test useless, you might want to select a different accountId.");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.Status, "ok");
            Assert.IsTrue(result.Data[0].Achievements.Count > 0);
        }

        [TestCategory("Integration test"), TestMethod]
        public void Account_achievements_for_a_list_of_players_return_achievements()
        {
            var result = WGApplication.GetPlayerAchievements(new[] { accountId });

            if (result.Data != null && result.Data[0] != null && result.Data[0].Achievements.Count == 0)
                Assert.Inconclusive("The selected player doesn't seem to have ANY achievements. This makes the test useless, you might want to select a different accountId.");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.Status, "ok");
            Assert.IsTrue(result.Data[0].Achievements.Count > 0);
        }

        [TestCategory("Integration test"), TestMethod]
        public void Account_achievements_specify_all_parameters_return_achievements()
        {
            var result = WGApplication.GetPlayerAchievements(new[] { accountId }, WGLanguageField.EN, null, null);

            if (result.Data != null && result.Data[0] != null && result.Data[0].Achievements.Count == 0)
                Assert.Inconclusive("The selected player doesn't seem to have ANY achievements. This makes the test useless, you might want to select a different accountId.");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.Status, "ok");
            Assert.IsTrue(result.Data[0].Achievements.Count > 0);
        }
    }
}

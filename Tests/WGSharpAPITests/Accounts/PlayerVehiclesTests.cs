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
using WGSharpAPI.Enums;

namespace WGSharpAPITests.Accounts
{
    [Category(TestConstants.Category.Integration)]
    public class PlayerVehiclesTests : BaseTestClass
    {
        [Test]
        public void Account_tanks_get_player_vehicles()
        {
            var result = WGApplication.GetPlayerVehicles(TestConstants.Just0rzAccount.AccountId);

            if (result.Data != null && result.Data[0] != null && result.Data[0].Tanks.Count == 0)
                Assert.Inconclusive("The selected player doesn't seem to have ANY tanks in his garage. This makes the test useless, you may try selecting a different accountId.");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Status, "ok");
            Assert.IsNotNull(result.Data[0].Tanks);
            Assert.IsTrue(result.Data[0].Tanks.Count > 0);
        }

        [Test]
        public void Account_tanks_get_vehicles_for_list_of_players()
        {
            var result = WGApplication.GetPlayerVehicles(new[] { TestConstants.Just0rzAccount.AccountId });

            if (result.Data != null && result.Data[0] != null && result.Data[0].Tanks.Count == 0)
                Assert.Inconclusive("The selected player doesn't seem to have ANY tanks in his garage. This makes the test useless, you may try selecting a different accountId.");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Status, "ok");
            Assert.IsNotNull(result.Data[0].Tanks);
            Assert.IsTrue(result.Data[0].Tanks.Count > 0);
        }

        [Test]
        public void Account_tanks_get_player_vehicles_specify_all_parameters()
        {
            var result = WGApplication.GetPlayerVehicles(new[] { TestConstants.Just0rzAccount.AccountId }, new long[] { TestConstants.Just0rzAccount.GrilleTankId }, WGLanguageField.EN, null, null);

            if (result.Data != null && result.Data[0] != null && result.Data[0].Tanks.Count == 0)
                Assert.Inconclusive("The selected player doesn't seem to have ANY tanks in his garage. This makes the test useless, you may try selecting a different accountId.");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Status, "ok");
            Assert.IsNotNull(result.Data[0].Tanks);
            Assert.IsTrue(result.Data[0].Tanks.Count > 0);
        }
    }
}

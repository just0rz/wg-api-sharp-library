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
using System;
using Moq;
using NUnit.Framework;
using WGSharpAPI.Interfaces;

namespace WGSharpAPITests.PlayersVehicles
{
    [Category(TestConstants.Category.Integration)]
    public class TankAchievementsTests : BaseTestClass
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
        public void PlayerVehicles_tanksachievements_get_all()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.SearchPlayerResult_1_valid));
            WGApplication.Request = wgRequestMock.Object;

            var result = WGApplication.GetTankAchievements(TestConstants.Just0rzAccount.AccountId);

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Data.Tanks.Count > 1);
            Assert.AreEqual("ok", result.Status);
        }

        [Test]
        public void PlayerVehicles_tanksachievements_get_all_specify_all_parameters()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.SearchPlayerResult_1_valid));
            WGApplication.Request = wgRequestMock.Object;

            var result = WGApplication.GetTankAchievements(TestConstants.Just0rzAccount.AccountId, new[] { TestConstants.Just0rzAccount.GrilleTankId }, WGSharpAPI.Enums.WGLanguageField.EN, "tank_id,achievements", null, null);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual("ok", result.Status);
        }
    }
}

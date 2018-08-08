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

namespace WGSharpAPITests.Encyclopedia
{
    [Category(TestConstants.Category.Integration)]
    public class TurretsTests : BaseTestClass
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
        public void Encyclopedia_tankturrets_get_all_turrets()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.SearchPlayerResult_1_valid));
            WGApplication.Request = wgRequestMock.Object;

            var result = WGApplication.GetTurrets();

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Data.Count > 1);
            Assert.AreEqual("ok", result.Status);
        }

        [Test]
        public void Encyclopedia_tankturrets_get_turrets_by_list_of_id()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.SearchPlayerResult_1_valid));
            WGApplication.Request = wgRequestMock.Object;

            var result = WGApplication.GetTurrets(new[] { TestConstants.Just0rzAccount.T54TurretId });

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.AreEqual("ok", result.Status);
        }

        [Test]
        public void Encyclopedia_tankturrets_get_1_turret_by_id()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.SearchPlayerResult_1_valid));
            WGApplication.Request = wgRequestMock.Object;

            var result = WGApplication.GetTurrets(TestConstants.Just0rzAccount.T54TurretId);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.AreEqual("ok", result.Status);
        }

        [Test]
        public void Encyclopedia_tankturrets_get_turrets_by_list_of_id_specify_all_parameters()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.SearchPlayerResult_1_valid));
            WGApplication.Request = wgRequestMock.Object;

            var result = WGApplication.GetTurrets(new[] { TestConstants.Just0rzAccount.T54TurretId }, WGLanguageField.EN, WGNation.All, "name");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.AreEqual("ok", result.Status);
        }

        [Test]
        public void Encyclopedia_tankturrets_get_turrets_by_list_of_id_specify_all_parameters_for_specific_nation()
        {
            var wgRequestMock = _mock.Create<IWGRequest>(MockBehavior.Loose);
            wgRequestMock.SetupGet(x => x.IsConsumed).Returns(false);
            wgRequestMock.Setup(x => x.GetResponse()).Returns(TestHelper.LoadJson(TestConstants.JsonResponse.SearchPlayerResult_1_valid));
            WGApplication.Request = wgRequestMock.Object;

            var result = WGApplication.GetTurrets(new[] { TestConstants.Just0rzAccount.T54TurretId }, WGLanguageField.EN, WGNation.USSR, "name");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.AreEqual("ok", result.Status);
        }
    }
}

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

namespace WGSharpAPITests.Encyclopedia
{
    [Category("Integration tests")]
    public class RadiosTests : BaseTestClass
    {
        [Test]
        public void Encyclopedia_tankengines_get_all_engines()
        {
            var result = WGApplication.GetEngines();

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Data.Count > 1);
            Assert.AreEqual("ok", result.Status);
        }

        [Test]
        public void Encyclopedia_tankengines_get_engines_by_list_of_id()
        {
            var result = WGApplication.GetEngines(new[] { TestConstants.Just0rzAccount.GrilleEngineId });

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.AreEqual("ok", result.Status);
        }

        [Test]
        public void Encyclopedia_tankengines_get_1_engine_by_id()
        {
            var result = WGApplication.GetEngines(TestConstants.Just0rzAccount.GrilleEngineId);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.AreEqual("ok", result.Status);
        }

        [Test]
        public void Encyclopedia_tankengines_get_engines_by_list_of_id_specify_all_parameters()
        {
            var result = WGApplication.GetEngines(new[] { TestConstants.Just0rzAccount.GrilleTankId }, WGLanguageField.EN, WGNation.All, "name");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.AreEqual("ok", result.Status);
        }

        [Test]
        public void Encyclopedia_tankengines_get_engines_by_list_of_id_specify_all_parameters_for_specific_nation()
        {
            var result = WGApplication.GetEngines(new[] { TestConstants.Just0rzAccount.GrilleTankId }, WGLanguageField.EN, WGNation.Germany, "name");

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.AreEqual("ok", result.Status);
        }
    }
}

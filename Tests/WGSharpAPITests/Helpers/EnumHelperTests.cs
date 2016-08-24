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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WGSharpAPI.Tools;

namespace WGSharpAPITests.Helpers
{
    [TestClass]
    public class EnumHelperTests
    {
        public enum MyTestEnum
        {
            [System.ComponentModel.Description("ValidFlag")]
            Valid,
            NoFlag,
        }

        [TestCategory("Unit test"), TestMethod]
        public void GetEnumDescription_FromEnumWithDescriptionAttribute_ReturnsDescriptionString()
        {
            var expectedDescription = "ValidFlag";
            var enumDescription = EnumHelper<MyTestEnum>.GetEnumDescription(MyTestEnum.Valid);

            Assert.AreEqual(expectedDescription, enumDescription);
        }

        [TestCategory("Unit test"), TestMethod]
        public void GetEnumDescription_FromEnumWithoutDescriptionAttribute_ReturnsEnumToString()
        {
            var expectedDescription = "NoFlag";
            var enumDescription = EnumHelper<MyTestEnum>.GetEnumDescription(MyTestEnum.NoFlag);

            Assert.AreEqual(expectedDescription, enumDescription);
        }

        [TestCategory("Unit test"), TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetEnumDescription_FromADifferentTypeThanEnum_ThrowsException()
        {
            try
            {
                var enumDescription = EnumHelper<int>.GetEnumDescription(1);
            }
            catch (TypeInitializationException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}

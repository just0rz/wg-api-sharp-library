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

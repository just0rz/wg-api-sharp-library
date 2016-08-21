using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WGSharpAPITests.PlayersVehicles
{
    [TestClass]
    public class VehicleStatisticsTests : BaseTestClass
    {
        [TestCategory("Integration test"), TestMethod]
        public void PlayerVehicles_tanksstats_get_all()
        {
            var result = WGApplication.GetTankStats(accountId);

            Assert.IsNotNull(result.Data); 
            Assert.IsTrue(result.Data.Count > 1); 
            Assert.AreEqual("ok", result.Status);
        }

        [TestCategory("Integration test"), TestMethod]
        public void PlayerVehicles_tanksstats_get_all_specify_all_parameters()
        {
            var result = WGApplication.GetTankStats(accountId, new[] { grilleTankId }, WGSharpAPI.Enums.WGLanguageField.EN, "tank_id", null, null);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
            Assert.AreEqual("ok", result.Status);
        }
    }
}

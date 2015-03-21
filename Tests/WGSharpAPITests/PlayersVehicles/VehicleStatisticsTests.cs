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
            Assert.AreEqual(result.Count, 1);
            Assert.IsTrue(result.Data.Count > 1);
            Assert.AreEqual(result.Status, "ok");
        }

        [TestCategory("Integration test"), TestMethod]
        public void PlayerVehicles_tanksstats_get_all_specify_all_parameters()
        {
            var result = WGApplication.GetTankStats(accountId, new[] { grilleTankId }, WGSharpAPI.Enums.WGLanguageField.EN, "tank_id", null, null);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.Data.Count, 1);
            Assert.AreEqual(result.Status, "ok");
        }
    }
}

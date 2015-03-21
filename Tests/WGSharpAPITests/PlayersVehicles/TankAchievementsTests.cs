using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WGSharpAPITests.PlayersVehicles
{
    [TestClass]
    public class TankAchievementsTests : BaseTestClass
    {
        [TestCategory("Integration test"), TestMethod]
        public void PlayerVehicles_tanksachievements_get_all()
        {
            var result = WGApplication.GetTankAchievements(accountId);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Count, 1);
            Assert.IsTrue(result.Data.Tanks.Count > 1);
            Assert.AreEqual(result.Status, "ok");
        }

        [TestCategory("Integration test"), TestMethod]
        public void PlayerVehicles_tanksachievements_get_all_specify_all_parameters()
        {
            var result = WGApplication.GetTankAchievements(accountId, new[] { grilleTankId }, WGSharpAPI.Enums.WGLanguageField.EN, "tank_id,achievements", null, null);

            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.Data.Tanks.Count, 1);
            Assert.AreEqual(result.Status, "ok");
        }
    }
}

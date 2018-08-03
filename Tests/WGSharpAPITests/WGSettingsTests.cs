using NUnit.Framework;
using WGSharpAPI;
using WGSharpAPI.Enums;

namespace WGSharpAPITests
{
    [Category(TestConstants.Category.Dev)]
    public class WGSettingsTests
    {
        [Test]
        public void WGSettings_WGRequestTarget_ReturnsDefault_WOT()
        {
            var wgSettings = new WGSettings();
            Assert.AreEqual(WGRequestTarget.WorldOfTanks, wgSettings.RequestTarget);
        }

        [TestCase(WGRequestTarget.WorldOfTanks, "api.worldoftanks.eu/wot")]
        [TestCase(WGRequestTarget.WorldOfWarships, "api.worldofwarships.eu/wows")]
        [TestCase(WGRequestTarget.WorldOfPlanes, "api.worldofwarplanes.eu/wowp")]
        public void WGSettings_WGRequestTarget_Returns_ExpectedBaseApiUri(WGRequestTarget requestTarget, string expectedBaseAPiUri)
        {
            var wgSettings = new WGSettings(WGRequestMethod.GET, WGRequestProtocol.HTTP, 10, requestTarget);

            Assert.AreEqual(requestTarget, wgSettings.RequestTarget, "Not sure how you ended up like this :/");
            Assert.AreEqual(expectedBaseAPiUri, wgSettings.BaseApiUri, "Wrong base API Uri or it actually changed and you didn't update this test");
        }
    }
}

namespace Tests.Steam.Market.Interface
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SteamAutoMarket.Steam.Market.Interface;

    [TestClass]
    public class MarketClientTests
    {
        private readonly MarketClient marketClient = new MarketClient(null);

        [TestMethod]
        public void CalculateSteamFeeTest()
        {
            var price = 1000d;
            var fee = this.marketClient.CalculateSteamFee(price);
            Assert.AreEqual(870, price - fee);

            price = 100;
            fee = this.marketClient.CalculateSteamFee(price);
            Assert.AreEqual(88, price - fee);

            price = 10;
            fee = this.marketClient.CalculateSteamFee(price);
            Assert.AreEqual(8.70, price - fee);

            price = 1;
            fee = this.marketClient.CalculateSteamFee(price);
            Assert.AreEqual(0.88, price - fee);

            price = 0.5;
            fee = this.marketClient.CalculateSteamFee(price);
            Assert.AreEqual(0.44, price - fee);

            price = 0.03;
            fee = this.marketClient.CalculateSteamFee(price);
            Assert.AreEqual(0.01, price - fee);
        }
    }
}
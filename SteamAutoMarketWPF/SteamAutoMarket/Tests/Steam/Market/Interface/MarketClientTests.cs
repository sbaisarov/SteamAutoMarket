namespace Tests.Steam.Market.Interface
{
    using FluentAssertions;

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
            var priceWithoutFee = this.marketClient.GetSteamPriceWithoutFee(price);
            priceWithoutFee.Should().Be("869.58");

            price = 100;
            priceWithoutFee = this.marketClient.GetSteamPriceWithoutFee(price);
            priceWithoutFee.Should().Be("86.97");

            price = 10;
            priceWithoutFee = this.marketClient.GetSteamPriceWithoutFee(price);
            priceWithoutFee.Should().Be("8.70");

            price = 1;
            priceWithoutFee = this.marketClient.GetSteamPriceWithoutFee(price);
            priceWithoutFee.Should().Be("0.88");

            price = 0.5;
            priceWithoutFee = this.marketClient.GetSteamPriceWithoutFee(price);
            priceWithoutFee.Should().Be("0.44");

            price = 0.03;
            priceWithoutFee = this.marketClient.GetSteamPriceWithoutFee(price);
            priceWithoutFee.Should().Be("0.01");
        }
    }
}
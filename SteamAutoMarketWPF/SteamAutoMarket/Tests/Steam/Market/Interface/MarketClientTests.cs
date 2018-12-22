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
            var priceWithoutFee = this.marketClient.GetSellingSteamPriceWithoutFee(price);
            priceWithoutFee.Should().Be("86958");

            price = 100;
            priceWithoutFee = this.marketClient.GetSellingSteamPriceWithoutFee(price);
            priceWithoutFee.Should().Be("8697");

            price = 10;
            priceWithoutFee = this.marketClient.GetSellingSteamPriceWithoutFee(price);
            priceWithoutFee.Should().Be("870");

            price = 1;
            priceWithoutFee = this.marketClient.GetSellingSteamPriceWithoutFee(price);
            priceWithoutFee.Should().Be("88");

            price = 0.5;
            priceWithoutFee = this.marketClient.GetSellingSteamPriceWithoutFee(price);
            priceWithoutFee.Should().Be("44");

            price = 0.03;
            priceWithoutFee = this.marketClient.GetSellingSteamPriceWithoutFee(price);
            priceWithoutFee.Should().Be("1");
        }
    }
}
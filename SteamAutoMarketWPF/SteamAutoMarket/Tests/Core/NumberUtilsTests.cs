namespace Tests.Core
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SteamAutoMarket.Core;

    [TestClass]
    public class NumberUtilsTests
    {
        [TestMethod]
        public void TryParseDoubleRubTest()
        {
            var price = "2,47pуб.";
            NumberUtils.TryParseDouble(price, out var result);
            result.Should().Be(2.47);

            price = "2pуб.";
            NumberUtils.TryParseDouble(price, out result);
            result.Should().Be(2);
        }

        [TestMethod]
        public void TryParseDoubleUsdTest()
        {
            var price = "$0.50USD";
            NumberUtils.TryParseDouble(price, out var result);
            result.Should().Be(0.5);

            price = "$5USD";
            NumberUtils.TryParseDouble(price, out result);
            result.Should().Be(5);
        }
    }
}
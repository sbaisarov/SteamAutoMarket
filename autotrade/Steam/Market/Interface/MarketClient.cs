using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Accord.Math;
using HtmlAgilityPack;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using Market.Enums;
using Market.Exceptions;
using Market.Models;
using Market.Models.Json;
using Market.Interface.Games;

namespace Market.Interface {
    public class MarketClient {
        private readonly SteamMarketHandler _steam;
        public readonly AvailableGames Games;
        public const int PublisherFeePercentDefault = 10;
        public const int SteamFeePercent = 5;

        public MarketClient(SteamMarketHandler steam) {
            _steam = steam;
            Games = new AvailableGames(_steam);
        }

        public Task<MarketProfile> ProfileAsync() {
            var result = new Task<MarketProfile>(Profile);
            result.Start();
            return result;
        }

        public WalletInfo WalletInfo() {
            var resp = _steam.Request(Urls.Market, Method.GET, Urls.Market, useAuthCookie: true);

            var walletInfoMatch = Regex.Match(resp.Data.Content, "(?<=g_rgWalletInfo = )(.*)(?=;)").Value;

            if (string.IsNullOrEmpty(walletInfoMatch))
                throw new SteamException("Unable to parse wallet info");

            return WalletInfo(walletInfoMatch);
        }

        public WalletInfo WalletInfo(string json) {
            var walletInfoDes = JsonConvert.DeserializeObject<JWalletInfo>(json);

            var walletInfo = new WalletInfo
            {
                Currency = walletInfoDes.Currency,
                WalletCountry = walletInfoDes.WalletCountry,
                WalletBalance = (double)walletInfoDes.WalletBalance / 100,
                MaxBalance = (double)walletInfoDes.MaxBalance / 100,
                WalletFee = (double)walletInfoDes.WalletFee / 100,
                WalletFeeMinimum = (double)walletInfoDes.WalletFeeMinimum / 100,
                WalletFeePercent = Convert.ToInt32(walletInfoDes.WalletFeePercent * 100),
                WalletPublisherFeePercent = Convert.ToInt32(walletInfoDes.WalletPublisherFeePercent * 100),
                WalletTradeMaxBalance = (double)walletInfoDes.WalletTradeMaxBalance / 100
            };

            return walletInfo;
        }

        public MarketProfile Profile() {
            var resp = _steam.Request(Urls.Market, Method.GET, Urls.Market, useAuthCookie: true);
            var content = resp.Data.Content;

            var doc = new HtmlDocument();
            doc.LoadHtml(content);

            var userLoginNode = doc.DocumentNode.SelectSingleNode("//span[@id='market_buynow_dialog_myaccountname']");

            if (userLoginNode == null)
                throw new SteamException("Unable to load profile");

            var userLogin = userLoginNode.InnerText;

            var userIdMatch = Regex.Match(content, "(?<=g_steamID = \")(.*)(?=\";)").Value;

            if (string.IsNullOrEmpty(userIdMatch))
                throw new SteamException("Unable to load profile (User ID)");

            long userId;
            var userIdParse = long.TryParse(userIdMatch, out userId);

            if (!userIdParse)
                throw new SteamException("Unable to parse user ID)");

            var userAvatarNode = doc.DocumentNode.SelectSingleNode("//span[@class='avatarIcon']");

            if (userAvatarNode == null)
                throw new SteamException("Unable to load profile (User avatar node)");

            var userAvatarImgNode = userAvatarNode.ChildNodes[1];

            if (userAvatarImgNode == null)
                throw new SteamException("Unable to load profile (User avatar img node");

            var userAvatarSrc = userAvatarImgNode.Attributes["src"].Value;

            if (string.IsNullOrEmpty(userAvatarSrc))
                throw new SteamException("Unable to load profile (User avatar img source");

            var walletInfoMatch = Regex.Match(content, "(?<=g_rgWalletInfo = )(.*)(?=;)").Value;

            if (string.IsNullOrEmpty(walletInfoMatch))
                throw new SteamException("Unable to parse wallet info");

            var walletInfo = WalletInfo(walletInfoMatch);

            var marketProfile = new MarketProfile
            {
                AvatarUrl = userAvatarSrc,
                Id = userId,
                Login = userLogin,
                WalletInfo = walletInfo
            };

            return marketProfile;
        }

        /// <summary>
        /// Returns no more than 100 items
        /// </summary>
        public MarketSearch Search(string query = "", int start = 0, int count = 10, bool searchInDescriptions = false,
            int appId = 0, EMarketSearchSortColumns sortColumn = EMarketSearchSortColumns.Name, ESort sort = ESort.Desc,
            IDictionary<string, string> custom = null) {
            var urlQuery = new Dictionary<string, string>
            {
                {"query", query},
                {"start", start.ToString() },
                {"count", count.ToString() },
                {"search_descriptions", (searchInDescriptions ? 1 : 0).ToString()},
                {"appid", appId.ToString() },
                {"sort_column", Utils.FirstCharacterToLower(sortColumn.ToString()) },
                {"sort_dir", Utils.FirstCharacterToLower(sort.ToString()) }
            };

            if (custom != null && custom.Count > 0) {
                urlQuery = urlQuery.Concat(custom).ToDictionary(x => x.Key, x => x.Value);
            }

            var resp = _steam.Request(Urls.Market + "/search/render/", Method.GET, Urls.Market, urlQuery, true);
            var respDes = JsonConvert.DeserializeObject<JMarketSearch>(resp.Data.Content);

            if (!respDes.Success) {
                throw new SteamException("Failed Search");
            }

            var marketSearch = new MarketSearch();

            if (respDes.TotalCount <= 0) {
                return marketSearch;
            }

            marketSearch.TotalCount = respDes.TotalCount;

            var html = respDes.ResultsHtml;
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var itemNodes = doc.DocumentNode.SelectNodes("//a[@class='market_listing_row_link']");

            if (itemNodes == null || itemNodes.Count <= 0)
                return marketSearch;

            int tempIndex = 0;

            foreach (var item in itemNodes) {
                tempIndex++;

                var nameNode = item.SelectSingleNode(".//span[@class='market_listing_item_name']");
                if (nameNode == null)
                    throw new SteamException($"Cannot parse item name. Index [{tempIndex}]");
                var name = nameNode.InnerText;

                var gameNode = item.SelectSingleNode(".//span[@class='market_listing_game_name']");
                if (gameNode == null)
                    throw new SteamException($"Cannot parse game name. Index [{tempIndex}]");
                var game = gameNode.InnerText;

                var imageUrlNode = item.SelectSingleNode(".//img[@class='market_listing_item_img']");

                var imageUrl = imageUrlNode?.Attributes["srcset"]?.Value;

                var urlAttr = item.Attributes["href"];
                if (urlAttr == null)
                    throw new SteamException($"Cannot find item url. Item [{name}], Index [{tempIndex}]");
                var url = urlAttr.Value;

                var quantityNode = item.SelectSingleNode(".//span[@class='market_listing_num_listings_qty']");
                if (quantityNode == null)
                    throw new SteamException($"Cannot find quantity. Item [{name}], Index [{tempIndex}]");

                var quantityParse = int.TryParse(quantityNode.InnerText, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var quantity);
                if (!quantityParse)
                    throw new SteamException($"Cannot parse quantity. Item [{name}], Index [{tempIndex}]");

                var minPriceNode = item.SelectSingleNode(".//span[@class='normal_price']");
                if (minPriceNode == null)
                    throw new SteamException($"Cannot find item price. Item [{name}], Index [{tempIndex}]");
                var minPriceClean = minPriceNode.InnerText.Split(' ')[0];

                double minPrice;
                var minPriceParse = double.TryParse(minPriceClean, NumberStyles.Currency,
                    CultureInfo.GetCultureInfo("en-US"), out minPrice);
                if (!minPriceParse)
                    throw new SteamException($"Cannot parse min. price. Item [{name}], Index [{tempIndex}]");

                marketSearch.Items.Add(new MarketSearchItem
                {
                    ImageUrl = imageUrl,
                    Name = name,
                    Quantity = quantity,
                    Game = game,
                    Url = url,
                    MinPrice = minPrice,
                    HashName = Utils.HashNameFromUrl(url),
                    AppId = Utils.AppIdFromUrl(url)
                });
            }

            return marketSearch;
        }

        public MarketItemInfo ItemPage(int appId, string hashName) {
            if (string.IsNullOrEmpty(hashName))
                throw new SteamException("HashName should not be empty");

            var resp = _steam.Request(Urls.Market + $"/listings/{appId}/{hashName}", Method.GET, Urls.Market, null, true);
            var content = resp.Data.Content;

            int nameId;
            var nameIdParse = int.TryParse(Regex.Match(content, @"(?<=Market_LoadOrderSpread\()(.*)(?=\);)").Value.Trim(), out nameId);

            if (!nameIdParse)
                throw new SteamException("Unable to find name ID");

            var publisherFeePercentMatch = Regex.Match(content, "(?<=\"publisher_fee_percent\":\")(.*?)(?=\")").Value;

            int publisherFeePercent;
            if (string.IsNullOrEmpty(publisherFeePercentMatch)) {
                publisherFeePercent = PublisherFeePercentDefault;
            } else {
                decimal publisherFeePercentOut;
                var publisherFeePercentParse = decimal.TryParse(publisherFeePercentMatch, NumberStyles.AllowDecimalPoint,
                    CultureInfo.InvariantCulture, out publisherFeePercentOut);

                if (!publisherFeePercentParse)
                    throw new SteamException("Cannot to parse publisher fee percent");

                publisherFeePercent = (int)Math.Round(publisherFeePercentOut * 100);
            }

            var item = new MarketItemInfo
            {
                NameId = nameId,
                PublisherFeePercent = publisherFeePercent
            };

            return item;
        }

        public Task<ECancelBuyOrderStatus> CancelBuyOrderAsync(long orderId) {
            var result = new Task<ECancelBuyOrderStatus>(() => CancelBuyOrder(orderId));
            result.Start();
            return result;
        }

        public ECancelBuyOrderStatus CancelBuyOrder(long orderId) {
            var data = new Dictionary<string, string>
            {
                {"sessionid", _steam.Auth.SessionId() },
                {"buy_orderid", orderId.ToString() }
            };

            var resp = _steam.Request(Urls.Market + "/cancelbuyorder/", Method.POST, Urls.Market, data, true);

            var respDes = JsonConvert.DeserializeObject<JSuccessInt>(resp.Data.Content);

            switch (respDes.Success) {
                case 1:
                    return ECancelBuyOrderStatus.Canceled;
                case 29:
                    return ECancelBuyOrderStatus.NotExist;
                default:
                    return ECancelBuyOrderStatus.Fail;
            }
        }

        public Task<CreateBuyOrder> CreateBuyOrderAsync(string hashName, int appId, int currency, double totalPrice, int quantity = 1) {
            var result = new Task<CreateBuyOrder>(() => CreateBuyOrder(hashName, appId, currency, totalPrice, quantity));
            result.Start();
            return result;
        }

        public CreateBuyOrder CreateBuyOrder(string hashName, int appId, int currency, double totalPrice, int quantity = 1) {
            var data = new Dictionary<string, string>
            {
                {"sessionid", _steam.Auth.SessionId() },
                {"currency", currency.ToString() },
                {"appid", appId.ToString() },
                {"market_hash_name", hashName },
                {"price_total", (totalPrice * 100 * quantity).ToString(CultureInfo.InvariantCulture)},
                {"quantity", quantity.ToString()}
            };

            var resp = _steam.Request(Urls.Market + "/createbuyorder/", Method.POST, Urls.Market, data, true);
            var respDes = JsonConvert.DeserializeObject<JCreateBuyOrder>(resp.Data.Content);

            var order = new CreateBuyOrder();

            switch (respDes.Success) {
                case 1:
                    order.Status = ECreateBuyOrderStatus.Success;
                    order.OrderId = respDes.BuyOrderId;
                    break;
                case 2:
                    order.Status = ECreateBuyOrderStatus.LowOrderPrice;
                    break;
                case 25:
                    order.Status = ECreateBuyOrderStatus.LimitOfOrders;
                    break;
                case 29:
                    order.Status = ECreateBuyOrderStatus.OrderAlreadyPlaced;
                    break;
                case 107:
                    order.Status = ECreateBuyOrderStatus.InsufficientFund;
                    break;
                case 16:
                default:
                    order.Status = ECreateBuyOrderStatus.Fail;
                    break;
            }

            return order;
        }

        public Task<ItemOrdersHistogram> ItemOrdersHistogramAsync(int nameId, string country, ELanguage lang, int currency) {
            var result = new Task<ItemOrdersHistogram>(() => ItemOrdersHistogram(nameId, country, lang, currency));
            result.Start();
            return result;
        }

        public ItemOrdersHistogram ItemOrdersHistogram(int nameId, string country, ELanguage lang, int currency) {
            var url = Urls.Market +
                $"/itemordershistogram?country={country}&language={lang}&currency={currency}&item_nameid={nameId}";
            var resp = _steam.Request(url, Method.GET, Urls.Market, null, true);
            var respDes = JsonConvert.DeserializeObject<JItemOrdersHistogram>(resp.Data.Content);

            if (respDes.Success != 1)
                throw new SteamException("Cannot load orders histogram");

            var histogram = new ItemOrdersHistogram
            {
                SellOrderGraph = ConvertOrderGraph(respDes.SellOrderGraph),
            };

            if (respDes.BuyOrderGraph != null) {
                histogram.BuyOrderGraph = ConvertOrderGraph(respDes.BuyOrderGraph);
            }

            if (respDes.MinSellPrice != null)
                histogram.MinSellPrice = (double)respDes.MinSellPrice / 100;
            if (respDes.HighBuyOrder != null)
                histogram.HighBuyOrder = (double)respDes.HighBuyOrder / 100;

            return histogram;
        }

        public OrderGraph ConvertOrderGraph(List<dynamic[]> collection) {
            var graph = new OrderGraph
            {
                Orders = new List<OrderGraphItem>()
            };

            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (collection.Count == 0)
                return graph;

            int tempCount = 0;
            var list = collection.Select(x => {
                var item = new OrderGraphItem
                {
                    Count = (int)x[1] - tempCount,
                    Price = (double)x[0],
                    Title = (string)x[2]
                };
                tempCount = item.Count;
                return item;
            }).ToList();

            graph.Orders = list;
            graph.Total = (int)collection.Last()[1];

            return graph;
        }

        public Task<MyListings> MyListingsAsync() {
            var result = new Task<MyListings>(MyListings);
            result.Start();
            return result;
        }

        //public List<HistoryItem> History(int count = 100, int start = 0)
        //{
        //    count = count > 500 ? 500 : count;

        //    var data = new Dictionary<string, string>
        //    {
        //        {"count", count.ToString()},
        //        {"start", start.ToString()}
        //    };

        //    var resp = _steam.Request(Urls.Market + "/myhistory/render/", Method.GET, Urls.Market, data, true);
        //    JMyHistory respDes;

        //    try
        //    {
        //        respDes = JsonConvert.DeserializeObject<JMyHistory>(resp.Data.Content);
        //    }
        //    catch
        //    {
        //        throw new SteamException("Cannot load market history");
        //    }

        //    if (!respDes.Success)
        //        throw new SteamException("Cannot load market history");

        //    if (respDes.TotalCount == null || respDes.TotalCount == 0)
        //        return new List<HistoryItem>();

        //    var doc = new HtmlDocument();
        //    doc.LoadHtml(respDes.Html);

        //    var historyNodes =
        //        doc.DocumentNode.SelectNodes(
        //            "//div[contains(@id, 'history_row_') and @class='market_listing_row market_recent_listing_row']");

        //    var hoversMatches = Regex.Matches(respDes.Hovers,
        //        @"(?<=CreateItemHoverFromContainer\( g_rgAssets,)(.*?)(?=\))");

        //    var hovers = hoversMatches.Cast<Match>()
        //        .Select(s => s.Value.Split(',').Apply(a => a.Trim(' ', '\''))) - Apply function is unknown, the method is unnecessary so far
        //        .Where(w => w[0].Contains("_name"))
        //        .Select(s => new HistoryItemHover
        //        {
        //            AppId = int.Parse(s[1]),
        //            ContextId = int.Parse(s[2]),
        //            AssetId = long.Parse(s[3]),
        //            HoverId = s[0].Replace("_name", "")
        //        });

        //    var assets = respDes.Assets.SelectMany(s => s.Value.SelectMany(c => c.Value.Select(m => m.Value)));

        //    var hoverAssetCompare = hovers
        //        .Select(s => new KeyValuePair<HistoryItemHover, JDescription>(s, assets.First(f => f.Id == s.AssetId)));

        //    var history = historyNodes.Select(s =>
        //    {
        //        var actionTypeString =
        //            s.SelectSingleNode("div[@class='market_listing_left_cell market_listing_gainorloss']")
        //                .InnerText.Trim();

        //        EMyHistoryActionType actionType;
        //        switch (actionTypeString)
        //        {
        //            case "+":
        //                actionType = EMyHistoryActionType.Buy;
        //                break;
        //            case "-":
        //                actionType = EMyHistoryActionType.Sell;
        //                break;
        //            default:
        //                actionType = EMyHistoryActionType.None;
        //                break;
        //        }

        //        var priceString = s.SelectSingleNode("div[contains(@class, 'market_listing_their_price')]")
        //            .InnerText.Trim()
        //            .Split(' ')[0];

        //        var dates =
        //            s.SelectNodes("div[contains(@class, 'market_listing_listed_date')]")
        //                .Select(x => x.InnerText.Trim()).ToArray();

        //        var memberString = s.SelectSingleNode(".//div[@class='market_listing_whoactedwith_name_block']");
        //        var member = memberString?.ChildNodes[2].InnerText.Trim() ?? s.SelectSingleNode("div[contains(@class, 'market_listing_whoactedwith')]").InnerText.Trim();

        //        return new HistoryItem
        //        {
        //            Asset = hoverAssetCompare.FirstOrDefault(w => w.Key.HoverId == s.Id).Value,
        //            Game = s.SelectSingleNode(".//span[contains(@class, 'market_listing_game_name')]").InnerText.Trim(),
        //            Name = s.SelectSingleNode(".//span[contains(@class, 'market_listing_item_name')]").InnerText.Trim(),
        //            ActionType = actionType,
        //            Member = member,
        //            PlaceDate = string.IsNullOrEmpty(dates[0]) ? (DateTime?)null : Convert.ToDateTime(dates[0]),
        //            DealDate = string.IsNullOrEmpty(dates[1]) ? (DateTime?)null : Convert.ToDateTime(dates[1]),
        //            Price =
        //                string.IsNullOrEmpty(priceString)
        //                    ? (double?)null
        //                    : double.Parse(priceString, NumberStyles.Currency, new CultureInfo("en-US"))

        //        };
        //    }).Where(w => w.ActionType != EMyHistoryActionType.None).ToList();

        //    return history;
        //}

        public MyListings MyListings() {
            var resp = _steam.Request(Urls.Market + "/mylistings/", Method.GET, Urls.Market, null, true);

            var respDes = JsonConvert.DeserializeObject<JMyListings>(resp.Data.Content);

            if (respDes.Success) {
                var html = respDes.ResultsHtml;
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                var root = doc.DocumentNode;

                var ordersCountNode = root.SelectSingleNode(".//span[@id='my_market_buylistings_number']");
                if (ordersCountNode == null)
                    throw new SteamException("Cannot find buy listings node");
                int ordersCount;
                var ordersCountParse = int.TryParse(ordersCountNode.InnerText, out ordersCount);
                if (!ordersCountParse)
                    throw new SteamException("Cannot parse buy listings node value");


                var sellCountNode = root.SelectSingleNode(".//span[@id='my_market_selllistings_number']");
                if (sellCountNode == null)
                    throw new SteamException("Cannot find sell listings node");
                int sellCount;
                var sellCountParse = int.TryParse(sellCountNode.InnerText, out sellCount);
                if (!sellCountParse)
                    throw new SteamException("Cannot parse sell listings node value");

                var confirmCountNode = root.SelectSingleNode(".//span[@id='my_market_listingstoconfirm_number']");
                int confirmCount = 0;
                if (confirmCountNode != null) {
                    var confirmCountParse = int.TryParse(confirmCountNode.InnerText, out confirmCount);
                    if (!confirmCountParse)
                        throw new SteamException("Cannot parse confirm listings node value");
                }

                var myListings = new MyListings
                {
                    ItemsToBuy = ordersCount,
                    ItemsToConfirm = confirmCount,
                    ItemsToSell = sellCount,
                    Orders = new List<MyListingsOrdersItem>()
                };

                if (ordersCount > 0) {
                    var ordersNodes = root.SelectNodes("//div[contains(@id,'mybuyorder_')]");
                    if (ordersNodes == null)
                        throw new SteamException("Cannot parse orders listings nodes");

                    int tempIndex = 0;
                    foreach (var item in ordersNodes) {
                        var priceAndQuantityNode = item.SelectSingleNode(".//span[@class='market_listing_price']");

                        if (priceAndQuantityNode == null)
                            throw new SteamException($"Cannot parse order listing price and quantity node. Item index [{tempIndex}]");

                        var priceAndQuantityString = priceAndQuantityNode.InnerText
                            .Replace("\r", string.Empty)
                            .Replace("\n", string.Empty)
                            .Replace("\t", string.Empty)
                            .Replace(" ", string.Empty);
                        var priceAndQuantitySplit = priceAndQuantityString.Split('@');

                        double price;
                        var priceParse = double.TryParse(priceAndQuantitySplit[1], NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"), out price);

                        if (!priceParse)
                            throw new SteamException($"Cannot parse order listing price. Item index [{tempIndex}]");

                        int quantity;
                        var quantityParse = int.TryParse(priceAndQuantitySplit[0], out quantity);

                        if (!quantityParse)
                            throw new SteamException($"Cannot parse order listing quantity. Item index [{tempIndex}]");

                        var orderIdMatch = Regex.Match(item.InnerHtml, "(?<=mbuyorder_)([0-9]*)(?=_name)");

                        if (!orderIdMatch.Success)
                            throw new SteamException($"Cannot find order listing ID. Item index [{tempIndex}]");

                        long orderId;
                        var odderIdParse = long.TryParse(orderIdMatch.Value, out orderId);

                        if (!odderIdParse)
                            throw new SteamException($"Cannot parse order listing ID. Item index [{tempIndex}]");

                        var urlNode = item.SelectSingleNode(".//a[@class='market_listing_item_name_link']");
                        if (urlNode == null)
                            throw new SteamException($"Cannot find order listing url. Item index [{tempIndex}]");

                        var url = urlNode.Attributes["href"].Value;

                        var hashName = Utils.HashNameFromUrl(url);
                        var appId = Utils.AppIdFromUrl(url);

                        var nameNode = urlNode.InnerText;

                        myListings.Orders.Add(new MyListingsOrdersItem
                        {
                            AppId = appId,
                            HashName = hashName,
                            Name = nameNode,
                            OrderId = orderId,
                            Price = price,
                            Quantity = quantity,
                            Url = url
                        });

                        tempIndex++;
                    }
                }

                myListings.SumOrderPricesToBuy = Math.Round(myListings.Orders.Sum(s => s.Price), 2);

                return myListings;
            } else {
                throw new SteamException("Cannot load market listings");
            }
        }

        public dynamic SellItem(int appId, int contextId, long assetId, int amount, double priceWithoutFee) {
            var data = new Dictionary<string, string>
            {
                { "appid", appId.ToString() },
                { "sessionid", _steam.Auth.SessionId() },
                { "contextid", contextId.ToString() },
                { "assetid", assetId.ToString() },
                { "amount", amount.ToString() },
                { "price", (Math.Round(priceWithoutFee, 2)*100).ToString(CultureInfo.InvariantCulture) }
            };

            var resp = _steam.Request(Urls.Market + "/sellitem/", Method.POST, Urls.Market, data, true).Data.Content;
            return JsonConvert.DeserializeObject<JSellItem>(resp);
        }
        
        public List<PriceHistoryDay> PriceHistory(int appId, string hashName) {
            var url = Urls.Market + $"/pricehistory/?appid={appId}&market_hash_name={hashName}";

            var resp = _steam.Request(url, Method.GET, Urls.Market, null, true);

            var respDes = JsonConvert.DeserializeObject<JPriceHistory>(resp.Data.Content);

            if (!respDes.Success)
                throw new SteamException($"Connot get price history for [{hashName}]");

            IEnumerable<dynamic> prices = Enumerable.ToList(respDes.Prices);

            var list = prices
                .Select((g, index) => {
                    int count = 0;
                    if (g[2] != null && !int.TryParse(g[2].ToString(), out count))
                        throw new SteamException($"Cannot parse items count in price history. Index: {index}");

                    double price = 0;
                    if (g[1] != null && !double.TryParse(g[1].ToString(), out price))
                        throw new SteamException($"Cannot parse item price in price history. Index: {index}");

                    return new PriceHistoryItem
                    {
                        DateTime = Utils.SteamTimeConvertor(g[0].ToString()),
                        Count = count,
                        Price = Math.Round(price, 2)
                    };
                })
                .GroupBy(x => x.DateTime.Date)
                .Select(g => new PriceHistoryDay
                {
                    Date = g.Key,
                    History = g.Select(p => p).ToList()
                })
                .ToList();

            return list;
        }

        public string AppFilters(int appId) {
            var resp = _steam.Request(Urls.Market + "/appfilters/" + appId, Method.GET, Urls.Market, useAuthCookie: true);
            var respDes = JsonConvert.DeserializeObject<JSuccess>(resp.Data.Content);

            if (!respDes.Success)
                throw new SteamException("Cannot load app filters");

            return resp.Data.Content;
        }
    }
}
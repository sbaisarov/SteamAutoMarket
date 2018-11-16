namespace Steam.Market.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    using HtmlAgilityPack;

    using Newtonsoft.Json;

    using RestSharp;

    using Steam.Market.Enums;
    using Steam.Market.Exceptions;
    using Steam.Market.Interface.Games;
    using Steam.Market.Models;
    using Steam.Market.Models.Json;

    public class MarketClient
    {
        public const int PublisherFeePercentDefault = 10;

        public const int SteamFeePercent = 5;

        private const string RemoveListingUrl = "https://steamcommunity.com/market/removelisting/";

        public readonly Dictionary<string, string> Currencies;

        public readonly AvailableGames Games;

        private readonly SteamMarketHandler steam;

        public MarketClient(SteamMarketHandler steam)
        {
            this.steam = steam;
            this.Games = new AvailableGames(this.steam);
            this.Currencies = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                "{\"1\": \"$\", \"2\": \"\u00a3\", \"3\": \"\u20ac\", \"4\": \"CHF\", \"5\": \"p\u0443\u0431\", \"6\": \"z\u0142\", \"7\": \"R$\", \"8\": \"\u00a5\", \"9\": \"kr\", \"10\": \"Rp\", \"11\": \"RM\", \"12\": \"P\", \"13\": \"S$\", \"14\": \"\u0e3f\", \"15\": \"\u20ab\", \"16\": \"\u20a9\", \"17\": \"TL\", \"18\": \"\u20b4\", \"19\": \"Mex$\", \"20\": \"CDN$\", \"22\": \"NZ$\", \"23\": \"\u00a5\", \"24\": \"\u20b9\", \"25\": \"CLP$\", \"26\": \"S\", \"27\": \"COL$\", \"28\": \"R\", \"29\": \"HK$\", \"30\": \"NT$\", \"31\": \"SR\", \"32\": \"AED\", \"34\": \"ARS$\", \"35\": \"\u20aa\", \"37\": \"\u20b8\", \"38\": \"KD\", \"39\": \"QR\", \"40\": \"\u20a1\", \"41\": \"$U\"}");
        }

        public int? CurrentCurrency { get; set; }

        public string AppFilters(int appId)
        {
            var resp = this.steam.Request(
                Urls.Market + "/appfilters/" + appId,
                Method.GET,
                Urls.Market,
                useAuthCookie: true);
            var respDes = JsonConvert.DeserializeObject<JSuccess>(resp.Data.Content);

            if (!respDes.Success)
            {
                throw new SteamException("Cannot load app filters");
            }

            return resp.Data.Content;
        }

        public ECancelBuyOrderStatus CancelBuyOrder(long orderId)
        {
            var data = new Dictionary<string, string>
                           {
                               { "sessionid", this.steam.Auth.SessionId() }, { "buy_orderid", orderId.ToString() }
                           };

            var resp = this.steam.Request(Urls.Market + "/cancelbuyorder/", Method.POST, Urls.Market, data, true);

            var respDes = JsonConvert.DeserializeObject<JSuccessInt>(resp.Data.Content);

            switch (respDes.Success)
            {
                case 1:
                    return ECancelBuyOrderStatus.Canceled;
                case 29:
                    return ECancelBuyOrderStatus.NotExist;
                default:
                    return ECancelBuyOrderStatus.Fail;
            }
        }

        public Task<ECancelBuyOrderStatus> CancelBuyOrderAsync(long orderId)
        {
            var result = new Task<ECancelBuyOrderStatus>(() => this.CancelBuyOrder(orderId));
            result.Start();
            return result;
        }

        public ECancelSellOrderStatus CancelSellOrder(long orderid)
        {
            var headers = new Dictionary<string, string>
                              {
                                  { "X-Prototype-Version", "1.7" },
                                  { "X-Requested-With", "XMLHttpRequest" },
                                  { "Referer", "https://steamcommunity.com/market/" },
                                  { "Origin", "https://steamcommunity.com" },
                                  { "Host", "steamcommunity.com" }
                              };
            var data = new Dictionary<string, string> { { "sessionid", this.steam.Auth.SessionId() } };

            var resp = this.steam.Request(
                RemoveListingUrl + orderid,
                Method.POST,
                Urls.Market,
                data,
                true,
                headers: headers);

            return resp.Data.IsSuccessful == false ? ECancelSellOrderStatus.Fail : ECancelSellOrderStatus.Canceled;
        }

        public OrderGraph ConvertOrderGraph(List<dynamic[]> collection)
        {
            var graph = new OrderGraph { Orders = new List<OrderGraphItem>() };

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Count == 0)
            {
                return graph;
            }

            var tempCount = 0;
            var list = collection.Select(
                x =>
                    {
                        var item = new OrderGraphItem
                                       {
                                           Count = (int)x[1] - tempCount, Price = (double)x[0], Title = (string)x[2]
                                       };
                        tempCount = item.Count;
                        return item;
                    }).ToList();

            graph.Orders = list;
            graph.Total = (int)collection.Last()[1];

            return graph;
        }

        public CreateBuyOrder CreateBuyOrder(
            string hashName,
            int appId,
            int currency,
            double totalPrice,
            int quantity = 1)
        {
            var data = new Dictionary<string, string>
                           {
                               { "sessionid", this.steam.Auth.SessionId() },
                               { "currency", currency.ToString() },
                               { "appid", appId.ToString() },
                               { "market_hash_name", hashName },
                               { "price_total", (totalPrice * 100 * quantity).ToString(CultureInfo.InvariantCulture) },
                               { "quantity", quantity.ToString() }
                           };

            var resp = this.steam.Request(Urls.Market + "/createbuyorder/", Method.POST, Urls.Market, data, true);
            var respDes = JsonConvert.DeserializeObject<JCreateBuyOrder>(resp.Data.Content);

            var order = new CreateBuyOrder();

            switch (respDes.Success)
            {
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
                default:
                    order.Status = ECreateBuyOrderStatus.Fail;
                    break;
            }

            return order;
        }

        public Task<CreateBuyOrder> CreateBuyOrderAsync(
            string hashName,
            int appId,
            int currency,
            double totalPrice,
            int quantity = 1)
        {
            var result = new Task<CreateBuyOrder>(
                () => this.CreateBuyOrder(hashName, appId, currency, totalPrice, quantity));
            result.Start();
            return result;
        }

        public SellListingsPage FetchSellOrders(int start = 0, int count = 100)
        {
            var sellListingsPage = new SellListingsPage { SellListings = new List<MyListingsSalesItem>() };

            var @params = new Dictionary<string, string> { { "start", $"{start}" }, { "count", $"{count}" } };
            var resp = this.steam.Request(Urls.Market + "/mylistings/", Method.GET, Urls.Market, @params, true);

            JMyListings respDes;
            try
            {
                respDes = JsonConvert.DeserializeObject<JMyListings>(resp.Data.Content);
            }
            catch (Exception e)
            {
                throw new SteamException($"Cannot load market listings - {e.Message}");
            }

            if (!respDes.Success)
            {
                throw new SteamException("Cannot load market listings");
            }

            var totalCount = respDes.ActiveListingsCount;

            var html = respDes.ResultsHtml;
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var root = doc.DocumentNode;

            var sellCountNode = root.SelectSingleNode(".//span[@id='my_market_selllistings_number']");
            if (sellCountNode == null)
            {
                throw new SteamException("Cannot find sell listings node");
            }

            var sellCountParse = int.TryParse(sellCountNode.InnerText, out var sellCount);
            if (!sellCountParse)
            {
                throw new SteamException("Cannot parse sell listings node value");
            }

            sellListingsPage.TotalCount = sellCount;

            var sellNodes = root.SelectNodes("//div[contains(@id,'mylisting_')]");

            if (sellNodes != null)
            {
                foreach (var item in sellNodes)
                {
                    var isConfirmation = item.InnerHtml.Contains("CancelMarketListingConfirmation");
                    if (isConfirmation) continue;

                    sellListingsPage.SellListings.Add(this.ProcessSellListings(item, $"{this.CurrentCurrency}"));
                }
            }

            return sellListingsPage;
        }

        public ItemOrdersHistogram ItemOrdersHistogram(int nameId, string country, ELanguage lang, int currency)
        {
            var url = Urls.Market
                      + $"/itemordershistogram?country={country}&language={lang}&currency={currency}&item_nameid={nameId}";
            var resp = this.steam.Request(url, Method.GET, Urls.Market, null, true);
            var respDes = JsonConvert.DeserializeObject<JItemOrdersHistogram>(resp.Data.Content);

            if (respDes.Success != 1)
            {
                throw new SteamException($"Cannot load orders histogram - error {respDes.Success}");
            }

            var histogram = new ItemOrdersHistogram { SellOrderGraph = this.ConvertOrderGraph(respDes.SellOrderGraph) };

            if (respDes.BuyOrderGraph != null)
            {
                histogram.BuyOrderGraph = this.ConvertOrderGraph(respDes.BuyOrderGraph);
            }

            if (respDes.MinSellPrice != null)
            {
                histogram.MinSellPrice = (double)respDes.MinSellPrice / 100;
            }

            if (respDes.HighBuyOrder != null)
            {
                histogram.HighBuyOrder = (double)respDes.HighBuyOrder / 100;
            }

            return histogram;
        }

        public Task<ItemOrdersHistogram> ItemOrdersHistogramAsync(
            int nameId,
            string country,
            ELanguage lang,
            int currency)
        {
            var result = new Task<ItemOrdersHistogram>(() => this.ItemOrdersHistogram(nameId, country, lang, currency));
            result.Start();
            return result;
        }

        public MarketItemInfo ItemPage(int appId, string hashName)
        {
            if (string.IsNullOrEmpty(hashName))
            {
                throw new SteamException("HashName should not be empty");
            }

            var resp = this.steam.Request(
                Urls.Market + $"/listings/{appId}/{Uri.EscapeDataString(hashName)}",
                Method.GET,
                Urls.Market,
                null,
                true);
            var content = resp.Data.Content;

            var nameIdParse = int.TryParse(
                Regex.Match(content, @"(?<=Market_LoadOrderSpread\()(.*)(?=\);)").Value.Trim(),
                out var nameId);

            if (!nameIdParse)
            {
                throw new SteamException($"Unable to find name ID of {hashName}");
            }

            var publisherFeePercentMatch = Regex.Match(content, "(?<=\"publisher_fee_percent\":\")(.*?)(?=\")").Value;

            int publisherFeePercent;
            if (string.IsNullOrEmpty(publisherFeePercentMatch))
            {
                publisherFeePercent = PublisherFeePercentDefault;
            }
            else
            {
                var publisherFeePercentParse = decimal.TryParse(
                    publisherFeePercentMatch,
                    NumberStyles.AllowDecimalPoint,
                    CultureInfo.InvariantCulture,
                    out var publisherFeePercentOut);

                if (!publisherFeePercentParse)
                {
                    throw new SteamException("Cannot to parse publisher fee percent");
                }

                publisherFeePercent = (int)Math.Round(publisherFeePercentOut * 100);
            }

            var item = new MarketItemInfo { NameId = nameId, PublisherFeePercent = publisherFeePercent };

            return item;
        }

        // public Task<MyListings> MyListingsAsync()
        // {
        // var result = new Task<MyListings>(MyListings);
        // result.Start();
        // return result;
        // }

        // public List<HistoryItem> History(int count = 100, int start = 0)
        // {
        // count = count > 500 ? 500 : count;

        // var data = new Dictionary<string, string>
        // {
        // {"count", count.ToString()},
        // {"start", start.ToString()}
        // };

        // var resp = _steam.Request(Urls.Market + "/myhistory/render/", Method.GET, Urls.Market, data, true);
        // JMyHistory respDes;

        // try
        // {
        // respDes = JsonConvert.DeserializeObject<JMyHistory>(resp.Data.Content);
        // }
        // catch
        // {
        // throw new SteamException("Cannot load market history");
        // }

        // if (!respDes.Success)
        // throw new SteamException("Cannot load market history");

        // if (respDes.TotalCount == null || respDes.TotalCount == 0)
        // return new List<HistoryItem>();

        // var doc = new HtmlDocument();
        // doc.LoadHtml(respDes.Html);

        // var historyNodes =
        // doc.DocumentNode.SelectNodes(
        // "//div[contains(@id, 'history_row_') and @class='market_listing_row market_recent_listing_row']");

        // var hoversMatches = Regex.Matches(respDes.Hovers,
        // @"(?<=CreateItemHoverFromContainer\( g_rgAssets,)(.*?)(?=\))");

        // var hovers = hoversMatches.Cast<Match>()
        // .Select(s => s.Value.Split(',').Apply(a => a.Trim(' ', '\''))) - Apply function is unknown, the method is unnecessary so far
        // .Where(w => w[0].Contains("_name"))
        // .Select(s => new HistoryItemHover
        // {
        // AppId = int.Parse(s[1]),
        // ContextId = int.Parse(s[2]),
        // AssetId = long.Parse(s[3]),
        // HoverId = s[0].Replace("_name", "")
        // });

        // var assets = respDes.Assets.SelectMany(s => s.Value.SelectMany(c => c.Value.Select(m => m.Value)));

        // var hoverAssetCompare = hovers
        // .Select(s => new KeyValuePair<HistoryItemHover, JDescription>(s, assets.First(f => f.Id == s.AssetId)));

        // var history = historyNodes.Select(s =>
        // {
        // var actionTypeString =
        // s.SelectSingleNode("div[@class='market_listing_left_cell market_listing_gainorloss']")
        // .InnerText.Trim();

        // EMyHistoryActionType actionType;
        // switch (actionTypeString)
        // {
        // case "+":
        // actionType = EMyHistoryActionType.Buy;
        // break;
        // case "-":
        // actionType = EMyHistoryActionType.Sell;
        // break;
        // default:
        // actionType = EMyHistoryActionType.None;
        // break;
        // }

        // var priceString = s.SelectSingleNode("div[contains(@class, 'market_listing_their_price')]")
        // .InnerText.Trim()
        // .Split(' ')[0];

        // var dates =
        // s.SelectNodes("div[contains(@class, 'market_listing_listed_date')]")
        // .Select(x => x.InnerText.Trim()).ToArray();

        // var memberString = s.SelectSingleNode(".//div[@class='market_listing_whoactedwith_name_block']");
        // var member = memberString?.ChildNodes[2].InnerText.Trim() ?? s.SelectSingleNode("div[contains(@class, 'market_listing_whoactedwith')]").InnerText.Trim();

        // return new HistoryItem
        // {
        // Asset = hoverAssetCompare.FirstOrDefault(w => w.Key.HoverId == s.Id).Value,
        // Game = s.SelectSingleNode(".//span[contains(@class, 'market_listing_game_name')]").InnerText.Trim(),
        // Name = s.SelectSingleNode(".//span[contains(@class, 'market_listing_item_name')]").InnerText.Trim(),
        // ActionType = actionType,
        // Member = member,
        // PlaceDate = string.IsNullOrEmpty(dates[0]) ? (DateTime?)null : Convert.ToDateTime(dates[0]),
        // DealDate = string.IsNullOrEmpty(dates[1]) ? (DateTime?)null : Convert.ToDateTime(dates[1]),
        // Price =
        // string.IsNullOrEmpty(priceString)
        // ? (double?)null
        // : double.Parse(priceString, NumberStyles.Currency, new CultureInfo("en-US"))

        // };
        // }).Where(w => w.ActionType != EMyHistoryActionType.None).ToList();

        // return history;
        // }
        public MyListings MyListings(string currency = "5", int start = 0, int count = 100)
        {
            var myListings = new MyListings
                                 {
                                     Orders = new List<MyListingsOrdersItem>(),
                                     Sales = new List<MyListingsSalesItem>(),
                                     ConfirmationSales = new List<MyListingsSalesItem>()
                                 };

            var @params = new Dictionary<string, string> { { "start", $"{start}" }, { "count", $"{count}" } };
            var resp = this.steam.Request(Urls.Market + "/mylistings/", Method.GET, Urls.Market, @params, true);

            JMyListings respDes;
            try
            {
                respDes = JsonConvert.DeserializeObject<JMyListings>(resp.Data.Content);
            }
            catch (Exception e)
            {
                throw new SteamException($"Cannot load market listings - {e.Message}");
            }

            if (!respDes.Success)
            {
                throw new SteamException("Cannot load market listings");
            }

            var totalCount = respDes.ActiveListingsCount;

            var html = respDes.ResultsHtml;
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var root = doc.DocumentNode;

            var ordersCountNode = root.SelectSingleNode(".//span[@id='my_market_buylistings_number']");
            if (ordersCountNode == null)
            {
                throw new SteamException("Cannot find buy listings node");
            }

            var ordersCountParse = int.TryParse(ordersCountNode.InnerText, out var ordersCount);
            if (!ordersCountParse)
            {
                throw new SteamException("Cannot parse buy listings node value");
            }

            var sellCountNode = root.SelectSingleNode(".//span[@id='my_market_selllistings_number']");
            if (sellCountNode == null)
            {
                throw new SteamException("Cannot find sell listings node");
            }

            var sellCountParse = int.TryParse(sellCountNode.InnerText, out var sellCount);
            if (!sellCountParse)
            {
                throw new SteamException("Cannot parse sell listings node value");
            }

            var confirmCountNode = root.SelectSingleNode(".//span[@id='my_market_listingstoconfirm_number']");
            var confirmCount = 0;
            if (confirmCountNode != null)
            {
                var confirmCountParse = int.TryParse(confirmCountNode.InnerText, out confirmCount);
                if (!confirmCountParse)
                {
                    throw new SteamException("Cannot parse confirm listings node value");
                }
            }

            myListings.Total = totalCount;
            myListings.ItemsToBuy = ordersCount;
            myListings.ItemsToConfirm = confirmCount;
            myListings.ItemsToSell = sellCount;

            var tempIndex = 0;
            var ordersNodes = root.SelectNodes("//div[contains(@id,'mybuyorder_')]");
            if (ordersNodes != null)
            {
                foreach (var item in ordersNodes)
                {
                    this.GetPendingTransactionData(item, tempIndex, myListings, ETransactionType.Order, currency, true);

                    tempIndex++;
                }

                myListings.SumOrderPricesToBuy = Math.Round(myListings.Orders.Sum(s => s.Price), 2);
            }

            this.ProcessMyListingsSellOrders(root, currency, myListings);

            @params = new Dictionary<string, string> { { "start", $"{start}" }, { "count", $"{count}" } };
            resp = this.steam.Request(Urls.Market + "/mylistings/", Method.GET, Urls.Market, @params, true);
            respDes = JsonConvert.DeserializeObject<JMyListings>(resp.Data.Content);
            html = respDes.ResultsHtml;
            doc.LoadHtml(html);
            root = doc.DocumentNode;

            this.ProcessMyListingsSellOrders(root, currency, myListings);

            return myListings;
        }

        public List<PriceHistoryDay> PriceHistory(int appId, string hashName)
        {
            var url = Urls.Market + $"/pricehistory/?appid={appId}&market_hash_name={Uri.EscapeDataString(hashName)}";

            var resp = this.steam.Request(url, Method.GET, Urls.Market, null, true);

            var respDes = JsonConvert.DeserializeObject<JPriceHistory>(resp.Data.Content);

            if (!respDes.Success)
            {
                throw new SteamException($"Cannot get price history for [{hashName}]");
            }

            IEnumerable<dynamic> prices = Enumerable.ToList(respDes.Prices);

            var list = prices.Select(
                (g, index) =>
                    {
                        var count = 0;
                        if (g[2] != null && !int.TryParse(g[2].ToString(), out count))
                        {
                            throw new SteamException($"Cannot parse items count in price history. Index: {index}");
                        }

                        double price = 0;
                        if (g[1] != null && !double.TryParse(g[1].ToString(), out price))
                        {
                            throw new SteamException($"Cannot parse item price in price history. Index: {index}");
                        }

                        return new PriceHistoryItem
                                   {
                                       DateTime = Utils.SteamTimeConvertor(g[0].ToString()),
                                       Count = count,
                                       Price = Math.Round(price, 2)
                                   };
                    }).GroupBy(x => x.DateTime.Date).Select(
                g => new PriceHistoryDay { Date = g.Key, History = g.Select(p => p).ToList() }).ToList();

            return list;
        }

        public Task<List<PriceHistoryDay>> PriceHistoryAsync(int appid, string hashName)
        {
            var result = new Task<List<PriceHistoryDay>>(() => this.PriceHistory(appid, hashName));
            result.Start();
            return result;
        }

        public MarketProfile Profile()
        {
            var resp = this.steam.Request(Urls.Market, Method.GET, Urls.Market, useAuthCookie: true);
            var content = resp.Data.Content;

            var doc = new HtmlDocument();
            doc.LoadHtml(content);

            var userLoginNode = doc.DocumentNode.SelectSingleNode("//span[@id='market_buynow_dialog_myaccountname']");

            if (userLoginNode == null)
            {
                throw new SteamException("Unable to load profile");
            }

            var userLogin = userLoginNode.InnerText;

            var userIdMatch = Regex.Match(content, "(?<=g_steamID = \")(.*)(?=\";)").Value;

            if (string.IsNullOrEmpty(userIdMatch))
            {
                throw new SteamException("Unable to load profile (User ID)");
            }

            var userIdParse = long.TryParse(userIdMatch, out var userId);

            if (!userIdParse)
            {
                throw new SteamException("Unable to parse user ID)");
            }

            var userAvatarNode = doc.DocumentNode.SelectSingleNode("//span[@class='avatarIcon']");

            if (userAvatarNode == null)
            {
                throw new SteamException("Unable to load profile (User avatar node)");
            }

            var userAvatarImgNode = userAvatarNode.ChildNodes[1];

            if (userAvatarImgNode == null)
            {
                throw new SteamException("Unable to load profile (User avatar img node");
            }

            var userAvatarSrc = userAvatarImgNode.Attributes["src"].Value;

            if (string.IsNullOrEmpty(userAvatarSrc))
            {
                throw new SteamException("Unable to load profile (User avatar img source");
            }

            var walletInfoMatch = Regex.Match(content, "(?<=g_rgWalletInfo = )(.*)(?=;)").Value;

            if (string.IsNullOrEmpty(walletInfoMatch))
            {
                throw new SteamException("Unable to parse wallet info");
            }

            var walletInfo = this.WalletInfo(walletInfoMatch);

            var marketProfile = new MarketProfile
                                    {
                                        AvatarUrl = userAvatarSrc,
                                        Id = userId,
                                        Login = userLogin,
                                        WalletInfo = walletInfo
                                    };

            return marketProfile;
        }

        public Task<MarketProfile> ProfileAsync()
        {
            var result = new Task<MarketProfile>(this.Profile);
            result.Start();
            return result;
        }

        public MarketSearch Search(
            string query = "",
            int start = 0,
            int count = 10,
            bool searchInDescriptions = false,
            int appId = 0,
            EMarketSearchSortColumns sortColumn = EMarketSearchSortColumns.Name,
            ESort sort = ESort.Desc,
            IDictionary<string, string> custom = null)
        {
            var urlQuery = new Dictionary<string, string>
                               {
                                   { "query", query },
                                   { "start", start.ToString() },
                                   { "count", count.ToString() },
                                   { "search_descriptions", (searchInDescriptions ? 1 : 0).ToString() },
                                   { "appid", appId.ToString() },
                                   { "sort_column", Utils.FirstCharacterToLower(sortColumn.ToString()) },
                                   { "sort_dir", Utils.FirstCharacterToLower(sort.ToString()) }
                               };

            if (custom != null && custom.Count > 0)
            {
                urlQuery = urlQuery.Concat(custom).ToDictionary(x => x.Key, x => x.Value);
            }

            var resp = this.steam.Request(Urls.Market + "/search/render/", Method.GET, Urls.Market, urlQuery, true);
            var respDes = JsonConvert.DeserializeObject<JMarketSearch>(resp.Data.Content);

            if (!respDes.Success)
            {
                throw new SteamException("Failed Search");
            }

            var marketSearch = new MarketSearch();

            if (respDes.TotalCount <= 0)
            {
                return marketSearch;
            }

            marketSearch.TotalCount = respDes.TotalCount;

            var html = respDes.ResultsHtml;
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var itemNodes = doc.DocumentNode.SelectNodes("//a[@class='market_listing_row_link']");

            if (itemNodes == null || itemNodes.Count <= 0)
            {
                return marketSearch;
            }

            var tempIndex = 0;

            foreach (var item in itemNodes)
            {
                tempIndex++;

                var nameNode = item.SelectSingleNode(".//span[@class='market_listing_item_name']");
                if (nameNode == null)
                {
                    throw new SteamException($"Cannot parse item name. Index [{tempIndex}]");
                }

                var name = nameNode.InnerText;

                var gameNode = item.SelectSingleNode(".//span[@class='market_listing_game_name']");
                if (gameNode == null)
                {
                    throw new SteamException($"Cannot parse game name. Index [{tempIndex}]");
                }

                var game = gameNode.InnerText;

                var imageUrlNode = item.SelectSingleNode(".//img[@class='market_listing_item_img']");

                var imageUrl = imageUrlNode?.Attributes["srcset"]?.Value;

                var urlAttr = item.Attributes["href"];
                if (urlAttr == null)
                {
                    throw new SteamException($"Cannot find item url. Item [{name}], Index [{tempIndex}]");
                }

                var url = urlAttr.Value;

                var quantityNode = item.SelectSingleNode(".//span[@class='market_listing_num_listings_qty']");
                if (quantityNode == null)
                {
                    throw new SteamException($"Cannot find quantity. Item [{name}], Index [{tempIndex}]");
                }

                var quantityParse = int.TryParse(
                    quantityNode.InnerText,
                    NumberStyles.AllowThousands,
                    CultureInfo.InvariantCulture,
                    out var quantity);

                if (!quantityParse)
                {
                    throw new SteamException($"Cannot parse quantity. Item [{name}], Index [{tempIndex}]");
                }

                var minPriceNode = item.SelectSingleNode(".//span[@class='normal_price']");
                if (minPriceNode == null)
                {
                    throw new SteamException($"Cannot find item price. Item [{name}], Index [{tempIndex}]");
                }

                var minPriceClean = minPriceNode.InnerText.Split(' ')[0];

                var minPriceParse = double.TryParse(
                    minPriceClean,
                    NumberStyles.Currency,
                    CultureInfo.GetCultureInfo("en-US"),
                    out var minPrice);
                if (!minPriceParse)
                {
                    throw new SteamException($"Cannot parse min. price. Item [{name}], Index [{tempIndex}]");
                }

                marketSearch.Items.Add(
                    new MarketSearchItem
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

        public dynamic SellItem(int appId, int contextId, long assetId, int amount, double priceWithoutFee)
        {
            var data = new Dictionary<string, string>
                           {
                               { "appid", appId.ToString() },
                               { "sessionid", this.steam.Auth.SessionId() },
                               { "contextid", contextId.ToString() },
                               { "assetid", assetId.ToString() },
                               { "amount", amount.ToString() },
                               {
                                   "price",
                                   (Math.Round(priceWithoutFee, 2) * 100).ToString(CultureInfo.InvariantCulture)
                               }
                           };

            var resp = this.steam.Request(Urls.Market + "/sellitem/", Method.POST, Urls.Market, data, true).Data
                .Content;
            return JsonConvert.DeserializeObject<JSellItem>(resp);
        }

        public WalletInfo WalletInfo()
        {
            var resp = this.steam.Request(Urls.Market, Method.GET, Urls.Market, useAuthCookie: true);

            var walletInfoMatch = Regex.Match(resp.Data.Content, "(?<=g_rgWalletInfo = )(.*)(?=;)").Value;

            if (string.IsNullOrEmpty(walletInfoMatch))
            {
                throw new SteamException("Unable to parse wallet info");
            }

            return this.WalletInfo(walletInfoMatch);
        }

        public WalletInfo WalletInfo(string json)
        {
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
                                     WalletPublisherFeePercent =
                                         Convert.ToInt32(walletInfoDes.WalletPublisherFeePercent * 100),
                                     WalletTradeMaxBalance = (double)walletInfoDes.WalletTradeMaxBalance / 100
                                 };

            return walletInfo;
        }

        private void GetPendingTransactionData(
            HtmlNode item,
            int tempIndex,
            MyListings myListings,
            ETransactionType type,
            string currency,
            bool processConfirmations)
        {
            var node = item.SelectSingleNode(".//span[@class='market_listing_price']");
            if (node == null)
            {
                throw new SteamException(
                    $"Cannot parse order listing price and quantity node. Item index [{tempIndex}]");
            }

            var date = Regex.Match(item.InnerText, @"Listed: (.+)?\s").Groups[1].Value.Trim();
            var game = item.SelectSingleNode("//span[@class='market_listing_game_name']").InnerText;
            if (type == ETransactionType.Order)
            {
                var priceAndQuantityString = node.InnerText.Replace("\r", string.Empty).Replace("\n", string.Empty)
                    .Replace("\t", string.Empty).Replace(" ", string.Empty);
                var priceAndQuantitySplit = priceAndQuantityString.Split('@');

                double price;
                try
                {
                    var priceParse = priceAndQuantitySplit[1].Replace(".", string.Empty);
                    var currencySymbol = this.Currencies[currency];
                    double.TryParse(priceParse.Replace(currencySymbol, string.Empty), out price);
                }
                catch (Exception)
                {
                    throw new SteamException($"Cannot parse order listing price. Item index [{tempIndex}]");
                }

                var quantityParse = int.TryParse(priceAndQuantitySplit[0], out var quantity);

                if (!quantityParse)
                {
                    throw new SteamException($"Cannot parse order listing quantity. Item index [{tempIndex}]");
                }

                var orderIdMatch = Regex.Match(item.InnerHtml, "(?<=mbuyorder_)([0-9]*)(?=_name)");

                if (!orderIdMatch.Success)
                {
                    throw new SteamException($"Cannot find order listing ID. Item index [{tempIndex}]");
                }

                var orderIdParse = long.TryParse(orderIdMatch.Value, out var orderId);

                if (!orderIdParse)
                {
                    throw new SteamException($"Cannot parse order listing ID. Item index [{tempIndex}]");
                }

                var imageUrl = item.SelectSingleNode($"//img[contains(@id, 'mylisting_{orderId}_image')]")
                    .Attributes["src"].Value;

                imageUrl = Regex.Match(imageUrl, "image/(.*)/").Groups[1].Value;

                var urlNode = item.SelectSingleNode(".//a[@class='market_listing_item_name_link']");
                if (urlNode == null)
                {
                    throw new SteamException($"Cannot find order listing url. Item index [{tempIndex}]");
                }

                var url = urlNode.Attributes["href"].Value;

                var hashName = Utils.HashNameFromUrl(url);
                var appId = Utils.AppIdFromUrl(url);

                var nameNode = urlNode.InnerText;

                myListings.Orders.Add(
                    new MyListingsOrdersItem
                        {
                            AppId = appId,
                            HashName = hashName,
                            Name = nameNode,
                            Date = date,
                            OrderId = orderId,
                            Price = price,
                            Quantity = quantity,
                            Url = url,
                            ImageUrl = imageUrl,
                            Game = game
                        });
            }
            else
            {
                var priceString = node.InnerText.Replace("\r", string.Empty).Replace("\n", string.Empty)
                    .Replace("\t", string.Empty).Replace(" ", string.Empty);
                double price;
                try
                {
                    var priceParse = priceString.Split('(')[0].Replace(".", string.Empty);
                    var currencySymbol = this.Currencies[currency];
                    double.TryParse(priceParse.Replace(currencySymbol, string.Empty), out price);
                }
                catch (Exception)
                {
                    throw new SteamException($"Cannot parse order listing price. Item index [{tempIndex}]");
                }

                var saleIdMatch = Regex.Match(item.InnerHtml, "(?<=mylisting_)([0-9]*)(?=_name)");
                if (!saleIdMatch.Success)
                {
                    throw new SteamException($"Cannot find sale listing ID. Item index [{tempIndex}]");
                }

                var saleIdParse = long.TryParse(saleIdMatch.Value, out var saleId);

                if (!saleIdParse)
                {
                    throw new SteamException($"Cannot parse sale listing ID. Item index [{tempIndex}]");
                }

                var imageUrl = item.SelectSingleNode($"//img[contains(@id, 'mylisting_{saleId}_image')]")
                    .Attributes["src"].Value.Replace("38fx38f", string.Empty).Replace(
                        "https://steamcommunity-a.akamaihd.net/economy/image/",
                        string.Empty);

                var urlNode = item.SelectSingleNode(".//a[@class='market_listing_item_name_link']");
                if (urlNode == null)
                {
                    throw new SteamException($"Cannot find sale listing url. Item index [{tempIndex}]");
                }

                var url = urlNode.Attributes["href"].Value;

                var hashName = Utils.HashNameFromUrl(url);
                var appId = Utils.AppIdFromUrl(url);

                var nameNode = urlNode.InnerText;

                var result = new MyListingsSalesItem
                                 {
                                     AppId = appId,
                                     HashName = hashName,
                                     Name = nameNode,
                                     Date = date,
                                     SaleId = saleId,
                                     Price = price,
                                     Url = url,
                                     ImageUrl = imageUrl,
                                     Game = game
                                 };

                var isConfirmation = item.InnerHtml.Contains("CancelMarketListingConfirmation");

                if (isConfirmation == false)
                {
                    myListings.Sales.Add(result);
                }
                else if (processConfirmations)
                {
                    myListings.ConfirmationSales.Add(result);
                }
            }
        }

        private void ProcessMyListingsSellOrders(HtmlNode root, string currency, MyListings myListings)
        {
            var saleNodes = root.SelectNodes("//div[contains(@id,'mylisting_')]");
            if (saleNodes != null)
            {
                var tempIndex = 0;
                foreach (var item in saleNodes)
                {
                    this.GetPendingTransactionData(item, tempIndex, myListings, ETransactionType.Sale, currency, false);

                    tempIndex++;
                }
            }
        }

        private MyListingsSalesItem ProcessSellListings(HtmlNode item, string currency)
        {
            var node = item.SelectSingleNode(".//span[@class='market_listing_price']");
            var date = Regex.Match(item.InnerText, @"Listed: (.+)?\s").Groups[1].Value.Trim();
            var priceString = node.InnerText.Replace("\r", string.Empty).Replace("\n", string.Empty)
                .Replace("\t", string.Empty).Replace(" ", string.Empty);
            var game = item.SelectSingleNode("//span[@class='market_listing_game_name']").InnerText;
            double price;
            try
            {
                var priceParse = priceString.Split('(')[0].Replace(".", string.Empty);
                var currencySymbol = this.Currencies[currency];
                double.TryParse(
                    priceParse.Replace(currencySymbol, string.Empty).Replace(
                        ",",
                        Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator),
                    out price);
            }
            catch (Exception)
            {
                throw new SteamException("Cannot parse order listing price");
            }

            var saleIdMatch = Regex.Match(item.InnerHtml, "(?<=mylisting_)([0-9]*)(?=_name)");
            if (!saleIdMatch.Success)
            {
                throw new SteamException("Cannot find sale listing ID");
            }

            var saleIdParse = long.TryParse(saleIdMatch.Value, out var saleId);

            if (!saleIdParse)
            {
                throw new SteamException("Cannot parse sale listing ID");
            }

            var imageUrl = item.SelectSingleNode($"//img[contains(@id, 'mylisting_{saleId}_image')]").Attributes["src"]
                .Value;

            imageUrl = Regex.Match(imageUrl, "image/(.*)/").Groups[1].Value;

            var urlNode = item.SelectSingleNode(".//a[@class='market_listing_item_name_link']");
            if (urlNode == null)
            {
                throw new SteamException("Cannot find sale listing url");
            }

            var url = urlNode.Attributes["href"].Value;

            var hashName = Utils.HashNameFromUrl(url);
            var appId = Utils.AppIdFromUrl(url);

            var nameNode = urlNode.InnerText;

            var result = new MyListingsSalesItem
                             {
                                 AppId = appId,
                                 HashName = hashName,
                                 Name = nameNode,
                                 Date = date,
                                 SaleId = saleId,
                                 Price = price,
                                 Url = url,
                                 ImageUrl = imageUrl,
                                 Game = game
                             };

            return result;
        }
    }
}
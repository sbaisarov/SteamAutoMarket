namespace SteamAutoMarket.Steam.Market
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using log4net.Core;

    using Newtonsoft.Json;

    using RestSharp;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.Steam.Market.Enums;
    using SteamAutoMarket.Steam.Market.Exceptions;
    using SteamAutoMarket.Steam.Market.Interface;
    using SteamAutoMarket.Steam.Market.Models;
    using SteamAutoMarket.Steam.Market.Models.Json.SteamStatus;
    using SteamAutoMarket.UI.Repository.Settings;

    public class SteamMarketHandler
    {
        private readonly object _requestsPerSecondLock;

        private float _minInterval;

        private float _requestsPerSecond;

        public SteamMarketHandler(ELanguage language, string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent)) throw new ArgumentException("Empty UserAgent");

            this.Settings = new Settings { UserAgent = userAgent, Language = language };
            this.Auth = new Auth(this);
            this.Client = new MarketClient(this);
            this.Inventory = new Invertory(this);

            this._requestsPerSecondLock = new object();
            this.RequestsPerSecond = 3;
        }

        public Auth Auth { get; set; }

        public MarketClient Client { get; }

        public Invertory Inventory { get; }

        public Settings Settings { get; }

        private DateTimeOffset? LastInvokeTime { get; set; }

        private TimeSpan? LastInvokeTimeSpan
        {
            get
            {
                if (this.LastInvokeTime.HasValue) return DateTimeOffset.Now - this.LastInvokeTime.Value;
                return null;
            }
        }

        private float RequestsPerSecond
        {
            get => this._requestsPerSecond;

            set
            {
                this._requestsPerSecond = value;
                lock (this._requestsPerSecondLock)
                {
                    this._minInterval = (int)(1000 / this._requestsPerSecond) + 1;
                }
            }
        }

        public bool CheckConnection()
        {
            var response = this.Request(Urls.SteamCommunity, Method.POST, string.Empty);
            return response.Data.StatusCode == HttpStatusCode.OK;
        }

        public Task<bool> CheckConnectionAsync()
        {
            var result = new Task<bool>(this.CheckConnection);
            result.Start();
            return result;
        }

        public SteamResponse Request(
            string url,
            Method method,
            string referer,
            IDictionary<string, string> @params = null,
            bool useAuthCookie = false,
            CookieContainer cookieContainer = null,
            IDictionary<string, string> headers = null,
            WebProxy proxy = null)
        {
            if (Logger.CurrentLogLevel == Level.Debug)
            {
                Logger.Log.Debug(
                    $"{method} steam request to {url}. Referer - {referer}, params - {StringUtils.DictionaryToString(@params)}, headers - {StringUtils.DictionaryToString(headers)}.\nProxy {proxy?.Address}");
            }

            this.RequestsPerSecondGuard();

            var client = new RestClient(url) { UserAgent = this.Settings.UserAgent, Timeout = 60 * 1000 };

            if (proxy != null)
            {
                client.Proxy = proxy;
            }

            if (useAuthCookie)
            {
                if (this.Auth.IsAuthorized)
                    client.CookieContainer = this.Auth.CookieContainer;
                else
                    throw new AuthorizationRequiredException();
            }
            else
            {
                client.CookieContainer = cookieContainer ?? new CookieContainer();
            }

            var request = new RestRequest { Method = method };

            if (@params != null && @params.Count > 0)
                foreach (var p in @params)
                    request.AddParameter(p.Key, p.Value);

            if (headers != null && headers.Count > 0)
                foreach (var h in headers)
                    request.AddHeader(h.Key, h.Value);

            request.AddHeader("Referer", referer);
            request.AddHeader("User-Agent", SettingsProvider.GetInstance().UserAgent);
            request.AddHeader("Origin", "https://steamcommunity.com");
            request.AddHeader("Host", "steamcommunity.com");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Accept-Language", "en-US;q=0.9,en;q=0.8,uk;q=0.7,es;q=0.6");
            request.AddHeader("Cache-Control", "no-cache");

            this.LastInvokeTime = DateTimeOffset.Now;
            var response = client.Execute(request);

            if (response.ErrorException != null)
            {
                Logger.Log.Debug(
                    $"Response failed with error - ({response.StatusDescription}) {response.ErrorException?.Message}");
                throw new RequestException(response.ErrorException?.Message);
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException($"Bad status code: {response.StatusCode}");

            if (Logger.CurrentLogLevel == Level.Debug)
            {
                Logger.Log.Debug(
                    $"Steam response({response.StatusCode}) - {StringUtils.ClearString(response.Content)}");
            }

            return new SteamResponse(response, client.CookieContainer);
        }

        public JSteamStatus SteamStatus()
        {
            var resp = this.Request(Urls.SteamStatus, Method.GET, string.Empty);
            return JsonConvert.DeserializeObject<JSteamStatus>(resp.Data.Content);
        }

        public Task<JSteamStatus> SteamStatusAsync()
        {
            var result = new Task<JSteamStatus>(this.SteamStatus);
            result.Start();
            return result;
        }

        private void RequestsPerSecondGuard()
        {
            if (this.RequestsPerSecond > 0 && this.LastInvokeTime.HasValue)
                lock (this._requestsPerSecondLock)
                {
                    var span = this.LastInvokeTimeSpan?.TotalMilliseconds;
                    if (span < this._minInterval)
                    {
                        var timeout = (int)this._minInterval - (int)span;
                        Thread.Sleep(timeout);
                    }
                }
        }
    }
}
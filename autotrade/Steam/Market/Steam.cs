using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Market.Enums;
using Market.Exceptions;
using Market.Models;
using Market.Models.Json.SteamStatus;
 
namespace Market
{
    public class Steam
    {
        public Settings Settings { get; }
        public Auth Auth { get; set; }
        public Invertory Invertory { get; }
        public Interface.Client Client { get; }

        private readonly object _requestsPerSecondLock;
        private float _requestsPerSecond;
        private float _minInterval;
        private DateTimeOffset? LastInvokeTime { get; set; }
        private TimeSpan? LastInvokeTimeSpan
        {
            get
            {
                if (LastInvokeTime.HasValue)
                {
                    return DateTimeOffset.Now - LastInvokeTime.Value;
                }
                return null;
            }
        }
        private float RequestsPerSecond
        {
            get => _requestsPerSecond;

            set
            {
                _requestsPerSecond = value;
                _minInterval = (int)(1000 / _requestsPerSecond) + 1;
            }
        }

        public Steam(ELanguage language, string userAgent)
        {

            if (string.IsNullOrEmpty(userAgent))
            {
                throw new ArgumentException("Empty UserAgent");
            }

            Settings = new Settings
            {
                UserAgent = userAgent,
                Language = language,
            };
            Auth = new Auth(this);
            Client = new Interface.Client(this);
            Invertory = new Invertory(this);

            _requestsPerSecondLock = new object();
            RequestsPerSecond = 3;
        }

        private void RequestsPerSecondGuard()
        {
            if (RequestsPerSecond > 0 && LastInvokeTime.HasValue)
            {
                lock (_requestsPerSecondLock)
                {
                    var span = LastInvokeTimeSpan?.TotalMilliseconds;
                    if (span < _minInterval)
                    {
                        var timeout = (int)_minInterval - (int)span;
                        Thread.Sleep(timeout);
                    }
                }
            }
        }

        public SteamResponse Request(string url, Method method, string referer,IDictionary<string, string> @params = null, bool useAuthCookie = false, CookieContainer cookieContainer = null)
        {

            RequestsPerSecondGuard();

            var client = new RestClient(url)
            {
                UserAgent = Settings.UserAgent
            };

            if (useAuthCookie)
            {
                if (Auth.IsAuthorized)
                {
                    client.CookieContainer = Auth.CookieContainer;
                }
                else
                { 
                    throw new AuthorizationRequiredException();
                }
                
            }
            else
            {
                client.CookieContainer = cookieContainer ?? new CookieContainer();
            }

            var request = new RestRequest
            {
                Method = method
            };

            if (@params != null && @params.Count > 0)
            {
                foreach (var p in @params)
                {
                    request.AddParameter(p.Key, p.Value);
                }
            }

            request.AddHeader("Referer", referer);
            request.AddHeader("Accept", "text/javascript, text/html, application/xml, text/xml, application/json, */*");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept-Language", "en-US,en;q=0.8,en-US;q=0.5,en;q=0.3");
            request.AddHeader("Cache-Control", "no-cache");

            LastInvokeTime = DateTimeOffset.Now;
            var response = client.Execute(request);

            if (response.ErrorException != null)
            {
                throw new RequestException(response.ErrorException.Message);
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new RequestException($"Bad status code: {response.StatusCode}");
            }

            return new SteamResponse(response, client.CookieContainer);
        }

        public Task<bool> CheckConnectionAsync()
        {
            var result = new Task<bool>(CheckConnection);
            result.Start();
            return result;
        }

        public bool CheckConnection()
        {
            var response = Request(Urls.SteamCommunity, Method.POST, string.Empty);
            return response.Data.StatusCode == HttpStatusCode.OK;
        }

        public Task<JSteamStatus> SteamStatusAsync()
        {
            var result = new Task<JSteamStatus>(SteamStatus);
            result.Start();
            return result;
        }

        public JSteamStatus SteamStatus()
        {
            var resp = Request(Urls.SteamStatus, Method.GET, string.Empty);
            return JsonConvert.DeserializeObject<JSteamStatus>(resp.Data.Content);
        }
    }
}

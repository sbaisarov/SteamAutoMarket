﻿using System.Net;
using RestSharp;

namespace Market.Models
{
    public class SteamResponse
    {
        public SteamResponse(IRestResponse data, CookieContainer cookieContainer)
        {
            Data = data;
            CookieContainer = cookieContainer;
        }

        public IRestResponse Data { get; }
        public CookieContainer CookieContainer { get; }
    }
}
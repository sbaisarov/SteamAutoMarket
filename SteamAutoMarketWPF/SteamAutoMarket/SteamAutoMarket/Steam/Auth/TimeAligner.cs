namespace SteamAutoMarket.Steam.Auth
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using SteamAutoMarket.Core;

    /// <summary>
    /// Class to help align system time with the Steam server time. Not super advanced; probably not taking some things into account that it should.
    /// Necessary to generate up-to-date codes. In general, this will have an error of less than a second, assuming Steam is operational.
    /// </summary>
    public class TimeAligner
    {
        private static bool _aligned;

        private static int _timeDifference;

        public static void AlignTime()
        {
            while (_aligned == false)
            {
                try
                {
                    var response = SteamWeb.Request(APIEndpoints.TWO_FACTOR_TIME_QUERY, "POST", dataString: null);
                    var query = JsonConvert.DeserializeObject<TimeQuery>(response);
                    _timeDifference = (int)(query.Response.ServerTime - Util.GetSystemUnixTime());
                    _aligned = true;
                }
                catch (Exception ex)
                {
                    Logger.Log.Error($"Error on steam time align - {ex.Message}.", ex);
                }
            }
        }

        public static async Task AlignTimeAsync()
        {
            var currentTime = Util.GetSystemUnixTime();
            var client = new WebClient();
            try
            {
                var response = await client.UploadStringTaskAsync(
                                   new Uri(APIEndpoints.TWO_FACTOR_TIME_QUERY),
                                   "steamid=0");
                var query = JsonConvert.DeserializeObject<TimeQuery>(response);
                _timeDifference = (int)(query.Response.ServerTime - currentTime);
                _aligned = true;
            }
            catch (WebException)
            {
            }
        }

        public static long GetSteamTime()
        {
            if (!_aligned)
            {
                AlignTime();
            }

            return Util.GetSystemUnixTime() + _timeDifference;
        }

        public static async Task<long> GetSteamTimeAsync()
        {
            if (!_aligned)
            {
                await AlignTimeAsync();
            }

            return Util.GetSystemUnixTime() + _timeDifference;
        }

        internal class TimeQuery
        {
            [JsonProperty("response")]
            internal TimeQueryResponse Response { get; set; }

            internal class TimeQueryResponse
            {
                [JsonProperty("server_time")]
                public long ServerTime { get; set; }
            }
        }
    }
}
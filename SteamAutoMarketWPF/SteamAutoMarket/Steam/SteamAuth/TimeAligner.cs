namespace Steam.SteamAuth
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    /// <summary>
    /// Class to help align system time with the Steam server time. Not super advanced; probably not taking some things into account that it should.
    /// Necessary to generate up-to-date codes. In general, this will have an error of less than a second, assuming Steam is operational.
    /// </summary>
    public class TimeAligner
    {
        private static bool _aligned = false;

        private static int _timeDifference = 0;

        public static void AlignTime()
        {
            long currentTime = Util.GetSystemUnixTime();
            using (WebClient client = new WebClient())
            {
                try
                {
                    string response = client.UploadString(APIEndpoints.TWO_FACTOR_TIME_QUERY, "steamid=0");
                    TimeQuery query = JsonConvert.DeserializeObject<TimeQuery>(response);
                    _timeDifference = (int)(query.Response.ServerTime - currentTime);
                    _aligned = true;
                }
                catch (WebException)
                {
                    return;
                }
            }
        }

        public static async Task AlignTimeAsync()
        {
            long currentTime = Util.GetSystemUnixTime();
            WebClient client = new WebClient();
            try
            {
                string response = await client.UploadStringTaskAsync(
                                      new Uri(APIEndpoints.TWO_FACTOR_TIME_QUERY),
                                      "steamid=0");
                TimeQuery query = JsonConvert.DeserializeObject<TimeQuery>(response);
                _timeDifference = (int)(query.Response.ServerTime - currentTime);
                _aligned = true;
            }
            catch (WebException)
            {
                return;
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
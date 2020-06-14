namespace Tests.PythonAPI
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Text.RegularExpressions;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

    [TestClass]
    public class Tests
    {
        public Tests()
        {
            ServicePointManager.ServerCertificateValidationCallback =
                ValidateServerCertificate;
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private static readonly byte[] steamGuardCodeTranslations =
        {
            50, 51, 52, 53, 54, 55, 56, 57, 66, 67, 68, 70, 71, 72, 74, 75, 77, 78, 80, 81, 82, 84, 86, 87, 88, 89
        };

        [TestMethod]
        public void ConfirmationHashTest()
        {
            var tag = "conf";
            var secret = "NBW4hOvL/mQmPtP1xx6xrejLVnU=";
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            var server = Uri.EscapeDataString(this.GenerateCodeOnServer(secret, tag, time));
            var correct = this.GenerateConfirmationHashForTime(secret, long.Parse(time), tag);

            server.Should().Be(correct);
        }

        public string GenerateSteamGuardCode(string sharedSecret, long time)
        {
            if (string.IsNullOrEmpty(sharedSecret))
            {
                return string.Empty;
            }

            var sharedSecretUnescaped = Regex.Unescape(sharedSecret);
            var sharedSecretArray = Convert.FromBase64String(sharedSecretUnescaped);
            var timeArray = new byte[8];

            time /= 30L;

            for (var i = 8; i > 0; i--)
            {
                timeArray[i - 1] = (byte)time;
                time >>= 8;
            }

            var hmacGenerator = new HMACSHA1();
            hmacGenerator.Key = sharedSecretArray;
            var hashedData = hmacGenerator.ComputeHash(timeArray);
            var codeArray = new byte[5];
            try
            {
                var b = (byte)(hashedData[19] & 0xF);
                var codePoint = (hashedData[b] & 0x7F) << 24 | (hashedData[b + 1] & 0xFF) << 16
                                                             | (hashedData[b + 2] & 0xFF) << 8
                                                             | (hashedData[b + 3] & 0xFF);

                for (var i = 0; i < 5; ++i)
                {
                    codeArray[i] = steamGuardCodeTranslations[codePoint % steamGuardCodeTranslations.Length];
                    codePoint /= steamGuardCodeTranslations.Length;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return Encoding.UTF8.GetString(codeArray);
        }

        [TestMethod]
        public void GuardCodeTest()
        {
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            var secret = "raTfvjLRdVOw+BUPbv9721JUmkA=";

            var server = this.GenerateSteamGuardCodeOnServer(secret, time);
            var correct = this.GenerateSteamGuardCode(secret, long.Parse(time));

            server.Should().Be(correct);
        }

        private string GenerateCodeOnServer(string identitySecret, string tag, string time)
        {
            using (var wb = new WebClient())
            {
                var response = wb.UploadString(
                    "https://shamanovski.pythonanywhere.com/api/gconfhash",
                    $"{identitySecret},{tag},{time},db3c06cb-2f70-4432-9914-f8dd7f3daf68,BFEBFBFF000906EA042E04E2");

                return JsonConvert.DeserializeObject<IDictionary<string, string>>(response)["result_0x23432"];
            }
        }

        private string GenerateConfirmationHashForTime(string secret, long time, string tag)
        {
            var decode = Convert.FromBase64String(secret);
            var n2 = 8;
            if (tag != null)
            {
                if (tag.Length > 32)
                {
                    n2 = 8 + 32;
                }
                else
                {
                    n2 = 8 + tag.Length;
                }
            }

            var array = new byte[n2];
            var n3 = 8;
            while (true)
            {
                var n4 = n3 - 1;
                if (n3 <= 0)
                {
                    break;
                }

                array[n4] = (byte)time;
                time >>= 8;
                n3 = n4;
            }

            if (tag != null)
            {
                Array.Copy(Encoding.UTF8.GetBytes(tag), 0, array, 8, n2 - 8);
            }

            try
            {
                var hmacGenerator = new HMACSHA1();
                hmacGenerator.Key = decode;
                var hashedData = hmacGenerator.ComputeHash(array);
                var encodedData = Convert.ToBase64String(hashedData, Base64FormattingOptions.None);
                var hash = WebUtility.UrlEncode(encodedData);
                return hash;
            }
            catch
            {
                return null;
            }
        }

        private string GenerateSteamGuardCodeOnServer(string sharedSecret, string time)
        {
            using (var wb = new WebClient())
            {
                var response = wb.UploadString(
                    "https://shamanovski.pythonanywhere.com/api/gguardcode",
                    $"{sharedSecret},{time},db3c06cb-2f70-4432-9914-f8dd7f3daf68,BFEBFBFF000906EA042E04E2");
                return JsonConvert.DeserializeObject<IDictionary<string, string>>(response)["result_0x23432"];
            }
        }
    }
}

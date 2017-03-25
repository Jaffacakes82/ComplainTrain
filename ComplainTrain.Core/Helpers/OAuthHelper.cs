using System;
using System.Collections.Generic;
using System.Net;
using ComplainTrain.Core.Interfaces;
using ComplainTrain.Core.Services;

namespace ComplainTrain.Core.Helpers
{
    public class OAuthHelper : IOAuthHelper
    {
        private const string TWITTER_API = "https://api.twitter.com/1.1/statuses/update.json";
        private const string HEADER_VALUE_BASE = "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", oauth_signature=\"{2}\", oauth_signature_method=\"{3}\", oauth_timestamp=\"{4}\", oauth_token=\"{5}\", oauth_version=\"{6}\"";
        private const string OAUTH_SIG_METHOD = "HMAC-SHA1";
        private const string OAUTH_VERSION = "1.0";

        public OAuthHelper(ICryptoStrategy cryptoStrategy)
        {
            this.ConsumerKey = /*Environment.GetEnvironmentVariable("CONSUMER_KEY")*/"NamCSGenGBdik5Pybozos3oWn";
            this.ConsumerSecret = /*Environment.GetEnvironmentVariable("CONSUMER_SECRET")*/"6x1oohBA7KYnwNOHkuhhrJlicM8V7eZLK5ZY8S2kErPvlxv6t1";
            this.ApiAccessToken = /*Environment.GetEnvironmentVariable("TWITTER_ACCESS_TOKEN")*/"2219490384-SVPGozzr3pkDFUWiFvHfdummDMU7A04x69jMFME";
            this.ApiAccessTokenSecret = /*Environment.GetEnvironmentVariable("TWITTER_ACCESS_TOKEN_SECRET")*/"4cOZQmXTO4elrf2RmPTCvGzWHvETPv8IjCUR0O4FrwSke";

            this.cryptoService = cryptoStrategy.CreateCrypto(typeof(HmacSha1Service));
        }

        private string ConsumerKey { get; set; }
        private string ConsumerSecret { get; set; }
        private string ApiAccessToken { get; set; }
        private string ApiAccessTokenSecret { get; set; }

        private readonly ICryptoService cryptoService;

        public KeyValuePair<string, string> GetOAuthHeader(string message)
        {
            string nonce = this.GenerateOAuthNonce();
            string timestamp = this.GenerateOAuthTimestamp();
            string signature = this.GenerateOAuthSignature(nonce, timestamp, message);

            string val = string.Format(
                    HEADER_VALUE_BASE, 
                    WebUtility.UrlEncode(this.ConsumerKey),
                    WebUtility.UrlEncode(nonce),
                    WebUtility.UrlEncode(signature),
                    WebUtility.UrlEncode(OAUTH_SIG_METHOD),
                    WebUtility.UrlEncode(timestamp),
                    WebUtility.UrlEncode(this.ApiAccessToken),
                    WebUtility.UrlEncode(OAUTH_VERSION));
            return new KeyValuePair<string, string>(
                "Authorization", 
                val);
        }

        private string GenerateOAuthNonce()
        {
            byte[] randBytes = new byte[32];
            new Random().NextBytes(randBytes);
            string base64nonce = Convert.ToBase64String(randBytes);
            base64nonce = base64nonce.Replace("+", string.Empty);
            base64nonce = base64nonce.Replace("/", string.Empty);
            base64nonce = base64nonce.Replace("=", string.Empty);
            return base64nonce;
        }

        private string GenerateOAuthTimestamp()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        }

        private string GenerateOAuthSignature(string nonce, string timestamp, string message)
        {
            string parameterString =
                string.Format(
                "oauth_consumer_key={0}" +
                "&oauth_nonce={1}" +
                "&oauth_signature_method={2}" +
                "&oauth_timestamp={3}" +
                "&oauth_token={4}" +
                "&oauth_version={5}" +
                "&status={6}",
                WebUtility.UrlEncode(this.ConsumerKey),
                WebUtility.UrlEncode(nonce),
                WebUtility.UrlEncode(OAUTH_SIG_METHOD),
                WebUtility.UrlEncode(timestamp),
                WebUtility.UrlEncode(this.ApiAccessToken),
                WebUtility.UrlEncode(OAUTH_VERSION),
                WebUtility.UrlEncode(message));

            string signatureBaseString =
                string.Format(
                    "POST" +
                    "&{0}" +
                    "&{1}",
                    WebUtility.UrlEncode(TWITTER_API),
                    WebUtility.UrlEncode(parameterString));

            string signingKey = string.Format(
                "{0}&{1}", 
                WebUtility.UrlEncode(this.ConsumerSecret), 
                WebUtility.UrlEncode(this.ApiAccessTokenSecret));

            return this.cryptoService.Encrypt(signatureBaseString, signingKey);
        }
    }
}
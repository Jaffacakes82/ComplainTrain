using System;
using System.Net;
using ComplainTrain.Core.Interfaces;

namespace ComplainTrain.Core.Services
{
    public class TwitterService : ITwitterService
    {
        private const string TWITTER_API = "https://api.twitter.com/1.1/statuses/update.json";
        private const string SIGNATURE_BASE = "POST&{0}&include_entities=true&oauth_consumer_key={1}&oauth_nonce={2}&oauth_signature_method={3}&oauth_timestamp={4}&oauth_token={5}&oauth_version={6}&status={7}";
        private const string OAUTH_SIG_METHOD = "HMAC_SHA1";
        private const string OAUTH_VERSION = "1.0";

        public void Tweet(string message)
        {
            throw new NotImplementedException();
        }

        private string GenerateOAuthSignature(string message)
        {
            string consumerKey = Environment.GetEnvironmentVariable("CONSUMER_KEY");
            string consumerSecret = Environment.GetEnvironmentVariable("CONSUMER_SECRET");
            string accessToken = Environment.GetEnvironmentVariable("TWITTER_ACCESS_TOKEN");
            string accessTokenSecret = Environment.GetEnvironmentVariable("TWITTER_ACCESS_TOKEN_SECRET");

            byte[] randBytes = new byte[32];
            new Random().NextBytes(randBytes);
            string oAuthNonce = Convert.ToBase64String(randBytes);
            string oAuthTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

            string signatureBaseToEncode = string.Format(SIGNATURE_BASE, TWITTER_API, consumerKey, oAuthNonce, OAUTH_SIG_METHOD, oAuthTimestamp, accessToken, OAUTH_VERSION, message);
			return string.Empty;
        }
    }
}
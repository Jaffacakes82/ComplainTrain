using System;
using System.Collections.Generic;
using System.Net;
using ComplainTrain.Core.Interfaces;
using ComplainTrain.Core.Settings;
using Microsoft.Extensions.Options;

namespace ComplainTrain.Core.Services
{
    public sealed class TwitterService : ITwitterService
    {
        private readonly IOAuthHelper authHelper;
        private readonly IHttpService httpService;
        private readonly WebSettings options;
        public TwitterService(IOAuthHelper authHelper, IHttpService httpService, IOptions<WebSettings> options)
        {
            this.authHelper = authHelper;
            this.httpService = httpService;
            this.options = options.Value;
        }
        public void Tweet(string message)
        {
            string status = string.Format("status={0}", WebUtility.UrlEncode(message));
            KeyValuePair<string, string> header = this.authHelper.GetOAuthHeader(message);
            IDictionary<string, IEnumerable<string>> headersDict = new Dictionary<string, IEnumerable<string>>();
            headersDict.Add(header.Key, new List<string> { header.Value });

            this.httpService.Post(
                this.options.TwitterUpdateEndpoint, 
                "application/x-www-form-urlencoded", 
                status, 
                headersDict);
        }
    }
}
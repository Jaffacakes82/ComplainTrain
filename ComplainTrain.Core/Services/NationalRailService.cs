using System.Collections.Generic;
using System.Threading.Tasks;
using ComplainTrain.Core.Classes;
using ComplainTrain.Core.Classes.SOAP;
using ComplainTrain.Core.Settings;
using Microsoft.Extensions.Options;
using ComplainTrain.Core.Interfaces;
using System;

namespace ComplainTrain.Core.Services
{
    public sealed class NationalRailService : INationalRailService
    {
        private readonly IHttpService httpService;
        private readonly ISerializer serializer;
        private readonly WebSettings options;
        
        public NationalRailService(IHttpService httpService, ISerializer serializer, IOptions<WebSettings> options)
        {
            this.httpService = httpService;
            this.serializer = serializer;
            this.options = options.Value;
        }

        public IList<Departure> GetDepartureBoard(string pageSize, string stationCode, string stationFilter, string timeOffset, string timeWindow)
        {
            string requestString = string.Format(
                SOAPWrapper.requestString,
                Environment.GetEnvironmentVariable("ACCESS_TOKEN"),
                pageSize,
                stationCode,
                stationFilter,
                timeOffset,
                timeWindow);

            string departuresAsString = this.httpService.Post(this.options.SOAPEndpoint, "text/xml", requestString, null);
            var soapWrapper = new SOAPWrapper();
            soapWrapper.SoapEnvelope = this.serializer.Deserialize<Envelope>(departuresAsString);
            return soapWrapper.Wash();
        }
    }
}

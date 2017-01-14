using System.Collections.Generic;
using System.Threading.Tasks;
using ComplainTrain.Core.Classes;
using System;
using ComplainTrain.Core.Classes.SOAP;
using ComplainTrain.Core.Settings;
using Microsoft.Extensions.Options;

namespace ComplainTrain.Core.Interfaces
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
        public async Task<IList<Departure>> GetDepartureBoard(string pageSize, string stationCode, string stationFilter, string timeOffset, string timeWindow)
        {
            string requestString = string.Format(
                SOAPWrapper.requestString,
                this.options.AccessToken,
                pageSize,
                stationCode,
                stationFilter,
                timeOffset,
                timeWindow);

            string departuresAsString = await this.httpService.Post(this.options.SOAPEndpoint, "text/xml", requestString);
            var soapWrapper = new SOAPWrapper();
            soapWrapper.SoapEnvelope = this.serializer.Deserialize<Envelope>(departuresAsString);
            return soapWrapper.Wash();
        }
    }
}

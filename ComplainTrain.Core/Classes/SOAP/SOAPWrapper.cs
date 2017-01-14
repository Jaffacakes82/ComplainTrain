using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ComplainTrain.Core.Classes.SOAP
{
    public class SOAPWrapper 
    {
        /// <summary>
        /// 0 - Access token
        /// 1 - Number of rows
        /// 2 - Station code
        /// 3 - Filter type (to/from)
        /// 4 - Time offset
        /// 5 - Time window
        /// </summary>
        public static string requestString = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:typ=\"http://thalesgroup.com/RTTI/2013-11-28/Token/types\" xmlns:ldb=\"http://thalesgroup.com/RTTI/2016-02-16/ldb/\"><soapenv:Header><typ:AccessToken><typ:TokenValue>{0}</typ:TokenValue></typ:AccessToken></soapenv:Header><soapenv:Body><ldb:GetDepBoardWithDetailsRequest><ldb:numRows>{1}</ldb:numRows><ldb:crs>{2}</ldb:crs><ldb:filterCrs></ldb:filterCrs><ldb:filterType>{3}</ldb:filterType><ldb:timeOffset>{4}</ldb:timeOffset><ldb:timeWindow>{5}</ldb:timeWindow></ldb:GetDepBoardWithDetailsRequest></soapenv:Body></soapenv:Envelope>";

        public virtual Envelope SoapEnvelope { get; set; }

        public IList<Departure> Wash()
        {
            throw new NotImplementedException();
        }
    }

    [XmlRoot(ElementName="location", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
	public class Location {
		[XmlElement(ElementName="locationName", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string LocationName { get; set; }
		[XmlElement(ElementName="crs", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string Crs { get; set; }
		[XmlElement(ElementName="via", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string Via { get; set; }
	}

	[XmlRoot(ElementName="origin", Namespace="http://thalesgroup.com/RTTI/2016-02-16/ldb/types")]
	public class Origin {
		[XmlElement(ElementName="location", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public Location Location { get; set; }
	}

	[XmlRoot(ElementName="destination", Namespace="http://thalesgroup.com/RTTI/2016-02-16/ldb/types")]
	public class Destination {
		[XmlElement(ElementName="location", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public Location Location { get; set; }
	}

	[XmlRoot(ElementName="callingPoint", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
	public class CallingPoint {
		[XmlElement(ElementName="locationName", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string LocationName { get; set; }
		[XmlElement(ElementName="crs", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string Crs { get; set; }
		[XmlElement(ElementName="st", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string St { get; set; }
		[XmlElement(ElementName="et", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string Et { get; set; }
	}

	[XmlRoot(ElementName="callingPointList", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
	public class CallingPointList {
		[XmlElement(ElementName="callingPoint", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public List<CallingPoint> CallingPoint { get; set; }
	}

	[XmlRoot(ElementName="subsequentCallingPoints", Namespace="http://thalesgroup.com/RTTI/2016-02-16/ldb/types")]
	public class SubsequentCallingPoints {
		[XmlElement(ElementName="callingPointList", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public CallingPointList CallingPointList { get; set; }
	}

	[XmlRoot(ElementName="service", Namespace="http://thalesgroup.com/RTTI/2016-02-16/ldb/types")]
	public class Service {
		[XmlElement(ElementName="std", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string Std { get; set; }
		[XmlElement(ElementName="etd", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string Etd { get; set; }
		[XmlElement(ElementName="platform", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string Platform { get; set; }
		[XmlElement(ElementName="operator", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string Operator { get; set; }
		[XmlElement(ElementName="operatorCode", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string OperatorCode { get; set; }
		[XmlElement(ElementName="serviceType", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string ServiceType { get; set; }
		[XmlElement(ElementName="serviceID", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string ServiceID { get; set; }
		[XmlElement(ElementName="origin", Namespace="http://thalesgroup.com/RTTI/2016-02-16/ldb/types")]
		public Origin Origin { get; set; }
		[XmlElement(ElementName="destination", Namespace="http://thalesgroup.com/RTTI/2016-02-16/ldb/types")]
		public Destination Destination { get; set; }
		[XmlElement(ElementName="subsequentCallingPoints", Namespace="http://thalesgroup.com/RTTI/2016-02-16/ldb/types")]
		public SubsequentCallingPoints SubsequentCallingPoints { get; set; }
	}

	[XmlRoot(ElementName="trainServices", Namespace="http://thalesgroup.com/RTTI/2016-02-16/ldb/types")]
	public class TrainServices {
		[XmlElement(ElementName="service", Namespace="http://thalesgroup.com/RTTI/2016-02-16/ldb/types")]
		public List<Service> Service { get; set; }
	}

	[XmlRoot(ElementName="GetStationBoardResult", Namespace="http://thalesgroup.com/RTTI/2016-02-16/ldb/")]
	public class GetStationBoardResult {
		[XmlElement(ElementName="generatedAt", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string GeneratedAt { get; set; }
		[XmlElement(ElementName="locationName", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string LocationName { get; set; }
		[XmlElement(ElementName="crs", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string Crs { get; set; }
		[XmlElement(ElementName="platformAvailable", Namespace="http://thalesgroup.com/RTTI/2015-11-27/ldb/types")]
		public string PlatformAvailable { get; set; }
		[XmlElement(ElementName="trainServices", Namespace="http://thalesgroup.com/RTTI/2016-02-16/ldb/types")]
		public TrainServices TrainServices { get; set; }
		[XmlAttribute(AttributeName="lt3", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Lt3 { get; set; }
		[XmlAttribute(AttributeName="lt5", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Lt5 { get; set; }
		[XmlAttribute(AttributeName="lt4", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Lt4 { get; set; }
		[XmlAttribute(AttributeName="lt", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Lt { get; set; }
		[XmlAttribute(AttributeName="lt2", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Lt2 { get; set; }
	}

	[XmlRoot(ElementName="GetDepBoardWithDetailsResponse", Namespace="http://thalesgroup.com/RTTI/2016-02-16/ldb/")]
	public class GetDepBoardWithDetailsResponse {
		[XmlElement(ElementName="GetStationBoardResult", Namespace="http://thalesgroup.com/RTTI/2016-02-16/ldb/")]
		public GetStationBoardResult GetStationBoardResult { get; set; }
		[XmlAttribute(AttributeName="xmlns")]
		public string Xmlns { get; set; }
	}

	[XmlRoot(ElementName="Body", Namespace="http://schemas.xmlsoap.org/soap/envelope/")]
	public class Body {
		[XmlElement(ElementName="GetDepBoardWithDetailsResponse", Namespace="http://thalesgroup.com/RTTI/2016-02-16/ldb/")]
		public GetDepBoardWithDetailsResponse GetDepBoardWithDetailsResponse { get; set; }
	}

	[XmlRoot(ElementName="Envelope", Namespace="http://schemas.xmlsoap.org/soap/envelope/")]
	public class Envelope {
		[XmlElement(ElementName="Body", Namespace="http://schemas.xmlsoap.org/soap/envelope/")]
		public Body Body { get; set; }
		[XmlAttribute(AttributeName="soap", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Soap { get; set; }
		[XmlAttribute(AttributeName="xsi", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Xsi { get; set; }
		[XmlAttribute(AttributeName="xsd", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Xsd { get; set; }
	}
}
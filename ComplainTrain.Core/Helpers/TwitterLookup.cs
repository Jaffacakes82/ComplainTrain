using System.Collections.Generic;

namespace ComplainTrain.Core.Helpers
{
    public static class TwitterLookup
    {
        public static IDictionary<string, string> TwitterHandles = new Dictionary<string, string>
        {
            { "Arriva Trains Wales", "@ArrivaTW" },
            { "c2c", "@c2c_Rail" },
            { "Caledonian Sleeper", "@CalSleeper" },
            { "Chiltern Railways", "@chilternrailway" },
            { "CrossCountry", "@CrossCountryUK" },
            { "East Midlands Trains", "@EMTrains" },
            { "Eurostar", "@Eurostar" },
            { "Gatwick Express", "@GatwickExpress" },
            { "Grand Central", "@GC_Rail" },
            { "Great Northern", "@GNRailUK" },
            { "Great Western Railway", "@GWRHelp" },
            { "Greater Anglia", "@greateranglia" },
            { "Heathrow Connect", "@HConnectHelp " },
            { "Heathrow Express", "@HeathrowExpress" },
            { "Hull Trains", "@Hull_Trains" },
            { "Island Line", "@IsleOfWightRail" },
            { "London Midland", "@LondonMidland" },
            { "London Overground", "@LDNOverground" },
            { "London Underground", "@TfLTravelAlerts" },
            { "Merseyrail", "@merseyrail" },
            { "Northern", "@northernassist" },
            { "ScotRail", "@ScotRail" },
            { "South West Trains", "@SW_Trains" },
            { "Southeastern", "@Se_Railway" },
            { "Southern", "@SouthernRailUK" },
            { "Stansted Express", "@Stansted_Exp" },
            { "TfL Rail", "@TfLRail" },
            { "Thameslink", "@TLRailUK" },
            { "TransPennine Express", "@TPExpressTrains" },
            { "Virgin Trains", "@VirginTrains" },
            { "Virgin Trains East Coast", "@Virgin_TrainsEC" }
        };
    }
}
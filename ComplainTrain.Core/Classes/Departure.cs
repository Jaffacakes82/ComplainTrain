using System.Collections.Generic;

namespace ComplainTrain.Core.Classes
{
    public class Departure
    {
        public virtual string ServiceId { get; set; }
        public virtual string StationName { get; set; }
        public virtual string StationCode { get; set; }
        public virtual string StationNotice { get; set; }
        public virtual string DestinationName { get; set; }
        public virtual string DestinationCode { get; set; }
        public virtual string Due { get; set; }
        public virtual string Expected { get; set; }
        public virtual string Platform { get; set; }
        public virtual string Operator { get; set; }
        public virtual string OperatorCode { get; set; }
        public virtual string TrainLength { get; set; }
        public virtual string CancellationReason { get; set; }
        public virtual string DelayReason { get; set; }
    }
}
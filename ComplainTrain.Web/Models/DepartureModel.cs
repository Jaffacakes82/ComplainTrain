using ComplainTrain.Core.Classes;

namespace ComplainTrain.Web.Models
{
    public class DepartureModel
    {
        public DepartureModel(Departure obj)
        {
            this.ServiceId = obj.ServiceId;
            this.StationName = obj.StationName;
            this.StationCode = obj.StationCode;
            this.StationNotice = obj.StationNotice;
            this.DestinationName = obj.DestinationName;
            this.DestinationCode = obj.DestinationCode;
            this.Due = obj.Due;
            this.Expected = obj.Expected;
            this.Platform = obj.Platform;
            this.Operator = obj.Operator;
            this.OperatorCode = obj.OperatorCode;
            this.TrainLength = obj.TrainLength;
            this.CancellationReason = obj.CancellationReason;
            this.DelayReason = obj.DelayReason;
        }

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
using ComplainTrain.Core.Classes;

namespace ComplainTrain.Web.Models
{
    public class ComplaintModel
    {
        public virtual string Operator { get; set; }
        public virtual string OriginalSearch { get; set; }
        public virtual string Destination { get; set; }
        public virtual string Expected { get; set; }

        public virtual string Due { get; set; }
    }
}
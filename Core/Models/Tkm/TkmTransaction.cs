using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Master;

namespace Core.Models.Tkm
{
    public class TkmTransaction : BaseModel
    {
        [Key]
        public long TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType Type { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        public string Remarks { get; set; }
        public string PictureUrl { get; set; }
        public long Value { get; set; }
        public bool IsApproved { get; set; }

        public virtual MasterUser User { get; set; }
    }

    public enum TransactionType
    {
        Income = 1,
        Outcome = 2
    }

    public enum TransactionStatus
    {
        Pending = 1,
        Approved = 2
    }

    public class TkmTransactionReportModel
    {
        public long TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public string TransactionDate { get; set; }
        public string Type { get; set; }
        public string Remarks { get; set; }
        public string PictureUrl { get; set; }
        public long Value { get; set; }
        public bool IsApproved { get; set; }

        public long? Income { get; set; }
        public long? Outcome { get; set; }
        public long? Balance { get; set; }
    }

}

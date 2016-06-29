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
        public TransactionStatus TransactionStatus { get; set; }

        public virtual MasterUser User { get; set; }
    }
    
    public enum TransactionType
    {
        Income,
        OutCome
    }

    public enum TransactionStatus
    {
        Pending,
        Approved
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
        public TransactionStatus TransactionStatus { get; set; }

        public int Income { get; set; }
        public int Outcome { get; set; }
        public int Balance { get; set; }
    }

}

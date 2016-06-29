using System;
using System.ComponentModel.DataAnnotations;

namespace TKM_Office_API.RequestParam
{
    public class StockCardOutOfStockGraphViewParam
    {

        public long? LocationId { get; set; }
        public long? ShelveId { get; set; }
        public long? ReaderModuleId { get; set; }
        public long? BinId { get; set; }
        public long? TransactionId { get; set; }
        public long? BrandId { get; set; }
        public int ScaleType { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Core;

namespace TKM_Office_API.RequestParam
{
    public class StockCardOutOfStockViewParam
    {
        public long? LocationId { get; set; }
        public long? ShelveId { get; set; }
        public long? ReaderModuleId { get; set; }
        public long? BinId { get; set; }
        public long? TransactionId { get; set; }
        public long? BrandId { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        public BaseSearchQueryModel SearchQuery { get; set; }
    }
}
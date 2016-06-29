using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;

namespace TKM_Office_API.RequestParam
{
    public class StockCardQtyBalanceParam
    {
        public long? LocationId { get; set; }
        public long? ShelveId { get; set; }
        public long? ReaderModuleId { get; set; }
        public long? BinId { get; set; }
        public long? TransactionId { get; set; }
        public long? BrandId { get; set; }
        public BaseSearchQueryModel SearchQuery { get; set; }
    }
}
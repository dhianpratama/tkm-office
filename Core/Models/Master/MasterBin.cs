using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Master
{
    public class MasterBin : BaseModelInstitution
    {
        [Key]
        public long BinId { get; set; }
        public string BinCode { get; set; }
        public decimal EmptyDistance { get; set; }
        public string Remarks { get; set; }
        public long? ReaderModuleId { get; set; }
        public int StackNo { get; set; }
        public int RowNo { get; set; }
        public long? ItemId { get; set; }

        public virtual MasterReaderModule ReaderModule { get; set; }
        public virtual MasterItem Item { get; set; }
    }

    public class MasterBinQuery {
        public string ShelveCode { get; set; }
        public string ModuleCode { get; set; }
        public int BinIndex { get; set; }
    }
}

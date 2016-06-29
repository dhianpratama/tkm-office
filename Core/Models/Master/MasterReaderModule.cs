using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Master
{
    public class MasterReaderModule : BaseModelInstitution
    {
        [Key]
        public long ReaderModuleId { get; set; }

        public string ReaderModuleCode { get; set; }
        public int NoOfRow { get; set; }
        public int NoOfStack { get; set; }
        public string Remarks { get; set; }
        public long? ShelveId { get; set; }
        public decimal DefaultBinEmptyDistance { get; set; }
        public int StackNo { get; set; }
        public int RowNo { get; set; }

        public virtual MasterShelve Shelve { get; set; }
        public virtual ICollection<MasterBin> Bins { get; set; }
    }
}

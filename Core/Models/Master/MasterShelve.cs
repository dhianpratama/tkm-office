using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Master
{
    public class MasterShelve : BaseModelInstitution
    {
        [Key]
        public long ShelveId { get; set; }

        public string ShelveCode { get; set; }
        public int MaxReaderModule { get; set; }
        public string Remarks { get; set; }

        public long LocationId { get; set; }

        public virtual ICollection<MasterReaderModule> Modules { get; set; }
    }

    public class MasterShelveQuery
    {
        public long ShelveId { get; set; }

        public string ShelveCode { get; set; }
        public int MaxReaderModule { get; set; }
        public string Remarks { get; set; }

        public long LocationId { get; set; }
        public virtual MasterLocationQuery Location { get; set; }
    }
}

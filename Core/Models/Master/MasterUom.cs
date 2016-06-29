using System.ComponentModel.DataAnnotations;

namespace Core.Models.Master
{
    public class MasterUom : BaseModel
    {
        [Key]
        public long UomId { get; set; }
        public string UomCode { get; set; }
        public string UomDescription { get; set; }
    }
}

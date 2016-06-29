using System.ComponentModel.DataAnnotations;

namespace Core.Models.Master
{
    public class MasterLocationType : BaseModelInstitution
    {
        [Key]
        public long LocationTypeId { get; set; }

        public string LocationTypeCode { get; set; }
        public string LocationTypeDescription { get; set; }
        public string Icon { get; set; }
    }
}

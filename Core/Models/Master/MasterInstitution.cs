using System.ComponentModel.DataAnnotations;

namespace Core.Models.Master
{
    public class MasterInstitution : BaseModel
    {
        [Key]
        public long InstitutionId { get; set; }
        public string InstitutionName { get; set; }
        public string InstitutionDescription { get; set; }
    }
}

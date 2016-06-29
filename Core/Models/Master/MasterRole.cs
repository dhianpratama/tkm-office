using System.ComponentModel.DataAnnotations;

namespace Core.Models.Master
{
    public class MasterRole : BaseModelInstitution
    {
        [Key]
        public long RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
    }
}

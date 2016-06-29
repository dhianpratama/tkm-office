using System.ComponentModel.DataAnnotations;

namespace Core.Models.Master
{
    public class MasterUserInstitutionRole : BaseModelInstitution
    {
        [Key]
        public long UserInstitutionRoleId { get; set; }
        public long UserInstitutionId { get; set; }
        public long RoleId { get; set; }

        public virtual MasterRole Role { get; set; }
        public virtual MasterUserInstitution UserInstitution { get; set; }
    }
}

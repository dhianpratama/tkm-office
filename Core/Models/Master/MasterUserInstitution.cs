using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Master
{
    public class MasterUserInstitution : BaseModelInstitution
    {
        [Key]
        public long UserInstitutionId { get; set; }
        public long UserId { get; set; }

        public virtual MasterUser User { get; set; }
        public virtual MasterInstitution Institution { get; set; }

        public virtual ICollection<MasterUserInstitutionRole> Roles { get; set; }
    }
}

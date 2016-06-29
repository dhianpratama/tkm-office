using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Master
{
    public class MasterUser : BaseModel
    {
        [Key]
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string FullName { get; set; }
        public long DefaultInstitutionId { get; set; }

        public virtual ICollection<MasterUserInstitution> Institutions { get; set; }
    }

    public class MasterUserView
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public long DefaultInstitutionId { get; set; }

        public virtual ICollection<MasterUserInstitution> Institutions { get; set; }
    }
}

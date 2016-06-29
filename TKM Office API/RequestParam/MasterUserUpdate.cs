using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Models.Master;

namespace TKM_Office_API.RequestParam
{
    public class MasterUserUpdate
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public long DefaultInstitutionId { get; set; }

        public virtual ICollection<MasterUserInstitution> Institutions { get; set; }
    }
}
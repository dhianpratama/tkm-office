using System.ComponentModel.DataAnnotations;

namespace Core.Models.Master
{
    public class MasterBrand : BaseModelInstitution
    {
        [Key]
        public long BrandId { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
    }
}

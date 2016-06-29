using System.ComponentModel.DataAnnotations;

namespace Core.Models.Master
{
    public class MasterItem : BaseModelInstitution
    {
        [Key]
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public long UomId { get; set; }
        public decimal ItemThickness { get; set; }
        public long BrandId { get; set; }
        public string ImageFilepath { get; set; }
        public string ImageFilename { get; set; }

        public virtual MasterUom Uom { get; set; }
        public virtual MasterBrand Brand { get; set; }
    }
}

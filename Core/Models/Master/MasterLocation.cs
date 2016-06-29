using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Master
{
    public class MasterLocation
    {
        public long LocationId { get; set; }
        public long InstitutionId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public long LocationTypeId { get; set; }
        public string Node { get; set; }
        public int NodeLevel { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }

    public class MasterLocationQuery
    {
        public long LocationId { get; set; }
        public long InstitutionId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public long LocationTypeId { get; set; }
        public string LocationTypeCode { get; set; }
        public string LocationTypeDescription { get; set; }
        public string Icon { get; set; }
        public string Node { get; set; }
        public int NodeLevel { get; set; }
        public long? ParentLocationId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }

    public class MasterLocationWithParentLocationId
    {
        public long LocationId { get; set; }
        public long InstitutionId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public long LocationTypeId { get; set; }
        public string Node { get; set; }
        public int NodeLevel { get; set; }
        public long? ParentLocationId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }

    public class MasterLocationDropdownList
    {
        public long LocationId { get; set; }
        public long InstitutionId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public long LocationTypeId { get; set; }
        public string Node { get; set; }
        public int NodeLevel { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}

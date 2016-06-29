using System.ComponentModel.DataAnnotations;

namespace Core.Models.Sys
{
    public class SysConfiguration : BaseModel
    {
        [Key]
        public long ConfigurationId { get; set; }        
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
    }
}

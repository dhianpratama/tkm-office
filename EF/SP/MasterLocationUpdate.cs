using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.SP
{
    public class MasterLocationUpdate : BaseStoredProcedure
    {
        public long ParamLocationId { get; set; }
        public string ParamLocationCode { get; set; }
        public string ParamLocationName { get; set; }
        public long ParamLocationTypeId { get; set; }
        public string ParamCreatedBy { get; set; }
        public DateTime ParamCreatedDate { get; set; }
    }
}

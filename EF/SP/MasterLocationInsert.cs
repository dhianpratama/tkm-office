using System;

namespace EF.SP
{
    public class MasterLocationInsert : BaseStoredProcedure
    {
        public long ParamInstitutionId { get; set; }
		public string ParamLocationCode	{ get; set; }
		public string ParamLocationName	{ get; set; }
		public long ParamLocationTypeId { get; set; }
		public long ParamParentLocationId	{ get; set; }
		public string ParamCreatedBy { get; set; }
        public DateTime ParamCreatedDate { get; set; }
    }
}

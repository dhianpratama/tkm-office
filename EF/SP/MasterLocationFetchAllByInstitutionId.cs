using Core.Models.Master;

namespace EF.SP
{
    public class MasterLocationFetchAllByInstitutionId : BaseStoredProcedure<MasterLocationQuery>
    {
        public long ParamInstitutionId { get; set; }
    }
}

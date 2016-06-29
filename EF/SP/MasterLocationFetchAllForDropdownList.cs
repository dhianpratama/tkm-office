using Core.Models.Master;

namespace EF.SP
{
    public class MasterLocationFetchAllForDropdownList : BaseStoredProcedure<MasterLocationDropdownList>
    {
        public long ParamInstitutionId { get; set; }
    }
}

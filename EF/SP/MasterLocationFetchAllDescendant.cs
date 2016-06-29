using Core.Models.Master;

namespace EF.SP
{
    public class MasterLocationFetchAllDescendant : BaseStoredProcedure<MasterLocationQuery>
    {
        public long ParamLocationId { get; set; }
    }
}

using Core.Models.Master;

namespace EF.SP
{
    public class MasterLocationFetchOne : BaseStoredProcedure<MasterLocationQuery>
    {
        public long ParamLocationId { get; set; }
    }
}

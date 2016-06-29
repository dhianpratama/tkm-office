using System.Collections.Generic;
using Core.Models.Master;

namespace Core.Services.Master
{
    public interface IShelveService
    {
        MasterShelve Save(MasterShelve data);
        void Delete(MasterShelve data);
        List<MasterShelveQuery> FetchAll();
        MasterShelve FetchCompleteShelve(long shelveId);
        MasterShelve FetchOne(long shelveId);
        List<MasterShelve> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery);
    }
}

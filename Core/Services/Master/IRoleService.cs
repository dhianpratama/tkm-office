using System.Collections.Generic;
using Core.Models.Master;

namespace Core.Services.Master
{
    public interface IRoleService
    {
        void Save(MasterRole data);
        void Delete(MasterRole data);
        List<MasterRole> FetchAll();
        MasterRole FetchOne(long roleId);
        List<MasterRole> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery);
    }
}

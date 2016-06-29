using System.Collections.Generic;
using Core.Models.Master;

namespace Core.Services.Master
{
    public interface ILocationTypeService
    {
        void Save(MasterLocationType data);
        void Delete(MasterLocationType data);
        List<MasterLocationType> FetchAll();
        MasterLocationType FetchOne(long locationTypeId);
        List<MasterLocationType> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery);
    }
}

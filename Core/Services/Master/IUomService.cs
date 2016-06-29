using System.Collections.Generic;
using Core.Models.Master;

namespace Core.Services.Master
{
    public interface IUomService
    {
        void Save(MasterUom data);
        void Delete(MasterUom data);
        List<MasterUom> FetchAll();
        MasterUom FetchOne(long uomId);
        List<MasterUom> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery);
    }
}

using System.Collections.Generic;
using Core.Models.Master;

namespace Core.Services.Master
{
    public interface IItemService
    {
        void Save(MasterItem data);
        void Delete(MasterItem data);
        List<MasterItem> FetchAll();
        MasterItem FetchOne(long itemId);
        List<MasterItem> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery);
    }
}

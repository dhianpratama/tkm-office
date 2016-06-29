using System.Collections.Generic;
using Core.Models.Master;

namespace Core.Services.Master
{
    public interface IBrandService
    {
        void Save(MasterBrand data);
        void Delete(MasterBrand data);
        List<MasterBrand> FetchAll();
        MasterBrand FetchOne(long brandId);
        List<MasterBrand> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery);
    }
}

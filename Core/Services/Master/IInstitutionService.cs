using System.Collections.Generic;
using Core.Models.Master;

namespace Core.Services.Master
{
    public interface IInstitutionService
    {
        void Save(MasterInstitution data);
        void Delete(MasterInstitution data);
        List<MasterInstitution> FetchAll();
        List<MasterInstitution> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery);
        MasterInstitution FetchOne(long institutionId);

    }
}

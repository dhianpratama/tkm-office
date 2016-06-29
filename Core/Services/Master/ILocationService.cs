using System.Collections.Generic;
using Core.Models.Master;

namespace Core.Services.Master
{
    public interface ILocationService
    {
        void Save(MasterLocationWithParentLocationId data);
        void Delete(MasterLocation data);
        List<MasterLocationQuery> FetchAll();
        List<MasterLocationQuery> FetchAllByInstitution();
        MasterLocationQuery FetchOne(long locationId);
        List<MasterLocationDropdownList> FetchAllForDropdownList();
    }
}

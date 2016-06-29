using System.Collections.Generic;
using Core.Models.Master;

namespace Core.Services.Master
{
    public interface IUserInstitutionRoleService
    {
        void Save(MasterUserInstitutionRole data);
        void Delete(MasterUserInstitutionRole data);
        List<MasterUserInstitutionRole> FetchAll();
        MasterUserInstitutionRole FetchOne(long userInstitutionRoleId);
        List<MasterUserInstitutionRole> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery, long userId);
        List<MasterUserInstitutionRole> FetchAllByUserAndInstitution(long userId, long institutionId);
    }
}

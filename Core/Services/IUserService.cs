using System.Collections.Generic;
using Core.Models.Master;

namespace Core.Services
{
    public interface IUserService
    {
        bool Exist(string userName);
        void CreateUser(string userName, string password);
        MasterUser Authenticate(string userName, string password);

        void CreateNewUser(string userName, string fullName, string password);
        void UpdateUserData(long userId, string fullName, long defaultInstitutionId, List<MasterUserInstitution> institutions);
        void Delete(MasterUser data);
        MasterUserView FetchOne(long userId);
        List<MasterUserView> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery);
        void ResetPassword(string userName, string newPassword);
        void ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
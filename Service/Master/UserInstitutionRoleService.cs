using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models.Master;
using Core.Services;
using Core.Services.Master;

namespace Service.Master
{
    public class UserInstitutionRoleService : IUserInstitutionRoleService
    {
        private readonly IRepository<MasterUserInstitutionRole> _userInstitutionRoleRepository;
        private readonly ISecurityService _securityService;
        private readonly IUserService _userService;

        public UserInstitutionRoleService(IRepository<MasterUserInstitutionRole> userInstitutionRoleRepository,
            ISecurityService securityService, IUserService userService)
        {
            _userInstitutionRoleRepository = userInstitutionRoleRepository;
            _securityService = securityService;
            _userService = userService;
        }

        public void Save(MasterUserInstitutionRole data)
        {
            _userInstitutionRoleRepository.Save(data);
            _userInstitutionRoleRepository.Commit();
        }

        public void Delete(MasterUserInstitutionRole data)
        {
            _userInstitutionRoleRepository.Delete(data);
            _userInstitutionRoleRepository.Commit();
        }

        public List<MasterUserInstitutionRole> FetchAll()
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            return
                _userInstitutionRoleRepository.Query()
                    .Where(u => u.IsActive && u.InstitutionId == institutionId)
                    .ToList();
        }

        public MasterUserInstitutionRole FetchOne(long userInstitutionRoleId)
        {
            return
                _userInstitutionRoleRepository.Query()
                    .FirstOrDefault(u => u.UserInstitutionRoleId == userInstitutionRoleId);
        }

        public List<MasterUserInstitutionRole> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery, long userId)
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            var user = _userService.FetchOne(userId);
            if (user == null) return null;

            var userInstitution = user.Institutions.FirstOrDefault(u => u.InstitutionId == institutionId);

            var result =
                _userInstitutionRoleRepository.Query()
                    .Where(
                        u =>
                            u.IsActive && u.UserInstitutionId == userInstitution.UserInstitutionId &&
                            u.InstitutionId == institutionId);
            searchQuery.TotalData = BaseSearchQueryExpression<MasterUserInstitutionRole>.DefaultQueryExpression(ref result, searchQuery);
            return result.ToList();
        }

        public List<MasterUserInstitutionRole> FetchAllByUserAndInstitution(long userId, long institutionId)
        {
            var user = _userService.FetchOne(userId);
            if (user == null) return null;

            var userInstitution = user.Institutions.FirstOrDefault(u => u.InstitutionId == institutionId);
            return _userInstitutionRoleRepository.Query()
                .Where(
                    u =>
                        u.IsActive && u.UserInstitutionId == userInstitution.UserInstitutionId &&
                        u.InstitutionId == institutionId).ToList();
        }
    }
}

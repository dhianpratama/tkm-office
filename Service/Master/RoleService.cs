using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Models.Master;
using Core.Services;
using Core.Services.Master;

namespace Service.Master
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<MasterRole> _roleRepository;
        private readonly ISecurityService _securityService;

        public RoleService(IRepository<MasterRole> roleRepository, ISecurityService securityService)
        {
            _roleRepository = roleRepository;
            _securityService = securityService;
        }

        public void Save(MasterRole data)
        {
            if (data.RoleId == 0)
            {
                data.InstitutionId = _securityService.GetCurrentInstitutionId();
            }
            _roleRepository.Save(data);
            _roleRepository.Commit();
        }

        public void Delete(MasterRole data)
        {
            _roleRepository.Delete(data);
            _roleRepository.Commit();
        }

        public List<MasterRole> FetchAll()
        {
            return _roleRepository.Query().Where(r => r.IsActive).ToList();
        }

        public MasterRole FetchOne(long roleId)
        {
            return _roleRepository.Query().First(r => r.RoleId == roleId);
        }

        public List<MasterRole> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery)
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            var result = _roleRepository.Query().Where(r => r.IsActive && r.InstitutionId == institutionId);
            searchQuery.TotalData = BaseSearchQueryExpression<MasterRole>.DefaultQueryExpression(ref result, searchQuery);
            return result.ToList();
        }
    }
}

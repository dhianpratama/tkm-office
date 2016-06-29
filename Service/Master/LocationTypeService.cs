using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models.Master;
using Core.Services;
using Core.Services.Master;

namespace Service.Master
{
    public class LocationTypeService : ILocationTypeService
    {
        private readonly IRepository<MasterLocationType> _locationTypeRepository;
        private readonly ISecurityService _securityService;

        public LocationTypeService(IRepository<MasterLocationType> locationTypeRepository, ISecurityService securityService)
        {
            _locationTypeRepository = locationTypeRepository;
            _securityService = securityService;
        }

        public void Save(MasterLocationType data)
        {
            if (data.LocationTypeId == 0)
            {
                var institutionId = _securityService.GetCurrentInstitutionId();
                data.InstitutionId = institutionId;
            }
            _locationTypeRepository.Save(data);
            _locationTypeRepository.Commit();
        }

        public void Delete(MasterLocationType data)
        {
            _locationTypeRepository.Delete(data);
            _locationTypeRepository.Commit();
        }

        public List<MasterLocationType> FetchAll()
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            return _locationTypeRepository.Query().Where(l => l.IsActive && l.InstitutionId == institutionId).ToList();
        }

        public MasterLocationType FetchOne(long locationTypeId)
        {
            return _locationTypeRepository.Query().FirstOrDefault(l => l.LocationTypeId == locationTypeId);
        }

        public List<MasterLocationType> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery)
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            var result = _locationTypeRepository.Query().Where(l => l.IsActive && l.InstitutionId == institutionId);
            searchQuery.TotalData = BaseSearchQueryExpression<MasterLocationType>.DefaultQueryExpression(ref result, searchQuery);
            return result.ToList();
        }
    }
}

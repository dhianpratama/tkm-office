using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core;
using Core.Models.Master;
using Core.Services;
using Core.Services.Master;

namespace Service.Master
{
    public class ShelveService : IShelveService
    {
        private readonly IRepository<MasterShelve> _shelveRepository;
        private readonly ILocationService _locationService;
        private readonly ISecurityService _securityService;
        private readonly IReaderModuleService _readerModuleService;

        public ShelveService(IRepository<MasterShelve> shelveRepository, ILocationService locationService,
            ISecurityService securityService, IReaderModuleService readerModuleService)
        {
            _shelveRepository = shelveRepository;
            _locationService = locationService;
            _securityService = securityService;
            _readerModuleService = readerModuleService;
        }

        public MasterShelve Save(MasterShelve data)
        {
            if (data.ShelveId == 0)
            {
                var institutionId = _securityService.GetCurrentInstitutionId();
                data.InstitutionId = institutionId;
            }
            _shelveRepository.Save(data);

            if(data.Modules != null) {
                _readerModuleService.Save(data.Modules.ToList());
            }

            _shelveRepository.Commit();

            return data;
        }

        public void Delete(MasterShelve data)
        {
            _shelveRepository.Delete(data);
            _shelveRepository.Commit();
        }

        public List<MasterShelveQuery> FetchAll()
        {
            var data = new List<MasterShelveQuery>();
            var locationList = _locationService.FetchAllByInstitution();
            var institutionId = _securityService.GetCurrentInstitutionId();
            _shelveRepository.Query().Where(s => s.IsActive && s.InstitutionId == institutionId).ToList().ForEach(s =>
            {
                var shelve = new MasterShelveQuery()
                {
                    ShelveId = s.ShelveId,
                    ShelveCode = s.ShelveCode,
                    MaxReaderModule = s.MaxReaderModule,
                    Remarks = s.Remarks,
                    LocationId = s.LocationId,
                    Location = locationList.FirstOrDefault(l => l.LocationId == s.LocationId)
                };
                data.Add(shelve);
            });
            return data;
        }

        public MasterShelve FetchCompleteShelve(long shelveId)
        {
            var shelve = _shelveRepository.Query()
                .Include(s => s.Modules.Select(m => m.Bins.Select(b => b.Item).Select(b => b.Brand)))
                .FirstOrDefault(s => s.ShelveId == shelveId);

            if (shelve == null) return null;

            shelve.Modules = shelve.Modules.Where(m => m.IsActive).Select(m =>
            {
                m.Bins = m.Bins.Where(b => b.IsActive).OrderBy(b => b.StackNo).ThenBy(b => b.RowNo).ToList();

                return m;
            }).OrderBy(m => m.StackNo).ThenBy(m => m.RowNo).ToList();

            return shelve;
        }

        public MasterShelve FetchOne(long shelveId)
        {
            return _shelveRepository.Query().FirstOrDefault(s => s.ShelveId == shelveId);
        }

        public List<MasterShelve> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery)
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            var result = _shelveRepository.Query().Where(s => s.IsActive && s.InstitutionId == institutionId);
            searchQuery.TotalData = BaseSearchQueryExpression<MasterShelve>.DefaultQueryExpression(ref result, searchQuery);
            return result.ToList();
        }
    }
}

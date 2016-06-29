using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core;
using Core.Models.Master;
using Core.Services;
using Core.Services.Master;

namespace Service.Master
{
    public class BinService : IBinService
    {
        private readonly IRepository<MasterBin> _binRepository;
        private readonly ISecurityService _securityService;

        public BinService(IRepository<MasterBin> binRepository, ISecurityService securityService)
        {
            _binRepository = binRepository;
            _securityService = securityService;
        }

        public void Save(MasterBin data)
        {
            if (data.BinId == 0)
            {
                var institutionId = _securityService.GetCurrentInstitutionId();
                data.InstitutionId = institutionId;
            }

            _binRepository.Save(data);
            _binRepository.Commit();
        }

        public void Save(List<MasterBin> data)
        {
            data.ForEach(bin => {
                _binRepository.Save(bin);
            });
            _binRepository.Commit();
        }

        public void Delete(MasterBin data)
        {
            _binRepository.Delete(data);
            _binRepository.Commit();
        }

        public List<MasterBin> FetchAll()
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            return _binRepository.Query().Include(b => b.ReaderModule).Where(b => b.IsActive && b.InstitutionId == institutionId).ToList();
        }

        public List<MasterBin> FetchAllByReaderModule(long readerModuleId)
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            return _binRepository.Query()
                .Include(b => b.ReaderModule)
                .Where(b => b.ReaderModuleId == readerModuleId && b.IsActive && b.InstitutionId == institutionId)
                .ToList();
        }

        public List<MasterBin> FetchAllByShelve(long shelveId)
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            return _binRepository.Query()
                .Include(b => b.ReaderModule)
                .Include(b => b.ReaderModule.Shelve)
                .Where(b => b.ReaderModule.Shelve.ShelveId == shelveId && b.IsActive && b.InstitutionId == institutionId)
                .ToList();
        }

        public MasterBin FetchOne(long binId)
        {
            return _binRepository.Query().Include(b => b.ReaderModule).FirstOrDefault(b => b.BinId == binId);
        }

        public MasterBin FetchOne(MasterBinQuery query)
        {
            var bin = _binRepository.Query()
            .Include(b => b.ReaderModule)
            .Include(b => b.Item)
            .OrderBy(b => b.BinCode)
            .FirstOrDefault(b => 
                b.ReaderModule != null && 
                b.ReaderModule.ReaderModuleCode == query.ModuleCode &&
                ((b.StackNo - 1) * b.ReaderModule.NoOfRow + b.RowNo) == query.BinIndex &&
                b.ReaderModule.Shelve != null && 
                b.ReaderModule.Shelve.ShelveCode == query.ShelveCode);

            return bin;
        }

        public long FetchLocationIdByBin(long binId)
        {
            var bin =
                _binRepository.Query()
                    .Include(b => b.ReaderModule)
                    .Include(b => b.ReaderModule.Shelve)
                    .FirstOrDefault(b => b.BinId == binId);
            return bin != null ? bin.ReaderModule.Shelve.LocationId : 0;
        }

        public List<MasterBin> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery)
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            var result = _binRepository.Query().Where(b => b.IsActive && b.IsActive && b.InstitutionId == institutionId).Include(b => b.ReaderModule);
            searchQuery.TotalData = BaseSearchQueryExpression<MasterBin>.DefaultQueryExpression(ref result, searchQuery);
            return result.ToList();
        }
    }
}

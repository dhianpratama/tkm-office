using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core;
using Core.Models.Master;
using Core.Services;
using Core.Services.Master;

namespace Service.Master
{
    public class ReaderModuleService : IReaderModuleService
    {
        private readonly IRepository<MasterReaderModule> _readerModuleRepository;
        private readonly IBinService _binService;
        private readonly ISecurityService _securityService;

        public ReaderModuleService(IRepository<MasterReaderModule> readerModuleRepository, IBinService binService,
            ISecurityService securityService)
        {
            _readerModuleRepository = readerModuleRepository;
            _binService = binService;
            _securityService = securityService;
        }

        public void Save(MasterReaderModule data)
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            if (data.ReaderModuleId == 0)
            {
                data.InstitutionId = institutionId;
            }
            _readerModuleRepository.Save(data);
            _readerModuleRepository.Commit();

            if (data.Bins == null || data.Bins.Count() == 0)
            {
                var count = 1;
                for (var i = 1; i <= data.NoOfStack; i++)
                {
                    for (var j = 1; j <= data.NoOfRow; j++)
                    {
                        var binCode = data.ReaderModuleCode + count.ToString("00");
                        var bin = new MasterBin()
                        {
                            BinCode = binCode,
                            EmptyDistance = data.DefaultBinEmptyDistance,
                            ReaderModuleId = data.ReaderModuleId,
                            StackNo = i,
                            RowNo = j,
                            InstitutionId = institutionId
                        };
                        _binService.Save(bin);
                        count++;
                    }
                }
            }
            else
            {
                _binService.Save(data.Bins.ToList());
            }

        }

        public void Save(List<MasterReaderModule> data)
        {
            data.ForEach(d =>
            {
                Save(d);
            });
        }

        public void Delete(MasterReaderModule data)
        {
            _readerModuleRepository.Delete(data);
            _readerModuleRepository.Commit();
        }

        public List<MasterReaderModule> FetchAll()
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            return _readerModuleRepository.Query().Where(r => r.IsActive && r.InstitutionId == institutionId).Include(r => r.Shelve).ToList();
        }

        public List<MasterReaderModule> FetchAllByShelve(long shelveId)
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            return _readerModuleRepository.Query().Include(r => r.Shelve).Where(r => r.IsActive && r.ShelveId == shelveId && r.InstitutionId == institutionId).ToList();
        }

        public MasterReaderModule FetchOne(long readerModuleId)
        {
            return _readerModuleRepository.Query().Include(r => r.Shelve).FirstOrDefault(r => r.ReaderModuleId == readerModuleId);
        }

        public MasterReaderModule FetchOne(string code)
        {
            var module = _readerModuleRepository.Query().Include(r => r.Shelve).Include(r => r.Bins.Select(b => b.Item)).FirstOrDefault(r => r.ReaderModuleCode == code);

            module.Bins = module.Bins.Where(b => b.IsActive).ToList();

            return module;
        }

        public List<MasterReaderModule> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery)
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            var result = _readerModuleRepository.Query().Include(r => r.Shelve).Where(r => r.IsActive && r.InstitutionId == institutionId);
            searchQuery.TotalData = BaseSearchQueryExpression<MasterReaderModule>.DefaultQueryExpression(ref result, searchQuery);
            return result.ToList();
        }
    }
}

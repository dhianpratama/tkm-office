using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Models.Master;
using Core.Services;
using Core.Services.Master;

namespace Service.Master
{
    public class BinItemService : IBinItemService
    {
        private readonly IRepository<MasterBinItem> _binItemRepository;
        private readonly ISecurityService _securityService;

        public BinItemService(IRepository<MasterBinItem> binItemRepository, ISecurityService securityService)
        {
            _binItemRepository = binItemRepository;
            _securityService = securityService;
        }

        public void Save(MasterBinItem data)
        {
            if (data.BinItemId == 0)
            {
                var institutionId = _securityService.GetCurrentUser().InstitutionId;
                data.InstitutionId = institutionId;
            }
            _binItemRepository.Save(data);
            _binItemRepository.Commit();
        }

        public void Save(List<MasterBinItem> data)
        {
            var institutionId = _securityService.GetCurrentUser().InstitutionId;
            data.ForEach(datum =>
            {
                if (datum.BinItemId == 0)
                {
                    datum.InstitutionId = institutionId;
                }
                _binItemRepository.Save(datum);
            });

            _binItemRepository.Commit();
        }

        public void Delete(MasterBinItem data)
        {
            _binItemRepository.Delete(data);
            _binItemRepository.Commit();
        }

        public List<MasterBinItem> FetchAll()
        {
            var institutionId = _securityService.GetCurrentUser().InstitutionId;
            return
                _binItemRepository.Query()
                    .Include(b => b.Bin)
                    .Include(b => b.Item)
                    .Where(b => b.IsActive && b.InstitutionId == institutionId)
                    .ToList();
        }

        public List<MasterBinItem> FetchAllByBinId(long binId)
        {
            var institutionId = _securityService.GetCurrentUser().InstitutionId;
            return
                _binItemRepository.Query()
                    .Include(b => b.Bin)
                    .Include(b => b.Item)
                    .Where(b => b.BinId == binId && b.IsActive && b.InstitutionId == institutionId)
                    .ToList();
        }

        public List<MasterBinItem> FetchAllByItemId(long itemId)
        {
            var institutionId = _securityService.GetCurrentUser().InstitutionId;
            return
                _binItemRepository.Query()
                    .Include(b => b.Bin)
                    .Include(b => b.Item)
                    .Where(b => b.ItemId == itemId && b.IsActive && b.InstitutionId == institutionId)
                    .ToList();
        }

        public MasterBinItem FetchOne(long binItemId)
        {
            return _binItemRepository.Query().FirstOrDefault(b => b.BinItemId == binItemId);
        }

        public List<MasterBinItem> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery)
        {
            var institutionId = _securityService.GetCurrentUser().InstitutionId;
            var result =
                _binItemRepository.Query().Where(b => b.IsActive && b.IsActive && b.InstitutionId == institutionId);
            searchQuery.TotalData = BaseSearchQueryExpression<MasterBinItem>.DefaultQueryExpression(ref result, searchQuery);
            return result.ToList();
        }
    }
}

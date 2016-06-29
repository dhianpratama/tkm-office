using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models.Master;
using Core.Services.Master;

namespace Service.Master
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IRepository<MasterInstitution> _institutionRepository;

        public InstitutionService(IRepository<MasterInstitution> institutionRepository)
        {
            _institutionRepository = institutionRepository;
        }

        public void Save(MasterInstitution data)
        {
            _institutionRepository.Save(data);
            _institutionRepository.Commit();
        }

        public void Delete(MasterInstitution data)
        {
            _institutionRepository.Delete(data);
            _institutionRepository.Commit();

        }

        public List<MasterInstitution> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery)
        {
            var result = _institutionRepository.Query().Where(i => i.IsActive);
            searchQuery.TotalData = BaseSearchQueryExpression<MasterInstitution>.DefaultQueryExpression(ref result, searchQuery);
            return result.ToList();
        }

        public List<MasterInstitution> FetchAll()
        {
            return _institutionRepository.Query().Where(i => i.IsActive).ToList();
        }

        public MasterInstitution FetchOne(long institutionId)
        {
            return _institutionRepository.Query().FirstOrDefault(i => i.InstitutionId == institutionId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models.Master;
using Core.Services;
using Core.Services.Master;

namespace Service.Master
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<MasterBrand> _brandRepository;
        private readonly ISecurityService _securityService;

        public BrandService(IRepository<MasterBrand> brandRepository, ISecurityService securityService)
        {
            _brandRepository = brandRepository;
            _securityService = securityService;
        }

        public void Save(MasterBrand data)
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            data.InstitutionId = institutionId;
            _brandRepository.Save(data);
            _brandRepository.Commit();
        }

        public void Delete(MasterBrand data)
        {
            _brandRepository.Delete(data);
            _brandRepository.Commit();
        }

        public List<MasterBrand> FetchAll()
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            return _brandRepository.Query().Where(b => b.InstitutionId == institutionId).ToList();
        }

        public MasterBrand FetchOne(long brandId)
        {
            return _brandRepository.Query().FirstOrDefault(b => b.BrandId == brandId);
        }

        public List<MasterBrand> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery)
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            var result = _brandRepository.Query().Where(b => b.IsActive && b.InstitutionId == institutionId);
            searchQuery.TotalData = BaseSearchQueryExpression<MasterBrand>.DefaultQueryExpression(ref result, searchQuery);
            return result.ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models.Master;
using Core.Services.Master;

namespace Service.Master
{
    public class UomService : IUomService
    {
        private readonly IRepository<MasterUom> _uomRepository;

        public UomService(IRepository<MasterUom> uomRepository)
        {
            _uomRepository = uomRepository;
        }

        public void Save(MasterUom data)
        {
            try
            {
                _uomRepository.Save(data);
                _uomRepository.Commit();
            }
            catch (Exception ex)
            {
                //TODO: Log Error
            }
        }

        public void Delete(MasterUom data)
        {
            try
            {
                _uomRepository.Delete(data);
                _uomRepository.Commit();
            }
            catch (Exception ex)
            {
                //TODO: Log Error
            }
        }

        public List<MasterUom> FetchAll()
        {
            try
            {
                return _uomRepository.Query().ToList();
            }
            catch (Exception ex)
            {
                //TODO: Log Error
                return null;
            }
        }

        public MasterUom FetchOne(long uomId)
        {
            try
            {
                return _uomRepository.Query().FirstOrDefault(u => u.UomId == uomId);
            }
            catch (Exception ex)
            {
                //TODO: Log Error
                return null;
            }
        }

        public List<MasterUom> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery)
        {
            var result = _uomRepository.Query().Where(u => u.IsActive);
            searchQuery.TotalData = BaseSearchQueryExpression<MasterUom>.DefaultQueryExpression(ref result, searchQuery);
            return result.ToList();
        }
    }
}

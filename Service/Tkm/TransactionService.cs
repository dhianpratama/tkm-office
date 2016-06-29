using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Models.Tkm;
using Core.Services;
using Core.Services.Tkm;

namespace Service.Tkm
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<TkmTransaction> _transactionRepository;
        private readonly ISecurityService _securityService;

        public TransactionService(
            IRepository<TkmTransaction> transactionRepository,
            ISecurityService securityService)
        {
            _transactionRepository = transactionRepository;
            _securityService = securityService;
        }

        public List<TkmTransaction> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery)
        {
            var result = _transactionRepository.Query().Where(b => b.IsActive);
            searchQuery.TotalData = BaseSearchQueryExpression<TkmTransaction>.DefaultQueryExpression(ref result, searchQuery);
            return result.ToList();
        }

        public TkmTransaction FetchOne(long transactionId)
        {
            return _transactionRepository.Query().FirstOrDefault(b => b.TransactionId == transactionId);
        }

        public void Save(TkmTransaction data)
        {
            var userId = _securityService.GetCurrentUserId();
            if (userId <= 0) return;
            data.UserId = userId;
            _transactionRepository.Save(data);
            _transactionRepository.Commit();
        }

        public void Delete(TkmTransaction data)
        {
            _transactionRepository.Delete(data);
            _transactionRepository.Commit();
        }

        public string GenerateReferenceNumber()
        {
            return string.Format("#{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}

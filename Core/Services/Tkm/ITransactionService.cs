using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Tkm;

namespace Core.Services.Tkm
{
    public interface ITransactionService
    {
        List<TkmTransaction> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery);
        TkmTransaction FetchOne(long transactionId);
        void Save(TkmTransaction data);
        void Delete(TkmTransaction data);
        string GenerateReferenceNumber();
        void ApproveTransaction(long transactionId);
    }
}

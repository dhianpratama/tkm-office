using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Models.Tkm;

namespace Service.Tkm
{
    public class TransactionReportService
    {
        private readonly IRepository<TkmTransaction> _transactionRepository;

        public TransactionReportService(
            IRepository<TkmTransaction> transactionRepository )
        {
            _transactionRepository = transactionRepository;
        }

        public List<TkmTransactionReportModel> GetReportData()
        {
            var result = new List<TkmTransactionReportModel>();

            return result;
        } 
    }
}

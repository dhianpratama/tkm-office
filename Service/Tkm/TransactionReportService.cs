using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Models.Tkm;
using Core.Services.Tkm;

namespace Service.Tkm
{
    public class TransactionReportService : ITransactionReportService
    {
        private readonly IRepository<TkmTransaction> _transactionRepository;

        public TransactionReportService(
            IRepository<TkmTransaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public List<TkmTransactionReportModel> GetReportData(DateTime startDate, DateTime? endDate)
        {
            var result = new List<TkmTransactionReportModel>();

            startDate = startDate.Date;
            var data = _transactionRepository.Query()
                .Where(e => e.IsActive && e.TransactionDate >= startDate);

            if (endDate != null)
            {
                endDate = endDate.Value.Date;
                data = data.Where(e => e.TransactionDate <= endDate);
            }

            long? balance = 0;
            data
                .OrderBy(e => e.TransactionDate)
                .ThenBy(e => e.LastUpdatedDate)
                .ToList().ForEach(d =>
                {
                    var t = d.Type.ToString();
                    var model = new TkmTransactionReportModel()
                    {
                        TransactionId = d.TransactionId,
                        TransactionCode = d.TransactionCode,
                        TransactionDate = d.TransactionDate.ToString("dd/MM/yyyy"),
                        Type = t,
                        Remarks = d.Remarks,
                        PictureUrl = d.PictureUrl,
                        Value = d.Value
                    };

                    if (d.Type == TransactionType.Income)
                    {
                        model.Income = d.Type == TransactionType.Income ? (long?) d.Value : null;
                        balance += model.Income;
                    }
                    else
                    {
                        model.Outcome = d.Type == TransactionType.Outcome ? (long?) d.Value : null;
                        balance -= model.Outcome;
                    }
                    model.Balance = balance;

                    result.Add(model);
                });

            return result;
        }
    }
}

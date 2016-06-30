using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Tkm;

namespace Core.Services.Tkm
{
    public interface ITransactionReportService
    {
        List<TkmTransactionReportModel> GetReportData(DateTime startDate, DateTime? endDate);
    }
}

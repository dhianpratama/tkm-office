using System.Collections.Generic;
using Core.Models.Master;

namespace Core.Services.Master
{
    public interface IReaderModuleService
    {
        void Save(MasterReaderModule data);
        void Save(List<MasterReaderModule> data);
        void Delete(MasterReaderModule data);
        List<MasterReaderModule> FetchAll();
        List<MasterReaderModule> FetchAllByShelve(long shelveId);
        MasterReaderModule FetchOne(long readerModuleId);
        List<MasterReaderModule> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery);

        MasterReaderModule FetchOne(string code);
    }
}

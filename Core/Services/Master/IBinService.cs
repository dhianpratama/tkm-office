using System.Collections.Generic;
using Core.Models.Master;

namespace Core.Services.Master
{
    public interface IBinService
    {
        void Save(MasterBin data);
        void Delete(MasterBin data);
        List<MasterBin> FetchAll();
        List<MasterBin> FetchAllByReaderModule(long readerModuleId);
        List<MasterBin> FetchAllByShelve(long shelveId);
        MasterBin FetchOne(long binId);
        long FetchLocationIdByBin(long binId);
        List<MasterBin> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery);
        void Save(List<MasterBin> data);

        MasterBin FetchOne(MasterBinQuery query);
    }
}

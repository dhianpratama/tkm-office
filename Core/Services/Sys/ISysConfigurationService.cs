using System.Collections.Generic;
using Core.Models.Sys;

namespace Core.Services.Sys
{
    public interface ISysConfigurationService
    {
        string GetConfig(string configKey);
        void Save(SysConfiguration data);
        void BulkSave(List<SysConfiguration> data);
        List<SysConfiguration> FetchAll();
    }
}

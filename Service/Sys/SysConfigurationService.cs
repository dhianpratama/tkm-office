using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models.Sys;
using Core.Services.Sys;

namespace Service.Sys
{
    public class SysConfigurationService : ISysConfigurationService
    {
        private readonly IRepository<SysConfiguration> _configRepository;

        public SysConfigurationService(IRepository<SysConfiguration> configRepository)
        {
            _configRepository = configRepository;
        }

        public string GetConfig(string configKey)
        {
            var config = _configRepository.Query().FirstOrDefault(cfg => cfg.ConfigKey == configKey);
            return config != null ? config.ConfigValue : "";
        }

        public void Save(SysConfiguration data)
        {
            _configRepository.Save(data);
            _configRepository.Commit();
        }

        public void BulkSave(List<SysConfiguration> data)
        {
            data.ForEach(d =>
            {
                _configRepository.Save(d);
            });
            _configRepository.Commit();
        }

        public List<SysConfiguration> FetchAll()
        {
            return _configRepository.Query().Where(cfg => cfg.IsActive).ToList();
        }
    }
}

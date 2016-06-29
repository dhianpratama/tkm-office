using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using Core.Models.Master;
using Core.Models.Sys;
using Core.Models.Tkm;
using SqlProviderService = System.Data.Entity.SqlServer.SqlProviderServices;

namespace EF
{
    public class SmartShelveContext : DbContext
    {
        public SmartShelveContext()
            : base("SmartShelveContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<MasterInstitution> MasterInstitutions { get; set; }
        public DbSet<MasterUser> MasterUsers { get; set; }
        public DbSet<MasterUom> MasterUoms { get; set; }
        public DbSet<MasterItem> MasterItems { get; set; }
        public DbSet<MasterLocationType> MasterLocationTypes { get; set; }
        public DbSet<MasterShelve> Shelves { get; set; }
        public DbSet<MasterReaderModule> MasterReaderModules { get; set; }
        public DbSet<MasterBin> MasterBins { get; set; }
        public DbSet<MasterBrand> MasterBrands { get; set; }
        public DbSet<SysConfiguration> SysConfigurations { get; set; }

        public DbSet<TkmTransaction> TkmTransactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

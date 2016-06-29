using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.UI.WebControls;
using Core;
using Core.Models.Master;
using Core.Services;
using Core.Services.Master;

namespace Service.Master
{
    public class ItemService : IItemService
    {
        public const string ItemImageFolder = "Content/Item";
        private readonly IRepository<MasterItem> _itemRepository;
        private readonly ISecurityService _securityService;

        public ItemService(IRepository<MasterItem> itemRepository, ISecurityService securityService)
        {
            _itemRepository = itemRepository;
            _securityService = securityService;
        }

        public void Save(MasterItem data)
        {

            var isNewImage = data.ItemId == 0 && !String.IsNullOrEmpty(data.ImageFilename);
            if (data.ItemId == 0)
            {
                var institutionId = _securityService.GetCurrentInstitutionId();
                data.InstitutionId = institutionId;
            }
            _itemRepository.Save(data);
            _itemRepository.Commit();

            if (!isNewImage) return;

            var imageDirPath = HostingEnvironment.MapPath("~/" + ItemImageFolder);
            if (imageDirPath == null) return;
            var source = Path.Combine(imageDirPath, "temp", data.ImageFilename);
            var destination = Path.Combine(imageDirPath, data.ItemId.ToString(), data.ImageFilename);
            if (!Directory.Exists(Path.Combine(imageDirPath, data.ItemId.ToString())))
                Directory.CreateDirectory(Path.Combine(imageDirPath, data.ItemId.ToString()));
            File.Move(source, destination);

        }

        public void Delete(MasterItem data)
        {
            _itemRepository.Delete(data);
            _itemRepository.Commit();
        }

        public List<MasterItem> FetchAll()
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            return
                _itemRepository.Query()
                    .Include(i => i.Uom)
                    .Include(i => i.Brand)
                    .Where(i => i.IsActive && i.InstitutionId == institutionId)
                    .ToList();
        }

        public MasterItem FetchOne(long itemId)
        {
            return
                _itemRepository.Query()
                    .Include(i => i.Uom)
                    .Include(i => i.Brand)
                    .FirstOrDefault(i => i.ItemId == itemId);
        }

        public List<MasterItem> FetchAllWithPagination(ref BaseSearchQueryModel searchQuery)
        {
            var institutionId = _securityService.GetCurrentInstitutionId();
            var result =
                _itemRepository.Query()
                    .Include(i => i.Uom)
                    .Include(i => i.Brand)
                    .Where(i => i.IsActive && i.InstitutionId == institutionId);
            searchQuery.TotalData = BaseSearchQueryExpression<MasterItem>.DefaultQueryExpression(ref result, searchQuery);
            return result.ToList();
        }
    }
}

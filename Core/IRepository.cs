using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IRepository<TModel> where TModel : BaseModel
    {
        void Save(TModel model);
        void Delete(TModel model);
        IQueryable<TModel> Query();
        void Commit();
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using Core;

namespace EF
{
    public class EfRepository<T> : IRepository<T> where T : BaseModel
    {
        private readonly EfUnitOfWork _unitOfWork;

        public EfRepository(IUnitOfWork unitOfWork)
        {
#if DEBUG
            Debug.WriteLine(string.Format("EfRepository {0} created", typeof(T).FullName));
#endif
            _unitOfWork = (EfUnitOfWork) unitOfWork;
        }

        public void Save(T model)
        {
            var idProp = typeof(T).GetProperties().FirstOrDefault(p => Attribute.IsDefined(p, typeof(KeyAttribute)));
            if (idProp != null && (long) idProp.GetValue(model) > 0)
            {
                _unitOfWork.Context.Entry(model).State = EntityState.Modified;
            }
            else
            {
                _unitOfWork.Context.Set<T>().Add(model);
                model.IsActive = true;
                model.CreatedBy = HttpContext.Current.User == null ? "" : HttpContext.Current.User.Identity.Name;
                model.CreatedDate = DateTime.Now;
            }
            model.LastUpdatedBy = HttpContext.Current.User == null ? "" : HttpContext.Current.User.Identity.Name;
            model.LastUpdatedDate = DateTime.Now;
        }

        public void Delete(T model)
        {
            model.IsActive = false;
            Save(model);
        }

        public IQueryable<T> Query()
        {
            return _unitOfWork.Context.Set<T>();
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
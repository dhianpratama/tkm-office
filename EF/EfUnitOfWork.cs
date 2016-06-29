using System.Diagnostics;
using Core;

namespace EF
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly SmartShelveContext _context;

        public EfUnitOfWork(SmartShelveContext context)
        {
#if DEBUG
            Debug.WriteLine("UOW Created");
            context.Database.Log += s => Debug.WriteLine(s);
#endif
            _context = context;
        }

        public SmartShelveContext Context
        {
            get { return _context; }
        }

        public void Dispose()
        {
#if DEBUG
            Debug.WriteLine("UOW Disposed");
#endif
            _context.Dispose();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
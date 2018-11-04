using System.Data.Entity;
using WebStore.Interface;
using WebStore.Models;

namespace WebStore.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }

        public EFUnitOfWork()
        {
            //Context = Context == null ? new StoreDBContext() : Context;
            //Original
            Context = new StoreEntities();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
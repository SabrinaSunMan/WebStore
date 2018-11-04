using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebStore.Interface;

namespace WebStore.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IUnitOfWork UnitOfWork { get; set; }

        private DbSet<T> _Objectset;

        private DbSet<T> ObjectSet
        {
            get
            {
                if (_Objectset == null)
                {
                    _Objectset = UnitOfWork.Context.Set<T>();
                }
                return _Objectset;
            }
        }

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void Create(T entity)
        {
            ObjectSet.Add(entity);
        }

        public void Delete(T entity)
        {
            ObjectSet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return ObjectSet;
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> filter)
        {
            return ObjectSet.Where(filter);
        }

        public T GetSingle(Expression<Func<T, bool>> filter)
        {
            return ObjectSet.SingleOrDefault(filter);
        }

        public void Commit()
        {
            UnitOfWork.Save();
        }

        protected void Update(T entity, params object[] keyValues)
        {
            var entry = UnitOfWork.Context.Entry<T>(entity);

            if (entry.State == EntityState.Detached)
            {
                var set = UnitOfWork.Context.Set<T>();
                T attachedEntity = set.Find(keyValues);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = UnitOfWork.Context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }
            //var entry = Context.Entry(entity);
            //entry.State = System.Data.EntityState.Modified;
            //ObjectSet.Attach(entity);
        }
    }
}
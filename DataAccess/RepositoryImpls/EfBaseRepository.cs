using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepositoryImpls
{
    public class EfBaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;

        public EfBaseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Guid Add(T item)
        {
            var entity = context.Set<T>().Add(item);
            context.SaveChanges();
            return entity.Entity.Id;
        }

        public void Delete(T item)
        {
            if (item == null)
                return;
            var inDbItem = context.Set<T>().SingleOrDefault(p => p.Id == item.Id);
            if (inDbItem == null)
                return;

            context.Set<T>().Remove(inDbItem);
            context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            return context.Set<T>().Where(predicate).ToList();
        }

        public T GetById(Guid id)
        {
            var inDbItem = context.Set<T>().SingleOrDefault(p => p.Id == id);
            return inDbItem;
        }

        public void Update(T item)
        {
            var inDbItem = context.Set<T>().SingleOrDefault(p => p.Id == item.Id);
            if (inDbItem == null)
                return;

            foreach (var prop in item.GetType().GetProperties()) 
            {
                var val = item.GetType().GetProperty(prop.Name).GetValue(item);
                inDbItem.GetType().GetProperty(prop.Name).SetValue(inDbItem, val);
            }
            context.SaveChanges();
        }
    }
}

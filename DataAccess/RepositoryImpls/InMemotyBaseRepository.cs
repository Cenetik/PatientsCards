﻿using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.RepositoryImpls
{
    public class InMemoryBaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private IEnumerable<T> data;

        public InMemoryBaseRepository(IEnumerable<T> data)
        {
            this.data = data;
        }

        public void Add(T item)
        {
            data = data.Append(item);
        }

        public void Delete(T item)
        {
            data = data.Where(p => p.Id != item.Id).ToList();            
        }

        public IEnumerable<T> GetAll()
        {
            return data.AsQueryable();
        }

        public IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            return data.Where(predicate).AsQueryable();
        }

        public void Update(T item)
        {
            var existed = data.FirstOrDefault(p => p.Id == item.Id);
            if (existed == null)
                throw new Exception("Не найден объект для изменения в БД с Id = " + item.Id + "!");
            data = data.Where(p => p.Id != item.Id).ToList();
            data = data.Append(item).ToList();
        }
    }
}

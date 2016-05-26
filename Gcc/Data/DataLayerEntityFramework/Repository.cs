using Gcc.Data.DataLayerAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcc.Data.DataLayerEntityFramework
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private GccContext _context;

        public Repository(GccContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
    }
}
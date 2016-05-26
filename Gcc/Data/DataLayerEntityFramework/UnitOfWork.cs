using Gcc.Data.DataLayerAbstraction;
using Gcc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcc.Data.DataLayerEntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
 
        private readonly GccContext _context;
        private IRepository<Cliente> _clienteRepository;
 
        public UnitOfWork()
        {
            _context = new GccContext();
        }
        public UnitOfWork(GccContext context)
        {
            _context = context;
        }
 
        public IRepository<Cliente> ClienteRepository
        {
            get
            {
                if (_clienteRepository == null)
                {
                    _clienteRepository = new Repository<Cliente>(_context);
                }
                return _clienteRepository;
            }
        } 

        public void Save()
        {
            _context.SaveChanges();
        }
 
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    

    }
}
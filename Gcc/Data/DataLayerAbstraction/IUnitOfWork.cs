using Gcc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gcc.Data.DataLayerAbstraction
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Cliente> ClienteRepository { get; }
        void Save();
    }
}
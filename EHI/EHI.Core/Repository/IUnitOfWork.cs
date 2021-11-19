using EHI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHI.Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Contact> ContactRepository { get; }
        Task<int> CommitAsync();
    }
}

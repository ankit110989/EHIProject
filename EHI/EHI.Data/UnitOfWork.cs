using EHI.Core.Entities;
using EHI.Core.Repository;
using EHI.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EHIDBContext _context;
        private Repository<Contact> _contactRepository;

        public UnitOfWork(EHIDBContext context)
        {
            this._context = context;
        }

        public IRepository<Contact> ContactRepository => _contactRepository ?? new Repository<Contact>(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
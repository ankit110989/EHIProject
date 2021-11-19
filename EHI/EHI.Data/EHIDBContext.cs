using EHI.Core.Entities;
using EHI.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace EHI.Data
{
    public class EHIDBContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public EHIDBContext(DbContextOptions<EHIDBContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ContactConfiguration());
        }
    }
}

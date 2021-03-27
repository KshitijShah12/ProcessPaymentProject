using Microsoft.EntityFrameworkCore;
using ProcessPaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPaymentApi.Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<ProcessPayment> processPayments { get; set; }

        public DbSet<ProcessStatus> processStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcessPayment>()
                .HasOne<ProcessStatus>(s => s.ProcessStatus)
                .WithOne(ad => ad.ProcessPayment)
                .HasForeignKey<ProcessStatus>(ad => ad.CardId);
        }
    }

}


﻿using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Comati3.Models
{
    public class ComatiContext :DbContext
    {
        public ComatiContext(DbContextOptions options ) :base(options)
        {
           
         }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Comati> Comaties { get; set; }
        public DbSet<ComatiMember> Members { get; set; }
        public DbSet<ComatiPayment> ComatiPayments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ComatiMember>()
                .HasOne(cm => cm.Comati)
                .WithMany(c => c.Members)
                .HasForeignKey(cm => cm.ComatiId);

            modelBuilder.Entity<ComatiMember>()
                .HasOne(cm => cm.Person)
                .WithMany(p => p.ComatiMemberships)
                .HasForeignKey(cm => cm.PersonId);

            modelBuilder.Entity<ComatiPayment>()
                .HasOne(p => p.Comati)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.ComatiId);

            modelBuilder.Entity<ComatiPayment>()
                .HasOne(p => p.Person)
                .WithMany(per => per.ComatiPayments)
                .HasForeignKey(p => p.PersonId);
        }

    }
}

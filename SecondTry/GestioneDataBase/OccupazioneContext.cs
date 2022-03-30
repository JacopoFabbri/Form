using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SecondTry
{
    public partial class OccupazioneContext : DbContext
    {
        public OccupazioneContext()
        {
        }

        public OccupazioneContext(DbContextOptions<OccupazioneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Occupazione> Occupaziones { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Occupazione;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Occupazione>(entity =>
            {
                entity.ToTable("Occupazione");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Commessa).HasMaxLength(50);

                entity.Property(e => e.DataInserimento)
                    .HasColumnType("datetime")
                    .HasColumnName("Data_Inserimento")
                    .HasDefaultValueSql("(((1)/(1))/(1900))");

                entity.Property(e => e.Indirizzo).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

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

        public virtual DbSet<Occupazione> Occupazione { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=93.95.221.22;Initial Catalog=Occupazione;Integrated Security=False;User Id=sa;Password=C0rtexlan?21");
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

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Business.Models;

namespace Business
{
    public partial class CommonContext : DbContext
    {
        private readonly string _connectionString;
        public CommonContext()
        {
        }

        public CommonContext(DbContextOptions<CommonContext> options)
            : base(options)
        {
        }

        public CommonContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public virtual DbSet<DealerApplicationConfiguration> DealerApplicationConfiguration { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=HCM-QA-T430\\AU2;Database=common;User ID=TitanDBA;Password=T1t@nDB4F0rHCM-DEV-DB\\AU2;Trusted_Connection=false;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DealerApplicationConfiguration>(entity =>
            {
                entity.HasKey(e => e.DealerApplicationConfigurationKey)
                    .HasName("PK_DEALER_APPLICATION_CONFIGURATION_KEY");

                entity.ToTable("DEALER_APPLICATION_CONFIGURATION");

                entity.HasIndex(e => new { e.DealerId, e.Application, e.DeviceId })
                    .HasName("UK_DEALER_ID_APPLICATION_DEVICE_ID")
                    .IsUnique();

                entity.Property(e => e.DealerApplicationConfigurationKey).HasColumnName("DEALER_APPLICATION_CONFIGURATION_KEY");

                entity.Property(e => e.AllowAccess)
                    .IsRequired()
                    .HasColumnName("ALLOW_ACCESS")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Application)
                    .IsRequired()
                    .HasColumnName("APPLICATION")
                    .HasMaxLength(40);

                entity.Property(e => e.CreationEmpId)
                    .IsRequired()
                    .HasColumnName("CREATION_EMP_ID")
                    .HasMaxLength(5);

                entity.Property(e => e.CreationTimestamp)
                    .HasColumnName("CREATION_TIMESTAMP")
                    .HasColumnType("datetime");

                entity.Property(e => e.DealerId)
                    .IsRequired()
                    .HasColumnName("DEALER_ID")
                    .HasMaxLength(40);

                entity.Property(e => e.DeviceDescription)
                    .HasColumnName("DEVICE_DESCRIPTION")
                    .HasMaxLength(40);

                entity.Property(e => e.DeviceId)
                    .HasColumnName("DEVICE_ID")
                    .HasMaxLength(40);

                entity.Property(e => e.ExpiredDate)
                    .HasColumnName("EXPIRED_DATE")
                    .HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

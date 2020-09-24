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
        public virtual DbSet<DealerConfiguration> DealerConfiguration { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=HCM-QA-T430,1441\\AU2;Database=common;User ID=TitanDBA;Password=T1t@nDB4F0rHCM-DEV-DB\\AU2;Trusted_Connection=false;");
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

            modelBuilder.Entity<DealerConfiguration>(entity =>
            {
                entity.HasKey(e => e.DealerId);

                entity.ToTable("DEALER_CONFIGURATION");

                entity.Property(e => e.DealerId)
                    .HasColumnName("DEALER_ID")
                    .HasMaxLength(40);

                entity.Property(e => e.AddressServerKey).HasColumnName("ADDRESS_SERVER_KEY");

                entity.Property(e => e.BypassIpValidation)
                    .IsRequired()
                    .HasColumnName("BYPASS_IP_VALIDATION")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.CountryCode).HasColumnName("COUNTRY_CODE");

                entity.Property(e => e.CurrentReleasedVersionNo)
                    .HasColumnName("CURRENT_RELEASED_VERSION_NO")
                    .HasMaxLength(40);

                entity.Property(e => e.DatabaseName)
                    .HasColumnName("DATABASE_NAME")
                    .HasMaxLength(40);

                entity.Property(e => e.DaylightSavingDayOfWeek).HasColumnName("DAYLIGHT_SAVING_DAY_OF_WEEK");

                entity.Property(e => e.DaylightSavingHourFrom).HasColumnName("DAYLIGHT_SAVING_HOUR_FROM");

                entity.Property(e => e.DaylightSavingHourTo).HasColumnName("DAYLIGHT_SAVING_HOUR_TO");

                entity.Property(e => e.DaylightSavingMinute).HasColumnName("DAYLIGHT_SAVING_MINUTE");

                entity.Property(e => e.DaylightSavingMinuteFrom).HasColumnName("DAYLIGHT_SAVING_MINUTE_FROM");

                entity.Property(e => e.DaylightSavingMinuteTo).HasColumnName("DAYLIGHT_SAVING_MINUTE_TO");

                entity.Property(e => e.DaylightSavingMonthFrom).HasColumnName("DAYLIGHT_SAVING_MONTH_FROM");

                entity.Property(e => e.DaylightSavingMonthTo).HasColumnName("DAYLIGHT_SAVING_MONTH_TO");

                entity.Property(e => e.DaylightSavingWeekFrom)
                    .HasColumnName("DAYLIGHT_SAVING_WEEK_FROM")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DaylightSavingWeekTo)
                    .HasColumnName("DAYLIGHT_SAVING_WEEK_TO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DealershipNotificationEmailAddress)
                    .HasColumnName("DEALERSHIP_NOTIFICATION_EMAIL_ADDRESS")
                    .HasMaxLength(255);

                entity.Property(e => e.EmailServerKey).HasColumnName("EMAIL_SERVER_KEY");

                entity.Property(e => e.IsDaylightSaving)
                    .IsRequired()
                    .HasColumnName("IS_DAYLIGHT_SAVING")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IsProduction)
                    .IsRequired()
                    .HasColumnName("IS_PRODUCTION")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.IsTitanDatabase)
                    .IsRequired()
                    .HasColumnName("IS_TITAN_DATABASE")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.IsTraining)
                    .IsRequired()
                    .HasColumnName("IS_TRAINING")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.LatestReleasedVersionNo)
                    .HasColumnName("LATEST_RELEASED_VERSION_NO")
                    .HasMaxLength(40);

                entity.Property(e => e.LicencingControlUserLimit).HasColumnName("LICENCING_CONTROL_USER_LIMIT");

                entity.Property(e => e.LockExceededLogIn)
                    .IsRequired()
                    .HasColumnName("LOCK_EXCEEDED_LOG_IN")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.MobileServerKey).HasColumnName("MOBILE_SERVER_KEY");

                entity.Property(e => e.NoOfLicenses).HasColumnName("NO_OF_LICENSES");

                entity.Property(e => e.NumberOfExtendedDay).HasColumnName("NUMBER_OF_EXTENDED_DAY");

                entity.Property(e => e.OverrideDatabaseName)
                    .HasColumnName("OVERRIDE_DATABASE_NAME")
                    .HasMaxLength(40);

                entity.Property(e => e.ServerName)
                    .HasColumnName("SERVER_NAME")
                    .HasMaxLength(40);

                entity.Property(e => e.ServerTimeZoneOffsetHour).HasColumnName("SERVER_TIME_ZONE_OFFSET_HOUR");

                entity.Property(e => e.ServerTimeZoneOffsetMinute).HasColumnName("SERVER_TIME_ZONE_OFFSET_MINUTE");

                entity.Property(e => e.StartCountingDate)
                    .HasColumnName("START_COUNTING_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.TimeZoneName)
                    .HasColumnName("TIME_ZONE_NAME")
                    .HasMaxLength(128);

                entity.Property(e => e.TimeZoneOffSet)
                    .HasColumnName("TIME_ZONE_OFF_SET")
                    .HasColumnType("decimal(5, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.TitanNotificationEmailAddress)
                    .HasColumnName("TITAN_NOTIFICATION_EMAIL_ADDRESS")
                    .HasMaxLength(255);

                entity.Property(e => e.TitanSysadPassword)
                    .HasColumnName("TITAN_SYSAD_PASSWORD")
                    .HasMaxLength(40);

                entity.Property(e => e.UseLicensingControl)
                    .IsRequired()
                    .HasColumnName("USE_LICENSING_CONTROL")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('0')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

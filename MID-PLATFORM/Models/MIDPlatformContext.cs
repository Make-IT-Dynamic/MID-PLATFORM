using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace MID_PLATFORM.Models
{
    public partial class MIDPlatformContext : DbContext
    {
        public MIDPlatformContext()
        {
        }

        public MIDPlatformContext(DbContextOptions<MIDPlatformContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Period> Periods { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<SmAgent> SmAgents { get; set; } = null!;
        public virtual DbSet<SmContract> SmContracts { get; set; } = null!;
        public virtual DbSet<SmContractLimit> SmContractLimits { get; set; } = null!;
        public virtual DbSet<SmContractStatus> SmContractStatuses { get; set; } = null!;
        public virtual DbSet<SmContractType> SmContractTypes { get; set; } = null!;
        public virtual DbSet<SmPriority> SmPriorities { get; set; } = null!;
        public virtual DbSet<SmTask> SmTasks { get; set; } = null!;
        public virtual DbSet<SmTaskStatus> SmTaskStatuses { get; set; } = null!;
        public virtual DbSet<SmTaskType> SmTaskTypes { get; set; } = null!;
        public virtual DbSet<SmWorkRecord> SmWorkRecords { get; set; } = null!;
        public virtual DbSet<SmWorkRecordType> SmWorkRecordTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //                .AddJsonFile("appsettings.json")
        //               .Build();
        //        string connectionString = configuration.GetConnectionString("DefaultConnection");

        //        optionsBuilder.UseSqlServer(connectionString);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.LongCode).HasMaxLength(200);

                entity.Property(e => e.LongDescription).HasMaxLength(1000);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.InverseParentNavigation)
                    .HasForeignKey(d => d.Parent)
                    .HasConstraintName("FK_Categories_Categories");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Categories_Users");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.Code, "UQ_Companies_Code")
                    .IsUnique();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Country).HasMaxLength(3);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Companies)
                    .HasPrincipalKey(p => p.CountryCode)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Companies_Countries");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Companies_Users");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasIndex(e => e.CountryCode, "UQ_Countries_CountryCode")
                    .IsUnique();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CountryCode).HasMaxLength(3);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Countries_Users");
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.Property(e => e.ActiveForSm)
                    .IsRequired()
                    .HasColumnName("ActiveForSM")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Periods)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Periods_Users");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => new { e.Company, e.Name }, "UQ_Person_Company_Name")
                    .IsUnique();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.CompanyNavigation)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.Company)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_People_Companies");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_People_Users");
            });

            modelBuilder.Entity<SmAgent>(entity =>
            {
                entity.HasKey(e => e.AgentId);

                entity.ToTable("SM_Agents");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.SmAgents)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_Agents_Users");
            });

            modelBuilder.Entity<SmContract>(entity =>
            {
                entity.HasKey(e => e.ContractId);

                entity.ToTable("SM_Contracts");

                entity.HasIndex(e => new { e.Code, e.Instance }, "UQ_SM_Contracts_Code_Instance")
                    .IsUnique();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Instance).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.SmContracts)
                    .HasForeignKey(d => d.Category)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_Contracts_Categories");

                entity.HasOne(d => d.CompanyNavigation)
                    .WithMany(p => p.SmContracts)
                    .HasForeignKey(d => d.Company)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_Contracts_Companies");

                entity.HasOne(d => d.ContactPersonNavigation)
                    .WithMany(p => p.SmContracts)
                    .HasForeignKey(d => d.ContactPerson)
                    .HasConstraintName("FK_SM_Contracts_People");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.SmContracts)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_Contracts_SM_ContractStatus");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.SmContracts)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_Contracts_SM_ContractTypes");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.SmContracts)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_Contracts_Users");
            });

            modelBuilder.Entity<SmContractLimit>(entity =>
            {
                entity.HasKey(e => e.ContractLimitsId);

                entity.ToTable("SM_ContractLimits");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Document).HasMaxLength(100);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.ContractNavigation)
                    .WithMany(p => p.SmContractLimits)
                    .HasForeignKey(d => d.Contract)
                    .HasConstraintName("FK_SM_ContractLimits_SM_ContractTypes");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.SmContractLimits)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_ContractLimits_Users");
            });

            modelBuilder.Entity<SmContractStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("SM_ContractStatus");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.SmContractStatuses)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_ContractStatus_Users");
            });

            modelBuilder.Entity<SmContractType>(entity =>
            {
                entity.HasKey(e => e.ContractTypeId);

                entity.ToTable("SM_ContractTypes");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.SmContractTypes)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_ContractTypes_Users");
            });

            modelBuilder.Entity<SmPriority>(entity =>
            {
                entity.HasKey(e => e.PriorityId);

                entity.ToTable("SM_Priorities");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.SmPriorities)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_Priorities_Users");
            });

            modelBuilder.Entity<SmTask>(entity =>
            {
                entity.HasKey(e => e.TaskId);

                entity.ToTable("SM_Tasks");

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.ReplyDate).HasColumnType("datetime");

                entity.Property(e => e.Subject).HasMaxLength(200);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.SmTasks)
                    .HasForeignKey(d => d.AssignedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_Tasks_SM_Agents");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.SmTasks)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("FK_SM_Tasks_Categories");

                entity.HasOne(d => d.ContractNavigation)
                    .WithMany(p => p.SmTasks)
                    .HasForeignKey(d => d.Contract)
                    .HasConstraintName("FK_SM_Tasks_SM_ContractTypes");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.SmTaskCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_SM_Tasks_Users");

                entity.HasOne(d => d.PriorityNavigation)
                    .WithMany(p => p.SmTasks)
                    .HasForeignKey(d => d.Priority)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_Tasks_SM_Priorities");

                entity.HasOne(d => d.RequesterNavigation)
                    .WithMany(p => p.SmTasks)
                    .HasForeignKey(d => d.Requester)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_Tasks_People");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.SmTasks)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_Tasks_SM_TaskStatus");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.SmTasks)
                    .HasForeignKey(d => d.Type)
                    .HasConstraintName("FK_SM_Tasks_SM_TaskTypes");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.SmTaskUserNavigations)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_Tasks_Users2");
            });

            modelBuilder.Entity<SmTaskStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("SM_TaskStatus");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.SmTaskStatuses)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_TaskStatus_Users");
            });

            modelBuilder.Entity<SmTaskType>(entity =>
            {
                entity.HasKey(e => e.TaskTypeId);

                entity.ToTable("SM_TaskTypes");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.SmTaskTypes)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskTypes_Users");
            });

            modelBuilder.Entity<SmWorkRecord>(entity =>
            {
                entity.HasKey(e => e.WorkRecordId);

                entity.ToTable("SM_WorkRecords");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.AgentNavigation)
                    .WithMany(p => p.SmWorkRecords)
                    .HasForeignKey(d => d.Agent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_WorkRecords_SM_Agents");

                entity.HasOne(d => d.TaskNavigation)
                    .WithMany(p => p.SmWorkRecords)
                    .HasForeignKey(d => d.Task)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_WorkRecords_SM_Tasks");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.SmWorkRecords)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_WorkRecords_SM_WorkRecordTypes");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.SmWorkRecords)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_WorkRecords_Users");
            });

            modelBuilder.Entity<SmWorkRecordType>(entity =>
            {
                entity.HasKey(e => e.WorkRecordTypeId);

                entity.ToTable("SM_WorkRecordTypes");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Billable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.SmWorkRecordTypes)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_WorkRecordTypes_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Timestamp);
                    //.IsRowVersion();
                    //.IsConcurrencyToken();

                entity.Property(e => e.User1)
                    //.HasMaxLength(50)
                    .HasColumnName("User");

                entity.HasOne(d => d.User1Navigation)
                    .WithMany(p => p.InverseUser1Navigation)
                    .HasForeignKey(d => d.User1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Users")
                    ;
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

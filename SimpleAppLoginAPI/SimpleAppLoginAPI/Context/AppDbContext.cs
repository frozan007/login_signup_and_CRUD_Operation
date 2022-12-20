using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SimpleAppLoginAPI.Models;
using System.Reflection.Emit;

namespace SimpleAppLoginAPI.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext() :
            base()
        {
            OnCreated();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {
            OnCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured ||
               (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
               !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
            {

            }
            CustomConfiguration(ref optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Signup> Signup { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Profile> Profile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.UsersMapping(modelBuilder);
            this.CustomizeUsersMapping(modelBuilder);

            this.SignupMapping(modelBuilder);
            this.CustomizeSignupMapping(modelBuilder);

            this.EmployeesMapping(modelBuilder);
            this.CustomizeEmployeesMapping(modelBuilder);

            this.ProfileMapping(modelBuilder);
            this.CustomizeProfileMapping(modelBuilder);

            RelationalshipsMapping(modelBuilder);
            CustomizeMapping(ref modelBuilder);
        }
        

        #region Users Mapping 
        private void UsersMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(@"Users", @"dbo");
            modelBuilder.Entity<User>().Property(x => x.Id).HasColumnName(@"Id").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(x => x.UserName).HasColumnName(@"UserName").HasColumnType(@"nvarchar(64)").ValueGeneratedNever();
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnName(@"Email").HasColumnType(@"nvarchar(64)").ValueGeneratedNever();
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName(@"Password").HasColumnType(@"nvarchar(64)").ValueGeneratedNever();
            modelBuilder.Entity<User>().HasKey("Id");
        }
        partial void CustomizeUsersMapping(ModelBuilder modelBuilder);
        #endregion

        #region Signup Mapping
        private void SignupMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Signup>().ToTable(@"Signup", @"dbo");
            modelBuilder.Entity<Signup>().Property(x => x.Id).HasColumnName(@"Id").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Signup>().Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType(@"int").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Signup>().Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType(@"nvarchar(128)").ValueGeneratedNever();
            modelBuilder.Entity<Signup>().Property(x => x.LastName).HasColumnName(@"LastName").HasColumnType(@"nvarchar(128)").ValueGeneratedNever();
            modelBuilder.Entity<Signup>().Property(x => x.DateOfBirth).HasColumnName(@"DateOfBirth").HasColumnType(@"datetime").ValueGeneratedNever();
            modelBuilder.Entity<Signup>().Property(x => x.Gender).HasColumnName(@"Gender").HasColumnType(@"smallint").ValueGeneratedNever();
            modelBuilder.Entity<Signup>().Property(x => x.UserName).HasColumnName(@"UserName").HasColumnType(@"nvarchar(64)").ValueGeneratedNever();
            modelBuilder.Entity<Signup>().Property(x => x.Email).HasColumnName(@"Email").HasColumnType(@"nvarchar(64)").ValueGeneratedNever();
            modelBuilder.Entity<Signup>().Property(x => x.Password).HasColumnName(@"Password").HasColumnType(@"nvarchar(64)").ValueGeneratedNever();
            modelBuilder.Entity<Signup>().HasKey("Id");
        }

        partial void CustomizeSignupMapping(ModelBuilder modelBuilder);
        #endregion

        #region Employee Mapping
        private void EmployeesMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable(@"Employees", @"dbo");
            modelBuilder.Entity<Employee>().Property(x => x.Id).HasColumnName(@"Id").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Employee>().Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType(@"nvarchar(128)").ValueGeneratedNever();
            modelBuilder.Entity<Employee>().Property(x => x.LastName).HasColumnName(@"LastName").HasColumnType(@"nvarchar(128)").ValueGeneratedNever();
            modelBuilder.Entity<Employee>().Property(x => x.Designation).HasColumnName(@"Designation").HasColumnType(@"nvarchar(128)").ValueGeneratedNever();
            modelBuilder.Entity<Employee>().Property(x => x.Status).HasColumnName(@"Status").HasColumnType(@"int)").ValueGeneratedNever();
            modelBuilder.Entity<Employee>().HasKey("Id");
        }
        partial void CustomizeEmployeesMapping(ModelBuilder modelBuilder);
        #endregion

        #region Profile Mapping
        private void ProfileMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>().ToTable(@"Profile", @"dbo");
            modelBuilder.Entity<Profile>().Property(x => x.Id).HasColumnName(@"Id").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Profile>().Property(x => x.EmpId).HasColumnName(@"Emp_Id").HasColumnType(@"int").ValueGeneratedNever();
            modelBuilder.Entity<Profile>().Property(x => x.ProfilePicture).HasColumnName(@"ProfilePicture").HasColumnType(@"image").ValueGeneratedNever();
            modelBuilder.Entity<Profile>().Property(x => x.FileName).HasColumnName(@"FileName").HasColumnType(@"nvarchar(64)").ValueGeneratedNever();
            modelBuilder.Entity<Profile>().HasKey("Id");
        }
        partial void CustomizeProfileMapping(ModelBuilder modelBuilder);
        #endregion

        partial void CustomConfiguration(ref DbContextOptionsBuilder optionsBuilder);

        private void RelationalshipsMapping(ModelBuilder modelBuilder) { }
        partial void CustomizeMapping(ref ModelBuilder modelBuilder);

        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(
                e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added ||
                     e.State == Microsoft.EntityFrameworkCore.EntityState.Modified ||
                     e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted
                );

        }

        partial void OnCreated();

        internal Task Update()
        {
            throw new NotImplementedException();
        }
    }
}


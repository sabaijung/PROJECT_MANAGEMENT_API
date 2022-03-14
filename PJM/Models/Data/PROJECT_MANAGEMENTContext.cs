using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PJM.Models.Data
{
    public partial class PROJECT_MANAGEMENTContext : DbContext
    {
        public PROJECT_MANAGEMENTContext()
        {
        }

        public PROJECT_MANAGEMENTContext(DbContextOptions<PROJECT_MANAGEMENTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-DKKFQ822;Initial Catalog=PROJECT_MANAGEMENT;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Thai_CI_AS");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentCode);

                entity.ToTable("department");

                entity.Property(e => e.DepartmentCode).HasColumnName("department_code");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(80)
                    .HasColumnName("department_name");

                entity.Property(e => e.Isused)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("isused")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.PositionCode);

                entity.ToTable("position");

                entity.Property(e => e.PositionCode).HasColumnName("position_code");

                entity.Property(e => e.DepartmentCode).HasColumnName("department_code");

                entity.Property(e => e.Isused)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("isused")
                    .IsFixedLength(true);

                entity.Property(e => e.PositionName)
                    .HasMaxLength(80)
                    .HasColumnName("position_name");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("projects");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("date")
                    .HasColumnName("date_end");

                entity.Property(e => e.DateStart)
                    .HasColumnType("date")
                    .HasColumnName("date_start");

                entity.Property(e => e.Detail)
                    .HasColumnType("text")
                    .HasColumnName("detail");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(120)
                    .HasColumnName("project_name");

                entity.Property(e => e.ProjectStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("project_status")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("users");

                entity.Property(e => e.Code)
                    .HasMaxLength(6)
                    .HasColumnName("code");

                entity.Property(e => e.Address)
                    .HasMaxLength(180)
                    .HasColumnName("address");

                entity.Property(e => e.AmphurCode)
                    .HasMaxLength(4)
                    .HasColumnName("amphur_code");

                entity.Property(e => e.DepartmentCode)
                    .HasMaxLength(2)
                    .HasColumnName("department_code");

                entity.Property(e => e.DistrictCode)
                    .HasMaxLength(6)
                    .HasColumnName("district_code");

                entity.Property(e => e.ImageProfile)
                    .HasMaxLength(50)
                    .HasColumnName("image_profile");

                entity.Property(e => e.InitialCode)
                    .HasMaxLength(2)
                    .HasColumnName("initial_code");

                entity.Property(e => e.Isused)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("isused")
                    .IsFixedLength(true);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .HasColumnName("lastname");

                entity.Property(e => e.Mobilephone)
                    .HasMaxLength(10)
                    .HasColumnName("mobilephone");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(35)
                    .HasColumnName("password");

                entity.Property(e => e.PositionCode)
                    .HasMaxLength(2)
                    .HasColumnName("position_code");

                entity.Property(e => e.Postcode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("postcode")
                    .IsFixedLength(true);

                entity.Property(e => e.ProvinceCode)
                    .HasMaxLength(2)
                    .HasColumnName("province_code");

                entity.Property(e => e.Role)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("role")
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .HasMaxLength(35)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

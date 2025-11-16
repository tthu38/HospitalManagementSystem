using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Business;

public partial class HospitalManagementContext : DbContext
{
    public HospitalManagementContext()
    {
    }

    public HospitalManagementContext(DbContextOptions<HospitalManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Admission> Admissions { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Billing> Billings { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=HospitalDB;User Id=sa;Password=12345;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__719FE488F35DF6EF");

            entity.ToTable("Admin");

            entity.HasIndex(e => e.Username, "UQ__Admin__536C85E4A7C57D3C").IsUnique();

            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<Admission>(entity =>
        {
            entity.HasKey(e => e.AdmissionId).HasName("PK__Admissio__C97EEC42C342B539");

            entity.ToTable("Admission");

            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Patient).WithMany(p => p.Admissions)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__Admission__Patie__5AEE82B9");

            entity.HasOne(d => d.Room).WithMany(p => p.Admissions)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Admission__RoomI__5BE2A6F2");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCC23797E8DF");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppointmentDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__Appointme__Docto__619B8048");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__Appointme__Patie__60A75C0F");
        });

        modelBuilder.Entity<Billing>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__Billing__11F2FC6A53B51D8F");

            entity.ToTable("Billing");

            entity.Property(e => e.Paid).HasDefaultValue(false);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Admission).WithMany(p => p.Billings)
                .HasForeignKey(d => d.AdmissionId)
                .HasConstraintName("FK__Billing__Admissi__656C112C");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BEDC7B24E5F");

            entity.ToTable("Department");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctor__2DC00EBF49DA4492");

            entity.ToTable("Doctor");

            entity.HasIndex(e => e.Username, "UQ__Doctor__536C85E46DEB327D").IsUnique();

            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Specialization).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Department).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Doctor__Departme__4D94879B");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDD69F5E9DB1");

            entity.ToTable("Feedback");

            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__Feedback__Doctor__6B24EA82");

            entity.HasOne(d => d.Patient).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__Feedback__Patien__6A30C649");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E12D0EBE4AB");

            entity.ToTable("Notification");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.RecipientRole).HasMaxLength(50);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__970EC366939F3105");

            entity.ToTable("Patient");

            entity.HasIndex(e => e.Username, "UQ__Patient__536C85E46B185F70").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__328639392AB42C00");

            entity.ToTable("Room");

            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.RoomNumber).HasMaxLength(20);
            entity.Property(e => e.RoomType).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

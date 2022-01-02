using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace nstu_timetable.DbContexts
{
    public partial class NstuTimetableContext : DbContext
    {
        public NstuTimetableContext()
        {
        }

        public NstuTimetableContext(DbContextOptions<NstuTimetableContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<TeacherContact> TeacherContacts { get; set; } = null!;
        public virtual DbSet<Timetable> Timetables { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("u1410979_nstu_timetable");

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("Faculty");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fullName");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("shortName");

                entity.Property(e => e.TypeSubtitle)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("typeSubtitle");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.HasIndex(e => e.GroupName, "Group_codeName_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseNumber).HasColumnName("courseNumber");

                entity.Property(e => e.FacultyId).HasColumnName("facultyId");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("groupName");

                entity.Property(e => e.ScheduleUrl)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("scheduleUrl");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Group_Faculty_id_fk");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Level).HasMaxLength(10);

                entity.Property(e => e.Logger).HasMaxLength(255);

                entity.Property(e => e.Url).HasMaxLength(255);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.HasIndex(e => e.FullName, "Teacher_fullName_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fullName");

                entity.Property(e => e.PhotoUrl)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("photoUrl");

                entity.Property(e => e.ShortAcademicDegree)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("shortAcademicDegree");
            });

            modelBuilder.Entity<TeacherContact>(entity =>
            {
                entity.ToTable("TeacherContact");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TeacherId).HasColumnName("teacherId");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.Value)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Timetable>(entity =>
            {
                entity.ToTable("Timetable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Classroom)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("classroom");

                entity.Property(e => e.EndDate).HasColumnName("endDate");

                entity.Property(e => e.StartDate).HasColumnName("startDate");

                entity.Property(e => e.Subject)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("subject");

                entity.Property(e => e.TeacherId).HasColumnName("teacherId");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("typeName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

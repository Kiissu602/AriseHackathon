using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace backend
{
    public partial class DBQuizContext : DbContext
    {
        public DBQuizContext()
        {
        }

        public DBQuizContext(DbContextOptions<DBQuizContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblInstructor> TblInstructors { get; set; }
        public virtual DbSet<TblTransactionTimeSlot> TblTransactionTimeSlots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=DBQuiz;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblInstructor>(entity =>
            {
                entity.ToTable("Tbl_Instructor");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActiveDay)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InstructorCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TimeEnd).HasColumnType("time(0)");

                entity.Property(e => e.TimeStart).HasColumnType("time(0)");
            });

            modelBuilder.Entity<TblTransactionTimeSlot>(entity =>
            {
                entity.ToTable("Tbl_TransactionTimeSlot");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.InstrutorCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TimeEnd).HasColumnType("time(0)");

                entity.Property(e => e.TimeStart).HasColumnType("time(0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

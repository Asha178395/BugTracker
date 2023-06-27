using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerDAL.Models;

public partial class BugtrackerdbContext : DbContext
{
    public BugtrackerdbContext()
    {
    }

    public BugtrackerdbContext(DbContextOptions<BugtrackerdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source =issueserver.database.windows.net;initial catalog =bugtrackerdb;user id=demouser;password=Syren@1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFCA76805314");

            entity.Property(e => e.CommentId).ValueGeneratedNever();
            entity.Property(e => e.Comment1)
                .IsUnicode(false)
                .HasColumnName("Comment");
            entity.Property(e => e.CommentedOn).HasColumnType("datetime");
            entity.Property(e => e.IssueId)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Emp).WithMany(p => p.Comments)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("fk_empid");

            entity.HasOne(d => d.Issue).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("fk_issueid");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("fk_parentcomment");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB990D2D66C5");

            entity.ToTable("Employee");

            entity.Property(e => e.EmpId).ValueGeneratedNever();
            entity.Property(e => e.EmpName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Project).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_projectid");
        });

        modelBuilder.Entity<Issue>(entity =>
        {
            entity.HasKey(e => e.IssueId).HasName("PK__Issues__6C8616047FDFED3A");

            entity.Property(e => e.IssueId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Actualdate)
                .HasColumnType("datetime")
                .HasColumnName("actualdate");
            entity.Property(e => e.Category)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.Dateidentified)
                .HasColumnType("datetime")
                .HasColumnName("dateidentified");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Identfiedemp).HasColumnName("identfiedemp");
            entity.Property(e => e.Images)
                .IsUnicode(false)
                .HasColumnName("images");
            entity.Property(e => e.IssueType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IterationNumber).HasDefaultValueSql("((1))");
            entity.Property(e => e.Lastmodifydonedate)
                .HasColumnType("datetime")
                .HasColumnName("lastmodifydonedate");
            entity.Property(e => e.Lastmodifydoneemp).HasColumnName("lastmodifydoneemp");
            entity.Property(e => e.LinkToParent)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("linkToParent");
            entity.Property(e => e.ModuleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("moduleName");
            entity.Property(e => e.Priority)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("priority");
            entity.Property(e => e.Progress)
                .IsUnicode(false)
                .HasColumnName("progress");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("projectId");
            entity.Property(e => e.Ressummary)
                .IsUnicode(false)
                .HasColumnName("ressummary");
            entity.Property(e => e.Seviority)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("seviority");
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("shortDescription");
            entity.Property(e => e.Status)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.StepsToReproduce).IsUnicode(false);
            entity.Property(e => e.Targetdate)
                .HasColumnType("datetime")
                .HasColumnName("targetdate");
            entity.Property(e => e.TestingType)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.AssignToNavigation).WithMany(p => p.IssueAssignToNavigations)
                .HasForeignKey(d => d.AssignTo)
                .HasConstraintName("fk");

            entity.HasOne(d => d.IdentfiedempNavigation).WithMany(p => p.IssueIdentfiedempNavigations)
                .HasForeignKey(d => d.Identfiedemp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_iden");

            entity.HasOne(d => d.Project).WithMany(p => p.Issues)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_projtid");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Project__761ABEF0AEF1DC0E");

            entity.ToTable("Project");

            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.HighPriorityIssues).HasDefaultValueSql("((0))");
            entity.Property(e => e.LowPriorityIssues).HasDefaultValueSql("((0))");
            entity.Property(e => e.MediumPriorityIssues).HasDefaultValueSql("((0))");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.S1seviourty)
                .HasDefaultValueSql("((0))")
                .HasColumnName("S1Seviourty");
            entity.Property(e => e.S2seviourty)
                .HasDefaultValueSql("((0))")
                .HasColumnName("S2Seviourty");
            entity.Property(e => e.S3seviourty)
                .HasDefaultValueSql("((0))")
                .HasColumnName("S3Seviourty");
            entity.Property(e => e.S4seviourty)
                .HasDefaultValueSql("((0))")
                .HasColumnName("S4Seviourty");
            entity.Property(e => e.TotalIssues).HasDefaultValueSql("((0))");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

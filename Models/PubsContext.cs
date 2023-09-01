using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MvcEFCore6.Models;

public partial class PubsContext : DbContext
{
    public PubsContext()
    {
    }

    public PubsContext(DbContextOptions<PubsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    public virtual DbSet<Titleauthor> Titleauthors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(local)\\sqlexpress;Database=Pubs;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuId).HasName("UPKCL_auidind");

            entity.ToTable("authors");

            entity.HasIndex(e => new { e.AuLname, e.AuFname }, "aunmind");

            entity.Property(e => e.AuId)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("au_id");
            entity.Property(e => e.Address)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.AuFname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("au_fname");
            entity.Property(e => e.AuLname)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("au_lname");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Contract).HasColumnName("contract");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasDefaultValueSql("('UNKNOWN')")
                .IsFixedLength()
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("state");
            entity.Property(e => e.Zip)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("zip");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PubId).HasName("UPKCL_pubind");

            entity.ToTable("publishers");

            entity.Property(e => e.PubId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pub_id");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValueSql("('USA')")
                .HasColumnName("country");
            entity.Property(e => e.PubName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("pub_name");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("state");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => e.TitleId).HasName("UPKCL_titleidind");

            entity.ToTable("titles");

            entity.HasIndex(e => e.Title1, "titleind");

            entity.Property(e => e.TitleId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("title_id");
            entity.Property(e => e.Advance)
                .HasColumnType("money")
                .HasColumnName("advance");
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("notes");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.PubId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pub_id");
            entity.Property(e => e.Pubdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("pubdate");
            entity.Property(e => e.Royalty).HasColumnName("royalty");
            entity.Property(e => e.Title1)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasDefaultValueSql("('UNDECIDED')")
                .IsFixedLength()
                .HasColumnName("type");
            entity.Property(e => e.YtdSales).HasColumnName("ytd_sales");

            entity.HasOne(d => d.Pub).WithMany(p => p.Titles)
                .HasForeignKey(d => d.PubId)
                .HasConstraintName("FK__titles__pub_id__1A14E395");
        });

        modelBuilder.Entity<Titleauthor>(entity =>
        {
            entity.HasKey(e => new { e.AuId, e.TitleId }).HasName("UPKCL_taind");

            entity.ToTable("titleauthor");

            entity.HasIndex(e => e.AuId, "auidind");

            entity.HasIndex(e => e.TitleId, "titleidind");

            entity.Property(e => e.AuId)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("au_id");
            entity.Property(e => e.TitleId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("title_id");
            entity.Property(e => e.AuOrd).HasColumnName("au_ord");
            entity.Property(e => e.Royaltyper).HasColumnName("royaltyper");

            entity.HasOne(d => d.Au).WithMany(p => p.Titleauthors)
                .HasForeignKey(d => d.AuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__titleauth__au_id__1DE57479");

            entity.HasOne(d => d.Title).WithMany(p => p.Titleauthors)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__titleauth__title__1ED998B2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

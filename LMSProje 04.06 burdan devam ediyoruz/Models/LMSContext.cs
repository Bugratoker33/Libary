﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LMSProje.Models;

public partial class LMSContext : DbContext
{
    public LMSContext()
    {
    }

    public LMSContext(DbContextOptions<LMSContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAdmin> TblAdmin { get; set; }

    public virtual DbSet<TblAdminApporoval> TblAdminApporoval { get; set; }

    public virtual DbSet<TblAuthor> TblAuthor { get; set; }

    public virtual DbSet<TblBooks> TblBooks { get; set; }

    public virtual DbSet<TblCategory> TblCategory { get; set; }

    public virtual DbSet<TblDateSeting> TblDateSeting { get; set; }

    public virtual DbSet<TblLimits> TblLimits { get; set; }

    public virtual DbSet<TblRecords> TblRecords { get; set; }

    public virtual DbSet<TblSeting> TblSeting { get; set; }

    public virtual DbSet<TblUser> TblUser { get; set; }

    public virtual DbSet<TblUserTypes> TblUserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-FCRUI1S;Initial Catalog=LMS;Integrated Security=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAdmin>(entity =>
        {
            entity.Property(e => e.AdminId).ValueGeneratedNever();
            entity.Property(e => e.Password).IsFixedLength();
            entity.Property(e => e.SaltOfPw).IsFixedLength();
        });

        modelBuilder.Entity<TblAdminApporoval>(entity =>
        {
            entity.HasOne(d => d.Record).WithMany(p => p.TblAdminApporoval).HasConstraintName("FK_tblAdminApporoval_tblRecords");
        });

        modelBuilder.Entity<TblAuthor>(entity =>
        {
            entity.Property(e => e.AuthorId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblBooks>(entity =>
        {
            entity.Property(e => e.BookId).ValueGeneratedNever();

            entity.HasOne(d => d.Author).WithMany(p => p.TblBooks).HasConstraintName("FK_tblBooks_tblAuthor");

            entity.HasOne(d => d.Category).WithMany(p => p.TblBooks).HasConstraintName("FK_tblBooks_tblCategory");
        });

        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.Property(e => e.CategoryId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblDateSeting>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblRecords>(entity =>
        {
            entity.HasOne(d => d.Book).WithMany(p => p.TblRecords).HasConstraintName("FK_tblRecords_tblBooks");

            entity.HasOne(d => d.User).WithMany(p => p.TblRecords).HasConstraintName("FK_tblRecords_tblUser");
        });

        modelBuilder.Entity<TblSeting>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.Property(e => e.SaltOfPw).IsFixedLength();
            entity.Property(e => e.UserPw).IsFixedLength();

            entity.HasOne(d => d.UserType).WithMany(p => p.TblUser).HasConstraintName("FK_tblUser_tblUserTypes");
        });

        modelBuilder.Entity<TblUserTypes>(entity =>
        {
            entity.Property(e => e.UserTypeId).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
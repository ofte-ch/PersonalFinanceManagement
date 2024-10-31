﻿// <auto-generated />
using System;
using DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ChiTietGiaoDichTaiKhoan", b =>
                {
                    b.Property<int>("ChiTietGiaoDichId")
                        .HasColumnType("int");

                    b.Property<int>("TaiKhoanGiaoDichId")
                        .HasColumnType("int");

                    b.HasKey("ChiTietGiaoDichId", "TaiKhoanGiaoDichId");

                    b.HasIndex("TaiKhoanGiaoDichId");

                    b.ToTable("ChiTietGiaoDichTaiKhoan");
                });

            modelBuilder.Entity("Domain.Entities.ChiTietGiaoDich", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("TheLoaiId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TheLoaiId");

                    b.ToTable("ChiTietGiaoDich");
                });

            modelBuilder.Entity("Domain.Entities.GiaoDich", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChiTietGiaoDichId")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LoaiGiaoDich")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("NgayGiaoDich")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TenGiaoDich")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("TongTien")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("ChiTietGiaoDichId");

                    b.ToTable("GiaoDich");
                });

            modelBuilder.Entity("Domain.Entities.LoaiTaiKhoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LoaiTaiKhoan");
                });

            modelBuilder.Entity("Domain.Entities.TaiKhoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LoaiTaiKhoanId")
                        .HasColumnType("int");

                    b.Property<double>("SoDu")
                        .HasColumnType("double");

                    b.Property<string>("TenTaiKhoan")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("LoaiTaiKhoanId");

                    b.ToTable("TaiKhoan");
                });

            modelBuilder.Entity("Domain.Entities.TheLoai", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhanLoai")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TenTheLoai")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TheLoai");
                });

            modelBuilder.Entity("ChiTietGiaoDichTaiKhoan", b =>
                {
                    b.HasOne("Domain.Entities.ChiTietGiaoDich", null)
                        .WithMany()
                        .HasForeignKey("ChiTietGiaoDichId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.TaiKhoan", null)
                        .WithMany()
                        .HasForeignKey("TaiKhoanGiaoDichId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.ChiTietGiaoDich", b =>
                {
                    b.HasOne("Domain.Entities.TheLoai", "TheLoai")
                        .WithMany()
                        .HasForeignKey("TheLoaiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TheLoai");
                });

            modelBuilder.Entity("Domain.Entities.GiaoDich", b =>
                {
                    b.HasOne("Domain.Entities.ChiTietGiaoDich", "ChiTietGiaoDich")
                        .WithMany()
                        .HasForeignKey("ChiTietGiaoDichId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChiTietGiaoDich");
                });

            modelBuilder.Entity("Domain.Entities.TaiKhoan", b =>
                {
                    b.HasOne("Domain.Entities.LoaiTaiKhoan", "LoaiTaiKhoan")
                        .WithMany("DSTaiKhoan")
                        .HasForeignKey("LoaiTaiKhoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoaiTaiKhoan");
                });

            modelBuilder.Entity("Domain.Entities.LoaiTaiKhoan", b =>
                {
                    b.Navigation("DSTaiKhoan");
                });
#pragma warning restore 612, 618
        }
    }
}

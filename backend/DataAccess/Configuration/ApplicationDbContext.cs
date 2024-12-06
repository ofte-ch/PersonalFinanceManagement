using System;
using Application.Interface;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Configuration;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<GiaoDich> GiaoDich { get; set; }
    //public DbSet<ChiTietGiaoDich> ChiTietGiaoDich { get; set; }
    public DbSet<TaiKhoan> TaiKhoan { get; set; }
    public DbSet<LoaiTaiKhoan> LoaiTaiKhoan { get; set; }
    public DbSet<TheLoai> TheLoai { get; set; }
    public DbSet<Token> Tokens { get; set; }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}
using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Interface;

public interface IApplicationDbContext
{
    DbSet<GiaoDich> GiaoDich { get; set; }
    DbSet<ChiTietGiaoDich> ChiTietGiaoDich { get; set; }
    DbSet<TaiKhoan> TaiKhoan { get; set; }
    DbSet<LoaiTaiKhoan> LoaiTaiKhoan { get; set; }
    DbSet<TheLoai> TheLoai { get; set; }
    Task<int> SaveChangesAsync();
}

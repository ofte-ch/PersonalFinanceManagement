using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class GiaoDichDTO
    {
        public int id { get; set; }
        public string TenGiaoDich { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        //public string LoaiGiaoDich { get; set; }

        public TheLoaiDTO TheLoai { get; set; } = null!;
        public double TongTien { get; set; }
        public string? GhiChu { get; set; }
        public virtual TaiKhoanDTO TaiKhoanGoc { get; set; } = null!;
        public virtual TaiKhoanDTO? TaiKhoanPhu { get; set; } = null!;

        public GiaoDichDTO (int id, string tenGiaoDich, DateTime ngayGiaoDich, TheLoaiDTO theLoai, double tongTien, string? ghiChu, TaiKhoanDTO taiKhoanGoc, TaiKhoanDTO? taiKhoanPhu)
        {
            this.id = id;
            TenGiaoDich = tenGiaoDich;
            NgayGiaoDich = ngayGiaoDich;
            TheLoai = theLoai;
            TongTien = tongTien;
            GhiChu = ghiChu;
            TaiKhoanGoc = taiKhoanGoc;
            TaiKhoanPhu = taiKhoanPhu;
        }

        public GiaoDichDTO() { }
    }
}

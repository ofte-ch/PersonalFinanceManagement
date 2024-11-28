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
        public string LoaiGiaoDich { get; set; }
        public double TongTien { get; set; }
        public string GhiChu { get; set; }
        public virtual ICollection<TaiKhoanDTO> TaiKhoanGiaoDich { get; set; } = null!;

    }
}

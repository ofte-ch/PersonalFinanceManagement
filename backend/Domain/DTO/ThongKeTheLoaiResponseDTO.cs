using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ThongKeTheLoaiResponseDTO
    {
        public int TheLoaiId { get; set; }
        public string TenTheLoai { get; set; }
        public double TongThu { get; set; }

        public int SoLuongGiaoDichThu { get; set; }
        public double TongChi { get; set; }

        public int SoLuongGiaoDichChi { get; set; }
    }
}

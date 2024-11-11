using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ThongKeTaiKhoanResponseDTO
    {
        public int TaiKhoanId { get; set; }
        public string TenTaiKhoan { get; set; }

        public string LoaiTaiKhoan { get; set; }

        public double TongThu { get; set; }

        public int SoLuongGiaoDichThu { get; set; }

        public double TongChi { get; set; }

        public int SoLuongGiaoDichChi { get; set; }
    }
}

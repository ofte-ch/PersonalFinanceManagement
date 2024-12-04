using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class LoaiTaiKhoanDTO
    {
        public int id { get; set; }
        public string ten { get; set; }

        public LoaiTaiKhoanDTO()
        {
        }
        public LoaiTaiKhoanDTO(int id, string ten)
        {
            this.id = id;
            this.ten = ten;
        }
    }
}

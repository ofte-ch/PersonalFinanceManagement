using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request.Accounts
{
    public class UpdateAccountRequest
    {
        public string TenTaiKhoan { get; set; }
        public int LoaiTaiKhoanId { get; set; }
        public double SoDu { get; set; }
    }
}

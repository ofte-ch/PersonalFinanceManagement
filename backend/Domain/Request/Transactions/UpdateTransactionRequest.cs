﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request.Transactions
{
    public class UpdateTransactionRequest
    {
        public String TenGiaoDich { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public int TaiKhoanChuyenId { get; set; }
        public int? TaiKhoanNhanId { get; set; }
        public int TheLoaiId { get; set; }
        public Double TongTien { get; set; }
        public String? GhiChu { get; set; }
    }
}
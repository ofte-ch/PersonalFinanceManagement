using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class TaiKhoan : BaseEntity 
{
    public String TenTaiKhoan { get; set; }
    public int LoaiTaiKhoanId { get; set; }
    [ForeignKey("LoaiTaiKhoanId")]
    public virtual LoaiTaiKhoan LoaiTaiKhoan { get; set; }
    public Double SoDu { get; set; }
}

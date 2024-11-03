using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Table("GiaoDich")]
public class GiaoDich : BaseEntity
{
    public String TenGiaoDich { get; set; }
    public DateTime NgayGiaoDich { get; set; }
    public String LoaiGiaoDich { get; set; }
    public int ChiTietGiaoDichId { get; set; }

    [ForeignKey("ChiTietGiaoDichId")]
    public virtual ChiTietGiaoDich ChiTietGiaoDich { get; set; } = null!;
    public double TongTien { get; set; }
    public String GhiChu { get; set; }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class ChiTietGiaoDich : BaseEntity
{
    public virtual ICollection<TaiKhoan> TaiKhoanGiaoDich { get; set; } = new Collection<TaiKhoan>();
    public int TheLoaiId { get; set; }
    [ForeignKey("TheLoaiId")]
    public virtual TheLoai TheLoai { get; set; }
}

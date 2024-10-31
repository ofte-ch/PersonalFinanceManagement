using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Table("LoaiTaiKhoan")]
public class LoaiTaiKhoan : BaseEntity
{
    public String Ten { get; set; }
    public virtual ICollection<TaiKhoan> DSTaiKhoan { get; set; } = new Collection<TaiKhoan>();
}

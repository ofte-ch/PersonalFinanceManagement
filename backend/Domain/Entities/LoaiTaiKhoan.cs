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
    [Required(ErrorMessage = "Tên loại tài khoản không được để trống.")]
    [RegularExpression(@"^[A-Za-z0-9\s]{1,50}$", ErrorMessage = "Tên loại tàu khoản không được chứa ký tự đặc biệt và không có khoảng trắng ở đầu/cuối.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Tên loại tài khoản phải có độ dài từ 1 đến 50 ký tự.")]
    public String Ten { get; set; }
    public virtual ICollection<TaiKhoan> DSTaiKhoan { get; set; } = new Collection<TaiKhoan>();
}

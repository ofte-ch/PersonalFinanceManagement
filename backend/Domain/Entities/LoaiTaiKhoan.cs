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
    [RegularExpression(@"^[\p{L}\s]{1,50}$", ErrorMessage = "Tên loại tài khoản chỉ được chứa chữ cái có dấu và dấu cách, không chứa ký tự đặc biệt hoặc khoảng trắng thừa.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Tên loại tài khoản phải có độ dài từ 1 đến 50 ký tự.")]
    public String Ten { get; set; }
    //public virtual ICollection<TaiKhoan> DSTaiKhoan { get; set; } = new Collection<TaiKhoan>();

    [ForeignKey("userId")]
    public virtual User User { get; set; }

    public bool TrangThai { get; set; }
}

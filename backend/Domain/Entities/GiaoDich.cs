using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Table("GiaoDich")]
public class GiaoDich : BaseEntity
{
    [Required(ErrorMessage = "Tên giao dịch không được để trống.")]
    [RegularExpression(@"^[A-Za-z0-9]{1,50}$", ErrorMessage = "Tên giao dịch không được chứa ký tự đặc biệt và không có khoảng trắng ở đầu/cuối.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Tên giao dịch phải có độ dài từ 1 đến 50 ký tự.")]
    public string TenGiaoDich { get; set; }

    [Required(ErrorMessage = "Ngày giao dịch không được để trống.")]
    [Range(typeof(DateTime), "1900-01-01", "2099-12-31", ErrorMessage = "Ngày giao dịch phải nằm trong khoảng từ năm 1900 đến 31/12/2099.")]
    public DateTime NgayGiaoDich { get; set; }

    [Required(ErrorMessage = "Loại giao dịch không được để trống.")]
    public string LoaiGiaoDich { get; set; }

    public int ChiTietGiaoDichId { get; set; }

    [ForeignKey("ChiTietGiaoDichId")]
    public virtual ChiTietGiaoDich ChiTietGiaoDich { get; set; } = null!;

    [Required(ErrorMessage = "Tổng tiền không được để trống.")]
    [Range(0, 100000000000, ErrorMessage = "Tổng tiền phải nằm trong khoảng từ 0 đến 100 tỷ.")]
    public double TongTien { get; set; }

    public string GhiChu { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("GiaoDich")]
public class GiaoDich : BaseEntity
{
    [Required(ErrorMessage = "Tên giao dịch không được để trống.")]
    [RegularExpression(@"^(?!.*^\s)(?!.*\s$)[\p{L}\p{N}\p{Zs}]{1,50}$", ErrorMessage = "Tên giao dịch không được chứa ký tự đặc biệt và không có khoảng trắng ở đầu/cuối.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Tên giao dịch phải có độ dài từ 1 đến 50 ký tự.")]
    public string TenGiaoDich { get; set; }

    [Required(ErrorMessage = "Ngày giao dịch không được để trống.")]
    [Range(typeof(DateTime), "1900-01-01", "2099-12-31", ErrorMessage = "Ngày giao dịch phải nằm trong khoảng từ năm 1900 đến 31/12/2099.")]
    public DateTime NgayGiaoDich { get; set; }

    //[Required(ErrorMessage = "Loại giao dịch không được để trống.")]
    //public string LoaiGiaoDich { get; set; }

    // Khóa ngoại cho tài khoản chuyển
    public int TaiKhoanGocId { get; set; }
    [ForeignKey("TaiKhoanGocId")]
    public virtual TaiKhoan TaiKhoanGoc { get; set; } = null!;

    // Khóa ngoại cho tài khoản nhận
    public int? TaiKhoanPhuId { get; set; }
    [ForeignKey("TaiKhoanPhuId")]
    public virtual TaiKhoan? TaiKhoanPhu { get; set; } = null!;

    public int TheLoaiId { get; set; }
    [ForeignKey("TheLoaiId")]
    public virtual TheLoai TheLoai { get; set; } = null!;

    [Required(ErrorMessage = "Tổng tiền không được để trống.")]
    [Range(0, 100000000000, ErrorMessage = "Tổng tiền phải nằm trong khoảng từ 0 đến 100 tỷ.")]
    public double TongTien { get; set; }

    [StringLength(100, ErrorMessage = "Ghi chú không được vượt quá 100 ký tự.")]
    public string? GhiChu { get; set; } = string.Empty;

}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Table("TaiKhoan")]
public class TaiKhoan : BaseEntity 
{
    [Required(ErrorMessage = "Tên tài khoản không được trống.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Tên tài khoản từ 1 đến 50 ký tự.")]
    [RegularExpression(@"^[A-Za-z0-9àáãạảăắằẳẵặâấầẩẫậèéẹẻẽêềếểễệđìíĩỉịòóõọỏôốồổỗộơớờởỡợùúũụủưứừửữựỳỵỷỹýÀÁÃẠẢĂẮẰẲẴẶÂẤẦẨẪẬÈÉẸẺẼÊỀẾỂỄỆĐÌÍĨỈỊÒÓÕỌỎÔỐỒỔỖỘƠỚỜỞỠỢÙÚŨỤỦƯỨỪỬỮỰỲỴỶỸÝ\s]{1,50}$", ErrorMessage = "Tên tài khoản không được chứa ký tự đặc biệt và không có khoảng trắng ở đầu/cuối.")]

    [CustomValidation(typeof(TaiKhoan), nameof(ValidateTenTaiKhoan))]
    public String TenTaiKhoan { get; set; }


    [Required(ErrorMessage = "Loại tài khoản không được trống.")]
    public int LoaiTaiKhoanId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    [ForeignKey("LoaiTaiKhoanId")]
    public virtual LoaiTaiKhoan LoaiTaiKhoan { get; set; }

    [Required(ErrorMessage = "Số dư không được để trống.")]
    [Range(-1_000_000_000_000, 1_000_000_000_000,
        ErrorMessage = "Số dư phải nằm trong khoảng -1,000,000,000,000 vnd đến 1,000,000,000,000 vnd.")]

    public Double SoDu { get; set; }


    //public virtual ICollection<ChiTietGiaoDich> ChiTietGiaoDich { get; set; } = new Collection<ChiTietGiaoDich>();


    /// <summary>
    /// Cập nhật số dư.
    /// </summary>
    /// <param name="soTien">Số tiền muốn thay đổi. Số dương để tăng, số âm để giảm.</param>
    /// <exception cref="ArgumentException">Nếu số dư nằm ngoài khoảng quy định sau khi cập nhật, sẽ ném lỗi.</exception>
    public void CapNhatSoDu(double soTien)
    {
        double soDuMoi = SoDu + soTien;
        if (soDuMoi < -1_000_000_000_000 || soDuMoi > 1_000_000_000_000)
        {
            throw new ArgumentException("Số dư sau khi cập nhật phải nằm trong khoảng -1,000,000,000,000 đến 1,000,000,000,000.");
        }
        SoDu = soDuMoi;
    }

    /// <summary>
    /// Xác thực tên tài khoản không chứa khoảng trắng ở đầu/cuối.
    /// </summary>
    /// <param name="value">Giá trị cần xác thực.</param>
    /// <param name="context">Ngữ cảnh xác thực.</param>
    /// <returns>Kết quả xác thực.</returns>
    public static ValidationResult ValidateTenTaiKhoan(string value, ValidationContext context)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new ValidationResult("Tên tài khoản không được để trống.");
        }

        if (value.StartsWith(" ") || value.EndsWith(" "))
        {
            return new ValidationResult("Tên tài khoản không được chứa khoảng trắng ở đầu hoặc cuối.");
        }

        return ValidationResult.Success;
    }
}

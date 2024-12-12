using Microsoft.EntityFrameworkCore.Migrations;
using BCrypt.Net;
using Domain.Entities;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM giaodich", true);
            migrationBuilder.Sql("DELETE FROM taikhoan", true);
            migrationBuilder.Sql("DELETE FROM loaitaikhoan", true);
            migrationBuilder.Sql("DELETE FROM theloai", true);
            migrationBuilder.Sql("DELETE FROM users", true);

            //// Seed data
            // users
            migrationBuilder.InsertData(table: "Users", columns: ["Name", "Username", "Password"], values: new object[] { "User One", "user@gmail.com", BCrypt.Net.BCrypt.HashPassword("12345678") });
            migrationBuilder.InsertData(table: "Users", columns: ["Name", "Username", "Password"], values: new object[] { "Admin One", "admin@gmail.com", BCrypt.Net.BCrypt.HashPassword("12345678") });

            //// loaitaikhoan
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId", "TrangThai"], values: new object[] { "Tài khoản thanh toán", "1", true });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId", "TrangThai"], values: new object[] { "Tài khoản tiết kiệm", "1", true });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId", "TrangThai"], values: new object[] { "Thẻ tín dụng", "2", true });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId", "TrangThai"], values: new object[] { "Đầu tư", "2", true });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId", "TrangThai"], values: new object[] { "Tài khoản thanh toán đặc biệt", "1", true });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId", "TrangThai"], values: new object[] { "Tài khoản doanh nghiệp", "1", true });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId", "TrangThai"], values: new object[] { "Tài khoản ngoại tệ", "2", true });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId", "TrangThai"], values: new object[] { "Thẻ ghi nợ", "2", true });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId", "TrangThai"], values: new object[] { "Thẻ tín dụng quốc tế", "1", true });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId", "TrangThai"], values: new object[] { "Tài khoản đầu tư dài hạn", "1", true });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId", "TrangThai"], values: new object[] { "Tài khoản tiết kiệm có kỳ hạn", "2", true });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId", "TrangThai"], values: new object[] { "Tài khoản hưu trí", "2", true });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId", "TrangThai"], values: new object[] { "Tài khoản bảo hiểm", "1", true });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId", "TrangThai"], values: new object[] { "Tài khoản đầu tư ngắn hạn", "2", true });


            // theloai
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Nhận chu cấp", "Nhận tiền chu cấp tư đâu đó", "Thu" });
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Nhận lương", "Nhận tiền lương từ đâu đó", "Thu" });
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Nhận tiền", "Nhận tiền từ nguồn nào đó", "Thu" });

            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Ăn uống", "Chi tiền cho việc ăn uống", "Chi" });
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Mua sắm", "Chi tiền cho việc mua sắm", "Chi" });
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Giáo dục", "Chi tiền cho giáo dục", "Chi" });
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Giao dịch giữa 2 tài khoản", "Chuyển tiền qua lại giữa 2 tài khoản", "Chi" });

            // taikhoan

            migrationBuilder.InsertData("TaiKhoan",
                ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"],
                ["VPBank", "1", 500000, "1"]);
            migrationBuilder.InsertData("TaiKhoan",
                ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"],
                ["TPBank", "3", 1000000, "2"]);
            migrationBuilder.InsertData("TaiKhoan",
                ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"],
                ["Thẻ tín dụng VPBank", "1", 2000000, "1"]);
            migrationBuilder.InsertData("TaiKhoan",
                ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"],
                ["Thẻ tiết kiệm Techcom", "2", 2000000, "1"]);
            migrationBuilder.InsertData("TaiKhoan",
                ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"],
                ["Thẻ ghi nợ ngân hàng", "4", 2000000, "2"]);
            migrationBuilder.InsertData("TaiKhoan", ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"], ["VPBank", "1", 500000, "1"]);
            migrationBuilder.InsertData("TaiKhoan", ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"], ["TPBank", "3", 1000000, "2"]);
            migrationBuilder.InsertData("TaiKhoan", ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"], ["Thẻ tín dụng VPBank", "1", 2000000, "1"]);
            migrationBuilder.InsertData("TaiKhoan", ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"], ["Thẻ tiết kiệm Techcom", "2", 2000000, "1"]);
            migrationBuilder.InsertData("TaiKhoan", ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"], ["Thẻ ghi nợ ngân hàng", "4", 2000000, "2"]);
            migrationBuilder.InsertData("TaiKhoan", ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"], ["Sacombank", "1", 1500000, "1"]);
            migrationBuilder.InsertData("TaiKhoan", ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"], ["BIDV", "2", 1200000, "2"]);
            migrationBuilder.InsertData("TaiKhoan", ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"], ["Agribank", "1", 1700000, "1"]);
            migrationBuilder.InsertData("TaiKhoan", ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"], ["Vietcombank", "2", 2200000, "2"]);
            migrationBuilder.InsertData("TaiKhoan", ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"], ["ACB", "1", 1800000, "1"]);


            // giaodich

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua hàng online", "2024-10-30 10:00:00", "1", null, "4", 200000, "GhiChu1"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nạp tiền di động", "2024-10-31 09:00:00", "2", null, "5", 40000, "GhiChu2"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nhận tiền", "2024-10-31 07:00:00", "3", null, "1", 100000, "GhiChu3"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán CHTL", "2024-10-31 08:00:00", "5", null, "4", 300000, "GhiChu4"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán tiền học", "2024-10-31 11:00:00", "3", null, "6", 2000000, "GhiChu5"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua hàng online", "2024-10-16 10:00:00", "2", null, "4", 250000, "GhiChu6"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nạp tiền di động", "2024-10-17 09:00:00", "5", null, "5", 50000, "GhiChu7"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nhận tiền", "2024-10-18 07:00:00", "3", null, "3", 5000000, "GhiChu8"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán CHTL", "2024-10-15 08:00:00", "1", null, "4", 60000, "GhiChu9"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán tiền học", "2024-10-19 11:00:00", "5", null, "6", 3500000, "GhiChu10"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua hàng online", "2024-10-23 10:00:00", "4", null, "5", 100000, "GhiChu11"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nạp tiền di động", "2024-10-11 09:00:00", "1", null, "4", 40000, "GhiChu12"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nhận tiền", "2024-10-14 07:00:00", "2", null, "2", 2000000, "GhiChu12"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán CHTL", "2024-10-25 08:00:00", "3", null, "2", 1700000, "GhiChu13"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán tiền học", "2024-10-12 11:00:00", "5", null, "4", 5800000, "GhiChu14"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua hàng online", "2024-10-30 10:00:00", "4", null, "6", 100000, "GhiChu15"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nạp tiền di động", "2024-10-31 09:00:00", "2", null, "5", 20000, "GhiChu16"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nhận tiền", "2024-10-31 07:00:00", "1", null, "2", 1000000, "GhiChu17"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán CHTL", "2024-10-31 08:00:00", "3", null, "5", 2000000, "GhiChu18"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán tiền học", "2024-10-31 11:00:00", "3", null, "6", 2000000, "GhiChu19"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Chuyển tiền qua lại", "2024-10-31 11:00:00", "3", "4", "6", 2000000, "GhiChu20"]);
            migrationBuilder.InsertData("GiaoDich",
    ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
    ["Mua sắm cuối năm", "2024-11-05 14:00:00", "1", null, "4", 1500000, "GhiChu21"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nạp tiền di động", "2024-11-10 09:00:00", "2", null, "5", 30000, "GhiChu22"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nhận lương", "2024-11-15 08:00:00", "3", null, "1", 10000000, "GhiChu23"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán hóa đơn", "2024-11-20 10:00:00", "4", null, "4", 500000, "GhiChu24"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Tiền học thêm", "2024-11-25 09:30:00", "5", null, "6", 3000000, "GhiChu25"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua quà Noel", "2024-12-01 15:00:00", "1", null, "4", 2000000, "GhiChu26"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nạp tiền di động", "2024-12-05 09:00:00", "2", null, "5", 50000, "GhiChu27"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nhận tiền thưởng", "2024-12-10 08:00:00", "3", null, "1", 5000000, "GhiChu28"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Trả nợ ngân hàng", "2024-12-15 10:00:00", "4", null, "4", 2000000, "GhiChu29"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Chi phí sinh hoạt", "2024-12-20 09:30:00", "5", null, "6", 2500000, "GhiChu30"]);
            migrationBuilder.InsertData("GiaoDich",
    ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
    ["Mua sắm Black Friday", "2024-11-01 14:00:00", "1", null, "4", 1200000, "GhiChu31"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Tiền ăn uống", "2024-11-02 12:00:00", "2", null, "6", 800000, "GhiChu32"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Sửa chữa xe", "2024-11-03 10:30:00", "3", null, "7", 1500000, "GhiChu33"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua sách", "2024-11-04 11:00:00", "4", null, "2", 300000, "GhiChu34"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Đi chơi cuối tuần", "2024-11-06 17:00:00", "5", null, "6", 2000000, "GhiChu35"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Khám sức khỏe", "2024-11-07 08:00:00", "1", null, "3", 1000000, "GhiChu36"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua quà sinh nhật", "2024-11-08 15:30:00", "2", null, "4", 500000, "GhiChu37"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nạp thẻ game", "2024-11-09 13:00:00", "3", null, "5", 100000, "GhiChu38"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Chi phí điện thoại", "2024-11-11 09:00:00", "4", null, "6", 400000, "GhiChu39"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua vé xem phim", "2024-11-12 19:00:00", "5", null, "4", 180000, "GhiChu40"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Đăng ký lớp học", "2024-11-13 10:00:00", "1", null, "3", 1200000, "GhiChu41"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua đồ trang trí", "2024-11-14 14:30:00", "2", null, "5", 700000, "GhiChu42"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Tiền taxi", "2024-11-16 18:00:00", "3", null, "6", 250000, "GhiChu43"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua quà cho bản thân", "2024-11-17 16:00:00", "4", null, "4", 900000, "GhiChu44"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Chi phí du lịch", "2024-11-18 12:00:00", "5", null, "6", 3000000, "GhiChu45"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua vé máy bay", "2024-11-19 09:00:00", "1", null, "5", 5000000, "GhiChu46"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua đồ điện tử", "2024-11-21 15:00:00", "2", null, "4", 2500000, "GhiChu47"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Tiền học phí", "2024-11-22 08:30:00", "3", null, "2", 1500000, "GhiChu48"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Chi phí bảo hiểm", "2024-11-23 11:00:00", "4", null, "3", 600000, "GhiChu49"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanGocId", "TaiKhoanPhuId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua sắm cho mùa đông", "2024-11-24 14:00:00", "5", null, "4", 1800000, "GhiChu50"]);
            //migrationBuilder
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

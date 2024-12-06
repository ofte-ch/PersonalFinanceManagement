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
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId"], values: new object[] { "Tài khoản thanh toán", "1" });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId"], values: new object[] { "Tài khoản tiết kiệm", "1" });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId"], values: new object[] { "Thẻ tín dụng", "2" });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten", "userId"], values: new object[] { "Đầu tư", "2" });

            // theloai
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Chu cấp", "Nhận tiền chu cấp tư đâu đó", "Thu" });
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Lương", "Nhận tiền lương từ đâu đó", "Thu" });
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Cộng", "Nhận tiền từ nguồn nào đó", "Thu" });

            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Ăn uống", "Chi tiền cho việc ăn uống", "Chi" });
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Mua sắm", "Chi tiền cho việc mua sắm", "Chi" });
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Giao dục", "Chi tiền cho giáo dục", "Chi" });

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


            // giaodich

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua hàng online", "2024-10-30 10:00:00", "1", null,"4", 200000, "GhiChu1"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nạp tiền di động", "2024-10-31 09:00:00", "2", null, "5", 40000, "GhiChu2"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nhận tiền", "2024-10-31 07:00:00", "3", "1", "1", 100000, "GhiChu3"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán CHTL", "2024-10-31 08:00:00", "5", null, "4", 300000, "GhiChu4"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán tiền học", "2024-10-31 11:00:00", "3", null, "6", 2000000, "GhiChu5"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua hàng online", "2024-10-16 10:00:00", "2", null, "4", 250000, "GhiChu6"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nạp tiền di động", "2024-10-17 09:00:00", "5", null, "5", 50000, "GhiChu7"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nhận tiền", "2024-10-18 07:00:00", "3", "4", "3", 5000000, "GhiChu8"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán CHTL", "2024-10-15 08:00:00", "1", null, "4", 60000, "GhiChu9"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán tiền học", "2024-10-19 11:00:00", "5", null, "6", 3500000, "GhiChu10"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua hàng online", "2024-10-23 10:00:00", "4", null, "5", 100000, "GhiChu11"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nạp tiền di động", "2024-10-11 09:00:00", "1", null, "4", 40000, "GhiChu12"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nhận tiền", "2024-10-14 07:00:00", "2", "5", "2", 2000000, "GhiChu12"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán CHTL", "2024-10-25 08:00:00", "3", null, "2", 1700000, "GhiChu13"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán tiền học", "2024-10-12 11:00:00", "5", null, "4", 5800000, "GhiChu14"]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Mua hàng online", "2024-10-30 10:00:00", "4", null, "6", 100000, "GhiChu15"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nạp tiền di động", "2024-10-31 09:00:00", "2", null, "5", 20000, "GhiChu16"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Nhận tiền", "2024-10-31 07:00:00", "1", null, "2", 1000000, "GhiChu17"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán CHTL", "2024-10-31 08:00:00", "3", null, "5", 2000000, "GhiChu18"]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "TaiKhoanChuyenId", "TaiKhoanNhanId", "TheLoaiId", "TongTien", "GhiChu"],
                ["Thanh toán tiền học", "2024-10-31 11:00:00", "3", null, "6", 2000000, "GhiChu19"]);

            //migrationBuilder
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

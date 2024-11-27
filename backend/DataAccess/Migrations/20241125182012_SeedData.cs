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
            migrationBuilder.Sql("DELETE FROM chitietgiaodich", true);
            migrationBuilder.Sql("DELETE FROM loaitaikhoan", true);
            migrationBuilder.Sql("DELETE FROM theloai", true);
            migrationBuilder.Sql("DELETE FROM users", true);

            //// Seed data
            // users
            migrationBuilder.InsertData(table: "Users", columns: ["Name", "Username", "Password", "RememberMe"], values: new object[] { "User One", "user", BCrypt.Net.BCrypt.HashPassword("12345678"), false });
            migrationBuilder.InsertData(table: "Users", columns: ["Name", "Username", "Password", "RememberMe"], values: new object[] { "Admin One", "admin", BCrypt.Net.BCrypt.HashPassword("12345678"), false });

            //// loaitaikhoan
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten"], values: new object[] { "Tài khoản thanh toán" });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten"], values: new object[] { "Tài khoản tiết kiệm" });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten"], values: new object[] { "Thẻ tín dụng" });
            migrationBuilder.InsertData(table: "LoaiTaiKhoan", columns: ["Ten"], values: new object[] { "Đầu tư" });

            // theloai
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Chu cấp", "Thể loại chu cấp", "Thu" });
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Lương", "Thể loại lương", "Thu" });
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Cộng", "Thể loại cộng", "Thu" });

            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Ăn uống", "Thể loại ăn uống", "Chi" });
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Mua sắm", "Thể loại mua sắm", "Chi" });
            migrationBuilder.InsertData(table: "TheLoai",
                columns: ["TenTheLoai", "MoTa", "PhanLoai"],
                values: new object[] { "Giao dục", "Thể loại giáo dục", "Chi" });

            // taikhoan

            migrationBuilder.InsertData("TaiKhoan",
                ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"],
                ["VPBank", "1", 562000, "1"]);
            migrationBuilder.InsertData("TaiKhoan",
                ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"],
                ["TPBank", "1", 15000, "2"]);
            migrationBuilder.InsertData("TaiKhoan",
                ["TenTaiKhoan", "LoaiTaiKhoanId", "SoDu", "UserId"],
                ["Thẻ tín dụng VPBank", "3", 1000000, "1"]);

            // chitietgiaodich

            migrationBuilder.InsertData("ChiTietGiaoDich",
                ["TheLoaiId"],
                [5]);
            migrationBuilder.InsertData("ChiTietGiaoDich",
                ["TheLoaiId"],
                [5]);
            migrationBuilder.InsertData("ChiTietGiaoDich",
                ["TheLoaiId"],
                [1]);
            migrationBuilder.InsertData("ChiTietGiaoDich",
                ["TheLoaiId"],
                [4]);
            migrationBuilder.InsertData("ChiTietGiaoDich",
                ["TheLoaiId"],
                [6]);

            migrationBuilder.Sql("INSERT INTO ChiTietGiaoDichTaiKhoan (ChiTietGiaoDichId, TaiKhoanGiaoDichId) VALUES (1, 1)");
            migrationBuilder.Sql("INSERT INTO ChiTietGiaoDichTaiKhoan (ChiTietGiaoDichId, TaiKhoanGiaoDichId) VALUES (2, 1)");
            migrationBuilder.Sql("INSERT INTO ChiTietGiaoDichTaiKhoan (ChiTietGiaoDichId, TaiKhoanGiaoDichId) VALUES (3, 2)");
            migrationBuilder.Sql("INSERT INTO ChiTietGiaoDichTaiKhoan (ChiTietGiaoDichId, TaiKhoanGiaoDichId) VALUES (4, 1)");
            migrationBuilder.Sql("INSERT INTO ChiTietGiaoDichTaiKhoan (ChiTietGiaoDichId, TaiKhoanGiaoDichId) VALUES (5, 2)");

            // giaodich

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Mua hàng online", "2024-10-30 10:00:00", "Chi", 1, 200000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Nạp tiền di động", "2024-10-31 09:00:00", "Chi", 1, 30000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Nhận tiền", "2024-10-31 07:00:00", "Chi", 1, 200000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Thanh toán CHTL", "2024-10-31 08:00:00", "Chi", 1, 31000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Thanh toán tiền học", "2024-10-31 11:00:00", "Chi", 1, 2300000, ""]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Mua hàng online", "2024-10-16 10:00:00", "Chi", 1, 200000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Nạp tiền di động", "2024-10-17 09:00:00", "Chi", 1, 30000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Nhận tiền", "2024-10-18 07:00:00", "Chi", 1, 200000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Thanh toán CHTL", "2024-10-15 08:00:00", "Chi", 1, 31000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Thanh toán tiền học", "2024-10-19 11:00:00", "Chi", 1, 2300000, ""]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Mua hàng online", "2024-10-23 10:00:00", "Chi", 1, 200000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Nạp tiền di động", "2024-10-11 09:00:00", "Chi", 1, 30000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Nhận tiền", "2024-10-14 07:00:00", "Chi", 1, 200000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Thanh toán CHTL", "2024-10-25 08:00:00", "Chi", 1, 31000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Thanh toán tiền học", "2024-10-12 11:00:00", "Chi", 1, 2300000, ""]);

            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Mua hàng online", "2024-10-30 10:00:00", "Chi", 1, 200000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Nạp tiền di động", "2024-10-31 09:00:00", "Chi", 1, 30000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Nhận tiền", "2024-10-31 07:00:00", "Chi", 1, 200000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Thanh toán CHTL", "2024-10-31 08:00:00", "Chi", 1, 31000, ""]);
            migrationBuilder.InsertData("GiaoDich",
                ["TenGiaoDich", "NgayGiaoDich", "LoaiGiaoDich", "ChiTietGiaoDichId", "TongTien", "GhiChu"],
                ["Thanh toán tiền học", "2024-10-31 11:00:00", "Chi", 1, 2300000, ""]);

            //migrationBuilder
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LoaiTaiKhoan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ten = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiTaiKhoan", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TheLoai",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenTheLoai = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MoTa = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoai", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChiTietGiaoDich",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TheLoaiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGiaoDich", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietGiaoDich_TheLoai_TheLoaiId",
                        column: x => x.TheLoaiId,
                        principalTable: "TheLoai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GiaoDich",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenGiaoDich = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NgayGiaoDich = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LoaiGiaoDich = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChiTietGiaoDichId = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<double>(type: "double", nullable: false),
                    GhiChu = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoDich", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiaoDich_ChiTietGiaoDich_ChiTietGiaoDichId",
                        column: x => x.ChiTietGiaoDichId,
                        principalTable: "ChiTietGiaoDich",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenTaiKhoan = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoaiTaiKhoanId = table.Column<int>(type: "int", nullable: false),
                    SoDu = table.Column<double>(type: "double", nullable: false),
                    ChiTietGiaoDichId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaiKhoan_ChiTietGiaoDich_ChiTietGiaoDichId",
                        column: x => x.ChiTietGiaoDichId,
                        principalTable: "ChiTietGiaoDich",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaiKhoan_LoaiTaiKhoan_LoaiTaiKhoanId",
                        column: x => x.LoaiTaiKhoanId,
                        principalTable: "LoaiTaiKhoan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGiaoDich_TheLoaiId",
                table: "ChiTietGiaoDich",
                column: "TheLoaiId");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_ChiTietGiaoDichId",
                table: "GiaoDich",
                column: "ChiTietGiaoDichId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_ChiTietGiaoDichId",
                table: "TaiKhoan",
                column: "ChiTietGiaoDichId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_LoaiTaiKhoanId",
                table: "TaiKhoan",
                column: "LoaiTaiKhoanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaoDich");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "ChiTietGiaoDich");

            migrationBuilder.DropTable(
                name: "LoaiTaiKhoan");

            migrationBuilder.DropTable(
                name: "TheLoai");
        }
    }
}

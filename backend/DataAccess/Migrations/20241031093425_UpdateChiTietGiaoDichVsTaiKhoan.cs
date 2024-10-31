using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChiTietGiaoDichVsTaiKhoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaiKhoan_ChiTietGiaoDich_ChiTietGiaoDichId",
                table: "TaiKhoan");

            migrationBuilder.DropIndex(
                name: "IX_TaiKhoan_ChiTietGiaoDichId",
                table: "TaiKhoan");

            migrationBuilder.DropColumn(
                name: "ChiTietGiaoDichId",
                table: "TaiKhoan");

            migrationBuilder.CreateTable(
                name: "ChiTietGiaoDichTaiKhoan",
                columns: table => new
                {
                    ChiTietGiaoDichId = table.Column<int>(type: "int", nullable: false),
                    TaiKhoanGiaoDichId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGiaoDichTaiKhoan", x => new { x.ChiTietGiaoDichId, x.TaiKhoanGiaoDichId });
                    table.ForeignKey(
                        name: "FK_ChiTietGiaoDichTaiKhoan_ChiTietGiaoDich_ChiTietGiaoDichId",
                        column: x => x.ChiTietGiaoDichId,
                        principalTable: "ChiTietGiaoDich",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietGiaoDichTaiKhoan_TaiKhoan_TaiKhoanGiaoDichId",
                        column: x => x.TaiKhoanGiaoDichId,
                        principalTable: "TaiKhoan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGiaoDichTaiKhoan_TaiKhoanGiaoDichId",
                table: "ChiTietGiaoDichTaiKhoan",
                column: "TaiKhoanGiaoDichId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietGiaoDichTaiKhoan");

            migrationBuilder.AddColumn<int>(
                name: "ChiTietGiaoDichId",
                table: "TaiKhoan",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_ChiTietGiaoDichId",
                table: "TaiKhoan",
                column: "ChiTietGiaoDichId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaiKhoan_ChiTietGiaoDich_ChiTietGiaoDichId",
                table: "TaiKhoan",
                column: "ChiTietGiaoDichId",
                principalTable: "ChiTietGiaoDich",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_BELIY_API.Migrations
{
    public partial class TableImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    IDImage = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDPro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.IDImage);
                    table.ForeignKey(
                        name: "FK_Images_Products_IDPro",
                        column: x => x.IDPro,
                        principalTable: "Products",
                        principalColumn: "IDPro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_IDPro",
                table: "Images",
                column: "IDPro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}

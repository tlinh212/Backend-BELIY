using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_BELIY_API.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    IDCat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IDParent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.IDCat);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    IdCus = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.IdCus);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    IDSupp = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameSupp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.IDSupp);
                });

            migrationBuilder.CreateTable(
                name: "WareHouses",
                columns: table => new
                {
                    IDWare = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameWare = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouses", x => x.IDWare);
                });

            migrationBuilder.CreateTable(
                name: "ImportBills",
                columns: table => new
                {
                    IDImp = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDSupp = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDWare = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    TotalMoney = table.Column<double>(type: "float", nullable: false),
                    DateImport = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportBills", x => x.IDImp);
                    table.ForeignKey(
                        name: "FK_ImportBills_Suppliers_IDSupp",
                        column: x => x.IDSupp,
                        principalTable: "Suppliers",
                        principalColumn: "IDSupp",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportBills_WareHouses_IDWare",
                        column: x => x.IDWare,
                        principalTable: "WareHouses",
                        principalColumn: "IDWare",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    IDPro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NamePro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IDCat = table.Column<int>(type: "int", nullable: false),
                    IDImp = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    SaleRate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.IDPro);
                    table.ForeignKey(
                        name: "FK_Products_Categories_IDCat",
                        column: x => x.IDCat,
                        principalTable: "Categories",
                        principalColumn: "IDCat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ImportBills_IDImp",
                        column: x => x.IDImp,
                        principalTable: "ImportBills",
                        principalColumn: "IDImp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImportBills_IDSupp",
                table: "ImportBills",
                column: "IDSupp");

            migrationBuilder.CreateIndex(
                name: "IX_ImportBills_IDWare",
                table: "ImportBills",
                column: "IDWare");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IDCat",
                table: "Products",
                column: "IDCat");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IDImp",
                table: "Products",
                column: "IDImp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ImportBills");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "WareHouses");
        }
    }
}

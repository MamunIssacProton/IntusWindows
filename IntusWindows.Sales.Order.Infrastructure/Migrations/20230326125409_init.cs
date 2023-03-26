using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntusWindows.Sales.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dimensions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    height = table.Column<decimal>(type: "numeric", maxLength: 4, nullable: true),
                    width = table.Column<decimal>(type: "numeric", maxLength: 4, nullable: true),
                    title = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    orderName = table.Column<string>(type: "text", nullable: true),
                    state = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Windows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    windowName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    totalSubElements = table.Column<int>(type: "integer", maxLength: 4, nullable: false),
                    quantityOfWindows = table.Column<int>(type: "integer", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Windows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    elementName = table.Column<string>(type: "text", nullable: true),
                    dimensionId = table.Column<string>(type: "text", nullable: true),
                    elementType = table.Column<int>(type: "integer", nullable: false),
                    WindowId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Elements_Dimensions_dimensionId",
                        column: x => x.dimensionId,
                        principalTable: "Dimensions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Elements_Windows_WindowId",
                        column: x => x.WindowId,
                        principalTable: "Windows",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderWindow",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    WindowsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderWindow", x => new { x.OrderId, x.WindowsId });
                    table.ForeignKey(
                        name: "FK_OrderWindow_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderWindow_Windows_WindowsId",
                        column: x => x.WindowsId,
                        principalTable: "Windows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Elements_dimensionId",
                table: "Elements",
                column: "dimensionId");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_WindowId",
                table: "Elements",
                column: "WindowId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderWindow_WindowsId",
                table: "OrderWindow",
                column: "WindowsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Elements");

            migrationBuilder.DropTable(
                name: "OrderWindow");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Dimensions");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Windows");
        }
    }
}

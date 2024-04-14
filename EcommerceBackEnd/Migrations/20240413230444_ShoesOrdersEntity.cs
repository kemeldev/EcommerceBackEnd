using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class ShoesOrdersEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoesOrdersDBTable",
                columns: table => new
                {
                    ShoesId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoesOrdersDBTable", x => new { x.ShoesId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_ShoesOrdersDBTable_OrdersDBTable_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "OrdersDBTable",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShoesOrdersDBTable_ShoesDBTable_ShoesId",
                        column: x => x.ShoesId,
                        principalTable: "ShoesDBTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoesOrdersDBTable_OrdersId",
                table: "ShoesOrdersDBTable",
                column: "OrdersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoesOrdersDBTable");
        }
    }
}

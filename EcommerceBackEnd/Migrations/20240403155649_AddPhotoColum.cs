using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoColum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "ShoesDBTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "ShoesDBTable");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QrCodeGenerator.Migrations
{
    /// <inheritdoc />
    public partial class AddedQrCodeToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QrCode",
                table: "Contacts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QrCode",
                table: "Contacts");
        }
    }
}

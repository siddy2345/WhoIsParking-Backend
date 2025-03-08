using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.WhoIsParking.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangePLZToZip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PLZ",
                table: "House",
                newName: "Zip");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Zip",
                table: "House",
                newName: "PLZ");
        }
    }
}

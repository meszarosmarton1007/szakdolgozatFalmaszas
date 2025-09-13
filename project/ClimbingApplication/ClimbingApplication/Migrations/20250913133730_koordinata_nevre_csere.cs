using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApplication.Migrations
{
    /// <inheritdoc />
    public partial class koordinata_nevre_csere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "koordinata",
                table: "FalmaszoHelyek",
                newName: "nev");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nev",
                table: "FalmaszoHelyek",
                newName: "koordinata");
        }
    }
}

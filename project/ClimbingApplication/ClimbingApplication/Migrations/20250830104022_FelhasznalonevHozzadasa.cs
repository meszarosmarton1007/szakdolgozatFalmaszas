using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApplication.Migrations
{
    /// <inheritdoc />
    public partial class FelhasznalonevHozzadasa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "felhasznaloNev",
                table: "Felhasznalok",
                type: "TEXT",
                nullable: false,
                defaultValue: "alapUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "felhasznaloNev",
                table: "Felhasznalok");
        }
    }
}

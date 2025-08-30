using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApplication.Migrations
{
    /// <inheritdoc />
    public partial class felhasznalok_egyediseg_efcontextben : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Felhasznalok_felhasznaloNev",
                table: "Felhasznalok",
                column: "felhasznaloNev",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Felhasznalok_felhasznaloNev",
                table: "Felhasznalok");
        }
    }
}

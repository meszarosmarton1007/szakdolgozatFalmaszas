using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApplication.Migrations
{
    /// <inheritdoc />
    public partial class Db_frissitese_valaszokhoz_felhasznalok_hozzaadasa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FelhasznaloID",
                table: "Valaszok",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Valaszok_FelhasznaloID",
                table: "Valaszok",
                column: "FelhasznaloID");

            migrationBuilder.AddForeignKey(
                name: "FK_Valaszok_Felhasznalok_FelhasznaloID",
                table: "Valaszok",
                column: "FelhasznaloID",
                principalTable: "Felhasznalok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Valaszok_Felhasznalok_FelhasznaloID",
                table: "Valaszok");

            migrationBuilder.DropIndex(
                name: "IX_Valaszok_FelhasznaloID",
                table: "Valaszok");

            migrationBuilder.DropColumn(
                name: "FelhasznaloID",
                table: "Valaszok");
        }
    }
}

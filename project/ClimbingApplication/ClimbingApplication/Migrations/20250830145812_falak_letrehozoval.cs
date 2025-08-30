using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApplication.Migrations
{
    /// <inheritdoc />
    public partial class falak_letrehozoval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FelhasznaloID",
                table: "Falak",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Falak_FelhasznaloID",
                table: "Falak",
                column: "FelhasznaloID");

            migrationBuilder.AddForeignKey(
                name: "FK_Falak_Felhasznalok_FelhasznaloID",
                table: "Falak",
                column: "FelhasznaloID",
                principalTable: "Felhasznalok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Falak_Felhasznalok_FelhasznaloID",
                table: "Falak");

            migrationBuilder.DropIndex(
                name: "IX_Falak_FelhasznaloID",
                table: "Falak");

            migrationBuilder.DropColumn(
                name: "FelhasznaloID",
                table: "Falak");
        }
    }
}

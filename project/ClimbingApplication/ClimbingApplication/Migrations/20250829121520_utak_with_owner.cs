using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApplication.Migrations
{
    /// <inheritdoc />
    public partial class utak_with_owner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Valaszok_Utak_UtakID",
                table: "Valaszok");

            migrationBuilder.DropIndex(
                name: "IX_Valaszok_UtakID",
                table: "Valaszok");

            migrationBuilder.DropColumn(
                name: "UtakID",
                table: "Valaszok");

            migrationBuilder.AddColumn<int>(
                name: "FelhasznaloID",
                table: "Utak",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Utak_FelhasznaloID",
                table: "Utak",
                column: "FelhasznaloID");

            migrationBuilder.AddForeignKey(
                name: "FK_Utak_Felhasznalok_FelhasznaloID",
                table: "Utak",
                column: "FelhasznaloID",
                principalTable: "Felhasznalok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utak_Felhasznalok_FelhasznaloID",
                table: "Utak");

            migrationBuilder.DropIndex(
                name: "IX_Utak_FelhasznaloID",
                table: "Utak");

            migrationBuilder.DropColumn(
                name: "FelhasznaloID",
                table: "Utak");

            migrationBuilder.AddColumn<int>(
                name: "UtakID",
                table: "Valaszok",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Valaszok_UtakID",
                table: "Valaszok",
                column: "UtakID");

            migrationBuilder.AddForeignKey(
                name: "FK_Valaszok_Utak_UtakID",
                table: "Valaszok",
                column: "UtakID",
                principalTable: "Utak",
                principalColumn: "ID");
        }
    }
}

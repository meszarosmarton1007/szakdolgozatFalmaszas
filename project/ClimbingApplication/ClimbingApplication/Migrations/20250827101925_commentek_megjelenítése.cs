using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApplication.Migrations
{
    /// <inheritdoc />
    public partial class commentek_megjelenítése : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}

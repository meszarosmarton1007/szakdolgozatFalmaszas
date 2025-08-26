using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApplication.Migrations
{
    /// <inheritdoc />
    public partial class hozzaszolasUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FelhasznaloID",
                table: "Hozzaszolasok",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Hozzaszolasok_FelhasznaloID",
                table: "Hozzaszolasok",
                column: "FelhasznaloID");

            migrationBuilder.AddForeignKey(
                name: "FK_Hozzaszolasok_Felhasznalok_FelhasznaloID",
                table: "Hozzaszolasok",
                column: "FelhasznaloID",
                principalTable: "Felhasznalok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hozzaszolasok_Felhasznalok_FelhasznaloID",
                table: "Hozzaszolasok");

            migrationBuilder.DropIndex(
                name: "IX_Hozzaszolasok_FelhasznaloID",
                table: "Hozzaszolasok");

            migrationBuilder.DropColumn(
                name: "FelhasznaloID",
                table: "Hozzaszolasok");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApplication.Migrations
{
    /// <inheritdoc />
    public partial class EFContext_frissites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Falak_Felhasznalok_FelhasznaloID",
                table: "Falak");

            migrationBuilder.DropForeignKey(
                name: "FK_FalmaszoHelyek_Felhasznalok_FelhasznalokID",
                table: "FalmaszoHelyek");

            migrationBuilder.DropForeignKey(
                name: "FK_Hozzaszolasok_Felhasznalok_FelhasznaloID",
                table: "Hozzaszolasok");

            migrationBuilder.DropForeignKey(
                name: "FK_Utak_Felhasznalok_FelhasznaloID",
                table: "Utak");

            migrationBuilder.DropForeignKey(
                name: "FK_Valaszok_Felhasznalok_FelhasznaloID",
                table: "Valaszok");

            migrationBuilder.AddForeignKey(
                name: "FK_Falak_Felhasznalok_FelhasznaloID",
                table: "Falak",
                column: "FelhasznaloID",
                principalTable: "Felhasznalok",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FalmaszoHelyek_Felhasznalok_FelhasznalokID",
                table: "FalmaszoHelyek",
                column: "FelhasznalokID",
                principalTable: "Felhasznalok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hozzaszolasok_Felhasznalok_FelhasznaloID",
                table: "Hozzaszolasok",
                column: "FelhasznaloID",
                principalTable: "Felhasznalok",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Utak_Felhasznalok_FelhasznaloID",
                table: "Utak",
                column: "FelhasznaloID",
                principalTable: "Felhasznalok",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Valaszok_Felhasznalok_FelhasznaloID",
                table: "Valaszok",
                column: "FelhasznaloID",
                principalTable: "Felhasznalok",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Falak_Felhasznalok_FelhasznaloID",
                table: "Falak");

            migrationBuilder.DropForeignKey(
                name: "FK_FalmaszoHelyek_Felhasznalok_FelhasznalokID",
                table: "FalmaszoHelyek");

            migrationBuilder.DropForeignKey(
                name: "FK_Hozzaszolasok_Felhasznalok_FelhasznaloID",
                table: "Hozzaszolasok");

            migrationBuilder.DropForeignKey(
                name: "FK_Utak_Felhasznalok_FelhasznaloID",
                table: "Utak");

            migrationBuilder.DropForeignKey(
                name: "FK_Valaszok_Felhasznalok_FelhasznaloID",
                table: "Valaszok");

            migrationBuilder.AddForeignKey(
                name: "FK_Falak_Felhasznalok_FelhasznaloID",
                table: "Falak",
                column: "FelhasznaloID",
                principalTable: "Felhasznalok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FalmaszoHelyek_Felhasznalok_FelhasznalokID",
                table: "FalmaszoHelyek",
                column: "FelhasznalokID",
                principalTable: "Felhasznalok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hozzaszolasok_Felhasznalok_FelhasznaloID",
                table: "Hozzaszolasok",
                column: "FelhasznaloID",
                principalTable: "Felhasznalok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Utak_Felhasznalok_FelhasznaloID",
                table: "Utak",
                column: "FelhasznaloID",
                principalTable: "Felhasznalok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Valaszok_Felhasznalok_FelhasznaloID",
                table: "Valaszok",
                column: "FelhasznaloID",
                principalTable: "Felhasznalok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

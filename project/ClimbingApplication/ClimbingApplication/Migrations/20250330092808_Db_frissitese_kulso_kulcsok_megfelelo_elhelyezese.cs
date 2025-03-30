using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApplication.Migrations
{
    /// <inheritdoc />
    public partial class Db_frissitese_kulso_kulcsok_megfelelo_elhelyezese : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Falak_Utak_FalID",
                table: "Falak");

            migrationBuilder.DropForeignKey(
                name: "FK_FalmaszoHelyek_Falak_FalID",
                table: "FalmaszoHelyek");

            migrationBuilder.DropForeignKey(
                name: "FK_Felhasznalok_FalmaszoHelyek_HelyID",
                table: "Felhasznalok");

            migrationBuilder.DropForeignKey(
                name: "FK_Felhasznalok_Hozzaszolasok_HozzaszolasokID",
                table: "Felhasznalok");

            migrationBuilder.DropForeignKey(
                name: "FK_Felhasznalok_Hozzaszolasok_ValaszoloID",
                table: "Felhasznalok");

            migrationBuilder.DropForeignKey(
                name: "FK_Hozzaszolasok_Hozzaszolasok_ValaszID",
                table: "Hozzaszolasok");

            migrationBuilder.DropForeignKey(
                name: "FK_Utak_Hozzaszolasok_HozzaszolasokID",
                table: "Utak");

            migrationBuilder.DropIndex(
                name: "IX_Hozzaszolasok_ValaszID",
                table: "Hozzaszolasok");

            migrationBuilder.DropIndex(
                name: "IX_Felhasznalok_HelyID",
                table: "Felhasznalok");

            migrationBuilder.DropIndex(
                name: "IX_Felhasznalok_HozzaszolasokID",
                table: "Felhasznalok");

            migrationBuilder.DropIndex(
                name: "IX_Felhasznalok_ValaszoloID",
                table: "Felhasznalok");

            migrationBuilder.DropColumn(
                name: "ValaszID",
                table: "Hozzaszolasok");

            migrationBuilder.DropColumn(
                name: "HelyID",
                table: "Felhasznalok");

            migrationBuilder.DropColumn(
                name: "HozzaszolasokID",
                table: "Felhasznalok");

            migrationBuilder.DropColumn(
                name: "ValaszokID",
                table: "Felhasznalok");

            migrationBuilder.DropColumn(
                name: "ValaszoloID",
                table: "Felhasznalok");

            migrationBuilder.RenameColumn(
                name: "HozzaszolasokID",
                table: "Utak",
                newName: "FalID");

            migrationBuilder.RenameIndex(
                name: "IX_Utak_HozzaszolasokID",
                table: "Utak",
                newName: "IX_Utak_FalID");

            migrationBuilder.RenameColumn(
                name: "ValaszokID",
                table: "Hozzaszolasok",
                newName: "UtakID");

            migrationBuilder.RenameColumn(
                name: "FalID",
                table: "FalmaszoHelyek",
                newName: "FelhasznalokID");

            migrationBuilder.RenameIndex(
                name: "IX_FalmaszoHelyek_FalID",
                table: "FalmaszoHelyek",
                newName: "IX_FalmaszoHelyek_FelhasznalokID");

            migrationBuilder.RenameColumn(
                name: "FalID",
                table: "Falak",
                newName: "FalmaszohelyID");

            migrationBuilder.RenameIndex(
                name: "IX_Falak_FalID",
                table: "Falak",
                newName: "IX_Falak_FalmaszohelyID");

            migrationBuilder.AddColumn<int>(
                name: "HozzaszolasID",
                table: "Valaszok",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Valaszok_HozzaszolasID",
                table: "Valaszok",
                column: "HozzaszolasID");

            migrationBuilder.CreateIndex(
                name: "IX_Hozzaszolasok_UtakID",
                table: "Hozzaszolasok",
                column: "UtakID");

            migrationBuilder.AddForeignKey(
                name: "FK_Falak_FalmaszoHelyek_FalmaszohelyID",
                table: "Falak",
                column: "FalmaszohelyID",
                principalTable: "FalmaszoHelyek",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FalmaszoHelyek_Felhasznalok_FelhasznalokID",
                table: "FalmaszoHelyek",
                column: "FelhasznalokID",
                principalTable: "Felhasznalok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hozzaszolasok_Utak_UtakID",
                table: "Hozzaszolasok",
                column: "UtakID",
                principalTable: "Utak",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Utak_Falak_FalID",
                table: "Utak",
                column: "FalID",
                principalTable: "Falak",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Valaszok_Hozzaszolasok_HozzaszolasID",
                table: "Valaszok",
                column: "HozzaszolasID",
                principalTable: "Hozzaszolasok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Falak_FalmaszoHelyek_FalmaszohelyID",
                table: "Falak");

            migrationBuilder.DropForeignKey(
                name: "FK_FalmaszoHelyek_Felhasznalok_FelhasznalokID",
                table: "FalmaszoHelyek");

            migrationBuilder.DropForeignKey(
                name: "FK_Hozzaszolasok_Utak_UtakID",
                table: "Hozzaszolasok");

            migrationBuilder.DropForeignKey(
                name: "FK_Utak_Falak_FalID",
                table: "Utak");

            migrationBuilder.DropForeignKey(
                name: "FK_Valaszok_Hozzaszolasok_HozzaszolasID",
                table: "Valaszok");

            migrationBuilder.DropIndex(
                name: "IX_Valaszok_HozzaszolasID",
                table: "Valaszok");

            migrationBuilder.DropIndex(
                name: "IX_Hozzaszolasok_UtakID",
                table: "Hozzaszolasok");

            migrationBuilder.DropColumn(
                name: "HozzaszolasID",
                table: "Valaszok");

            migrationBuilder.RenameColumn(
                name: "FalID",
                table: "Utak",
                newName: "HozzaszolasokID");

            migrationBuilder.RenameIndex(
                name: "IX_Utak_FalID",
                table: "Utak",
                newName: "IX_Utak_HozzaszolasokID");

            migrationBuilder.RenameColumn(
                name: "UtakID",
                table: "Hozzaszolasok",
                newName: "ValaszokID");

            migrationBuilder.RenameColumn(
                name: "FelhasznalokID",
                table: "FalmaszoHelyek",
                newName: "FalID");

            migrationBuilder.RenameIndex(
                name: "IX_FalmaszoHelyek_FelhasznalokID",
                table: "FalmaszoHelyek",
                newName: "IX_FalmaszoHelyek_FalID");

            migrationBuilder.RenameColumn(
                name: "FalmaszohelyID",
                table: "Falak",
                newName: "FalID");

            migrationBuilder.RenameIndex(
                name: "IX_Falak_FalmaszohelyID",
                table: "Falak",
                newName: "IX_Falak_FalID");

            migrationBuilder.AddColumn<int>(
                name: "ValaszID",
                table: "Hozzaszolasok",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HelyID",
                table: "Felhasznalok",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HozzaszolasokID",
                table: "Felhasznalok",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValaszokID",
                table: "Felhasznalok",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValaszoloID",
                table: "Felhasznalok",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hozzaszolasok_ValaszID",
                table: "Hozzaszolasok",
                column: "ValaszID");

            migrationBuilder.CreateIndex(
                name: "IX_Felhasznalok_HelyID",
                table: "Felhasznalok",
                column: "HelyID");

            migrationBuilder.CreateIndex(
                name: "IX_Felhasznalok_HozzaszolasokID",
                table: "Felhasznalok",
                column: "HozzaszolasokID");

            migrationBuilder.CreateIndex(
                name: "IX_Felhasznalok_ValaszoloID",
                table: "Felhasznalok",
                column: "ValaszoloID");

            migrationBuilder.AddForeignKey(
                name: "FK_Falak_Utak_FalID",
                table: "Falak",
                column: "FalID",
                principalTable: "Utak",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FalmaszoHelyek_Falak_FalID",
                table: "FalmaszoHelyek",
                column: "FalID",
                principalTable: "Falak",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Felhasznalok_FalmaszoHelyek_HelyID",
                table: "Felhasznalok",
                column: "HelyID",
                principalTable: "FalmaszoHelyek",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Felhasznalok_Hozzaszolasok_HozzaszolasokID",
                table: "Felhasznalok",
                column: "HozzaszolasokID",
                principalTable: "Hozzaszolasok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Felhasznalok_Hozzaszolasok_ValaszoloID",
                table: "Felhasznalok",
                column: "ValaszoloID",
                principalTable: "Hozzaszolasok",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Hozzaszolasok_Hozzaszolasok_ValaszID",
                table: "Hozzaszolasok",
                column: "ValaszID",
                principalTable: "Hozzaszolasok",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Utak_Hozzaszolasok_HozzaszolasokID",
                table: "Utak",
                column: "HozzaszolasokID",
                principalTable: "Hozzaszolasok",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApplication.Migrations
{
    /// <inheritdoc />
    public partial class Db_letrehozas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hozzaszolasok",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    hozzaszolas = table.Column<string>(type: "TEXT", nullable: false),
                    ValaszokID = table.Column<int>(type: "INTEGER", nullable: false),
                    ValaszID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hozzaszolasok", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Hozzaszolasok_Hozzaszolasok_ValaszID",
                        column: x => x.ValaszID,
                        principalTable: "Hozzaszolasok",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Valaszok",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    valasz = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valaszok", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Utak",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    kep = table.Column<string>(type: "TEXT", nullable: false),
                    nehezseg = table.Column<string>(type: "TEXT", nullable: false),
                    nev = table.Column<string>(type: "TEXT", nullable: false),
                    leiras = table.Column<string>(type: "TEXT", nullable: false),
                    letrehozva = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    HozzaszolasokID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utak", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Utak_Hozzaszolasok_HozzaszolasokID",
                        column: x => x.HozzaszolasokID,
                        principalTable: "Hozzaszolasok",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Falak",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nev = table.Column<string>(type: "TEXT", nullable: false),
                    kep = table.Column<string>(type: "TEXT", nullable: false),
                    letrehozva = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    FalID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Falak", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Falak_Utak_FalID",
                        column: x => x.FalID,
                        principalTable: "Utak",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FalmaszoHelyek",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    orszag = table.Column<string>(type: "TEXT", nullable: false),
                    cim = table.Column<string>(type: "TEXT", nullable: false),
                    honlap = table.Column<string>(type: "TEXT", nullable: false),
                    koordinata = table.Column<string>(type: "TEXT", nullable: false),
                    leiras = table.Column<string>(type: "TEXT", nullable: false),
                    FalID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FalmaszoHelyek", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FalmaszoHelyek_Falak_FalID",
                        column: x => x.FalID,
                        principalTable: "Falak",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Felhasznalok",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    vezetekNev = table.Column<string>(type: "TEXT", nullable: false),
                    keresztNev = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    jelszo = table.Column<string>(type: "TEXT", nullable: false),
                    szuletesiIdo = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    telefonszam = table.Column<string>(type: "TEXT", nullable: false),
                    rang = table.Column<string>(type: "TEXT", nullable: false),
                    HelyID = table.Column<int>(type: "INTEGER", nullable: false),
                    HozzaszolasokID = table.Column<int>(type: "INTEGER", nullable: false),
                    ValaszokID = table.Column<int>(type: "INTEGER", nullable: false),
                    ValaszoloID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Felhasznalok", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Felhasznalok_FalmaszoHelyek_HelyID",
                        column: x => x.HelyID,
                        principalTable: "FalmaszoHelyek",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Felhasznalok_Hozzaszolasok_HozzaszolasokID",
                        column: x => x.HozzaszolasokID,
                        principalTable: "Hozzaszolasok",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Felhasznalok_Hozzaszolasok_ValaszoloID",
                        column: x => x.ValaszoloID,
                        principalTable: "Hozzaszolasok",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Falak_FalID",
                table: "Falak",
                column: "FalID");

            migrationBuilder.CreateIndex(
                name: "IX_FalmaszoHelyek_FalID",
                table: "FalmaszoHelyek",
                column: "FalID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Hozzaszolasok_ValaszID",
                table: "Hozzaszolasok",
                column: "ValaszID");

            migrationBuilder.CreateIndex(
                name: "IX_Utak_HozzaszolasokID",
                table: "Utak",
                column: "HozzaszolasokID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Felhasznalok");

            migrationBuilder.DropTable(
                name: "Valaszok");

            migrationBuilder.DropTable(
                name: "FalmaszoHelyek");

            migrationBuilder.DropTable(
                name: "Falak");

            migrationBuilder.DropTable(
                name: "Utak");

            migrationBuilder.DropTable(
                name: "Hozzaszolasok");
        }
    }
}

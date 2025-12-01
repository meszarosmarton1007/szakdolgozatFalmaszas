using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApplication.Migrations
{
    /// <inheritdoc />
    public partial class fal_utak_cascade_javitas2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Falak_FalmaszoHelyek_FalmaszohelyID",
                table: "Falak");

            migrationBuilder.DropForeignKey(
                name: "FK_Utak_Falak_FalID",
                table: "Utak");

            migrationBuilder.AddForeignKey(
                name: "FK_Falak_FalmaszoHelyek_FalmaszohelyID",
                table: "Falak",
                column: "FalmaszohelyID",
                principalTable: "FalmaszoHelyek",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Utak_Falak_FalID",
                table: "Utak",
                column: "FalID",
                principalTable: "Falak",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Falak_FalmaszoHelyek_FalmaszohelyID",
                table: "Falak");

            migrationBuilder.DropForeignKey(
                name: "FK_Utak_Falak_FalID",
                table: "Utak");

            migrationBuilder.AddForeignKey(
                name: "FK_Falak_FalmaszoHelyek_FalmaszohelyID",
                table: "Falak",
                column: "FalmaszohelyID",
                principalTable: "FalmaszoHelyek",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Utak_Falak_FalID",
                table: "Utak",
                column: "FalID",
                principalTable: "Falak",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

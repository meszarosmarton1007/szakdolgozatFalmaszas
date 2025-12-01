using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApplication.Migrations
{
    /// <inheritdoc />
    public partial class fal_utak_cascade_javitas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utak_Falak_FalID",
                table: "Utak");

            migrationBuilder.AddForeignKey(
                name: "FK_Utak_Falak_FalID",
                table: "Utak",
                column: "FalID",
                principalTable: "Falak",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utak_Falak_FalID",
                table: "Utak");

            migrationBuilder.AddForeignKey(
                name: "FK_Utak_Falak_FalID",
                table: "Utak",
                column: "FalID",
                principalTable: "Falak",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

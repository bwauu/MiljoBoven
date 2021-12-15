using Microsoft.EntityFrameworkCore.Migrations;

namespace MiljoBoven.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Samples_ErrandId",
                table: "Samples",
                column: "ErrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ErrandId",
                table: "Pictures",
                column: "ErrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Errands_ErrandId",
                table: "Pictures",
                column: "ErrandId",
                principalTable: "Errands",
                principalColumn: "ErrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Samples_Errands_ErrandId",
                table: "Samples",
                column: "ErrandId",
                principalTable: "Errands",
                principalColumn: "ErrandId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Errands_ErrandId",
                table: "Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Samples_Errands_ErrandId",
                table: "Samples");

            migrationBuilder.DropIndex(
                name: "IX_Samples_ErrandId",
                table: "Samples");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_ErrandId",
                table: "Pictures");
        }
    }
}

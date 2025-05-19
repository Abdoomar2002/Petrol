using Microsoft.EntityFrameworkCore.Migrations;

namespace Petrol.Migrations
{
    public partial class ProgramId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Training",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Training_ProgramId",
                table: "Training",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Training_Programs_ProgramId",
                table: "Training",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Training_Programs_ProgramId",
                table: "Training");

            migrationBuilder.DropIndex(
                name: "IX_Training_ProgramId",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Training");
        }
    }
}

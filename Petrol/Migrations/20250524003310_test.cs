using Microsoft.EntityFrameworkCore.Migrations;

namespace Petrol.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Training_ProgramTypes_ProgramTypeId",
                table: "Training");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramTypeId",
                table: "Training",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TrainingTypeId",
                table: "Training",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TrainingType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Training_TrainingTypeId",
                table: "Training",
                column: "TrainingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Training_ProgramTypes_ProgramTypeId",
                table: "Training",
                column: "ProgramTypeId",
                principalTable: "ProgramTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_TrainingType_TrainingTypeId",
                table: "Training",
                column: "TrainingTypeId",
                principalTable: "TrainingType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Training_ProgramTypes_ProgramTypeId",
                table: "Training");

            migrationBuilder.DropForeignKey(
                name: "FK_Training_TrainingType_TrainingTypeId",
                table: "Training");

            migrationBuilder.DropTable(
                name: "TrainingType");

            migrationBuilder.DropIndex(
                name: "IX_Training_TrainingTypeId",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "TrainingTypeId",
                table: "Training");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramTypeId",
                table: "Training",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_ProgramTypes_ProgramTypeId",
                table: "Training",
                column: "ProgramTypeId",
                principalTable: "ProgramTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

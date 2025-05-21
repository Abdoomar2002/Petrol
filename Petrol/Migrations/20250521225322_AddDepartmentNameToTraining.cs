using Microsoft.EntityFrameworkCore.Migrations;

namespace Petrol.Migrations
{
    public partial class AddDepartmentNameToTraining : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "Training",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "Training");
        }
    }
}

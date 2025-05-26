using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Petrol.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    ManagerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgramTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    Total = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    FinanceNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinanceNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    HireDate = table.Column<DateTime>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    RetireDate = table.Column<DateTime>(nullable: false),
                    EmplymentDate = table.Column<DateTime>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    CurrentJob = table.Column<string>(nullable: true),
                    Section = table.Column<string>(nullable: true),
                    SSN = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Religon = table.Column<string>(nullable: true),
                    AcademicQualification = table.Column<string>(nullable: true),
                    QualificationType = table.Column<string>(nullable: true),
                    JobType = table.Column<string>(nullable: true),
                    HasMaster = table.Column<string>(nullable: true),
                    JobStatus = table.Column<string>(nullable: true),
                    DepartmentName = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ProgramTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Programs_ProgramTypes_ProgramTypeId",
                        column: x => x.ProgramTypeId,
                        principalTable: "ProgramTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    PlaceId = table.Column<int>(nullable: false),
                    ProgramTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Training_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Training_ProgramTypes_ProgramTypeId",
                        column: x => x.ProgramTypeId,
                        principalTable: "ProgramTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTraining",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    TrainingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTraining", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeTraining_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTraining_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FollowingReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Men = table.Column<int>(nullable: false),
                    Women = table.Column<int>(nullable: false),
                    ProgramOrganizer = table.Column<string>(nullable: true),
                    ProgramCost = table.Column<double>(nullable: false),
                    FoodCost = table.Column<double>(nullable: false),
                    HotelCost = table.Column<double>(nullable: false),
                    TransitionsCost = table.Column<double>(nullable: false),
                    TicketsCost = table.Column<double>(nullable: false),
                    LastNightCost = table.Column<double>(nullable: false),
                    OthersCost = table.Column<double>(nullable: false),
                    TotalCost = table.Column<double>(nullable: false),
                    TrainingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowingReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowingReports_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentPresenceNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(nullable: false),
                    PresenceNumber = table.Column<int>(nullable: false),
                    FollowingReportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentPresenceNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentPresenceNumbers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentPresenceNumbers_FollowingReports_FollowingReportId",
                        column: x => x.FollowingReportId,
                        principalTable: "FollowingReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPresenceNumbers_DepartmentId",
                table: "DepartmentPresenceNumbers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPresenceNumbers_FollowingReportId",
                table: "DepartmentPresenceNumbers",
                column: "FollowingReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTraining_EmployeeId",
                table: "EmployeeTraining",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTraining_TrainingId",
                table: "EmployeeTraining",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowingReports_TrainingId",
                table: "FollowingReports",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ProgramTypeId",
                table: "Programs",
                column: "ProgramTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Training_PlaceId",
                table: "Training",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Training_ProgramTypeId",
                table: "Training",
                column: "ProgramTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentPresenceNumbers");

            migrationBuilder.DropTable(
                name: "EmployeeTraining");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FollowingReports");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "ProgramTypes");
        }
    }
}

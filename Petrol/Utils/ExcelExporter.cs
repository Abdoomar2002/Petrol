using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Petrol.Models;
using Petrol.Services;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Petrol.Utils
{
    public class ExcelExporter
    {
        private readonly TrainingService _trainingService;
        private readonly ProgramService _programService;
        private readonly ProgramTypeService _programTypeService;
        private readonly DepartmentService _departmentService;
        private readonly DepartmentPresenceNumberService _deptPresenceService;
        private readonly FollowingReportService _followingReportService;

        public ExcelExporter()
        {
            _trainingService = new TrainingService();
            _programService = new ProgramService();
            _programTypeService = new ProgramTypeService();
            _departmentService = new DepartmentService();
            _deptPresenceService = new DepartmentPresenceNumberService();
            _followingReportService = new FollowingReportService();

            // EPPlus licensing (required for versions 5.0 and above)
            ExcelPackage.License.SetNonCommercialPersonal("<Your Name>");
        }
        /*   public void GenerateManagementReport(DateTime startDate, DateTime endDate, string programType)
           {
               using (var package = new ExcelPackage())
               {
                   var worksheet = package.Workbook.Worksheets.Add("Management Report");
                   worksheet.Cells.Style.Font.Name = "Arial";
                   worksheet.Cells.Style.Font.Size = 12;

                   var Departments = _departmentService.GetAll<Department>().Select(x => x.Name).ToList();
                   worksheet.Cells[1, 1].Value = "البرنامج";
                   worksheet.Cells[1, 2].Value = "";
                   for (int i = 0; i < Departments.Count; i++)
                   {
                       worksheet.Cells[1, i + 3].Value = Departments[i];
                       worksheet.Cells[1, i + 3].Style.TextRotation = 90;
                   }
                   worksheet.Cells[1, Departments.Count + 3].Value = "الاجمالي";
                   worksheet.Cells[1, Departments.Count + 3].Style.TextRotation = 90;
                   // Get all departments for column headers
                   var reports = _followingReportService.GetAllWithNestedInclude(x => x.Include(y => y.Training).ThenInclude(t => t.ProgramType).Include(d => d.DepartmentsPresenceNumber).ThenInclude(r => r.Department)).Where(x => x.Training.From.Date >= startDate.Date && x.Training.To.Date <= endDate.Date).ToList();
                   if (programType != "كل الشركة"&&programType!="")
                   {

                       reports = reports.Where(x => x.Training.ProgramType.Type == programType).ToList();
                   }
                   if (reports.Count > 0)
                   {
                       int i = 2;

                       foreach (var report in reports)
                       {
                           worksheet.Cells[i, 1].Value = report.Training.Name;
                           worksheet.Cells[i, 2].Value = report.Training.ProgramType.Type;
                           for (int j = 0; j < Departments.Count; j++)
                           {
                               worksheet.Cells[i, j + 3].Value = report.DepartmentsPresenceNumber.Find(x => x.Department.Name == Departments[j]).PresenceNumber;
                           }
                           var total = report.Men + report.Women;
                           worksheet.Cells[i, Departments.Count+3].Value = total;
                           i++;
                       }
                   }


                   // Auto-fit columns
                   worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                   var SaveFileDialog = new SaveFileDialog();
                   SaveFileDialog.Filter = "Excel Sheet *|.xlsx";
                   var file = SaveFileDialog.ShowDialog();
                   if (file == DialogResult.OK)
                   {

                       File.WriteAllBytes(SaveFileDialog.FileName, package.GetAsByteArray());
                       Console.WriteLine($"Report generated successfully at: {SaveFileDialog.FileName}");
                   }
                   // Save the file
               }
           }*/
        public void GenerateManagementReport(DateTime startDate, DateTime endDate, string programType)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Management Report");
                worksheet.Cells.Style.Font.Name = "Arial";
                worksheet.Cells.Style.Font.Size = 12;

                var Departments = _departmentService.GetAll<Department>().Select(x => x.Name).ToList();
                worksheet.Cells[1, 1].Value = "البرنامج";
                worksheet.Cells[1, 2].Value = "";
                for (int i = 0; i < Departments.Count; i++)
                {
                    worksheet.Cells[1, i + 3].Value = Departments[i];
                    worksheet.Cells[1, i + 3].Style.TextRotation = 90;
                }
                worksheet.Cells[1, Departments.Count + 3].Value = "الاجمالي";
                worksheet.Cells[1, Departments.Count + 3].Style.TextRotation = 90;

                // Get all departments for column headers
                var reports = _followingReportService.GetAllWithNestedInclude(x => x.Include(y => y.Training).ThenInclude(t => t.ProgramType).Include(d => d.DepartmentsPresenceNumber).ThenInclude(r => r.Department))
                    .Where(x => x.Training.From.Date >= startDate.Date && x.Training.To.Date <= endDate.Date)
                    .ToList();

                if (programType != "كل الشركة" && programType != "")
                {
                    reports = reports.Where(x => x.Training.ProgramType.Type == programType).ToList();
                }

                if (reports.Count > 0)
                {
                    // Group reports by year and month
                    var groupedReports = reports
                        .GroupBy(r => new { Year = r.Training.From.Year, Month = r.Training.From.Month })
                        .OrderBy(g => g.Key.Year)
                        .ThenBy(g => g.Key.Month);

                    int row = 2; // Start from row 2 (after headers)

                    foreach (var monthGroup in groupedReports)
                    {
                        // Add month header
                        var monthYear = $"{monthGroup.Key.Year}/{monthGroup.Key.Month:D2}";
                        worksheet.Cells[row, 1].Value = monthYear;
                        worksheet.Cells[row, 1, row, Departments.Count + 3].Merge = true; // Merge cells for the month header
                        worksheet.Cells[row, 1].Style.Font.Bold = true;
                        worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        row++;

                        // Add training data for this month
                        foreach (var report in monthGroup)
                        {
                            worksheet.Cells[row, 1].Value = report.Training.Name;
                            worksheet.Cells[row, 2].Value = report.Training.ProgramType.Type;
                            for (int j = 0; j < Departments.Count; j++)
                            {
                                var presence = report.DepartmentsPresenceNumber.Find(x => x.Department.Name == Departments[j])?.PresenceNumber ?? 0;
                                worksheet.Cells[row, j + 3].Value = presence;
                            }
                            var total = report.Men + report.Women;
                            worksheet.Cells[row, Departments.Count + 3].Value = total;
                            row++;
                        }
                    }
                }

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var SaveFileDialog = new SaveFileDialog();
                SaveFileDialog.Filter = "Excel Sheet *|.xlsx";
                var file = SaveFileDialog.ShowDialog();
                if (file == DialogResult.OK)
                {
                    File.WriteAllBytes(SaveFileDialog.FileName, package.GetAsByteArray());
                    Console.WriteLine($"Report generated successfully at: {SaveFileDialog.FileName}");
                }
            }
        }
        public void GenerateFinanceReport(DateTime startDate, DateTime endDate, string programType)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Finance Report");
                worksheet.Cells.Style.Font.Name = "Arial";
                worksheet.Cells.Style.Font.Size = 12;

                // Row 1: Custom string (you can modify this later)
                // Define column headers (starting from row 2)
                var headers = new string[]
                {
                    "الرقم",
                    "اسم التدريب",
                    "إجمالي تكلفة البرنامج",
                    "تكلفة الطعام",
                    "تكاليف أخرى",
                    "التكلفة الإجمالية",
                    "عدد الأشخاص",
                    "الرجال",
                    "النساء",
                    "المدة",
                    "نوع البرنامج",
                    "اسم الإدارة",
                    "منظم البرنامج"
                };

                for (int col = 0; col < headers.Length; col++)
                {
                    worksheet.Cells[2, col + 1].Value = headers[col];
                    worksheet.Cells[2, col + 1].Style.Font.Bold = true;
                    worksheet.Cells[2, col + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[2, col + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    worksheet.Cells[2, col + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                // Fetch and filter reports
                var reports = _followingReportService.GetAllWithNestedInclude(x => x.Include(y => y.Training).ThenInclude(t => t.ProgramType))
                    .Where(x => x.Training.From.Date >= startDate.Date && x.Training.To.Date <= endDate.Date)
                    .ToList();

                if (!string.IsNullOrEmpty(programType) && programType != "كل الشركة")
                {
                    reports = reports.Where(x => x.Training.ProgramType.Type == programType).ToList();
                }

                if (reports.Count > 0)
                {
                    // Group reports by year and month
                    var groupedReports = reports
                        .GroupBy(r => new { Year = r.Training.From.Year, Month = r.Training.From.Month })
                        .OrderBy(g => g.Key.Year)
                        .ThenBy(g => g.Key.Month);

                    int row = 3; // Start from row 3 (after custom header and column headers)
                    int rowCounter = 1;

                    foreach (var monthGroup in groupedReports)
                    {
                        // Add month header
                        var monthYear = $"{monthGroup.Key.Year}/{monthGroup.Key.Month:D2}";
                        worksheet.Cells[row, 1].Value = monthYear;
                        worksheet.Cells[row, 1, row, headers.Length].Merge = true;
                        worksheet.Cells[row, 1].Style.Font.Bold = true;
                        worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        row++;

                        // Add training data for this month
                        foreach (var report in monthGroup)
                        {
                            var totalProgramCost = report.ProgramCost + report.HotelCost + report.LastNightCost + report.TransitionsCost + report.TicketsCost;
                            var totalPersons = report.Men + report.Women;
                            var dateRange = $"{report.Training.From.Date:yyyy/MM/dd} : {report.Training.To.Date:MM/dd}";

                            worksheet.Cells[row, 1].Value = rowCounter++;
                            worksheet.Cells[row, 2].Value = report.Training.Name;
                            worksheet.Cells[row, 3].Value = totalProgramCost;
                            worksheet.Cells[row, 4].Value = report.FoodCost;
                            worksheet.Cells[row, 5].Value = report.OthersCost;
                            worksheet.Cells[row, 6].Value = report.TotalCost;
                            worksheet.Cells[row, 7].Value = totalPersons;
                            worksheet.Cells[row, 8].Value = report.Men;
                            worksheet.Cells[row, 9].Value = report.Women;
                            worksheet.Cells[row, 10].Value = dateRange;
                            worksheet.Cells[row, 11].Value = report.Training.ProgramType.Type;
                            worksheet.Cells[row, 12].Value = report.Training.DepartmentName;
                            worksheet.Cells[row, 13].Value = report.ProgramOrganizer;

                            row++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("لا يوجد تدريبات مطابقة لعناصر البحث", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Sheet *|.xlsx";
                var result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    File.WriteAllBytes(saveFileDialog.FileName, package.GetAsByteArray());
                    Console.WriteLine($"Report generated successfully at: {saveFileDialog.FileName}");
                }
            }
        }
    }
}
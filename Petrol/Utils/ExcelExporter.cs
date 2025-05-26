using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Petrol.Models;
using Petrol.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
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

            ExcelPackage.License.SetNonCommercialPersonal("abcde");
        }

        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";
            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }

        public void GenerateManagementReport(DateTime startDate, DateTime endDate, string programType)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Management Report");
                worksheet.Cells.Style.Font.Name = "Arial";
                worksheet.Cells.Style.Font.Size = 12;
                var monthlyTotalRows = new List<int>();

                var Departments = _departmentService.GetAll<Department>().Select(x => x.Name).ToList();
                worksheet.Cells[1, 1].Value = "البرنامج";
                worksheet.Cells[1, 2].Value = "";
                for (int i = 0; i < Departments.Count; i++)
                {
                    worksheet.Cells[1, i + 3].Value = Departments[i];
                    worksheet.Cells[1, i + 3].Style.TextRotation = 90;
                    worksheet.Cells[1, i + 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 3].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }
                worksheet.Cells[1, Departments.Count + 3].Value = "الاجمالي";
                worksheet.Cells[1, Departments.Count + 3].Style.TextRotation = 90;
                worksheet.Cells[1, Departments.Count + 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells[1, Departments.Count + 3].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                var reports = _followingReportService.GetAllWithNestedInclude(x => x.Include(y => y.Training).ThenInclude(t => t.TrainingType).Include(r => r.Training).ThenInclude(y => y.Program).ThenInclude(o => o.ProgramType).Include(d => d.DepartmentsPresenceNumber).ThenInclude(r => r.Department)).Where(x => x.Training.From.Date >= startDate.Date && x.Training.To.Date <= endDate.Date).ToList();


                if (programType != "كل الأنواع" && !string.IsNullOrWhiteSpace(programType))
                {
                    reports = reports.Where(x => x.Training.Program.ProgramType.Type == programType).ToList();
                }

                if (reports.Count > 0)
                {
                    var groupedReports = reports
                        .GroupBy(r => new { r.Training.From.Year, r.Training.From.Month })
                        .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month);

                    int row = 2;
                    foreach (var monthGroup in groupedReports)
                    {
                        var monthYear = $"{monthGroup.Key.Year}/{monthGroup.Key.Month:D2}";
                        int monthStartRow = row + 1;

                        worksheet.Cells[row, 1].Value = monthYear;
                        worksheet.Cells[row, 1, row, Departments.Count + 3].Merge = true;
                        worksheet.Cells[row, 1].Style.Font.Bold = true;
                        worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[row, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(Color.LightYellow);
                        row++;

                        foreach (var report in monthGroup)
                        {
                            worksheet.Cells[row, 1].Value = report.Training.Name;
                            worksheet.Cells[row, 2].Value = report.Training.TrainingType.Name;

                            for (int j = 0; j < Departments.Count; j++)
                            {
                                var presence = report.DepartmentsPresenceNumber.FirstOrDefault(x => x.Department.Name == Departments[j])?.PresenceNumber ?? 0;
                                worksheet.Cells[row, j + 3].Value = presence;
                            }

                            var total = report.Men + report.Women;
                            worksheet.Cells[row, Departments.Count + 3].Value = total;
                            row++;
                        }

                        // Add monthly total row
                        worksheet.Cells[row, 1].Value = "إجمالي الشهر";
                        monthlyTotalRows.Add(row);
                        for (int j = 0; j < Departments.Count; j++)
                        {
                            var col = j + 3;
                            worksheet.Cells[row, col].Formula = $"SUM({GetExcelColumnName(col)}{monthStartRow}:{GetExcelColumnName(col)}{row - 1})";
                            worksheet.Cells[row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Color.LightSkyBlue);
                            worksheet.Cells[row, col].Style.Font.Bold = true;
                        }
                        worksheet.Cells[row, Departments.Count + 3].Formula = $"SUM({GetExcelColumnName(Departments.Count + 3)}{monthStartRow}:{GetExcelColumnName(Departments.Count + 3)}{row - 1})";
                        worksheet.Cells[row, Departments.Count + 3].Style.Fill.PatternType=ExcelFillStyle.Solid;
                        worksheet.Cells[row, Departments.Count + 3].Style.Fill.BackgroundColor.SetColor(Color.LightSkyBlue);
                        worksheet.Cells[row, Departments.Count + 3].Style.Font.Bold = true;
                        row++;
                    }

                    // Add grand total row
                    worksheet.Cells[row, 1].Value = "الإجمالي الكلي";
                    for (int j = 0; j < Departments.Count; j++)
                    {
                        var col = j + 3;
                        worksheet.Cells[row, col].Formula = $"SUM({string.Join(",", monthlyTotalRows.Select(r => GetExcelColumnName(col) + r))})";
                        worksheet.Cells[row, col].Style.Fill.PatternType=ExcelFillStyle.Solid;
                        worksheet.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        worksheet.Cells[row, col].Style.Font.Bold = true;
                    }
                    worksheet.Cells[row, Departments.Count+3].Formula = $"SUM({string.Join(",", monthlyTotalRows.Select(r => GetExcelColumnName(Departments.Count+3) + r))})";
                    worksheet.Cells[row, Departments.Count + 3].Style.Fill.PatternType=ExcelFillStyle.Solid;

                    worksheet.Cells[row, Departments.Count + 3].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                    worksheet.Cells[row, Departments.Count + 3].Style.Font.Bold = true;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var saveFileDialog = new SaveFileDialog { Filter = "Excel Sheet *|.xlsx" };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(saveFileDialog.FileName, package.GetAsByteArray());
                    Console.WriteLine($"Report generated successfully at: {saveFileDialog.FileName}");
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
                var monthlyTotalRows = new List<int>();

                var headers = new string[] { "الرقم", "اسم التدريب", "إجمالي تكلفة البرنامج", "تكلفة الطعام", "تكاليف أخرى", "التكلفة الإجمالية", "عدد الأشخاص", "الرجال", "النساء", "المدة", "نوع البرنامج", "اسم الإدارة", "منظم البرنامج" };

                for (int col = 0; col < headers.Length; col++)
                {
                    worksheet.Cells[1, col + 1].Value = headers[col];
                    worksheet.Cells[1, col + 1].Style.Font.Bold = true;
                    worksheet.Cells[1, col + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, col + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet.Cells[1, col + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                var reports = _followingReportService.GetAllWithNestedInclude(
                    x => x.Include(y => y.Training)
                    .ThenInclude(t => t.TrainingType)
                    .Include(y => y.Training)
                    .ThenInclude(z => z.Program)
                    .ThenInclude(u => u.ProgramType)).Where(x => x.Training.From.Date >= startDate.Date && x.Training.To.Date <= endDate.Date).ToList();

                if (!string.IsNullOrEmpty(programType) && programType != "كل الأنواع")
                {
                    reports = reports.Where(x => x.Training?.Program?.ProgramType?.Type == programType).ToList();
                }

                if (reports.Count > 0)
                {
                    var groupedReports = reports
                        .GroupBy(r => new { r.Training.From.Year, r.Training.From.Month })
                        .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month);

                    int row = 2;
                    int rowCounter = 1;

                    foreach (var monthGroup in groupedReports)
                    {
                        var monthYear = $"{monthGroup.Key.Year}/{monthGroup.Key.Month:D2}";
                        int monthStartRow = row + 1;

                        worksheet.Cells[row, 1].Value = monthYear;
                        worksheet.Cells[row, 1, row, headers.Length].Merge = true;
                        worksheet.Cells[row, 1].Style.Font.Bold = true;
                        worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[row, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(Color.LightYellow);
                        row++;

                        foreach (var report in monthGroup)
                        {
                            worksheet.Cells[row, 1].Value = rowCounter++;
                            worksheet.Cells[row, 2].Value = report.Training.Name;
                            worksheet.Cells[row, 3].Value = report.ProgramCost + report.HotelCost + report.LastNightCost + report.TransitionsCost + report.TicketsCost;
                            worksheet.Cells[row, 4].Value = report.FoodCost;
                            worksheet.Cells[row, 5].Value = report.OthersCost;
                            worksheet.Cells[row, 6].Value = report.TotalCost;
                            worksheet.Cells[row, 7].Value = report.Men + report.Women;
                            worksheet.Cells[row, 8].Value = report.Men;
                            worksheet.Cells[row, 9].Value = report.Women;
                            worksheet.Cells[row, 10].Value = $"{report.Training.From:yyyy/MM/dd} : {report.Training.To:MM/dd}";
                            worksheet.Cells[row, 11].Value = report.Training.TrainingType.Name;
                            worksheet.Cells[row, 12].Value = report.Training.DepartmentName;
                            worksheet.Cells[row, 13].Value = report.ProgramOrganizer;
                            row++;
                        }

                        // Monthly total row
                        worksheet.Cells[row, 1].Value = "إجمالي الشهر";
                        monthlyTotalRows.Add(row);
                        for (int col = 3; col <= 9; col++)
                        {
                            worksheet.Cells[row, col].Formula = $"SUM({GetExcelColumnName(col)}{monthStartRow}:{GetExcelColumnName(col)}{row - 1})";
                            worksheet.Cells[row, col].Style.Fill.PatternType=ExcelFillStyle.Solid;
                            worksheet.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Color.LightSkyBlue);
                            worksheet.Cells[row, col].Style.Font.Bold = true;
                        }
                        row++;
                    }

                    // Grand total row
                    worksheet.Cells[row, 1].Value = "الإجمالي الكلي";
                    for (int col = 3; col <= 9; col++)
                    {
                        worksheet.Cells[row, col].Formula = $"SUM({string.Join(",", monthlyTotalRows.Select(r => GetExcelColumnName(col) + r))})";
                        worksheet.Cells[row, col].Style.Fill.PatternType=ExcelFillStyle.Solid;
                        worksheet.Cells[row, col].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                        worksheet.Cells[row, col].Style.Font.Bold = true;
                    }
                }
                else
                {
                    UserMessages.Error("لا يوجد تدريبات مطابقة لعناصر البحث");
                    return;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var saveFileDialog = new SaveFileDialog { Filter = "Excel Sheet *|.xlsx" };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(saveFileDialog.FileName, package.GetAsByteArray());
                    Console.WriteLine($"Report generated successfully at: {saveFileDialog.FileName}");
                }
            }
        }
    }
}

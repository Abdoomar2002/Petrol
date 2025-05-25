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
    internal class ExcelReader
    {

        private readonly EmployeeService _employeeService;
        private readonly ProgramService _programService;
        private readonly PlaceService _placeService;
        private readonly TrainingService _trainingService;
        private readonly EmployeeTrainingService _employeeTrainingService;
        private readonly FollowingReportService _followingReportService;
        private readonly DepartmentPresenceNumberService _deptPresenceService;
        private readonly DepartmentService _departmentService = new DepartmentService();

        public ExcelReader()
        {
            _employeeService = new EmployeeService();
            _programService = new ProgramService();
            _placeService = new PlaceService();
            _trainingService = new TrainingService();
            _employeeTrainingService = new EmployeeTrainingService();
            _followingReportService = new FollowingReportService();
            _deptPresenceService = new DepartmentPresenceNumberService();
            _departmentService = new DepartmentService();

            // EPPlus licensing
            ExcelPackage.License.SetNonCommercialPersonal("<Your Name>");

        }

        public void ImportTrainingDataFromExcel(string filePath)
        {
            // Validate file existence
            if (!File.Exists(filePath))
            {
                UserMessages.Error("الملف غير موجود!");
                return;
            }

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // First sheet
                if (worksheet == null || worksheet.Dimension == null)
                {
                    UserMessages.Error("الملف فارغ أو غير صالح!");
                    return;
                }

                // Validate column headers
                var expectedHeaders = new[] { "Employee Id", "Program Name", "Training From Date", "Training To Date", "Place Name", "Program Id", "Place Id" };
                for (int col = 1; col <= expectedHeaders.Length; col++)
                {
                    if (worksheet.Cells[1, col].Text != expectedHeaders[col - 1])
                    {
                       UserMessages.Error($"العمود {col} يجب أن يكون: {expectedHeaders[col - 1]}");
                        return;
                    }
                }

                // Fetch existing data for validation
                var existingEmployees = _employeeService.GetAll<Employee>().ToDictionary(e => e.Id, e => e);
                var existingPrograms = _programService.GetAll<Models.Program>().ToList();
                var existingPlaces = _placeService.GetAll<Place>().ToList();
                var existingTrainings = _trainingService.GetAll<Training>().ToList();
                var existingEmployeeTrainings = _employeeTrainingService.GetAll<EmployeeTraining>().ToList();

                // Process each row starting from row 2 (after headers)
                int rowCount = worksheet.Dimension.Rows;
                for (int row = 2; row <= rowCount; row++)
                {
                    try
                    {
                        // Read row data
                        string employeeIdStr = worksheet.Cells[row, 1].Text;
                        string programName = worksheet.Cells[row, 2].Text;
                        string fromDateStr = worksheet.Cells[row, 3].Text;
                        string toDateStr = worksheet.Cells[row, 4].Text;
                        string placeName = worksheet.Cells[row, 5].Text;

                        // Validate required fields
                        if (string.IsNullOrWhiteSpace(employeeIdStr) || string.IsNullOrWhiteSpace(programName) ||
                            string.IsNullOrWhiteSpace(fromDateStr) || string.IsNullOrWhiteSpace(toDateStr) ||
                            string.IsNullOrWhiteSpace(placeName))
                        {
                            UserMessages.Error($"بيانات الصف {row} غير مكتملة!");
                            continue;
                        }

                        // Parse Employee Id
                        if (!int.TryParse(employeeIdStr, out int employeeId) || !existingEmployees.ContainsKey(employeeId))
                        {
                            UserMessages.Error($"رقم الموظف في الصف {row} غير صالح أو غير موجود!");
                            continue;
                        }

                        // Parse dates
                        if (!DateTime.TryParse(fromDateStr, out DateTime fromDate) || !DateTime.TryParse(toDateStr, out DateTime toDate))
                        {
                            UserMessages.Error($"تاريخ التدريب في الصف {row} غير صالح!");
                            continue;
                        }

                        if (fromDate > toDate)
                        {
                            UserMessages.Error($"تاريخ البداية يجب أن يكون قبل تاريخ النهاية في الصف {row}!");
                            continue;
                        }

                        // Step 1: Create or get Program (distinct by name)
                        var program = existingPrograms.FirstOrDefault(p => p.Name == programName);
                        if (program == null)
                        {
                            program = new Models.Program { Name = programName };
                            _programService.Add(program);
                            _programService.SaveChanges();
                            existingPrograms.Add(program); // Update local cache
                        }

                        // Step 2: Create or get Place (distinct by name)
                        var place = existingPlaces.FirstOrDefault(p => p.Name == placeName);
                        if (place == null)
                        {
                            place = new Place { Name = placeName }; // No additional info as per requirement
                            _placeService.Add(place);
                            _placeService.SaveChanges();
                            existingPlaces.Add(place); // Update local cache
                        }

                        // Step 3: Create or get Training (distinct by name and date range)
                        var training = existingTrainings.FirstOrDefault(t =>
                            t.Name == programName &&
                            t.From.Date == fromDate.Date &&
                            t.To.Date == toDate.Date);

                        if (training == null)
                        {
                            training = new Training
                            {
                                Name = programName,
                                ProgramId = program.Id,
                                PlaceId = place.Id,
                                From = fromDate,
                                To = toDate
                            };
                            _trainingService.Add(training);
                            _trainingService.SaveChanges();
                            existingTrainings.Add(training); // Update local cache
                        }

                        // Step 4: Link Employee to Training (no duplicates)
                        var employeeTrainingExists = existingEmployeeTrainings.Any(et =>
                            et.EmployeeId == employeeId &&
                            et.TrainingId == training.Id);

                        if (!employeeTrainingExists)
                        {
                            var employeeTraining = new EmployeeTraining
                            {
                                EmployeeId = employeeId,
                                TrainingId = training.Id
                            };
                            _employeeTrainingService.Add(employeeTraining);
                            _employeeTrainingService.SaveChanges();
                            existingEmployeeTrainings.Add(employeeTraining); // Update local cache
                        }
                        else
                        {
                            UserMessages.Warning($"الموظف {employeeId} قد أخذ التدريب '{programName}' من قبل في الصف {row}!");
                        }
                    }
                    catch (Exception ex)
                    {
                        UserMessages.Error($"خطأ أثناء معالجة الصف {row}: {ex.Message}");
                        continue;
                    }
                }

                UserMessages.Info("تم استيراد البيانات بنجاح!");
            }
        }
        public void ReImportReports(string[] filePaths)
        {
            if (filePaths == null || filePaths.Length == 0)
            {
                UserMessages.Error("يرجى اختيار ملفات التقارير!");
                return;
            }

            var existingTrainings = _trainingService.GetAll<Training>().ToList();
            var existingPrograms = _programService.GetAll<Models.Program>().ToList();
            var existingPlaces = _placeService.GetAll<Place>().ToList();
            var existingFollowingReports = _followingReportService.GetAllWithNestedInclude(x => x.Include(y => y.Training)).ToList();
            var existingDeptPresence = _deptPresenceService.GetAll<DepartmentPresenceNumber>().ToList();
            var existingEmployeeTrainings = _employeeTrainingService.GetAll<EmployeeTraining>().ToList();
            var existingEmployees = _employeeService.GetAll<Employee>().ToDictionary(e => e.Id, e => e);

            foreach (var filePath in filePaths)
            {
                if (!File.Exists(filePath))
                {
                    UserMessages.Error($"الملف {Path.GetFileName(filePath)} غير موجود!");
                    continue;
                }

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    if (worksheet == null || worksheet.Dimension == null)
                    {
                        UserMessages.Error($"الملف {Path.GetFileName(filePath)} فارغ أو غير صالح!");
                        continue;
                    }

                    // Determine report type based on worksheet name
                    bool isManagementReport = worksheet.Name == "Management Report";
                    bool isFinanceReport = worksheet.Name == "Finance Report";

                    if (!isManagementReport && !isFinanceReport)
                    {
                        UserMessages.Error($"الملف {Path.GetFileName(filePath)} ليس تقرير إدارة أو مالية صالح!");
                        continue;
                    }

                    int row = 3; // Start after custom header (row 1) and column headers (row 2)
                    while (!string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                    {
                        if (DateTime.TryParse(worksheet.Cells[row, 1].Text, out _)) // Skip month headers
                        {
                            row++;
                            continue;
                        }

                        try
                        {
                            // Read common data
                            string trainingName = worksheet.Cells[row, isFinanceReport ? 2 : 1].Text; // Adjust column based on report type
                            string fromDateStr = isFinanceReport ? worksheet.Cells[row, 10].Text.Split(':')[0].Trim() : ""; // Extract from date
                            string toDateStr = isFinanceReport ? worksheet.Cells[row, 10].Text.Split(':')[1].Trim() : ""; // Extract to date
                            if (!DateTime.TryParse(fromDateStr, out DateTime fromDate) || !DateTime.TryParse(toDateStr, out DateTime toDate))
                            {
                                row++;
                                continue; // Skip invalid date rows
                            }

                            // Find existing training
                            var existingTraining = existingTrainings.FirstOrDefault(t =>
                                t.Name == trainingName &&
                                t.From.Date == fromDate.Date &&
                                t.To.Date == toDate.Date);

                            if (existingTraining == null)
                            {
                                UserMessages.Warning($"التدريب '{trainingName}' من {fromDate:yyyy/MM/dd} إلى {toDate:MM/dd} غير موجود في الصف {row}!");
                                row++;
                                continue;
                            }

                            if (isFinanceReport)
                            {
                                // Handle Finance Report data
                                var totalProgramCost = double.Parse(worksheet.Cells[row, 3].Text);
                                var foodCost = double.Parse(worksheet.Cells[row, 4].Text);
                                var othersCost = double.Parse(worksheet.Cells[row, 5].Text);
                                var totalCost = double.Parse(worksheet.Cells[row, 6].Text);
                                int totalPersons = int.Parse(worksheet.Cells[row, 7].Text);
                                int men = int.Parse(worksheet.Cells[row, 8].Text);
                                int women = int.Parse(worksheet.Cells[row, 9].Text);
                                string programType = worksheet.Cells[row, 11].Text;
                                string departmentName = worksheet.Cells[row, 12].Text;
                                string organizer = worksheet.Cells[row, 13].Text;

                                var followingReport = existingFollowingReports.FirstOrDefault(fr => fr.TrainingId == existingTraining.Id);
                                if (followingReport == null)
                                {
                                    followingReport = new FollowingReport
                                    {
                                        TrainingId = existingTraining.Id,
                                        ProgramCost = totalProgramCost,
                                        FoodCost = foodCost,
                                        OthersCost = othersCost,
                                        TotalCost = totalCost,
                                        Men = men,
                                        Women = women,
                                        ProgramOrganizer = organizer
                                    };
                                    _followingReportService.Add(followingReport);
                                }
                                else
                                {
                                    followingReport.ProgramCost = totalProgramCost;
                                    followingReport.FoodCost = foodCost;
                                    followingReport.OthersCost = othersCost;
                                    followingReport.TotalCost = totalCost;
                                    followingReport.Men = men;
                                    followingReport.Women = women;
                                    followingReport.ProgramOrganizer = organizer;
                                    _followingReportService.Update(followingReport);
                                }
                                _followingReportService.SaveChanges();
                            }
                            else // Management Report
                            {
                                // Handle Management Report data (assuming department presence data)
                                var departments = _departmentService.GetAll<Department>().ToList();
                                for (int col = 4; col < worksheet.Dimension.Columns; col++) // Start after "البرنامج" and "نوع البرنامج"
                                {
                                    int presence = int.Parse(worksheet.Cells[row, col].Text ?? "0");
                                    if (presence > 0)
                                    {
                                        string deptName = departments[col - 4].Name; // Adjust index based on column position
                                        var deptPresence = existingDeptPresence.FirstOrDefault(dp =>
                                                                                       dp.Department.Name == deptName);

                                        if (deptPresence == null)
                                        {
                                            deptPresence = new DepartmentPresenceNumber
                                            {
                                               
                                                DepartmentId = departments.First(d => d.Name == deptName).Id,
                                                PresenceNumber = presence
                                            };
                                            _deptPresenceService.Add(deptPresence);
                                        }
                                        else
                                        {
                                            deptPresence.PresenceNumber = presence;
                                            _deptPresenceService.Update(deptPresence);
                                        }
                                    }
                                }
                                _deptPresenceService.SaveChanges();
                            }

                            row++;
                        }
                        catch (Exception ex)
                        {
                            UserMessages.Error($"خطأ أثناء معالجة الصف {row} في الملف {Path.GetFileName(filePath)}: {ex.Message}");
                            row++;
                            continue;
                        }
                    }
                }
            }

            UserMessages.Info("تم إعادة استيراد التقارير بنجاح!");
        }
    }
}


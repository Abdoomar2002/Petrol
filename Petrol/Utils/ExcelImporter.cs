
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Petrol.Data;
using Petrol.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace Petrol.Utils 
{
public class ExcelImporter
{
    private readonly AppDbContext dbContext;
        private List<Department> departments;

    public ExcelImporter(AppDbContext context)
    {
        dbContext = context;
            ExcelPackage.License.SetNonCommercialPersonal("abcde");
            departments = dbContext.Departments.ToList();

        }

        public void ImportAuto(string filePath, int sheetType)
    {
        switch (sheetType)
        {
            case 1: ImportFromSheet1(filePath); break;
            case 2: ImportFromSheet2(filePath); break;
            case 3: ImportFromSheet3(filePath); break;
            default: throw new ArgumentException("Invalid sheet type.");
        }
    }

    public void ImportFromSheet1(string filePath)
    {
        
        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                string financeNumber = worksheet.Cells[row, 1].Text.Trim();
                string name = worksheet.Cells[row, 2].Text.Trim();
                DateTime.TryParse(worksheet.Cells[row, 3].Text, out DateTime hireDate);
                DateTime.TryParse(worksheet.Cells[row, 4].Text, out DateTime birthDate);
                DateTime.TryParse(worksheet.Cells[row, 5].Text, out DateTime retireDate);
                string level = worksheet.Cells[row, 6].Text.Trim();
                string currentJob = worksheet.Cells[row, 7].Text.Trim();
                DateTime.TryParse(worksheet.Cells[row, 8].Text, out DateTime employmentDate);
                string departmentName = worksheet.Cells[row, 9].Text.Trim();
                string gender = worksheet.Cells[row, 10].Text.Trim();
                string academicQualification = worksheet.Cells[row, 11].Text.Trim();
                string qualificationType = worksheet.Cells[row, 12].Text.Trim();
                string religion = worksheet.Cells[row, 13].Text.Trim();
                string jobType = worksheet.Cells[row, 14].Text.Trim();

                var department = EnsureDepartmentExists(departmentName);
                var employee = new Employee
                {
                    FinanceNumber = financeNumber,
                    Name = name,
                    HireDate = hireDate,
                    BirthDate = birthDate,
                    RetireDate = retireDate,
                    Level = level,
                    CurrentJob = currentJob,
                    EmplymentDate = employmentDate,
                    DepartmentId = department?.Id ?? 0,
                    DepartmentName=departmentName,
                    Sex = gender,
                    AcademicQualification = academicQualification,
                    QualificationType = qualificationType,
                    Religon = religion,
                    JobType = jobType
                };
                dbContext.Employees.Add(employee);
            }
            dbContext.SaveChanges();
        }
    }

    public void ImportFromSheet2(string filePath)
    {
        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                string financeNumber = worksheet.Cells[row, 1].Text.Trim();
                if (dbContext.Employees.Any(e => e.FinanceNumber == financeNumber)) continue;

                string name = worksheet.Cells[row, 2].Text.Trim();
                DateTime.TryParse(worksheet.Cells[row, 3].Text, out DateTime hireDate);
                DateTime.TryParse(worksheet.Cells[row, 4].Text, out DateTime birthDate);
                string level = worksheet.Cells[row, 5].Text.Trim();
                string currentJob = worksheet.Cells[row, 6].Text.Trim();
                DateTime.TryParse(worksheet.Cells[row, 7].Text, out DateTime employmentDate);
                string departmentName = worksheet.Cells[row, 8].Text.Trim();
                string gender = worksheet.Cells[row, 9].Text.Trim();
                string academicQualification = worksheet.Cells[row, 10].Text.Trim();
                string qualificationType = worksheet.Cells[row, 11].Text.Trim();
                string religion = worksheet.Cells[row, 12].Text.Trim();
                string jobType = worksheet.Cells[row, 13].Text.Trim();

                var department = EnsureDepartmentExists(departmentName);
                var employee = new Employee
                {
                    FinanceNumber = financeNumber,
                    Name = name,
                    HireDate = hireDate,
                    BirthDate = birthDate,
                    Level = level,
                    CurrentJob = currentJob,
                    EmplymentDate = employmentDate,
                    DepartmentId = department?.Id ?? 0,
                    DepartmentName=departmentName,
                    Sex = gender,
                    AcademicQualification = academicQualification,
                    QualificationType = qualificationType,
                    Religon = religion,
                    JobType = jobType
                };
                dbContext.Employees.Add(employee);
            }
            dbContext.SaveChanges();
        }
    }

    public void ImportFromSheet3(string filePath)
    {
        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                string financeNumber = worksheet.Cells[row, 1].Text.Trim();
                if (dbContext.Employees.Any(e => e.FinanceNumber == financeNumber)) continue;

                string name = worksheet.Cells[row, 2].Text.Trim();
                DateTime.TryParse(worksheet.Cells[row, 3].Text, out DateTime hireDate);
                DateTime.TryParse(worksheet.Cells[row, 4].Text, out DateTime birthDate);
                string departmentName = worksheet.Cells[row, 5].Text.Trim();
                string currentJob = worksheet.Cells[row, 6].Text.Trim();
                DateTime.TryParse(worksheet.Cells[row, 7].Text, out DateTime employmentDate);
                string level = worksheet.Cells[row, 8].Text.Trim();
                string jobType = worksheet.Cells[row, 9].Text.Trim();
                string academicQualification = worksheet.Cells[row, 10].Text.Trim();

                var department = EnsureDepartmentExists(departmentName);
                var employee = new Employee
                {
                    FinanceNumber = financeNumber,
                    Name = name,
                    HireDate = hireDate,
                    BirthDate = birthDate,
                    DepartmentId = department?.Id ?? 0,
                    CurrentJob = currentJob,
                    EmplymentDate = employmentDate,
                    DepartmentName=departmentName,
                    Level = level,
                    JobType = jobType,
                    AcademicQualification = academicQualification
                };
                dbContext.Employees.Add(employee);
            }
            dbContext.SaveChanges();
        }
    }

    private Department EnsureDepartmentExists(string departmentName)
    {
        if (string.IsNullOrWhiteSpace(departmentName)) return null;
        var dept = dbContext.Departments.FirstOrDefault(d => d.Name == departmentName);
        if (dept == null)
        {
            dept = new Department { Name = departmentName };
            dbContext.Departments.Add(dept);
            dbContext.SaveChanges();
        }
        return dept;
    }
        public void ImportTrainingsWithEmployees(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // First sheet
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    string financeNumber = worksheet.Cells[row, 1].Text.Trim();
                    string programName = worksheet.Cells[row, 2].Text.Trim();
                    DateTime.TryParse(worksheet.Cells[row, 3].Text, out DateTime fromDate);
                    DateTime.TryParse(worksheet.Cells[row, 4].Text, out DateTime toDate);
                    string placeName = worksheet.Cells[row, 5].Text.Trim();

                    // Ensure Place
                    var place = dbContext.Places.FirstOrDefault(p => p.Name == placeName);
                    if (place == null && !string.IsNullOrWhiteSpace(placeName))
                    {
                        place = new Place { Name = placeName };
                        dbContext.Places.Add(place);
                        dbContext.SaveChanges();
                    }

                    // Ensure Program
                    var program = dbContext.Programs.FirstOrDefault(p => p.Name == programName);
                    var programType = dbContext.ProgramTypes.FirstOrDefault();
                    if (program == null && !string.IsNullOrWhiteSpace(programName))
                    {
                        program = new Models.Program { Name = programName ,ProgramTypeId=programType.Id};
                        dbContext.Programs.Add(program);
                        dbContext.SaveChanges();
                    }
                    if(place== null && !string.IsNullOrWhiteSpace(placeName))
                    {
                        place = new Place { Name = placeName };
                        dbContext.Places.Add(place);
                        dbContext.SaveChanges();
                    }
                    // Ensure Training
                    var training = dbContext.Training.FirstOrDefault(t =>
                        t.Name == programName &&
                        t.From == fromDate &&
                        t.To == toDate &&place!=null&&
                        t.PlaceId == place.Id);
                    var trainingType = dbContext.TrainingType.FirstOrDefault(x=>x.Id==4);

                    if (training == null)
                    {
                        training = new Training
                        {
                            Name = programName,
                            From = fromDate,
                            To = toDate,
                            PlaceId = place?.Id??0,
                            ProgramId = program.Id,
                            TrainingTypeId = trainingType.Id,
                        };
                        dbContext.Training.Add(training);
                        dbContext.SaveChanges();
                    }

                    // Find Employee
                    var employee = dbContext.Employees.FirstOrDefault(e => e.FinanceNumber == financeNumber);
                    if (employee == null) continue; // skip if employee doesn't exist

                    // Link Employee to Training
                    bool alreadyLinked = dbContext.EmployeeTraining
                        .Any(et => et.EmployeeId == employee.Id && et.TrainingId == training.Id);

                    if (!alreadyLinked)
                    {
                        var employeeTraining = new EmployeeTraining
                        {
                            EmployeeId = employee.Id,
                            TrainingId = training.Id
                        };
                        dbContext.EmployeeTraining.Add(employeeTraining);
                    }
                }

                dbContext.SaveChanges();
            }
        }
        public void ImportFinanceReport(string filePath, string programType,int sheetnumber)
        {
            var columnNameMappings = new Dictionary<string, List<string>>
        {
            { "ProgramName", new List<string> { "اسم البرنامج" } },
            { "ProgramCost", new List<string> { "رسوم", "الرسوم", "القيمة" } },
            { "FoodCost", new List<string> { "بريك"} },
            { "OtherCost", new List<string> { "أخري","أخرى","اخرى","اخري"} },
            { "TotalCost", new List<string> { "اجمالي", "إجمالي", "اجمالى","إجمالى","قيمة","قيمه" } },
            { "Women", new List<string> { "السيدات" } },
            { "Men", new List<string> { "الذكور" } },
            { "DateRange", new List<string> { "التاريخ" } },
            { "TrainingType", new List<string> { "نوع التدريب","نوع" } },
            { "Organizer", new List<string> { "الجهة المنفذة", "جهة التنفيذ" } },
            { "TotalNumber", new List<string> { "العدد" } },
            { "Others", new List<string> { "أخرى", "احمالى" } },
            { "Supervisor", new List<string> { "مشرف" } },
            { "Accommodation", new List<string> { "الإقامة" } },
            { "Notes", new List<string> { "ملاحظات", "ملاحضات" } },
            { "Permission", new List<string> { "اذون صرف" } },
            // Add more as needed
        };
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // First sheet
            var colIndices = new Dictionary<string, int>();
            for (int col = 1; col <= worksheet.Dimension.Columns; col++)
            {
                string header = worksheet.Cells[1, col].Text.Trim(); // Adjust row index if needed
                foreach (var mapping in columnNameMappings)
                {
                    if (mapping.Value.Any(name => header.Contains(name)))
                    {
                        colIndices[mapping.Key] = col;
                        break;
                    }
                }
            }
                int rowCount = worksheet.Dimension.Rows;

                string currentMonthHeader = "";
                int? month = null;
                int? year = null;

                for (int row = 2; row <= rowCount; row++)
                {
                 


                    // Parse data row
                    string trainingName = colIndices.ContainsKey("ProgramName") ? worksheet.Cells[row, colIndices["ProgramName"]].Text.Trim() : "";
                    double.TryParse(colIndices.ContainsKey("TotalCost") ? worksheet.Cells[row, colIndices["TotalCost"]].Text : "0", out double totalCost);
                    double.TryParse(colIndices.ContainsKey("ProgramCost") ? worksheet.Cells[row, colIndices["ProgramCost"]].Text : "0", out double programCost);
                    double.TryParse(colIndices.ContainsKey("FoodCost") ? worksheet.Cells[row, colIndices["FoodCost"]].Text : "0", out double foodCost);
                    double.TryParse(colIndices.ContainsKey("OtherCost") ? worksheet.Cells[row, colIndices["OtherCost"]].Text : "0", out double othersCost);
                    int.TryParse(colIndices.ContainsKey("Women") ? worksheet.Cells[row, colIndices["Women"]].Text : "0", out int women);
                    int.TryParse(colIndices.ContainsKey("Men") ? worksheet.Cells[row, colIndices["Men"]].Text : "0", out int men);
                    string dateRange = colIndices.ContainsKey("DateRange") ? worksheet.Cells[row, colIndices["DateRange"]].Text.Trim() : "";
                    string trainingTypeName = colIndices.ContainsKey("TrainingType") ? worksheet.Cells[row, colIndices["TrainingType"]].Text.Trim() : "";
                    string trainingOrganizer = colIndices.ContainsKey("Organizer") ? worksheet.Cells[row, colIndices["Organizer"]].Text.Trim() : "";
                    int.TryParse(colIndices.ContainsKey("TotalNumber") ? worksheet.Cells[row, colIndices["TotalNumber"]].Text : "0", out int totalNumber);

                    var toDate = new DateTime();
                    var fromDate = new DateTime();
                    if (dateRange.Contains(":"))
                    {
                        var parts = dateRange.Split(':');
                        if (parts.Length == 2)
                        {
                            DateTime.TryParse(parts[0].Trim(), out fromDate);
                            DateTime.TryParse(parts[1].Trim(), out toDate);
                            if (fromDate > toDate) fromDate = fromDate.AddYears(toDate.Year - fromDate.Year);
                        }
                    }
                    else
                    {
                        DateTime.TryParse(dateRange.Trim(), out fromDate);
                       
                    }
                    if (fromDate == new DateTime())
                        continue;
                    // Ensure training starts in current month/year
                    trainingName =Normalize(trainingName);
                            // Find matching training
                            var training = dbContext.Training.Include(z=>z.TrainingType).AsEnumerable().FirstOrDefault(t =>
                                checkNames(t.Name,trainingName)&&
                                t.From.Date == fromDate.Date );

                            if (training != null)
                            {
                                // Update TrainingType from Excel
                                if (training.TrainingType.Name != trainingTypeName)

                                {
                                    var type = dbContext.TrainingType.FirstOrDefault(x => x.Name == trainingName);
                                    if (type != null)
                                        training.TrainingType = type;
                                    else
                                    {
                                        type = new TrainingType() { Name = trainingTypeName };
                                        dbContext.Add(type);
                                        training.TrainingType = type;
                                    }

                                }

                                // Update Program.Type from parameter
                                var program = dbContext.Programs.Include(e=>e.ProgramType).FirstOrDefault(p => p.Id == training.ProgramId);
                                if (program != null)
                                {
                                    if (program.ProgramType.Type != programType)

                                      {
                                        var type = dbContext.ProgramTypes.FirstOrDefault(x => x.Type == programType);
                                        if (type != null)
                                            program.ProgramType = type;
                                        else
                                        {
                                            type = new ProgramType() { Type = programType };
                                            dbContext.Add(type);
                                            program.ProgramType = type;
                                        }

                                    }
                                }
                                
                                // Add following report
                                var report = new FollowingReport
                                {
                                    TrainingId = training.Id,
                                    ProgramCost = programCost,
                                    FoodCost = foodCost,
                                    OthersCost = othersCost,
                                    TotalCost = totalCost,
                                    Women = women,
                                    Men = men,
                                    ProgramOrganizer= trainingOrganizer,
                                };
                                dbContext.FollowingReports.Add(report);
                            }
                        }
                    
                

                dbContext.SaveChanges();
            }
        }
        public void ImportAdminReport(string filePath, string programType, int sheetnumber)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                string sheetName = filePath; // Or the file name, whichever is more reliable

                int startYear = 0;
                int endYear = 0;
                var yearMatch = Regex.Match(sheetName, @"(\d{4})\s*-\s*(\d{4})");
                if (yearMatch.Success)
                {
                    startYear = int.Parse(yearMatch.Groups[1].Value);
                    endYear = int.Parse(yearMatch.Groups[2].Value);
                }
               

                if(startYear>endYear)
                {
                    startYear = int.Parse(yearMatch.Groups[2].Value);
                    endYear = int.Parse(yearMatch.Groups[1].Value);
                }
                // Set the date range
                DateTime fromDate = new DateTime(startYear, 7, 1); // July 1st
                DateTime toDate = new DateTime(endYear, 6, 30);    // June 30th


                // Departments from DB
                var trainings = dbContext.Training.Include(x => x.TrainingType).Where(x => x.From.Date >= fromDate && x.To.Date <= toDate).ToList() ;

                // Build column mapping for header row
                var colIndices = new Dictionary<string, int>();
                for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                {
                    string header = worksheet.Cells[1, col].Text.Trim();
                    colIndices[Normalize(header)] = col;
                }

                for (int row = 2; row <= rowCount; row++)
                {


                    // Extract data columns
                    string trainingName = worksheet.Cells[row, 1].Text.Trim();
                   
                    trainingName = Normalize(trainingName);

                            var training =trainings
                                .FirstOrDefault(t => checkNames(t.Name, trainingName));

                            if (training != null)
                            {
                                var program = dbContext.Programs.Include(e => e.ProgramType).FirstOrDefault(p => p.Id == training.ProgramId);
                                if (program != null && program.ProgramType.Type != programType)
                                {
                                    var type = dbContext.ProgramTypes.FirstOrDefault(x => x.Type == programType);
                                    if (type == null)
                                    {
                                        type = new ProgramType { Type = programType };
                                        dbContext.Add(type);
                                    }
                                    program.ProgramType = type;
                                }

                                // Build DepartmentPresenceNumbers list
                                var departmentPresenceList = new List<DepartmentPresenceNumber>();
                                foreach (var dept in departments)
                                {
                                    string normalizedDeptName = Normalize(dept.Name);
                                    if (colIndices.TryGetValue(normalizedDeptName, out int colIdx))
                                    {
                                        int.TryParse(worksheet.Cells[row, colIdx].Text, out int presenceNumber);
                                        departmentPresenceList.Add(new DepartmentPresenceNumber
                                        {
                                            Department = dept,
                                            PresenceNumber = presenceNumber
                                        });
                                    }
                                    else
                                    {
                                        // Department column not found, set to zero
                                        departmentPresenceList.Add(new DepartmentPresenceNumber
                                        {
                                            Department = dept,
                                            PresenceNumber = 0
                                        });
                                    }
                                }

                                // locate for the following report that have same training Id then update its value
                                var followingReport = dbContext.FollowingReports.FirstOrDefault(x => x.TrainingId == training.Id);
                                if(followingReport!=null)
                                {
                                    followingReport.DepartmentsPresenceNumber = departmentPresenceList;
                                    
                                    dbContext.FollowingReports.Update(followingReport);
                                }
                               
                            }
                        }
                    
                

                dbContext.SaveChanges();
            }
        }

        public string Normalize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            StringBuilder normalizedText = new StringBuilder(input);

            // Normalize variations of "ا"
            normalizedText.Replace("أ", "ا")
                         .Replace("إ", "ا")
                         .Replace("آ", "ا")
                         .Replace("ى", "ي")
                         .Replace("ئ", "ي")
                         .Replace("ؤ", "و")
                         .Replace("ة", "ه");

            // Normalize variations of "ي"
            normalizedText.Replace("ى", "ي")
                         .Replace("ئ", "ي");

            // Normalize variations of "و"
            normalizedText.Replace("ؤ", "و");

            // Normalize variations of "ه"
            normalizedText.Replace("ة", "ه");

            // Add more replacements as needed
            // Example: Normalize variations of "ك" and "ک" (if needed)
            normalizedText.Replace("ک", "ك");

            return normalizedText.ToString();
        }
        public bool checkNames(string t1,string t2) 
        {
          return  Normalize(t1).Contains(t2);
        }

        }

}
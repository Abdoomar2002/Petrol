using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Programs
{
    public partial class AddTraining : UserControl
    {
        private TrainingService service;
        private ProgramService programService;
        private Models.Program EditedProgram;
        private EmployeeService employeeService;
        private Department ActiveDepartment;
        private List<Employee> Employees;
        public AddTraining()
        {
            InitializeComponent();
            service = new TrainingService();
            programService = new ProgramService();
            employeeService = new EmployeeService();
        }
        public void SetProgramId(int id)
        {
            EditedProgram = programService.GetById<Models.Program>(id);
            var lastId = service.GetTheLastId<Training>();
            CodeTxt.Text= lastId.ToString();
            var departments = new DepartmentService().GetAll<Department>();
            DepartmentBox.Items.Clear();
            DepartmentBox.Items.Add("كل الشركة");
            DepartmentBox.Items.AddRange(departments.Select(x => x.Name).ToArray());
            DepartmentBox.SelectedIndex = -1;
            var Places = new PlaceService().GetAll<Place>();
            PlaceTxt.AutoCompleteCustomSource.AddRange(Places.Select(x => x.Name).ToArray());
            Employees = employeeService.GetAll<Employee>().ToList();
            EmployeeNameTxt.AutoCompleteCustomSource.AddRange(Employees.Select(x => x.Name).ToArray());
            EmployeeFinanceNumberTxt.AutoCompleteCustomSource.AddRange(Employees.Select(x => x.FinanceNumber).ToArray());
            var trainingTypes = new ProgramTypeService().GetAll<TrainingType>().Select(x=>x.Name).ToArray();
            TrainingTypeTxt.AutoCompleteCustomSource.AddRange(trainingTypes);

        }
        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Edit", EditedProgram.Id);
        }

        private void AddEmployeeBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EmployeeNameTxt.Text.Trim()) || string.IsNullOrEmpty(EmployeeFinanceNumberTxt.Text.Trim()))
            {
                UserMessages.Error("من فضلك املئ كل الخانات الفارغة");
                return;
            }
            var employee = employeeService.GetAll<Employee>().FirstOrDefault(x => x.FinanceNumber == EmployeeFinanceNumberTxt.Text.Trim());
            if (employee == null)
            {
                UserMessages.Error("هذا الموظف غير موجود");
                return;
            }
            if (EmployeeData.Rows.Cast<DataGridViewRow>().Any(x => x.Cells[1].Value?.ToString() == employee.FinanceNumber))
            {
                UserMessages.Error("هذا الموظف موجود بالفعل");
                return;
            }
            EmployeeData.Rows.Add(EmployeeData.Rows.Count + 1, employee.FinanceNumber, employee.Name, employee.DepartmentName, employee.RetireDate);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TrainingNameTxt.Text.Trim()) ||
                string.IsNullOrEmpty(PlaceTxt.Text.Trim()) ||
                string.IsNullOrEmpty(StartDate.Value.ToString()) ||
                string.IsNullOrEmpty(EndDate.Value.ToString()) ||
                DepartmentBox.SelectedIndex == -1 ||
                string.IsNullOrEmpty(TrainingTypeTxt.Text.Trim()))
            {
                UserMessages.Error("من فضلك املئ كل الخانات الفارغة الخاصة ببيانات التدريب");
                return;
            }
            try
            {

                var Place = new PlaceService().Find<Place>(x => x.Name == PlaceTxt.Text.Trim()).FirstOrDefault();
                if (Place == null)
                {
                    UserMessages.Error("هذا المكان غير موجود");
                    return;
                }
                bool f = false;
                var department = new DepartmentService().FindDepartmentByName(DepartmentBox.Text.Trim());
                if (department == null)
                {
                    if (DepartmentBox.Text== "كل الشركة")
                    {
                        f = true;
                    }
                    else
                    {


                        UserMessages.Error("هذه الادارة غير موجوده");
                        return;
                    }
                }
                if (StartDate.Value.Date > EndDate.Value.Date)
                {
                    UserMessages.Error("تاريخ البداية يجب ان يكون قبل تاريخ النهاية");
                    return;
                }
                var typeSevice = new ProgramTypeService();

                var trainingType = new ProgramTypeService().GetAll<TrainingType>().FirstOrDefault(x => x.Name == TrainingTypeTxt.Text.Trim());
                if (trainingType == null) 
                {
                    trainingType = new TrainingType() { Name = TrainingTypeTxt.Text };
                    typeSevice.Add(trainingType);
                    typeSevice.SaveChanges();
                }
                var training = new Training
                {
                    Name = TrainingNameTxt.Text.Trim(),
                    PlaceId = Place.Id,
                    ProgramId = EditedProgram.Id,
                    From = StartDate.Value,
                    To = EndDate.Value,
                    TrainingTypeId = trainingType.Id,
                    DepartmentName = f ? "كل الشركة" : department.Name,

                };
                typeSevice.Attach(trainingType);
                service.Add(training);
                service.SaveChanges();

                foreach (DataGridViewRow row in EmployeeData.Rows)
                {
                    var employeeFinanceNumber = row.Cells[1].Value?.ToString() ?? "";
                    var employee = employeeService.GetAll<Employee>().FirstOrDefault(x => x.FinanceNumber == employeeFinanceNumber);
                    if (employee != null)
                    {

                        var employeeTraining = new EmployeeTraining
                        {
                            TrainingId = training.Id,
                            EmployeeId = employee.Id,
                        };
                        service.Add(employeeTraining);
                        service.SaveChanges();
                    }
                }
                UserMessages.Info("تمت الاضافة بنجاح");
            }
            catch (Exception ex)
            {
                UserMessages.Error(ex.Message);
            }
        }

        private void EmployeeNameTxt_TextChanged(object sender, EventArgs e)
        {
            var employee = employeeService.GetAll<Employee>().FirstOrDefault(x => x.Name == EmployeeNameTxt.Text.Trim());
            if (employee != null)
            {
                EmployeeFinanceNumberTxt.Text= employee.FinanceNumber;
                EmployeeDepartmentTxt.Text= employee.DepartmentName;
                RemainTxt.Text= ConvertDateToSentence(employee.RetireDate);
            }
            else
            {
                EmployeeFinanceNumberTxt.Text= string.Empty;
                EmployeeDepartmentTxt.Text= string.Empty;
                RemainTxt.Text= string.Empty;
            }
        }

        private void EmployeeFinanceNumberTxt_TextChanged(object sender, EventArgs e)
        {
            var employee = employeeService.GetAll<Employee>().FirstOrDefault(x => x.FinanceNumber == EmployeeFinanceNumberTxt.Text.Trim());
            if (employee != null)
            {
                EmployeeNameTxt.Text= employee.Name;
                EmployeeDepartmentTxt.Text= employee.DepartmentName;
                RemainTxt.Text= ConvertDateToSentence(employee.RetireDate);
            }
            else
            {
                EmployeeNameTxt.Text= string.Empty;
                EmployeeDepartmentTxt.Text= string.Empty;
                RemainTxt.Text= string.Empty;
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            EmployeeData.Rows.Clear();
            EmployeeNameTxt.Text= string.Empty;
            EmployeeFinanceNumberTxt.Text= string.Empty;
            EmployeeDepartmentTxt.Text= string.Empty;
            DepartmentBox.SelectedIndex = -1;
            RemainTxt.Text= string.Empty;
            CodeTxt.Text= "";
            TrainingNameTxt.Text= string.Empty;
            PlaceTxt.Text= string.Empty;
            StartDate.Value = DateTime.Now;
            EndDate.Value = DateTime.Now;
            TrainingTypeTxt.Text =string.Empty;
            SetProgramId(EditedProgram.Id);

        }

        private void EmployeeData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 5)
            {
                EmployeeData.Rows.RemoveAt(e.RowIndex);
                for (int i = 0; i < EmployeeData.Rows.Count; i++)
                {
                    EmployeeData.Rows[i].Cells[0].Value = i + 1;
                }
            }
        }
        private string ConvertDateToSentence(DateTime date)
        {
            date = date.AddYears(DateTime.Now.Year * -1);
            date = date.AddMonths(DateTime.Now.Month * -1);
            date = date.AddDays(DateTime.Now.Day * -1);
            string FormatNumber(int number, string singular, string dual, string plural, string accusative)
            {
                if (number == 1)
                    return $"1 {singular}";
                else if (number == 2)
                    return $"2 {dual}";
                else if (number >= 3 && number <= 10)
                    return $"{number} {plural}";
                else
                    return $"{number} {accusative}";
            }

            var day = FormatNumber(date.Day, "يوم", "يومان", "أيام", "يومًا");
            var month = FormatNumber(date.Month, "شهر", "شهران", "أشهر", "شهرًا");
            var year = FormatNumber(date.Year, "سنة", "سنتان", "سنوات", "سنةً");

            return $"المدة: {year}، {month}، {day}";
        }

        private void DepartmentBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DepartmentBox.SelectedIndex > 1)
            {
                EmployeeNameTxt.AutoCompleteCustomSource.Clear();
                EmployeeFinanceNumberTxt.AutoCompleteCustomSource.Clear();
                EmployeeNameTxt.AutoCompleteCustomSource.AddRange(Employees.Where(x => x.DepartmentName == DepartmentBox.Text.Trim()).Select(x => x.Name).ToArray());
                EmployeeFinanceNumberTxt.AutoCompleteCustomSource.AddRange(Employees.Where(x => x.DepartmentName == DepartmentBox.Text.Trim()).Select(x => x.FinanceNumber).ToArray());

            }
            else
            {
                EmployeeNameTxt.AutoCompleteCustomSource.Clear();
                EmployeeFinanceNumberTxt.AutoCompleteCustomSource.Clear();
                EmployeeNameTxt.AutoCompleteCustomSource.AddRange(Employees.Select(x => x.Name).ToArray());
                EmployeeFinanceNumberTxt.AutoCompleteCustomSource.AddRange(Employees.Select(x => x.FinanceNumber).ToArray());
            }
        }
    }
}

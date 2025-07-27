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
        private bool isProgramming = false;
        public AddTraining()
        {
            InitializeComponent();
            service = new TrainingService();
            programService = new ProgramService();
            employeeService = new EmployeeService();
        }
        public void SetProgramId(int id)
        {
            ClearInputs(false);
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
            EmployeeNameTxt.Text = string.Empty;
            EmployeeFinanceNumberTxt.Focus();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TrainingNameTxt.Text.Trim()) ||
                string.IsNullOrEmpty(PlaceTxt.Text.Trim()) ||
                string.IsNullOrEmpty(StartDate.Text.Trim()) ||
                string.IsNullOrEmpty(EndDate.Text.Trim()) ||
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
                if (!DateValidator.IsValidDate(StartDate.Text))
                {
                    UserMessages.Error("يرجى إدخال تاريخ البداية بالصيغة الصحيحة dd/MM/yyyy");
                    StartDate.Focus();
                    return;
                }

                if (!DateValidator.IsValidDate(EndDate.Text))
                {
                    UserMessages.Error("يرجى إدخال تاريخ النهاية بالصيغة الصحيحة dd/MM/yyyy");
                    EndDate.Focus();
                    return;
                }

                var startDate = DateValidator.ParseDate(StartDate.Text).Value.Date;
                var endDate = DateValidator.ParseDate(EndDate.Text).Value.Date;
                if (startDate > endDate)
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
                                    From = startDate,
                To = endDate,
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
            if (!isProgramming) 
            {
            var employee = employeeService.GetAll<Employee>().FirstOrDefault(x => x.Name == EmployeeNameTxt.Text.Trim());
            if (employee != null)
            {
                    isProgramming = true;
                EmployeeFinanceNumberTxt.Text= employee.FinanceNumber;
                EmployeeDepartmentTxt.Text= employee.DepartmentName;
                RemainTxt.Text= ConvertDateToSentence(employee?.RetireDate??new DateTime());
                    isProgramming = false;
                }
            else
            {
                    isProgramming = true;
                EmployeeFinanceNumberTxt.Text= string.Empty;
                EmployeeDepartmentTxt.Text= string.Empty;
                RemainTxt.Text= string.Empty;
                    isProgramming=false;
                }
            }
        }

        private void EmployeeFinanceNumberTxt_TextChanged(object sender, EventArgs e)
        {
            if(!isProgramming)
            {

            var employee = employeeService.GetAll<Employee>().FirstOrDefault(x => x.FinanceNumber == EmployeeFinanceNumberTxt.Text.Trim());
            if (employee != null)
            {
                    isProgramming = true;
                EmployeeNameTxt.Text= employee.Name;
                EmployeeDepartmentTxt.Text= employee.DepartmentName;
                RemainTxt.Text= ConvertDateToSentence(employee?.RetireDate??new DateTime());
                    isProgramming = false;
            }
            else
            {
                    isProgramming = true;
                EmployeeNameTxt.Text= string.Empty;
                EmployeeDepartmentTxt.Text= string.Empty;
                RemainTxt.Text= string.Empty;
                    isProgramming = false;
            }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            ClearInputs(true);

        }
        private void ClearInputs(bool Clicked)
        {
            EmployeeData.Rows.Clear();
            EmployeeNameTxt.Text = string.Empty;
            EmployeeFinanceNumberTxt.Text = string.Empty;
            EmployeeDepartmentTxt.Text = string.Empty;
            DepartmentBox.SelectedIndex = -1;
            RemainTxt.Text = string.Empty;
            CodeTxt.Text = "";
            TrainingNameTxt.Text = string.Empty;
            PlaceTxt.Text = string.Empty;
            StartDate.Text = DateValidator.FormatDate(DateTime.Now);
            EndDate.Text = DateValidator.FormatDate(DateTime.Now);
            TrainingTypeTxt.Text = string.Empty;
           if(Clicked)
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
            if (date == DateTime.MinValue || date == DateTime.MaxValue)
            {
                return "لا يوجد تاريخ تقاعد";
            }
            var timestamp=date-DateTime.Now;

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

            var day = FormatNumber(((int) timestamp.TotalDays%365)%30, "يوم", "يومان", "أيام", "يومًا");
            var month = FormatNumber((int)timestamp.TotalDays%365/30, "شهر", "شهران", "أشهر", "شهرًا");
            var year = FormatNumber((int)timestamp.TotalDays/365, "سنة", "سنتان", "سنوات", "سنةً");

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

        // KeyDown navigation methods
        private void CodeTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TrainingNameTxt.Focus();
            }
        }

        private void TrainingNameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PlaceTxt.Focus();
            }
        }

        private void PlaceTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                StartDate.Focus();
            }
        }

        private void StartDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EndDate.Focus();
            }
        }

        private void EndDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DepartmentBox.Focus();
            }
        }

        private void DepartmentBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TrainingTypeTxt.Focus();
            }
        }

        private void TrainingTypeTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // End of form - no further navigation
            }
        }

        private void EmployeeNameTxt_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void EmployeeFinanceNumberTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               AddEmployeeBtn.PerformClick();
            }
        }
    }
}

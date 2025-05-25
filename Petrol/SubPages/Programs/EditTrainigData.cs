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
    public partial class EditTrainingData : UserControl
    {
        private TrainingService service;
        private ProgramService programService;
        private EmployeeService employeeService;
        private PlaceService placeService;
        private ProgramTypeService programTypeService;
        private Training EditedTraining;
        private Models.Program EditedProgram;
        private List<Employee> Employees;

        public EditTrainingData()
        {
            InitializeComponent();
            service = new TrainingService();
            programService = new ProgramService();
            employeeService = new EmployeeService();
            placeService = new PlaceService();
            programTypeService = new ProgramTypeService();
        }

        public void SetTrainingId(int trainingId, int programId)
        {
            EditedProgram = programService.GetById<Models.Program>(programId);
            EditedTraining = service.GetAllWithInclude(x=>x.TrainingType).FirstOrDefault(t=>t.Id==trainingId);
            CodeTxt.Text= EditedTraining.Id.ToString();
            TrainingNameTxt.Text= EditedTraining.Name;

            var departments = new DepartmentService().GetAll<Department>();
            DepartmentBox.Items.Clear();
            DepartmentBox.Items.Add("كل الشركة");
            DepartmentBox.Items.AddRange(departments.Select(x => x.Name).ToArray());

            var places = placeService.GetAll<Place>();
            PlaceTxt.AutoCompleteCustomSource.AddRange(places.Select(x => x.Name).ToArray());
            var place = places.FirstOrDefault(x => x.Id == EditedTraining.PlaceId);
            if (place != null) PlaceTxt.Text= place.Name;

            Employees = employeeService.GetAll<Employee>().ToList();
            EmployeeNameTxt.AutoCompleteCustomSource.AddRange(Employees.Select(x => x.Name).ToArray());
            EmployeeFinanceNumberTxt.AutoCompleteCustomSource.AddRange(Employees.Select(x => x.FinanceNumber).ToArray());
            TrainingTypeTxt.Text = EditedTraining.TrainingType.Name;
            var trainingTypes = new ProgramTypeService().GetAll<TrainingType>().Select(x => x.Name).ToArray();
            TrainingTypeTxt.AutoCompleteCustomSource.AddRange(trainingTypes);


            StartDate.Value = EditedTraining.From;
            EndDate.Value = EditedTraining.To;


            var employeeTrainings = service.GetAll<EmployeeTraining>().Where(x => x.TrainingId == trainingId).ToList();
            foreach (var empTraining in employeeTrainings)
            {
                var employee = Employees.FirstOrDefault(x => x.Id == empTraining.EmployeeId);
                if (employee != null)
                {
                    EmployeeData.Rows.Add(EmployeeData.Rows.Count + 1, employee.FinanceNumber, employee.Name, employee.DepartmentName, employee.RetireDate);
                }
            }
            DepartmentBox.SelectedItem = EditedTraining.DepartmentName;
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Training", EditedProgram.Id);
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
            if (EmployeeData.Rows.Cast<DataGridViewRow>().Any(x => x.Cells[0].Value != null && x.Cells[1].Value.ToString() == employee.FinanceNumber))
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
                string.IsNullOrEmpty(TrainingTypeTxt.Text))
            {
                UserMessages.Error("من فضلك املئ كل الخانات الفارغة الخاصة ببيانات التدريب");
                return;
            }
            try
            {
                var place = placeService.Find<Place>(x => x.Name == PlaceTxt.Text.Trim()).FirstOrDefault();
                if (place == null)
                {
                    UserMessages.Error("هذا المكان غير موجود");
                    return;
                }
                var f = false;
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
                var trainingType = programTypeService.GetAll<TrainingType>().FirstOrDefault(x => x.Name == TrainingTypeTxt.Text.Trim());
                if (trainingType == null) 
                {
                    trainingType=new TrainingType() { Name=TrainingTypeTxt.Text.Trim() };
                    typeSevice.Add(trainingType);
                    typeSevice.SaveChanges();
                }
                EditedTraining.Name = TrainingNameTxt.Text.Trim();
                EditedTraining.PlaceId = place.Id;
                EditedTraining.ProgramId = EditedProgram.Id;
                EditedTraining.From = StartDate.Value;
                EditedTraining.To = EndDate.Value;
                EditedTraining.TrainingTypeId = trainingType.Id;
                EditedTraining.DepartmentName = f ? "كل الشركة" : department.Name;

                service.Update(EditedTraining);

                var existingEmployeeTrainings = service.GetAll<EmployeeTraining>().Where(x => x.TrainingId == EditedTraining.Id).ToList();
                foreach (var empTraining in existingEmployeeTrainings)
                {
                    service.Delete(empTraining);
                }

                foreach (DataGridViewRow row in EmployeeData.Rows)
                {
                    var employeeFinanceNumber = row.Cells[1].Value?.ToString() ?? "";
                    var employee = employeeService.GetAll<Employee>().FirstOrDefault(x => x.FinanceNumber == employeeFinanceNumber);
                    if (employee != null)
                    {
                        var employeeTraining = new EmployeeTraining
                        {
                            TrainingId = EditedTraining.Id,
                            EmployeeId = employee.Id,
                        };
                        service.Add(employeeTraining);
                    }
                }
                service.SaveChanges();
                UserMessages.Info("تم التعديل بنجاح");
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
                DepartmentBox.SelectedItem = employee.DepartmentName;
                RemainTxt.Text= ConvertDateToSentence(employee.RetireDate);
            }
            else
            {
                EmployeeFinanceNumberTxt.Text= string.Empty;
                DepartmentBox.SelectedIndex = -1;
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

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            var confirmResult = UserMessages.Warning("هل انت متأكد من حذف هذا التدريب؟");
            if (confirmResult != DialogResult.Yes)
            {
                return;
            }
            try
            {
                var employeeTrainings = service.GetAll<EmployeeTraining>().Where(x => x.TrainingId == EditedTraining.Id).ToList();
                foreach (var empTraining in employeeTrainings)
                {
                    service.Delete(empTraining);
                }
                service.Delete(EditedTraining);
                service.SaveChanges();
                UserMessages.Info("تم الحذف بنجاح");
                var form = (Form1)this.ParentForm;
                form.ProgramNavigation("Training", EditedProgram.Id);
            }
            catch (Exception ex)
            {
                UserMessages.Error(ex.Message);
            }
            // Clearing the form as in AddTraining
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
            SetTrainingId(EditedTraining.Id, EditedProgram.Id);
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
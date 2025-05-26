using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Employees
{
    public partial class AddEmployee : UserControl
    {
        private EmployeeService EmployeeService;
        private Department[] Departments; 
        public AddEmployee()
        {
            InitializeComponent();
            EmployeeService = new EmployeeService();
        }
        public void LoadData() {


            // add deparments to departments box 
            Departments = new DepartmentService().GetAll<Department>().ToArray();
            var depanmes=Departments.Select(x=>x.Name).ToArray();
            DepartmentBox.DataSource = depanmes;
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.EmployeeNavigation("Main");
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (IsAnyBoxEmpty())
            {
                UserMessages.Error("من فضلك املئ البيانات بالكامل");
                return;
            }
            // check if the finance number is already in the db
            if (EmployeeService.IsFinanceNumberExists(FinanceNumTxt.Text.Trim()))
            {
                UserMessages.Error("رقم الموظف موجود مسبقا");
                return;
            }
            //get the department
            var department =Departments.FirstOrDefault(x=>x.Name== DepartmentBox.Text.Trim());

            // copy the data from the boxes to employee object to save in the db
            Employee employee = new Employee()
            {
                FinanceNumber = FinanceNumTxt.Text.Trim(),
                Name = NameTxt.Text.Trim(),
                HireDate = HireDate.Value,
                BirthDate = BirthDate.Value,
                RetireDate = RetireDate.Value,
                EmplymentDate = EmploymentDate.Value,
                Level = LevelBox.Text.Trim(),
                CurrentJob = CurrentJobTxt.Text.Trim(),
                Section = SectionTxt.Text.Trim(),
                AcademicQualification = QualificationTxt.Text.Trim(),
                DepartmentName = DepartmentBox.Text.Trim(),
                HasMaster = MasterBox.Text.Trim(),
                JobStatus = StatusBox.Text.Trim(),
                JobType = JobTypeTxt.Text.Trim(),
                Sex = SexBox.Text.Trim(),
                SSN = SSNTxt.Text.Trim(),
                Religon = ReligonBox.Text.Trim(),
                QualificationType = QualTypeBox.Text.Trim(),
                Department = department,
            };
            try
            {
                EmployeeService.AddEmployee(employee);
                EmployeeService.SaveChanges();
                UserMessages.Info("تم حفظ البيانات بنجاح");
            }
            catch (Exception ex)
            {
                UserMessages.Error("حدث خطأ اثناء حفظ البيانات");
                return;

            }
        }
        private bool IsAnyBoxEmpty ()
        {
            return FinanceNumTxt.Text.Trim().Trim().Length == 0 ||
                NameTxt.Text.Trim().Trim().Length == 0 ||
                LevelBox.SelectedIndex == -1 ||
                CurrentJobTxt.Text.Trim().Trim().Length == 0 ||
                SectionTxt.Text.Trim().Trim().Length == 0 ||
                SSNTxt.Text.Trim().Trim().Length == 0 ||
                DepartmentBox.SelectedIndex == -1 ||
                SexBox.SelectedIndex == -1 ||
                QualificationTxt.Text.Trim().Trim().Length == 0 ||
                QualTypeBox.SelectedIndex == -1 ||
                ReligonBox.SelectedIndex == -1 ||
                JobTypeTxt.Text.Trim().Trim().Length == 0 ||
                MasterBox.SelectedIndex == -1 ||
                StatusBox.SelectedIndex == -1
                ;
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            FinanceNumTxt.Text= "";
            NameTxt.Text= "";
            LevelBox.SelectedIndex = -1;
            CurrentJobTxt.Text= "";
            SectionTxt.Text= "";
            SSNTxt.Text= "";
            DepartmentBox.SelectedIndex = -1;
            SexBox.SelectedIndex = -1;
            QualificationTxt.Text= "";
            QualTypeBox.SelectedIndex = -1;
            ReligonBox.SelectedIndex = -1;
            JobTypeTxt.Text= "";
            MasterBox.SelectedIndex = -1;
            StatusBox.SelectedIndex = -1;
            HireDate.Value = DateTime.Now;
            BirthDate.Value = DateTime.Now;
            RetireDate.Value = DateTime.Now;
            EmploymentDate.Value = DateTime.Now;

        }

        private void BirthDate_ValueChanged(object sender, EventArgs e)
        {
            RetireDate.Value=BirthDate.Value.AddYears(60);
        }
    }
}

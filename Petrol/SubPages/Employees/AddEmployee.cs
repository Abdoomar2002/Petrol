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
        public AddEmployee()
        {
            InitializeComponent();
            EmployeeService = new EmployeeService();
        }
        public void LoadData() {
            // add deparments to departments box 
            var departments = new DepartmentService().GetAll<Department>().Select(x=>x.Name).ToArray();
            DepartmentBox.DataSource = departments;
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ShowEmployeeMain();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (IsAnyBoxEmpty())
            {
                UserMessages.Error("من فضلك املئ البيانات بالكامل");
                return;
            }
            // check if the finance number is already in the db
            if (EmployeeService.IsFinanceNumberExists(FinanceNumTxt.Text))
            {
                UserMessages.Error("رقم الموظف موجود مسبقا");
                return;
            }
            //get the department
            var department = new DepartmentService().FindDepartmentByName(DepartmentBox.Text);

            // copy the data from the boxes to employee object to save in the db
            Employee employee = new Employee()
            {
                FinanceNumber = FinanceNumTxt.Text,
                Name = NameTxt.Text,
                HireDate = HireDate.Value,
                BirthDate = BirthDate.Value,
                RetireDate = RetireDate.Value,
                EmplymentDate = EmploymentDate.Value,
                Level = LevelBox.Text,
                CurrentJob = CurrentJobTxt.Text,
                Section = SectionTxt.Text,
                AcademicQualification = QualificationTxt.Text,
                DepartmentName = DepartmentBox.Text,
                HasMaster = MasterBox.Text,
                JobStatus = StatusBox.Text,
                JobType = JobTypeTxt.Text,
                Sex = SexBox.Text,
                SSN = SSNTxt.Text,
                Religon = ReligonBox.Text,
                QualificationType = QualTypeBox.Text,
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
            return FinanceNumTxt.Text.Trim().Length == 0 ||
                NameTxt.Text.Trim().Length == 0 ||
                LevelBox.SelectedIndex == -1 ||
                CurrentJobTxt.Text.Trim().Length == 0 ||
                SectionTxt.Text.Trim().Length == 0 ||
                SSNTxt.Text.Trim().Length == 0 ||
                DepartmentBox.SelectedIndex == -1 ||
                SexBox.SelectedIndex == -1 ||
                QualificationTxt.Text.Trim().Length == 0 ||
                QualTypeBox.SelectedIndex == -1 ||
                ReligonBox.SelectedIndex == -1 ||
                JobTypeTxt.Text.Trim().Length == 0 ||
                MasterBox.SelectedIndex == -1 ||
                StatusBox.SelectedIndex == -1
                ;
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            FinanceNumTxt.Text = "";
            NameTxt.Text = "";
            LevelBox.SelectedIndex = -1;
            CurrentJobTxt.Text = "";
            SectionTxt.Text = "";
            SSNTxt.Text = "";
            DepartmentBox.SelectedIndex = -1;
            SexBox.SelectedIndex = -1;
            QualificationTxt.Text = "";
            QualTypeBox.SelectedIndex = -1;
            ReligonBox.SelectedIndex = -1;
            JobTypeTxt.Text = "";
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

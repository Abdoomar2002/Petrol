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

            // Validate date fields
            if (!DateValidator.IsValidDate(HireDate.Text))
            {
                UserMessages.Error("يرجى إدخال تاريخ التعيين بالصيغة الصحيحة dd/MM/yyyy");
                HireDate.Focus();
                return;
            }

            if (!DateValidator.IsValidDate(BirthDate.Text))
            {
                UserMessages.Error("يرجى إدخال تاريخ الميلاد بالصيغة الصحيحة dd/MM/yyyy");
                BirthDate.Focus();
                return;
            }

            if (!DateValidator.IsValidDate(RetireDate.Text))
            {
                UserMessages.Error("يرجى إدخال تاريخ التقاعد بالصيغة الصحيحة dd/MM/yyyy");
                RetireDate.Focus();
                return;
            }

            if (!DateValidator.IsValidDate(EmploymentDate.Text))
            {
                UserMessages.Error("يرجى إدخال تاريخ التوظيف بالصيغة الصحيحة dd/MM/yyyy");
                EmploymentDate.Focus();
                return;
            }

            // copy the data from the boxes to employee object to save in the db
            Employee employee = new Employee()
            {
                FinanceNumber = FinanceNumTxt.Text.Trim(),
                Name = NameTxt.Text.Trim(),
                HireDate = DateValidator.ParseDate(HireDate.Text).Value,
                BirthDate = DateValidator.ParseDate(BirthDate.Text).Value,
                RetireDate = DateValidator.ParseDate(RetireDate.Text).Value,
                EmplymentDate = DateValidator.ParseDate(EmploymentDate.Text).Value,
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
            HireDate.Text = DateValidator.FormatDate(DateTime.Now);
            BirthDate.Text = DateValidator.FormatDate(DateTime.Now);
            RetireDate.Text = DateValidator.FormatDate(DateTime.Now);
            EmploymentDate.Text = DateValidator.FormatDate(DateTime.Now);

        }

        private void BirthDate_TextChanged(object sender, EventArgs e)
        {
            if (DateValidator.IsValidDate(BirthDate.Text))
            {
                var birthDate = DateValidator.ParseDate(BirthDate.Text).Value;
                var retireDate = birthDate.AddYears(60);
                RetireDate.Text = DateValidator.FormatDate(retireDate);
            }
        }

        // KeyDown navigation methods
        private void FinanceNumTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NameTxt.Focus();
            }
        }

        private void NameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LevelBox.Focus();
            }
        }

        private void LevelBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CurrentJobTxt.Focus();
            }
        }

        private void CurrentJobTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SectionTxt.Focus();
            }
        }

        private void SectionTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SSNTxt.Focus();
            }
        }

        private void SSNTxt_KeyDown(object sender, KeyEventArgs e)
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
                SexBox.Focus();
            }
        }

        private void SexBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QualificationTxt.Focus();
            }
        }

        private void QualificationTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QualTypeBox.Focus();
            }
        }

        private void QualTypeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ReligonBox.Focus();
            }
        }

        private void ReligonBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                JobTypeTxt.Focus();
            }
        }

        private void JobTypeTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MasterBox.Focus();
            }
        }

        private void MasterBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                StatusBox.Focus();
            }
        }

        private void StatusBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EmploymentDate.Focus();
            }
        }

        private void HireDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BirthDate.Focus();
            }
        }

        private void BirthDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RetireDate.Focus();
            }
        }

        private void RetireDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EmploymentDate.Focus();
            }
        }

        private void EmploymentDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // End of form - no further navigation
            }
        }

    }
}

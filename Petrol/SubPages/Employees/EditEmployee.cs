using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Employees
{
    public partial class EditEmployee : UserControl
    {
        private EmployeeService service;
        private Employee EditedEmployee;
        public EditEmployee()
        {
            InitializeComponent();
            service = new EmployeeService();
        }
        public void SetEmployeeId(int id)
        {
       
        
            var departments = new DepartmentService().GetAll<Department>().Select(x => x.Name).ToArray();
            DepartmentBox.DataSource = departments;
            var employee = service.GetEmployee(id);
            if (employee == null) return;
            FinanceNumTxt.Text= employee.FinanceNumber.ToString();
            NameTxt.Text= employee.Name;
            DepartmentBox.Text= employee.DepartmentName;
            CurrentJobTxt.Text= employee.CurrentJob;
            JobTypeTxt.Text= employee.JobType;
            StatusBox.Text= employee.JobStatus;
            LevelBox.Text= employee.Level;
            SectionTxt.Text= employee.Section;
            SSNTxt.Text= employee.SSN;
            BirthDate.Value = employee.BirthDate;
            HireDate.Value = employee.HireDate;
            RetireDate.Value = employee.RetireDate;
            EmploymentDate.Value = employee.EmplymentDate;
            ReligonBox.Text= employee.Religon;
            QualificationTxt.Text= employee.AcademicQualification;
            QualTypeBox.Text= employee.QualificationType;
            MasterBox.Text= employee.HasMaster;
            SexBox.Text= employee.Sex;
            EditedEmployee = employee;


        }
        private void AddProgramBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.EmployeeNavigation("AddProgram", EditedEmployee.Id);
        }

        private void ShowProgramsBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.EmployeeNavigation("Programs", EditedEmployee.Id);
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.EmployeeNavigation("Main");
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(IsAnyBoxEmpty())
            {
                UserMessages.Error("من فضلك أملئ البيانات المفقودة");
                return;
            }
            try 
            {
            // check the finance number dose not exists in the db
            if (service.IsFinanceNumberExists(FinanceNumTxt.Text.Trim(), EditedEmployee.FinanceNumber))
            {
                UserMessages.Error("رقم الموظف موجود مسبقا");
                return;
            }
            //get the department
            var department = new DepartmentService().FindDepartmentByName(DepartmentBox.Text.Trim());
            if (department == null)
            {
                UserMessages.Error("القسم غير موجود");
                return;
            }
            // copy the data from the boxes to employee object to save in the db
            EditedEmployee.FinanceNumber = FinanceNumTxt.Text.Trim();
            EditedEmployee.Name = NameTxt.Text.Trim();
            EditedEmployee.DepartmentName = DepartmentBox.Text.Trim();
            EditedEmployee.CurrentJob = CurrentJobTxt.Text.Trim();
            EditedEmployee.JobType = JobTypeTxt.Text.Trim();
            EditedEmployee.JobStatus = StatusBox.Text.Trim();
            EditedEmployee.Level = LevelBox.Text.Trim();
            EditedEmployee.Section = SectionTxt.Text.Trim();
            EditedEmployee.SSN = SSNTxt.Text.Trim();
            EditedEmployee.BirthDate = BirthDate.Value;
            EditedEmployee.HireDate = HireDate.Value;
            EditedEmployee.RetireDate = RetireDate.Value;
            EditedEmployee.EmplymentDate = EmploymentDate.Value;
            EditedEmployee.Religon = ReligonBox.Text.Trim();
            EditedEmployee.AcademicQualification = QualificationTxt.Text.Trim();
            EditedEmployee.QualificationType = QualTypeBox.Text.Trim();
            EditedEmployee.HasMaster = MasterBox.Text.Trim();
            EditedEmployee.Sex = SexBox.Text.Trim();
            EditedEmployee.Department = department;
            EditedEmployee.DepartmentId = department.Id;
            // save the employee in the db
            service.Update(EditedEmployee);
            service.SaveChanges();
            UserMessages.Info("تم تعديل الموظف بنجاح");
            }catch(Exception ex)
            {
                UserMessages.Error("حدث خطأ أثناء تعديل الموظف");
                Console.WriteLine(ex.Message);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            var result = UserMessages.Warning("هل تريد حذف الموظف");
            if (result == DialogResult.Yes)
            {
                try
                {
                    service.Delete(EditedEmployee);
                    service.SaveChanges();
                    UserMessages.Info("تم حذف الموظف بنجاح");
                    var form = (Form1)this.ParentForm;
                    form.EmployeeNavigation("Main");
                }
                catch (Exception ex)
                {
                    UserMessages.Error("حدث خطأ أثناء حذف الموظف");
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private bool IsAnyBoxEmpty()
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

    }
}

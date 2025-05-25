using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Windows.Forms;

namespace Petrol.SubPages.Departments
{
    public partial class EditDepartment : UserControl
    {
        private DepartmentService service;
        private Department EditedDepartment;
        public EditDepartment()
        {
            InitializeComponent();
            service = new DepartmentService();
        }
        public void SetDepartmentID(int id)
        {
       
            var department = service.GetById<Department>(id);
            if (department != null)
            {
                EditedDepartment = department;
                NameTxt.Text = department.Name;
                CodeTxt.Text = department.Id.ToString();
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.DeparmentNavigation("Main");
        }

        private void ShowEmployeesBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.DeparmentNavigation("Programs",EditedDepartment.Id);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (NameTxt.Text.Trim().Trim().Length > 0)
            {
                try
                {
                    // check there is no deparment has same Name
                    var checkDepartmentName = service.FindDepartmentByName(NameTxt.Text.Trim().Trim());
                    if (checkDepartmentName == null || EditedDepartment.Name == NameTxt.Text.Trim().Trim())
                    {
                        EditedDepartment.Name = NameTxt.Text.Trim();
                        service.Update(EditedDepartment);
                        service.SaveChanges();
                        UserMessages.Info($"تم تعديل الادارة بنجاح\nبكود {EditedDepartment.Id}");
                    }
                    else
                    {
                        UserMessages.Error("يوجد إدارة بنفس الأسم");
                    }
                }
                catch (Exception ex)
                {
                    UserMessages.Error(ex.Message);
                }
            }
            else
            {
                UserMessages.Error("من فضلك املئ البيانات بالكامل");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            // Warning
            var result = UserMessages.Warning("هل انت متأكد من حذف الادارة");
            if (result == DialogResult.Yes)
            {
                var result2 = UserMessages.Warning("سوف يتم مسح الموظفين والتدريبات الخاصة بهذه الادارة"); 
                if (result2 == DialogResult.Yes)
                if (EditedDepartment != null)
                {
                    try
                    {
                        service.Delete(EditedDepartment);
                        service.SaveChanges();
                        UserMessages.Info($"تم حذف الادارة بنجاح\nبكود {EditedDepartment.Id}");
                        EditedDepartment = null;
                        NameTxt.Text = "";
                        CodeTxt.Text = "";
                        BackBtn_Click(sender, e);
                        }
                    catch (Exception ex)
                    {
                        UserMessages.Error(ex.Message);
                    }
                }
                else
                {
                    UserMessages.Error("من فضلك اختر إدارة أولا");
                }
            }
        }
    }
}

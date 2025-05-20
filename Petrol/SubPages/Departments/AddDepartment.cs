using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petrol.SubPages.Departments
{
    public partial class AddDepartment : UserControl
    {
        private DepartmentService service;
        public AddDepartment()
        {
            InitializeComponent();
            service = new DepartmentService();
        }
        public void LoadData() 
        {
            var lastId= service.GetTheLastId<Department>();
            CodeTxt.Text = lastId.ToString();
        }
        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;  
            form.DeparmentNavigation("Main");

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (NameTxt.Text.Trim().Length > 0) 
            {
                try
                {

                    // check there is no deparment has same Name
                    var checkDepartmentName = service.FindDepartmentByName(NameTxt.Text.Trim());
                    if (checkDepartmentName == null)
                    {

                        Department department = new Department()
                        {
                            Name = NameTxt.Text,
                        };
                        service.Add(department);
                        service.SaveChanges();
                        UserMessages.Info($"تم حفظ الادارة بنجاح\nبكود {department.Id}");
                        LoadData();
                    }
                    else 
                    {
                        UserMessages.Error("يوجد إدارة بنفس الأسم");
                    }
                }
                catch (Exception ex) 
                {
                    UserMessages.Error("خطأ في حفظ البيانات");
                }
            }
            else
            {
                UserMessages.Error("من فضلك ادخل اسم الادارة أولا");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            NameTxt.Text = "";
            CodeTxt.Text = "";
            LoadData();
        }
    }
}

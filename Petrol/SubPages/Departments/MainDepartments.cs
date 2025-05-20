using Petrol.Models;
using Petrol.Services;
using Petrol.SubPages.Employees;
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
    public partial class MainDepartments : UserControl
    {
        private DepartmentService service;
        public MainDepartments()
        {
            InitializeComponent();
            service = new DepartmentService();
        }

        private void AddDepartmentBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.DeparmentNavigation("Add");
        }
        public void LoadData()
        {
            var departments = service.GetAllWithInclude(d => d.Employees).ToList();
            DepartmentData.Rows.Clear();
            
                var i = 1;
                foreach (var department in departments)
                {
                    DepartmentData.Rows.Add( i++,department.Id, department.Name, department.Employees.Count);
                }
            
           
        }

        private void DepartmentData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var id = int.Parse(DepartmentData.Rows[e.RowIndex].Cells[1].Value?.ToString()??"0");
            if (id == 0) return;
            var form = (Form1)this.ParentForm;
            form.DeparmentNavigation("Edit",id);
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            DepartmentData.Rows.Clear();
            var departments = service.Search(SearchTxt.Text);
            if (departments == null || departments.Count() == 0)
            {
                UserMessages.Error("لا يوجد إدارات بنفس الاسم");
                return;
            }
            var i = 1;
            foreach (var department in departments)
            {
                DepartmentData.Rows.Add(i++, department.Id, department.Name, department.Employees.Count);
            }

        }
    }
}

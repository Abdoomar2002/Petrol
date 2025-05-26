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
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;

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
            var departments = service.GetAllWithInclude(t=>t.Employees).Where(x=>x.Name.Contains(SearchTxt.Text)||x.Id.ToString().Contains(SearchTxt.Text));
            if (departments == null || departments.Count() == 0)
            {
                UserMessages.Error("لا يوجد إدارات بنفس الاسم");
                return;
            }
            var i = 1;
            foreach (var department in departments)
            {
                DepartmentData.Rows.Add(i++, department.Id, department.Name, department?.Employees?.Count??0);
            }

        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (DepartmentData.Rows.Count == 0)
            {
                UserMessages.Error("لا يوجد بيانات للطباعة");
                return;
            }

            // Create a new DataGridView with only visible columns
            var filteredGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            foreach (DataGridViewColumn col in DepartmentData.Columns)
            {
                if (col.Visible || !(col.ValueType is DataGridViewImageCell))
                    filteredGrid.Columns.Add((DataGridViewColumn)col.Clone());
            }

            // Copy rows
            foreach (DataGridViewRow row in DepartmentData.Rows)
            {
                if (!row.IsNewRow)
                {
                    var newRowIndex = filteredGrid.Rows.Add();
                    for (int i = 0; i < DepartmentData.Columns.Count; i++)
                    {
                        if (DepartmentData.Columns[i].Visible)
                        {
                            var targetIndex = filteredGrid.Columns
                                .Cast<DataGridViewColumn>()
                                .ToList()
                                .FindIndex(c => c.HeaderText == DepartmentData.Columns[i].HeaderText);

                            filteredGrid.Rows[newRowIndex].Cells[targetIndex].Value = row.Cells[i].Value;
                        }
                    }
                }
            }

            // Titles
            var Main = $"تقرير عن الادارات";
            var sub = DepartmentData.Rows.Count - 1 != service.GetAll<Department>().Count() ? $"نتيجة البحث عن {SearchTxt.Text}" : "جميع الادارات";
         //   var filteredGridTitle = $ ذات نوع {TrainingTypeBox.Text} من {StartDate.Value.ToString("dd/MM/yyyy")} إلى {EndDate.Value.ToString("dd/MM/yyyy")}";
            // Pass filtered grid
            PdfGenerator.GeneratePdf(Main, sub, "", filteredGrid);

        }
    }
}

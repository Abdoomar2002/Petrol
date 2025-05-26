using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Employees
{
    public partial class MainEmployee : UserControl
    {
        private EmployeeService service;
        public MainEmployee()
        {
            InitializeComponent();
            service = new EmployeeService();
        }

        private void AddEmployeeBtn_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.ParentForm;
            form1.EmployeeNavigation("Add");
        }

        private void EmployeesData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var id = (int)(EmployeesData.Rows[e.RowIndex].Cells[0].Value ??0);
            if (id == 0) return;
            var form = (Form1)this.ParentForm;
            form.EmployeeNavigation("Edit",id);
        }
        public void LoadData() 
        {
       
     
        
            EmployeesData.Rows.Clear();
            var employees = service.GetAllEmployees();
            var i = 1;
            foreach (var employee in employees)
            {
                EmployeesData.Rows.Add(employee.Id,i++, employee.FinanceNumber, employee.Name,employee.DepartmentName, employee.CurrentJob, employee.JobType);
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            var search = SearchTxt.Text;
            EmployeesData.Rows.Clear();
            var employees = service.Search(search);
            var i = 1;
            foreach (var employee in employees)
            {
                    EmployeesData.Rows.Add(employee.Id, i++, employee.FinanceNumber, employee.Name, employee.DepartmentName, employee.CurrentJob, employee.JobType);
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (EmployeesData.Rows.Count == 0)
            {
                UserMessages.Error("لا يوجد بيانات للطباعة");
                return;
            }

            // Create a new DataGridView with only visible columns
            var filteredGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            foreach (DataGridViewColumn col in EmployeesData.Columns)
            {
                if (col.Visible)
                    filteredGrid.Columns.Add((DataGridViewColumn)col.Clone());
            }

            // Copy rows
            foreach (DataGridViewRow row in EmployeesData.Rows)
            {
                if (!row.IsNewRow)
                {
                    var newRowIndex = filteredGrid.Rows.Add();
                    for (int i = 0; i < EmployeesData.Columns.Count; i++)
                    {
                        if (EmployeesData.Columns[i].Visible)
                        {
                            var targetIndex = filteredGrid.Columns
                                .Cast<DataGridViewColumn>()
                                .ToList()
                                .FindIndex(c => c.HeaderText == EmployeesData.Columns[i].HeaderText);

                            filteredGrid.Rows[newRowIndex].Cells[targetIndex].Value = row.Cells[i].Value;
                        }
                    }
                }
            }

            // Titles
            var Main = "تقرير الموظفين";
            var sub = EmployeesData.Rows.Count - 1 != service.GetAll<Employee>().Count() ? $"نتيجة البحث عن {SearchTxt.Text}" : "جميع الموظفين";

            // Pass filtered grid
            PdfGenerator.GeneratePdf(Main, sub, "", filteredGrid);
        }

    }
}

using Petrol.Services;
using System;
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
            var i = 0;
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
            var i = 0;
            foreach (var employee in employees)
            {
                    EmployeesData.Rows.Add(employee.Id, i++, employee.FinanceNumber, employee.Name, employee.DepartmentName, employee.CurrentJob, employee.JobType);
            }
        }
    }
}

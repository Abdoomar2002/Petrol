using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petrol.SubPages.Employees
{
    public partial class MainEmployee : UserControl
    {
        public MainEmployee()
        {
            InitializeComponent();
        }

        private void AddEmployeeBtn_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.ParentForm;
            form1.ShowEmployeeAdd();
        }

        private void EmployeesData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ShowEmployeeEdit();
        }
        public void LoadData() 
        {
            EmployeesData.Rows.Clear();
            EmployeesData.RowCount = 2;
        }
    }
}

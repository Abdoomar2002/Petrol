using Petrol.SubPages.Employees;
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
        public MainDepartments()
        {
            InitializeComponent();
        }

        private void AddDepartmentBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.DeparmentNavigation("Add");
        }
        public void LoadData()
        {
            DepartmentData.Rows.Clear();
            DepartmentData.RowCount = 2;
        }

        private void DepartmentData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.DeparmentNavigation("Edit");
        }
    }
}

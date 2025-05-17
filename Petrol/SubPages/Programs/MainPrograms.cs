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

namespace Petrol.SubPages.Programs
{
    public partial class MainPrograms : UserControl
    {
        public MainPrograms()
        {
            InitializeComponent();
        }

        private void AddProgramBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Add");
        }
        public void LoadData()
        {
            ProgramsData.Rows.Clear();
            ProgramsData.RowCount = 2;
        }

        private void ProgramsData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Edit");
        }
    }
}

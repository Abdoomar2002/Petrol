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
    public partial class TraningData : UserControl
    {
        public TraningData()
        {
            InitializeComponent();
        
            data.Rows.Clear();
            data.RowCount = 2;
       
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Edit");
        }

        private void data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Edit Training");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petrol.SubPages.Reports
{
    public partial class MainReports : UserControl
    {
        public MainReports()
        {
            InitializeComponent();
        }

        private void FinanceReportBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ReportsNavigation("Finance");
        }

        private void MangmentReportBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ReportsNavigation("Mangment");
        }

        private void EmployeeReportBtn_Click(object sender, EventArgs e)
        {
            var form1 = (Form1)this.ParentForm;
            form1.ReportsNavigation("Employee");
        }

        private void FollowingReportBtn_Click(object sender, EventArgs e)
        {
            var form1 =(Form1)this.ParentForm;
            form1.ReportsNavigation("Following");
        }
    }
}

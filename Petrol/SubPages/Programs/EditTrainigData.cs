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
    public partial class EditTrainigData : UserControl
    {
        public EditTrainigData()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Training");
        }

        private void data_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
           
        }
    }
}

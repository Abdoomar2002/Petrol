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

namespace Petrol.SubPages.Places
{
    public partial class MainPlaces : UserControl
    {
        public MainPlaces()
        {
            InitializeComponent();
        }

        private void AddPlaceBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.PlacesNavigation("Add");
        }
        public void LoadData()
        {
            PlacesData.Rows.Clear();
            PlacesData.RowCount = 2;
        }

        private void PlacesData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.PlacesNavigation("Edit");
        }
    }
}

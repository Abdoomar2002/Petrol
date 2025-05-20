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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petrol.SubPages.Places
{
    public partial class MainPlaces : UserControl
    {
        private PlaceService service;
        public MainPlaces()
        {
            InitializeComponent();
            service = new PlaceService();
        }

        private void AddPlaceBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.PlacesNavigation("Add");
        }
        public void LoadData()
        {
            // load the data from the db to the datagridview
            var places = service.GetAll<Place>();
            PlacesData.Rows.Clear();
            var i = 1;
            foreach (var place in places)
            {
                PlacesData.Rows.Add(i++,place.Id, place.Name,  place.Address, place.PhoneNumber, place.ManagerName);
            }
            
        }

        private void PlacesData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var placeId = (int)(PlacesData.Rows[e.RowIndex].Cells[1].Value??0);
            if(placeId == 0) return;
            var form = (Form1)this.ParentForm;
            form.PlacesNavigation("Edit",placeId);
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            var searchText = SearchTxt.Text;
            if (string.IsNullOrEmpty(searchText))
            {
                LoadData();
                return;
            }
            var places = service.Search(searchText);
            if (places.Count() == 0)
            {
                UserMessages.Error("لا توجد نتائج");
                return;
            }
            PlacesData.Rows.Clear();
            var i = 1;
            foreach (var place in places)
            {       
              PlacesData.Rows.Add(i++, place.Id, place.Name, place.Address, place.PhoneNumber, place.ManagerName);
            }
        }
    }
}

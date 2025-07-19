using Petrol.Models;
using Petrol.Services;
using Petrol.SubPages.Employees;
using Petrol.SubPages.Programs;
using Petrol.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;

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

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (PlacesData.Rows.Count == 0)
            {
                UserMessages.Error("لا يوجد بيانات للطباعة");
                return;
            }

            // Create a new DataGridView with only visible columns
            var filteredGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            foreach (DataGridViewColumn col in PlacesData.Columns)
            {
                if (col.Visible && !(col.ValueType is DataGridViewImageCell))
                    filteredGrid.Columns.Add((DataGridViewColumn)col.Clone());
            }

            // Copy rows
            foreach (DataGridViewRow row in PlacesData.Rows)
            {
                if (!row.IsNewRow)
                {
                    var newRowIndex = filteredGrid.Rows.Add();
                    for (int i = 0; i < PlacesData.Columns.Count; i++)
                    {
                        if (PlacesData.Columns[i].Visible)
                        {
                            var targetIndex = filteredGrid.Columns
                                .Cast<DataGridViewColumn>()
                                .ToList()
                                .FindIndex(c => c.HeaderText == PlacesData.Columns[i].HeaderText);

                            filteredGrid.Rows[newRowIndex].Cells[targetIndex].Value = row.Cells[i].Value;
                        }
                    }
                }
            }

            // Titles
            var Main = $"تقرير اماكن";
            var sub = $"نتيجة البحث عن {SearchTxt.Text}";
        //    var filteredGridTitle = $"تدريبات ذات نوع {TrainingTypeBox.Text} من {StartDate.Value.ToString("dd/MM/yyyy")} إلى {EndDate.Value.ToString("dd/MM/yyyy")}";
            // Pass filtered grid
            PdfGenerator.GeneratePdf(Main, sub, "", filteredGrid);

        }

    }
}

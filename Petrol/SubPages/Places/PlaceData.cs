using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Places
{
    public partial class PlaceData : UserControl
    {
        private Place EditedPlace;
        private PlaceService service;
        public PlaceData()
        {
            InitializeComponent();
            service = new PlaceService();
        }
        public void SetPlaceId(int id) 
        {
            var place = service.GetAllWithNestedInclude(x=>x.Include(y=>y.Trainings).ThenInclude(t=>t.TrainingType)).FirstOrDefault(x => x.Id == id);
            if (place != null) 
            {
                EditedPlace = place;
                Data.Rows.Clear();
                var i = 1;
                foreach (var training in place.Trainings)
                {
                    Data.Rows.Add(i++, training.Id, training.Name, training.From.ToString("yyyy/MM/dd"), training.To.ToString("yyyy/MM/dd"));
                }
            }
            
        }
        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.PlacesNavigation("Edit");
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            var searchText = SearchTxt.Text;
            if (string.IsNullOrEmpty(searchText))
            {
                SetPlaceId(EditedPlace.Id);
                return;
            }
            Data.Rows.Clear();
            var i = 1;
            foreach (var training in EditedPlace.Trainings.Where(x => x.Name.Contains(searchText)||x.Id.ToString().Contains(searchText)))
            {
                Data.Rows.Add(i++, training.Id, training.Name, training.From.ToString("yyyy/MM/dd"), training.To.ToString("yyyy/MM/dd"));
            }
        }

        private void FilterBtn_Click(object sender, EventArgs e)
        {
            var searchText = SearchTxt.Text;
            Data.Rows.Clear();
            var i = 1;
            var Searchresult = EditedPlace.Trainings.Where(x => x.Name.Contains(searchText) || x.Id.ToString().Contains(searchText));
            if (Searchresult.Count() == 0)
            {
                UserMessages.Error("لا توجد نتائج");
                return;
            }
            var result = Searchresult.Where(x => x.From.Date >= StartDate.Value.Date && x.To <= EndDate.Value.Date).Where(z => z.TrainingType.Name == ProgramTypeBox.Text);
            foreach (var training in result)
            {
                Data.Rows.Add(i++, training.Id, training.Name, training.From.ToString("yyyy/MM/dd"), training.To.ToString("yyyy/MM/dd"));
            }
        }
    }
}

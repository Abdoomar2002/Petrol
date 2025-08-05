using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
using Petrol.SubPages.Programs;
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
            DataGridViewHelper.FixIndexColumnSorting(Data);
        }
        public void SetPlaceId(int id) 
        {
            var place = service.GetAllWithNestedInclude(x=>x.Include(y=>y.Trainings).ThenInclude(t=>t.TrainingType).Include(t=>t.Trainings)).FirstOrDefault(x => x.Id == id);
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
            var types = place.Trainings.Select(x => x.TrainingType.Name).Distinct().ToArray();
            TrainingTypeBox.Items.Clear();
            TrainingTypeBox.Items.Add("كل الأنواع");
            TrainingTypeBox.Items.AddRange(types);
            
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
            if (!DateValidator.IsValidDate(StartDate.Text))
            {
                UserMessages.Error("يرجى إدخال تاريخ البداية بالصيغة الصحيحة dd/MM/yyyy");
                StartDate.Focus();
                return;
            }

            if (!DateValidator.IsValidDate(EndDate.Text))
            {
                UserMessages.Error("يرجى إدخال تاريخ النهاية بالصيغة الصحيحة dd/MM/yyyy");
                EndDate.Focus();
                return;
            }

            var startDate = DateValidator.ParseDate(StartDate.Text).Value.Date;
            var endDate = DateValidator.ParseDate(EndDate.Text).Value.Date;
            if (startDate > endDate)
            {
                UserMessages.Error("يجب أن يكون تاريخ البداية اصغر من تاريخ النهاية");
                return;
            }

            var result = Searchresult.Where(x => x.From.Date >= startDate && x.To.Date <= endDate).Where(z =>TrainingTypeBox.SelectedIndex>0&& z.TrainingType.Name == TrainingTypeBox.Text);
            foreach (var training in result)
            {
                Data.Rows.Add(i++, training.Id, training.Name, training.From.ToString("yyyy/MM/dd"), training.To.ToString("yyyy/MM/dd"));
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (Data.Rows.Count == 0)
            {
                UserMessages.Error("لا يوجد بيانات للطباعة");
                return;
            }

            // Create a new DataGridView with only visible columns
            var filteredGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            foreach (DataGridViewColumn col in Data.Columns)
            {
                if (col.Visible )
                    filteredGrid.Columns.Add((DataGridViewColumn)col.Clone());
            }

            // Copy rows
            foreach (DataGridViewRow row in Data.Rows)
            {
                if (!row.IsNewRow)
                {
                    var newRowIndex = filteredGrid.Rows.Add();
                    for (int i = 0; i < Data.Columns.Count; i++)
                    {
                        if (Data.Columns[i].Visible)
                        {
                            var targetIndex = filteredGrid.Columns
                                .Cast<DataGridViewColumn>()
                                .ToList()
                                .FindIndex(c => c.HeaderText == Data.Columns[i].HeaderText);

                            filteredGrid.Rows[newRowIndex].Cells[targetIndex].Value = row.Cells[i].Value;
                        }
                    }
                }
            }

            // Titles
            var Main = $"تقرير التدريبات داخل"+" "+ EditedPlace.Name;
            var sub = $"نتيجة البحث عن {SearchTxt.Text}";
            var filteredGridTitle = $"تدريبات ذات نوع {TrainingTypeBox.Text} من {StartDate.Text} إلى {EndDate.Text}";
            // Pass filtered grid
            PdfGenerator.GeneratePdf(Main, sub, filteredGridTitle, filteredGrid);

        }

        private void SearchTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchBtn.PerformClick();
            }
        }

    }
}

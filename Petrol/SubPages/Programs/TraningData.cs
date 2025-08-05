using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Programs
{
    public partial class TraningData : UserControl
    {
        private TrainingService service;
        private ProgramService programService;
        private Models.Program EditedProgram;


        public TraningData()
        {
            InitializeComponent();
            programService = new ProgramService();
            service = new TrainingService();
          
        }
        public void SetProgramId(int id)
        {
            EditedProgram = programService.GetAllWithNestedInclude(x => x.Include(y => y.Trainings).ThenInclude(t => t.TrainingType).Include(t => t.Trainings).ThenInclude(y => y.Place))?.FirstOrDefault(t => t.Id == id) ?? null;
            data.Rows.Clear();
            var types = service.GetAll<TrainingType>().Select(x => x.Name).ToArray();
            TrainingTypeBox.Items.Clear();
            TrainingTypeBox.Items.Add("كل الأنواع");
            TrainingTypeBox.Items.AddRange(types);
            int i = 1;
            foreach (var training in EditedProgram.Trainings)
            {
                data.Rows.Add(i++, training.Id, training.Name, training.TrainingType.Name, training.From.ToString("yyyy/MM/dd"), training.To.ToString("yyyy/MM/dd"), training.Place.Name);
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Edit", EditedProgram.Id);
        }

        private void data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var id = (int)(data.Rows[e.RowIndex].Cells[1].Value ?? 0);
            if (id == 0) return;
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Edit Training", id, EditedProgram.Id);
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            data.Rows.Clear();
            var trainings = service.GetAllWithNestedInclude(x => x.Include(y => y.TrainingType).Include(t => t.Place)).Where(x => x.Id.ToString().Contains(SearchTxt.Text) || x.Name.Contains(SearchTxt.Text) || x.TrainingType.Name.Contains(SearchTxt.Text) || x.Place.Name.Contains(SearchTxt.Text)).Where(p => p.ProgramId == EditedProgram.Id);
            int i = 1;
            foreach (var training in trainings)
            {
                data.Rows.Add(i++, training.Id, training.Name, training.TrainingType.Name, training.From.ToString("yyyy/MM/dd"), training.To.ToString("yyyy/MM/dd"), training.Place.Name);
            }
        }

        private void FilterBtn_Click(object sender, EventArgs e)
        {
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

            data.Rows.Clear();
            var trainings = service.GetAllWithNestedInclude(x => x.Include(y => y.TrainingType).Include(t => t.Place)).Where(x => x.Id.ToString().Contains(SearchTxt.Text) || x.Name.Contains(SearchTxt.Text) || x.TrainingType.Name.Contains(SearchTxt.Text) || x.Place.Name.Contains(SearchTxt.Text)).Where(p => p.ProgramId == EditedProgram.Id && p.From.Date >= startDate && p.To.Date <= endDate && (TrainingTypeBox.SelectedIndex < 1 || p.TrainingType.Name == TrainingTypeBox.Text));
            int i = 1;
            foreach (var training in trainings)
            {
                data.Rows.Add(i++, training.Id, training.Name, training.TrainingType.Name, training.From.ToString("yyyy/MM/dd"), training.To.ToString("yyyy/MM/dd"), training.Place.Name);
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (data.Rows.Count == 0)
            {
                UserMessages.Error("لا يوجد بيانات للطباعة");
                return;
            }

            // Create a new DataGridView with only visible columns
            var filteredGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            foreach (DataGridViewColumn col in data.Columns)
            {
                if (col.Visible )
                    filteredGrid.Columns.Add((DataGridViewColumn)col.Clone());
            }

            // Copy rows
            foreach (DataGridViewRow row in data.Rows)
            {
                if (!row.IsNewRow)
                {
                    var newRowIndex = filteredGrid.Rows.Add();
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        if (data.Columns[i].Visible)
                        {
                            var targetIndex = filteredGrid.Columns
                                .Cast<DataGridViewColumn>()
                                .ToList()
                                .FindIndex(c => c.HeaderText == data.Columns[i].HeaderText);

                            filteredGrid.Rows[newRowIndex].Cells[targetIndex].Value = row.Cells[i].Value;
                        }
                    }
                }
            }

            // Titles
            var Main = $"تقرير بالتدريبات الخاصة " + EditedProgram.Name;
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

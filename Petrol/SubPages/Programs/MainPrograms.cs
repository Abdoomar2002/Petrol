using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Programs
{
    public partial class MainPrograms : UserControl
    {
        private ProgramService service = new ProgramService();
        public MainPrograms()
        {
            InitializeComponent();
            service = new ProgramService();
            LoadData();
            DataGridViewHelper.FixIndexColumnSorting(ProgramsData);
        }

        private void AddProgramBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Add");
        }
        public void LoadData()
        {



            ProgramsData.Rows.Clear();
            var programs = service.GetAllWithInclude(x => x.ProgramType);
            int i = 1;
            foreach (var program in programs)
            {
                ProgramsData.Rows.Add(i++, program.Id, program.Name, program.ProgramType.Type);
            }
        }

        private void ProgramsData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var programId = (int)(ProgramsData.Rows[e.RowIndex].Cells[1].Value ?? 0);
            if (programId == 0) return;

            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Edit", programId);
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            ProgramsData.Rows.Clear();
            var programs = service.GetAllWithInclude(x => x.ProgramType).Where(x => x.Id.ToString().Contains(SearchTxt.Text) || x.Name.Contains(SearchTxt.Text) || x.ProgramType.Type.Contains(SearchTxt.Text));
            int i = 1;
            foreach (var program in programs)
            {
                ProgramsData.Rows.Add(i++, program.Id, program.Name, program.ProgramType.Type);
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (ProgramsData.Rows.Count == 0)
            {
                UserMessages.Error("لا يوجد بيانات للطباعة");
                return;
            }

            // Create a new DataGridView with only visible columns
            var filteredGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            foreach (DataGridViewColumn col in ProgramsData.Columns)
            {
                if (col.Visible )
                    filteredGrid.Columns.Add((DataGridViewColumn)col.Clone());
            }

            // Copy rows
            foreach (DataGridViewRow row in ProgramsData.Rows)
            {
                if (!row.IsNewRow)
                {
                    var newRowIndex = filteredGrid.Rows.Add();
                    for (int i = 0; i < ProgramsData.Columns.Count; i++)
                    {
                        if (ProgramsData.Columns[i].Visible)
                        {
                            var targetIndex = filteredGrid.Columns
                                .Cast<DataGridViewColumn>()
                                .ToList()
                                .FindIndex(c => c.HeaderText == ProgramsData.Columns[i].HeaderText);

                            filteredGrid.Rows[newRowIndex].Cells[targetIndex].Value = row.Cells[i].Value;
                        }
                    }
                }
            }

            // Titles
            var Main = $"تقرير البرامج";
            var sub = $"نتيجة البحث عن {SearchTxt.Text}";
            //  var filteredGridTitle = $"تدريبات ذات نوع {TrainingTypeBox.Text} من {StartDate.Value.ToString("dd/MM/yyyy")} إلى {EndDate.Value.ToString("dd/MM/yyyy")}";
            // Pass filtered grid
            PdfGenerator.GeneratePdf(Main, sub, "", filteredGrid);

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

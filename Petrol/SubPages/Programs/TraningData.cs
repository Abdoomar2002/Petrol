using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
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
            EditedProgram = programService.GetAllWithNestedInclude(x => x.Include(y=>y.Trainings).ThenInclude(t=>t.ProgramType).Include(t=>t.Trainings).ThenInclude(y=>y.Place))?.FirstOrDefault(t => t.Id == id)??null;
            data.Rows.Clear();
            int i = 1;
            foreach (var training in EditedProgram.Trainings)
            {
                data.Rows.Add(i++, training.Id, training.Name, training.ProgramType.Type, training.From.ToString("yyyy/MM/dd"), training.To.ToString("yyyy/MM/dd"),training.Place.Name);
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Edit",EditedProgram.Id);
        }

        private void data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var id = (int)(data.Rows[e.RowIndex].Cells[1].Value ?? 0);
            if (id == 0) return;
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Edit Training",id,EditedProgram.Id);
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            data.Rows.Clear();
            var trainings = service.GetAllWithNestedInclude(x => x.Include(y => y.ProgramType).Include(t => t.Place)).Where(x => x.Id.ToString().Contains(SearchTxt.Text) || x.Name.Contains(SearchTxt.Text) || x.ProgramType.Type.Contains(SearchTxt.Text) || x.Place.Name.Contains(SearchTxt.Text)).Where(p=>p.ProgramId==EditedProgram.Id);
            int i = 1;
            foreach (var training in trainings)
            {
                data.Rows.Add(i++, training.Id, training.Name, training.ProgramType.Type, training.From.ToString("yyyy/MM/dd"), training.To.ToString("yyyy/MM/dd"), training.Place.Name);
            }
        }

        private void FilterBtn_Click(object sender, EventArgs e)
        {
            data.Rows.Clear();
            var trainings = service.GetAllWithNestedInclude(x => x.Include(y => y.ProgramType).Include(t => t.Place)).Where(x => x.Id.ToString().Contains(SearchTxt.Text) || x.Name.Contains(SearchTxt.Text) || x.ProgramType.Type.Contains(SearchTxt.Text) || x.Place.Name.Contains(SearchTxt.Text)).Where(p => p.ProgramId == EditedProgram.Id&&p.From.Date>=StartDate.Value.Date&&p.To<=EndDate.Value.Date&&(ProgramTypeBox.SelectedIndex==-1||p.ProgramType.Type==ProgramTypeBox.Text));
            int i = 1;
            foreach (var training in trainings)
            {
                data.Rows.Add(i++, training.Id, training.Name, training.ProgramType.Type, training.From.ToString("yyyy/MM/dd"), training.To.ToString("yyyy/MM/dd"), training.Place.Name);
            }
        }
    }
}

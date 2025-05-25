using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Finances
{
    public partial class ProgramCost : UserControl
    {
        private FollowingReportService followingReportService;
        private ProgramService programService;
        private IEnumerable<Models.Program> Programs;
        public ProgramCost()
        {
            InitializeComponent();
        }
        public void LoadData() 
        {  
            programService = new ProgramService();
            followingReportService = new FollowingReportService();
            Programs = programService.GetAllWithNestedInclude(x => x.Include(y => y.Trainings).ThenInclude(t => t.TrainingType).Include(y => y.Trainings).ThenInclude(t => t.Place).Include(r=>r.ProgramType));
            ProgramIdTxt.AutoCompleteCustomSource.AddRange(Programs.Select(x=>x.Id.ToString()).ToArray());
            ProgramNameTxt.AutoCompleteCustomSource.AddRange(Programs.Select(x=>x.Name).ToArray());
            var Departments = new DepartmentService().GetAll<Department>();
            RangeBox.Items.Clear();
            RangeBox.Items.Add("كل الشركة");
            RangeBox.Items.AddRange(Departments.Select(x => x.Name).ToArray());
            RangeBox.SelectedIndex = 0;

        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.FinanceNavigation("Main");
        }

        private void ProgramNameTxt_TextChanged(object sender, EventArgs e)
        {
            var program=Programs.FirstOrDefault(x=>x.Name==ProgramNameTxt.Text);
            if(program != null) 
            {
                ProgramIdTxt.Text = program.Id.ToString();
                ProgramTypeBox.SelectedItem=program.ProgramType.Type;

            }
            else 
            {
                ProgramIdTxt.Text = string.Empty;
                ProgramTypeBox.SelectedIndex = -1;
            }
        }

        private void ProgramIdTxt_TextChanged(object sender, EventArgs e)
        {
            var program = Programs.FirstOrDefault(x => x.Id.ToString() == ProgramIdTxt.Text);
            if (program != null)
            {
                ProgramNameTxt.Text = program.Name;
                ProgramTypeBox.SelectedItem = program.ProgramType.Type;

            }
            else
            {
                ProgramNameTxt.Text = string.Empty;
                ProgramTypeBox.SelectedIndex = -1;
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {

            // check the program is exist in the db
            var program = Programs.FirstOrDefault(x => x.Id.ToString() == ProgramIdTxt.Text);
            if (program == null) 
            {
                UserMessages.Error("لا يوجد برنامج بهذا الاسم");
                return;
            }
            TrainingData.Rows.Clear();
            var trainingIds=program.Trainings.Select(x=>x.Id).ToList();
            if(RangeBox.SelectedIndex > 0)
                trainingIds=program.Trainings.Where(x=>x.DepartmentName==RangeBox.SelectedItem).Select(x=>x.Id).ToList();
            Dictionary<int,double>trainingCost=followingReportService.GetAll<Models.FollowingReport>().Where(r=>trainingIds.Contains(r.TrainingId)).ToDictionary(z=>z.TrainingId,q=>q.TotalCost);
            var i = 1;
            foreach (var training in program.Trainings)
            {

                TrainingData.Rows.Add(i++, training.Id, training.Name, training.TrainingType.Name,
                training.From.ToString("yyyy/MM/dd"), training.To.ToString("yyyy/MM/dd"), training.Place.Name,trainingCost.ContainsKey(training.Id)? trainingCost?[training.Id] ?? 0:0);
            }
        }
    }
}

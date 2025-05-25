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
    public partial class GeneralCost : UserControl
    {
        private ProgramTypeService programTypeService;
        public GeneralCost()
        {
            InitializeComponent();
            programTypeService = new ProgramTypeService();
        }
        public void LoadData() 
        {
            var Departments = new DepartmentService().GetAll<Department>();
            RangeBox.Items.Clear();
            RangeBox.Items.Add("كل الشركة");
            RangeBox.Items.AddRange(Departments.Select(r=>r.Name).ToArray());
            RangeBox.SelectedIndex = 0;
            var ProgramsTypes = programTypeService.GetAll<ProgramType>().Select(x => x.Type);
            var TrainingTypes = programTypeService.GetAll<TrainingType>().Select(x => x.Name);
            ProgramTypeBox.Items.Clear();
            ProgramTypeBox.Items.Add("كل الأنواع");
            ProgramTypeBox.Items.AddRange(ProgramsTypes.ToArray());
            ProgramTypeBox.SelectedIndex = 0;
            TrainingTypeBox.Items.Clear();
            TrainingTypeBox.Items.Add("كل الأنواع");
            TrainingTypeBox.Items.AddRange(TrainingTypes.ToArray());
            TrainingTypeBox.SelectedIndex = 0;



        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.FinanceNavigation("Main");
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (StartDate.Value > EndDate.Value) 
            {
                UserMessages.Error("يجب ان يكون التاريخ صحيح"); 
                return;
            }
            TrainingData.Rows.Clear();
            var ProgramsTypes = programTypeService.GetAllWithNestedInclude(x => x.Include(y => y.Trainings).ThenInclude(t => t.Place)
            .Include(y => y.Trainings).ThenInclude(t => t.TrainingType).Include(r=>r.Programs).ThenInclude(p=>p.Trainings));
            if (ProgramTypeBox.SelectedIndex > 0) 
            {
                ProgramsTypes = ProgramsTypes.Where(a => a.Type == ProgramTypeBox.Text);
            }
            if (TrainingTypeBox.SelectedIndex > 0) 
            {
                ProgramsTypes = ProgramsTypes.Where(a => a.Trainings.Any(t => t.TrainingType.Name == TrainingTypeBox.Text));
            }
            var fSeverices = new FollowingReportService();
            var placeSeverices = new PlaceService();
            foreach (var programType in ProgramsTypes) 
            {
                foreach (var train in programType.Programs) 
                {
                List<int> trainingIds;
                if (RangeBox.SelectedIndex > 0) 
                   trainingIds= train.Trainings.Where(z=>z.DepartmentName==RangeBox.Text).Select(a => a.Id).ToList();
                else 
                   trainingIds= train.Trainings.Select(a => a.Id).ToList();
                
                    Dictionary<int, double> Costs = fSeverices.GetAll<Models.FollowingReport>().Where(z => trainingIds.Contains(z.Id)).ToDictionary(z => z.TrainingId, u => u.TotalCost);
                var i = 1;
                    foreach(var training in train.Trainings)
                    if(training.From.Date>=StartDate.Value.Date&&training.To.Date<=EndDate.Value.Date)
                    TrainingData.Rows.Add(i++,training.Id,training.Name,training.TrainingType.Name,training.From.ToString("yyyy/MM/dd"),training.To.ToString("yyyy/MM/dd")
                        ,placeSeverices.GetById<Place>(training.PlaceId)?.Name??"", training.DepartmentName,Costs.ContainsKey(training.Id)? Costs?[training.Id]??0:0);
                }
            }
        }
    }
}

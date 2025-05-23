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
            .Include(y => y.Trainings).ThenInclude(t => t.ProgramType));
            if (ProgramTypeBox.SelectedIndex > 0) 
            {
                ProgramsTypes = ProgramsTypes.Where(a => a.Type == ProgramTypeBox.Text);
            }
            var fSeverices = new FollowingReportService();
            foreach (var programType in ProgramsTypes) 
            {
                List<int> trainingIds;
                if (RangeBox.SelectedIndex > 0) 
                   trainingIds= programType.Trainings.Where(z=>z.DepartmentName==RangeBox.Text).Select(a => a.Id).ToList();
                else 
                   trainingIds= programType.Trainings.Select(a => a.Id).ToList();
                
                    Dictionary<int, double> Costs = fSeverices.GetAll<Models.FollowingReport>().Where(z => trainingIds.Contains(z.Id)).ToDictionary(z => z.TrainingId, u => u.TotalCost);
                var i = 1;
                foreach (var training in programType.Trainings) 
                {
                    if(training.From.Date>=StartDate.Value.Date&&training.To.Date<=EndDate.Value.Date)
                    TrainingData.Rows.Add(i++,training.Id,training.Name,training.ProgramType.Type,training.From.ToString("yyyy/MM/dd"),training.To.ToString("yyyy/MM/dd")
                        , training.Place.Name, training.DepartmentName,Costs.Count>0? Costs?[training.Id]??0:0);
                }
            }
        }
    }
}

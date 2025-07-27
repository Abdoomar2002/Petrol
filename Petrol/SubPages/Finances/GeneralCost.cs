using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
using Petrol.SubPages.Employees;
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
        private FollowingReportService followingReportService;
        private DepartmentService deptService = new DepartmentService();
        public GeneralCost()
        {
            InitializeComponent();
            followingReportService = new FollowingReportService();
            deptService = new DepartmentService();
            programTypeService = new ProgramTypeService();
        }
        public void LoadData() 
        {
            var Departments = new DepartmentService().GetAll<Department>();
            RangeBox.Items.Clear();
            RangeBox.Items.Add("كل الشركة");
            RangeBox.Items.AddRange(Departments.Select(r=>r.Name).Distinct().ToArray());
            RangeBox.SelectedIndex = 0;
            var ProgramsTypes = programTypeService.GetAll<ProgramType>().Select(x => x.Type).ToList();
            var TrainingTypes = programTypeService.GetAll<TrainingType>().Select(x => x.Name).ToList();
            ProgramsTypes.ForEach(z => Helper.Normalize(z));
            TrainingTypes.ForEach(z => Helper.Normalize(z));
           
            
            ProgramTypeBox.Items.Clear();
            ProgramTypeBox.Items.Add("كل الأنواع");
            ProgramTypeBox.Items.AddRange(ProgramsTypes.Distinct().ToArray());
            ProgramTypeBox.SelectedIndex = 0;
            TrainingTypeBox.Items.Clear();
            TrainingTypeBox.Items.Add("كل الأنواع");
            TrainingTypeBox.Items.AddRange(TrainingTypes.Distinct().ToArray());
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
            .Include(y => y.Trainings).ThenInclude(t => t.TrainingType).Include(r=>r.Programs).ThenInclude(p=>p.Trainings).ThenInclude(z=>z.TrainingType)).ToList();
           
            var fSeverices = new FollowingReportService();
            var placeSeverices = new PlaceService();
            var count= ProgramsTypes.Count();
            var list=ProgramsTypes.SelectMany(z=>z.Programs).SelectMany(y=>y.Trainings).ToList();
            var i = 1;
            List<int> trainingIds;
            if (RangeBox.SelectedIndex > 0)
            {
                var dept = deptService.FindDepartmentByName(RangeBox.Text);
                var allFollowingReports = followingReportService.GetAllWithInclude(z => z.DepartmentsPresenceNumber).ToList();
                var realNumber = allFollowingReports.Where(x => x.DepartmentsPresenceNumber.Where(z => z.DepartmentId == dept.Id && z.PresenceNumber > 0).Any()).ToList();
                trainingIds = realNumber.Select(x => x.TrainingId).ToList();
            }
            else
                trainingIds = list.Select(a => a.Id).ToList();

            var reports = fSeverices.GetAllWithInclude(x=>x.Training).Where(z => trainingIds.Contains(z.Id)&& z.Training.From.Date >= StartDate.Value.Date && z.Training.To.Date <= EndDate.Value.Date);
            var dic = reports.Select(z => new List<double> { z.TrainingId, z.TotalCost }).ToList();
            Dictionary<double, double> Costs = new Dictionary<double, double>();
            foreach (var report in dic)
            {
                Costs[report[0]] = report[1];
            }
            foreach (var training in list)
            {
                var cost = Costs.ContainsKey(training.Id) ? Costs?[training.Id] ?? 0 : 0;
                if (RangeBox.SelectedIndex > 0)
                {
                    var dept = deptService.FindDepartmentByName(RangeBox.Text);

                    var trainersNumbers = reports.Where(z => z.TrainingId == training.Id).FirstOrDefault();
                    var totalNumber = trainersNumbers.Men + trainersNumbers.Women;
                    var deptNumber = trainersNumbers.DepartmentsPresenceNumber.Where(z => z.DepartmentId == dept.Id).FirstOrDefault();
                    cost = cost * deptNumber?.PresenceNumber / totalNumber ?? 0;
                }

                if (ProgramTypeBox.SelectedIndex > 0)
                {
                    if (Helper.Normalize(training.Program.ProgramType.Type) != ProgramTypeBox.Text) continue;
                }
                if (TrainingTypeBox.SelectedIndex > 0)
                {
                    if (Helper.Normalize(training.TrainingType.Name) != TrainingTypeBox.Text) continue;
                }
                if (!(training.From.Date >= StartDate.Value.Date && training.To.Date <= EndDate.Value.Date))
                    continue;

                        TrainingData.Rows.Add(i++, training.Id, training.Name, training.TrainingType.Name, training.From.ToString("yyyy/MM/dd"), training.To.ToString("yyyy/MM/dd")
                            , placeSeverices.GetById<Place>(training.PlaceId)?.Name ?? "", training.DepartmentName,cost);

                /*     List<int> trainingIds;
                     if (RangeBox.SelectedIndex > 0)
                     {
                         var dept = deptService.FindDepartmentByName(RangeBox.Text);
                         var allFollowingReports = followingReportService.GetAllWithInclude(z => z.DepartmentsPresenceNumber).ToList();
                         var realNumber = allFollowingReports.Where(x => x.DepartmentsPresenceNumber.Where(z => z.DepartmentId == dept.Id && z.PresenceNumber > 0).Any()).ToList();
                         trainingIds = realNumber.Select(x => x.TrainingId).ToList();
                     }
                     else
                         trainingIds = train.Trainings.Select(a => a.Id).ToList();

                     var reports = fSeverices.GetAll<Models.FollowingReport>().Where(z => trainingIds.Contains(z.Id));
                 var dic=    reports.Select(z => new List<double> { z.TrainingId, z.TotalCost }).ToList();
                     Dictionary<double, double> Costs = new Dictionary<double, double>();
                     foreach(var report in dic)
                     {
                         Costs[report[0]] = report[1];
                     }

                     foreach(var training in train.Trainings)
                     if(training.From.Date>=StartDate.Value.Date&&training.To.Date<=EndDate.Value.Date)
                     TrainingData.Rows.Add(i++,training.Id,training.Name,training.TrainingType.Name,training.From.ToString("yyyy/MM/dd"),training.To.ToString("yyyy/MM/dd")
                         ,placeSeverices.GetById<Place>(training.PlaceId)?.Name??"", training.DepartmentName,Costs.ContainsKey(training.Id)? Costs?[training.Id]??0:0);
                 */


            }

        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (TrainingData.Rows.Count == 0)
            {
                UserMessages.Error("لا يوجد بيانات للطباعة");
                return;
            }

            // Create a new DataGridView with only visible columns
            var filteredGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            foreach (DataGridViewColumn col in TrainingData.Columns)
            {
                if (col.Visible && !(col.ValueType is DataGridViewImageCell))
                    filteredGrid.Columns.Add((DataGridViewColumn)col.Clone());
            }

            // Copy rows
            foreach (DataGridViewRow row in TrainingData.Rows)
            {
                if (!row.IsNewRow)
                {
                    var newRowIndex = filteredGrid.Rows.Add();
                    for (int i = 0; i < TrainingData.Columns.Count; i++)
                    {
                        if (TrainingData.Columns[i].Visible)
                        {
                            var targetIndex = filteredGrid.Columns
                                .Cast<DataGridViewColumn>()
                                .ToList()
                                .FindIndex(c => c.HeaderText == TrainingData.Columns[i].HeaderText);

                            filteredGrid.Rows[newRowIndex].Cells[targetIndex].Value = row.Cells[i].Value;
                        }
                    }
                }
            }

            // Titles
            var Main = $"تقرير تكلفة عامة";
            var sub = $"نتيجة البحث عن {ProgramTypeBox.Text}" ;
            var filteredGridTitle = $"تدريبات ذات نوع {TrainingTypeBox.Text} من {StartDate.Value.ToString("dd/MM/yyyy")} إلى {EndDate.Value.ToString("dd/MM/yyyy")}";
            // Pass filtered grid
            PdfGenerator.GeneratePdf(Main, sub, filteredGridTitle, filteredGrid);

        }
    }
}

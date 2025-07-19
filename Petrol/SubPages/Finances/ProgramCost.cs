using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using Xceed.Document.NET;

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
            var names = Programs.Select(x => x.ProgramType.Type).Distinct().ToArray();
            ProgramTypeBox.Items.Clear();
            ProgramTypeBox.Items.Add("كل الأنواع");
            ProgramTypeBox.Items.AddRange(names);
            

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
            var reports=followingReportService.GetAll<Models.FollowingReport>().Where(r => trainingIds.Contains(r.TrainingId));
           
            var dic=reports.Select(z => new List<double> { z.TrainingId,z.TotalCost }).ToList();
            Dictionary<double,double>trainingCost=new Dictionary<double,double>();
            foreach (var report in dic) {
                trainingCost[report[0]]= report[1];
            }
            var i = 1;
            foreach (var training in program.Trainings)
            {

                TrainingData.Rows.Add(i++, training.Id, training.Name, training.TrainingType.Name,
                training.From.ToString("yyyy/MM/dd"), training.To.ToString("yyyy/MM/dd"), training.Place.Name,trainingCost.ContainsKey(training.Id)? trainingCost?[training.Id] ?? 0:0);
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
            var Main = $"تقرير تكلفة برنامج";
            var sub = $"نتيجة البحث عن {ProgramNameTxt.Text} ولتخصص {ProgramTypeBox.Text}";
            var filteredGridTitle = $"تدريبات ضمن  {RangeBox.Text}";
            // Pass filtered grid
            PdfGenerator.GeneratePdf(Main, sub, filteredGridTitle, filteredGrid);

        }

    }
}

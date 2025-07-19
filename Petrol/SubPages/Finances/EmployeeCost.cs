using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
using Petrol.SubPages.Employees;
using Petrol.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using Xceed.Document.NET;

namespace Petrol.SubPages.Finances
{
    public partial class EmployeeCost : UserControl
    {
        private FollowingReportService followingReportService;
        private EmployeeService employeeService;
        private IEnumerable<Employee> Employees;
        
        
        public EmployeeCost()
        {
            InitializeComponent();
        }
        public void LoadData() 
        {
             employeeService = new EmployeeService();
            followingReportService = new FollowingReportService();
            Employees = employeeService.GetAllWithNestedInclude(x => x.Include(y => y.Trainings).ThenInclude(t => t.Training).ThenInclude(p => p.Place)
            .Include(y => y.Trainings).ThenInclude(t => t.Training).ThenInclude(u => u.TrainingType)
            );
            EmployeeNameTxt.AutoCompleteCustomSource.AddRange(Employees.Select(x=>x.Name).ToArray());
            EmployeeFinanceNumberTxt.AutoCompleteCustomSource.AddRange(Employees.Select(x=>x.Id.ToString()).ToArray());
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.FinanceNavigation("Main");
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            //check the employee exist
            var employee = Employees.FirstOrDefault(x => x.FinanceNumber == EmployeeFinanceNumberTxt.Text);
            if (employee == null) 
            {
                UserMessages.Error("لا يوجد موظف بهذا الاسم"); return;
            }
            var trainingIds=employee.Trainings.Select(x => x.TrainingId).ToArray(); 
            Dictionary<int,double>trainingCost=followingReportService.GetAll<Models.FollowingReport>().Where(c=>trainingIds.Contains(c.TrainingId)).ToDictionary(z=>z.TrainingId,r=>r.TotalCost/(r.Men+r.Women));
            TrainingData.Rows.Clear();
            var i = 1;
            foreach (var training in employee.Trainings)
            {

                TrainingData.Rows.Add(i++, training.TrainingId, training.Training.Name, training.Training.TrainingType.Name,
                    training.Training.From.ToString("yyyy/MM/dd"), training.Training.To.ToString("yyyy/MM/dd"), training.Training.Place.Name,trainingCost.ContainsKey(training.Id) ? trainingCost?[training.TrainingId]??0:0);
            }

        }

        private void EmployeeNameTxt_TextChanged(object sender, EventArgs e)
        {
            var employee = Employees.FirstOrDefault(x => x.Name == EmployeeNameTxt.Text);
            if (employee != null) 
            {
                EmployeeFinanceNumberTxt.Text = employee.FinanceNumber;
            }
            else 
            {
                EmployeeFinanceNumberTxt.Text=string.Empty;
            }
        }

        private void EmployeeFinanceNumberTxt_TextChanged(object sender, EventArgs e)
        {
            var employee=Employees.FirstOrDefault(x=>x.FinanceNumber == EmployeeFinanceNumberTxt.Text);
            if (employee != null)
            {
                EmployeeNameTxt.Text = employee.Name;
            }
            else 
            {
                EmployeeNameTxt.Text=string.Empty;
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
            var Main = $"تقرير تكلفة الموظف ";
            var sub = TrainingData.Rows.Count - 1 != employeeService.GetAll<Employee>().Count() ? $"نتيجة البحث عن {EmployeeNameTxt.Text}" : "جميع الموظفين";
           // var filteredGridTitle = $"تدريبات ذات نوع {TrainingTypeBox.Text} من {StartDate.Value.ToString("dd/MM/yyyy")} إلى {EndDate.Value.ToString("dd/MM/yyyy")}";
            // Pass filtered grid
            PdfGenerator.GeneratePdf(Main, sub, "", filteredGrid);

        }
    }
}

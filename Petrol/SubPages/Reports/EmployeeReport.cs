using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Reports
{
    public partial class EmployeeReport : UserControl
    {
        private ProgramService service;
        List<Models.Program> Programs;
        public EmployeeReport()
        {
            InitializeComponent();
            service = new ProgramService();
        }
        public void LoadData()
        {
            Programs = service.GetAllWithInclude(x => x.ProgramType,y=>y.Trainings).ToList();
            ProgramNameTxt.AutoCompleteCustomSource.AddRange(Programs.Select(c => c.Name).ToArray());
            ProgramIdTxt.AutoCompleteCustomSource.AddRange(Programs.Select(c => c.Id.ToString()).ToArray());
            var Departments = service.GetAll<Models.Department>();
            RangeBox.Items.Clear();
            RangeBox.Items.Add("كل الشركة");
            RangeBox.Items.AddRange(Departments.Select(x => x.Name).ToArray());
            RangeBox.SelectedIndex = 0;
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ReportsNavigation("Main");
        }

        private void NotTakers_CheckedChanged(object sender, EventArgs e)
        {
            if (NotTakers.Checked)
            {
                EmployeeData.Columns[4].Visible = false;
                EmployeeData.Columns[5].Visible = false;
                EmployeeData.Columns[6].Visible = false;

            }
            else
            {
                EmployeeData.Columns[4].Visible = true;
                EmployeeData.Columns[5].Visible = true;
                EmployeeData.Columns[6].Visible = true;

            }
        }

        private void ProgramNameTxt_TextChanged(object sender, EventArgs e)
        {
            var program = Programs.FirstOrDefault(x => x.Name == ProgramNameTxt.Text);
            if (program != null)
            {
                ProgramIdTxt.Text = program.Id.ToString();
                ProgramTypeBox.SelectedItem = program.ProgramType.Type;
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
            
            if (NotTakers.Checked == Takers.Checked)
            {
                UserMessages.Error("يجب ان تختار بين حاصلين وغير حاصلين أولا");
                return;
            }
            var program = Programs.FirstOrDefault(x => x.Id.ToString() == ProgramIdTxt.Text);
            if (program == null)
            {
                UserMessages.Error("يجب ان تختار برنامج اولا");
                return;
            }
            EmployeeData.Rows.Clear();
            if (Takers.Checked == true)
            {
                var trainings = new EmployeeTrainingService().GetAllWithNestedInclude(x => x.Include(t => t.Training).ThenInclude(p => p.Place).Include(l => l.Employee)).Where(x => x.Training.ProgramId == program.Id).ToList();
                if (RangeBox.SelectedIndex > 0)
                {
                    trainings = trainings.Where(x => x.Training.DepartmentName == RangeBox.Text).ToList();
                }
                var i = 1;
                foreach (var training in trainings)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(EmployeeData, i++, training.Employee.FinanceNumber, training.Employee.Name, training.Employee.DepartmentName, training.Training.Place.Name, training.Training.From.ToString("yyyy/MM/dd"), training.Training.To.ToString("yyyy/MM/dd"));
                    EmployeeData.Rows.Add(row);
                }
            }
            else 
            {
                var trainingsId = program.Trainings.Select(x => x.Id).ToList();
                //get all employees those were not taking these training
                var employees = new EmployeeService().GetAllWithInclude(x => x.Trainings).ToList();
                if (RangeBox.SelectedIndex > 0)
                {
                    employees = employees.Where(x => x.DepartmentName == RangeBox.Text).ToList();
                }
                employees = employees.Where(r => !r.Trainings.Where(x => trainingsId.Contains(x.TrainingId)).Any()).ToList();
                var i = 1;
                foreach (var employee in employees)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(EmployeeData, i++, employee.FinanceNumber, employee.Name, employee.DepartmentName);
                    EmployeeData.Rows.Add(row);
                }
            }
        }
    }
}

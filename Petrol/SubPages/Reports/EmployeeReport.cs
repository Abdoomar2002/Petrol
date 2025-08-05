using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
using Petrol.SubPages.Programs;
using Petrol.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using Xceed.Document.NET;

namespace Petrol.SubPages.Reports
{
    public partial class EmployeeReport : UserControl
    {
        private ProgramService service;
        List<Models.Program> Programs;
        private DepartmentService deptService;
        private bool isProgramming = false;
        public EmployeeReport()
        {
            InitializeComponent();
            deptService = new DepartmentService();
            service = new ProgramService();
            DataGridViewHelper.FixIndexColumnSorting(EmployeeData);
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
            var names = Programs.Select(x => x.ProgramType.Type).Distinct().ToArray();
            ProgramTypeBox.Items.Clear();
            ProgramTypeBox.Items.Add("كل الأنواع");
            ProgramTypeBox.Items.AddRange(names);
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
            if (!isProgramming)
            {
                var program = Programs.FirstOrDefault(x => x.Name == ProgramNameTxt.Text);
                if (program != null)
                {
                    isProgramming = true;
                    ProgramIdTxt.Text = program.Id.ToString();
                    ProgramTypeBox.SelectedItem = program.ProgramType.Type;
                    isProgramming = false;
                }
                else
                {
                    isProgramming = true;
                    ProgramIdTxt.Text = string.Empty;
                    ProgramTypeBox.SelectedIndex = -1;
                    isProgramming = false;
                }
            }
        }

        private void ProgramIdTxt_TextChanged(object sender, EventArgs e)
        {
            if (!isProgramming)
            {
                var program = Programs.FirstOrDefault(x => x.Id.ToString() == ProgramIdTxt.Text);
                if (program != null)
                {
                    isProgramming = true;
                    ProgramNameTxt.Text = program.Name;
                    ProgramTypeBox.SelectedItem = program.ProgramType.Type;
                    isProgramming = false;
                }
                else
                {
                    isProgramming = true;
                    ProgramNameTxt.Text = string.Empty;
                    ProgramTypeBox.SelectedIndex = -1;
                    isProgramming = false;
                }
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
                    var dept = deptService.FindDepartmentByName(RangeBox.Text);
                    trainings = trainings.Where(x => x.Employee.DepartmentId == dept.Id).GroupBy(x => x.EmployeeId).Select(x => x.FirstOrDefault()).ToList();
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
                    // get the department 
                    var dept = deptService.FindDepartmentByName(RangeBox.Text);
                    employees = employees.Where(x => x.DepartmentId == dept.Id).ToList();
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
        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (EmployeeData.Rows.Count == 0)
            {
                UserMessages.Error("لا يوجد بيانات للطباعة");
                return;
            }

            // Create a new DataGridView with only visible columns
            var filteredGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            foreach (DataGridViewColumn col in EmployeeData.Columns)
            {
                if (col.Visible )
                    filteredGrid.Columns.Add((DataGridViewColumn)col.Clone());
            }

            // Copy rows
            foreach (DataGridViewRow row in EmployeeData.Rows)
            {
                if (!row.IsNewRow)
                {
                    var newRowIndex = filteredGrid.Rows.Add();
                    for (int i = 0; i < EmployeeData.Columns.Count; i++)
                    {
                        if (EmployeeData.Columns[i].Visible)
                        {
                            var targetIndex = filteredGrid.Columns
                                .Cast<DataGridViewColumn>()
                                .ToList()
                                .FindIndex(c => c.HeaderText == EmployeeData.Columns[i].HeaderText);

                            filteredGrid.Rows[newRowIndex].Cells[targetIndex].Value = row.Cells[i].Value;
                        }
                    }
                }
            }

            // Titles
            var Main = Takers.Checked?Takers.Text.ToString():NotTakers.Text.ToString();
            var sub = $"نتيجة البحث عن {ProgramNameTxt.Text}";
            var filteredGridTitle = $"تدريبات ذات نوع {ProgramTypeBox.Text} ضمن {RangeBox.Text}";
            // Pass filtered grid
            PdfGenerator.GeneratePdf(Main, sub, filteredGridTitle, filteredGrid);

        }

    }
}



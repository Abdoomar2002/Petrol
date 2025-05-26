using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Employees
{
    public partial class EmployeeData : UserControl
    {
        private EmployeeService service;
        private Employee EditedEmployee;
        public EmployeeData()
        {
            InitializeComponent();
            service = new EmployeeService();
        }
        public void SetEmployeeId(int id)
        {
            var employee = service.GetAllWithNestedInclude(x => x.Include(y => y.Trainings).ThenInclude(u => u.Training).ThenInclude(t =>  t.Place).Include(y=>y.Trainings).ThenInclude(u => u.Training).ThenInclude(t => t.TrainingType)).FirstOrDefault(x => x.Id == id);
            if (employee == null) return;
            EditedEmployee = employee;
            Data.Rows.Clear();
            var types = service.GetAll<TrainingType>().Select(x => x.Name).ToArray();
            TrainingTypeBox.Items.Clear();
            TrainingTypeBox.Items.Add("كل الأنواع");
            TrainingTypeBox.Items.AddRange(types);
            var i = 1;
            foreach (var training in employee.Trainings)
            {
                Data.Rows.Add(i++, training.Training.Id, training.Training.Name, training.Training.From.ToString("dd/MM/yyyy"), training.Training.To.ToString("dd/MM/yyyy"), training.Training.Place.Name, Properties.Resources.delete);
            }


        }
        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.EmployeeNavigation("Edit",EditedEmployee.Id);
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            var searchText = SearchTxt.Text;
            if (string.IsNullOrEmpty(searchText))
            {
                SetEmployeeId(EditedEmployee.Id);
                return;
            }
            Data.Rows.Clear();
            var i = 1;
            foreach (var training in EditedEmployee.Trainings.Where(x => x.Training.Name.Contains(searchText) || x.Training.Id.ToString().Contains(searchText) || x.Training.Place.Name.Contains(searchText)))
            {
                Data.Rows.Add(i++, training.Training.Id, training.Training.Name, training.Training.From.ToString("dd/MM/yyyy"), training.Training.To.ToString("dd/MM/yyyy"), training.Training.Place.Name, Properties.Resources.delete);
            }
        }



        private void Filter_Click(object sender, EventArgs e)
        {
            var searchText = SearchTxt.Text;
            Data.Rows.Clear();
            var i = 1;
            var Searchresult = EditedEmployee.Trainings.Where(x => x.Training.Name.Contains(searchText) || x.Training.Id.ToString().Contains(searchText) || x.Training.Place.Name.Contains(searchText));
            if (Searchresult.Count() == 0)
            {
                UserMessages.Error("لا توجد نتائج");
                return;
            }
            var result = Searchresult.Where(x => x.Training.From.Date >= StartDate.Value.Date && x.Training.To.Date <= EndDate.Value.Date).Where(z => TrainingTypeBox.SelectedIndex < 1 || z.Training.TrainingType.Name == TrainingTypeBox.Text);
            foreach (var training in result)
            {
                Data.Rows.Add(i++, training.Training.Id, training.Training.Name, training.Training.From.ToString("dd/MM/yyyy"), training.Training.To.ToString("dd/MM/yyyy"), training.Training.Place.Name, Properties.Resources.delete);
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (Data.Rows.Count == 0)
            {
                UserMessages.Error("لا يوجد بيانات للطباعة");
                return;
            }

            // Create a new DataGridView with only visible columns
            var filteredGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            foreach (DataGridViewColumn col in Data.Columns)
            {
                if (col.Visible|| !(col.ValueType  is  DataGridViewImageCell))
                    filteredGrid.Columns.Add((DataGridViewColumn)col.Clone());
            }

            // Copy rows
            foreach (DataGridViewRow row in Data.Rows)
            {
                if (!row.IsNewRow)
                {
                    var newRowIndex = filteredGrid.Rows.Add();
                    for (int i = 0; i < Data.Columns.Count; i++)
                    {
                        if (Data.Columns[i].Visible)
                        {
                            var targetIndex = filteredGrid.Columns
                                .Cast<DataGridViewColumn>()
                                .ToList()
                                .FindIndex(c => c.HeaderText ==     Data.Columns[i].HeaderText);

                            filteredGrid.Rows[newRowIndex].Cells[targetIndex].Value = row.Cells[i].Value;
                        }
                    }
                }
            }

            // Titles
            var Main = $"تقرير تدريبات الموظف / {EditedEmployee.Name}";
            var sub = Data.Rows.Count - 1 != EditedEmployee.Trainings.Count ? $"نتيجة البحث عن {SearchTxt.Text}" : "جميع التدريبات";
            var filteredGridTitle = $"تدريبات ذات نوع {TrainingTypeBox.Text} من {StartDate.Value.ToString("dd/MM/yyyy")} إلى {EndDate.Value.ToString("dd/MM/yyyy")}";
            // Pass filtered grid
            PdfGenerator.GeneratePdf(Main, sub, filteredGridTitle, filteredGrid);

        }
    }
}

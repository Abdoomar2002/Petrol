using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Employees
{
    public partial class EmployeeData : UserControl
    {
        private EmployeeService service;
        private Employee EditedEmployee;
        private List<EmployeeTraining> trainings;
        public EmployeeData()
        {
            InitializeComponent();
            service = new EmployeeService();
        }
        public void SetEmployeeId(int id)
        {
            EditedEmployee = null;
           EditedEmployee = service.GetAllWithInclude().FirstOrDefault(x => x.Id == id);
            if (EditedEmployee == null) return;
            Data.Rows.Clear();
            trainings = new EmployeeTrainingService().GetAllWithNestedInclude(z=>z.Include(y=>y.Training).ThenInclude(t => t.Place).Include(u => u.Training).ThenInclude(t => t.TrainingType))
    .Include(u => u.Training)
    .Where(x => x.EmployeeId == EditedEmployee.Id)
    .ToList();
            var types = service.GetAll<TrainingType>().Select(x => x.Name).Distinct().ToArray();
            TrainingTypeBox.Items.Clear();
            TrainingTypeBox.Items.Add("كل الأنواع");
            TrainingTypeBox.Items.AddRange(types);
            var i = 1;
            foreach (var training in trainings.OrderBy(x=>x.Training.From))
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
            var searchText =Helper.Normalize(SearchTxt.Text);
            if (string.IsNullOrEmpty(searchText))
            {
                SetEmployeeId(EditedEmployee.Id);
                return;
            }
            Data.Rows.Clear();
            var i = 1;
            foreach (var training in trainings.Where(x =>Helper.Normalize(x.Training.Name).Contains(searchText) || x.Training.Id.ToString().Contains(searchText) ||Helper.Normalize(x.Training.Place.Name).Contains(searchText)))
            {
                Data.Rows.Add(i++, training.Training.Id, training.Training.Name, training.Training.From.ToString("dd/MM/yyyy"), training.Training.To.ToString("dd/MM/yyyy"), training.Training.Place.Name, Properties.Resources.delete);
            }
        }



        private void Filter_Click(object sender, EventArgs e)
        {
            var searchText = SearchTxt.Text;
            Data.Rows.Clear();
            var i = 1;
            var Searchresult = EditedEmployee.Trainings.Where(x => x.Training.Name.Contains(searchText) || x.Training.Id.ToString().Contains(searchText) || x.Training.Place.Name.Contains(searchText)).OrderBy(x=>x.Training.From);
            if (Searchresult.Count() == 0)
            {
                UserMessages.Error("لا توجد نتائج");
                return;
            }
            if (!DateValidator.IsValidDate(StartDate.Text))
            {
                UserMessages.Error("يرجى إدخال تاريخ البداية بالصيغة الصحيحة dd/MM/yyyy");
                StartDate.Focus();
                return;
            }

            if (!DateValidator.IsValidDate(EndDate.Text))
            {
                UserMessages.Error("يرجى إدخال تاريخ النهاية بالصيغة الصحيحة dd/MM/yyyy");
                EndDate.Focus();
                return;
            }

            var startDate = DateValidator.ParseDate(StartDate.Text).Value.Date;
            var endDate = DateValidator.ParseDate(EndDate.Text).Value.Date;
            if (startDate > endDate)
            {
                UserMessages.Error("يجب أن يكون تاريخ البداية اصغر من تاريخ النهاية");
                return;
            }

            var result = Searchresult.Where(x => x.Training.From.Date >= startDate && x.Training.To.Date <= endDate).Where(z => TrainingTypeBox.SelectedIndex < 1 || z.Training.TrainingType.Name == TrainingTypeBox.Text);
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
            Data.Columns[6].Visible = false;
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
            var filteredGridTitle = $"تدريبات ذات نوع {TrainingTypeBox.Text} من {StartDate.Text} إلى {EndDate.Text}";
            // Pass filtered grid
            PdfGenerator.GeneratePdf(Main, sub, filteredGridTitle, filteredGrid);
            Data.Columns[6].Visible = true;

        }

        private void SearchTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchBtn.PerformClick();
            }
        }

        private void Data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //apply remove training data from trainingDataTable 
            if (e.RowIndex < 0 || e.ColumnIndex != 6) return; // Check if the clicked cell is in the delete column
            var warning = UserMessages.Warning("هل انت متاكد من انك تريد مسح التدريب");
            if(warning==DialogResult.Yes)
            {
                var trainingId = (int)Data.Rows[e.RowIndex].Cells[1].Value;
                var trainingService = new EmployeeTrainingService();
                var training = trainingService.GetAll<EmployeeTraining>().FirstOrDefault(x => x.TrainingId == trainingId && x.EmployeeId == EditedEmployee.Id);
                if(training==null)
                {
                    UserMessages.Error("لا يوجد تدريب بهذا المعرف\nستتم اعادة تحميل الصفحة للتحديث");
                    SetEmployeeId(EditedEmployee.Id);
                    return;
                }
                try
                {
                    trainingService.Delete(training);
                    trainingService.SaveChanges();
                    UserMessages.Info("تم حذف التدريب بنجاح");
                    SetEmployeeId(EditedEmployee.Id);
                }
                catch (Exception ex)
                {
                    UserMessages.Error("حدث خطأ أثناء حذف التدريب");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

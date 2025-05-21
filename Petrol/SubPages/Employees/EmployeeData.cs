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
            var employee = service.GetAllWithNestedInclude(x => x.Include(y => y.Trainings).ThenInclude(u => u.Training).ThenInclude(t =>  t.Place).Include(y=>y.Trainings).ThenInclude(u => u.Training).ThenInclude(t => t.Program)).FirstOrDefault(x => x.Id == id);
            if (employee == null) return;
            EditedEmployee = employee;
            Data.Rows.Clear();
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

        private void ProgramTypeBox_SelectedIndexChanged(object sender, EventArgs e)
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
            var result = Searchresult.Where(x => x.Training.From.Date >= StartDate.Value.Date && x.Training.To <= EndDate.Value.Date).Where(z => z.Training.ProgramType.Type == ProgramTypeBox.Text);
            foreach (var training in result)
            {
                Data.Rows.Add(i++, training.Training.Id, training.Training.Name, training.Training.From.ToString("dd/MM/yyyy"), training.Training.To.ToString("dd/MM/yyyy"), training.Training.Place.Name, Properties.Resources.delete);
            }
        }
    }
}

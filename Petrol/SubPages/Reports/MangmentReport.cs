using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Reports
{
    public partial class MangmentReport : UserControl
    {
        private FollowingReportService service;
        public MangmentReport()
        {
            InitializeComponent();
            service = new FollowingReportService();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form =(Form1)this.ParentForm;
            form.ReportsNavigation("Main");
        }
        public void LoadData() 
        {
            var Departments = service.GetAll<Department>().Select(x=>x.Name);
            MangmentData.Columns.Clear();
            MangmentData.Columns.Add("Id", "م");
            MangmentData.Columns.Add("TrainingName", "اسم التدريب");
            MangmentData.Columns.Add("TrainingType", "نوع التدريب");
            foreach (var department in Departments)
            {
                MangmentData.Columns.Add(department, department);
            }
            MangmentData.Columns.Add("Total", "اجمالى");

        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            var startDate = StartDate.Value.Date;
            var endDate = EndDate.Value.Date;
            if (startDate > endDate)
            {
                UserMessages.Error("يجب أن يكون تاريخ البداية اصغر من تاريخ النهاية");
                return;
            }
            var reports = service.GetAllWithNestedInclude(x => x.Include(y => y.Training).ThenInclude(t => t.ProgramType).Include(d=>d.DepartmentsPresenceNumber).ThenInclude(r=>r.Department)).Where(x => x.Training.From.Date >= startDate && x.Training.To <= endDate.Date).ToList();
            if (ProgramTypeBox.SelectedIndex > 0)
            {
                var programType = ProgramTypeBox.SelectedItem.ToString();
                reports = reports.Where(x => x.Training.ProgramType.Type == programType).ToList();
            }
            if (reports.Count > 0)
            {
                int i = 1;
                MangmentData.Rows.Clear();
                foreach (var report in reports)
                {
                   
                    var row = new DataGridViewRow();
                    row.CreateCells(MangmentData, i++, report.Training.Name, report.Training.ProgramType.Type);
                    foreach (var department in report.DepartmentsPresenceNumber)
                    {
                        var departmentName = department.Department.Name;
                        var number = department.PresenceNumber;
                        var index=MangmentData.Columns[departmentName].Index;
                        row.Cells[index].Value = number;
                    }
                    var total = report.Men+report.Women;
                    var tIndex = MangmentData.Columns["Total"].Index;
                    row.Cells[tIndex].Value = total;
                    MangmentData.Rows.Add(row);
                }
            }

            else
            {
                UserMessages.Error("لا يوجد تدريبات مطابقة لعناصر البحث");
            }
        }
    }
}

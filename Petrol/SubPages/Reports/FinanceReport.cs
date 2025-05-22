using Petrol.Services;
using System;
using Petrol.Utils;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Petrol.SubPages.Reports
{
    public partial class FinanceReport : UserControl
    {
        private FollowingReportService service;
        public FinanceReport()
        {
            InitializeComponent();
            service = new FollowingReportService();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ReportsNavigation("Main");
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
            var reports = service.GetAllWithNestedInclude(x=>x.Include(y=>y.Training).ThenInclude(t=>t.ProgramType)).Where(x=>x.Training.From.Date>=startDate&&x.Training.To<=endDate.Date).ToList();
            if (ProgramTypeBox.SelectedIndex>0)
            {
                var programType = ProgramTypeBox.SelectedItem.ToString();
                reports = reports.Where(x => x.Training.ProgramType.Type == programType).ToList();
            }
            if (reports.Count > 0)
            {
                int i = 1;
                FinanceData.Rows.Clear();
                foreach (var report in reports)
                {
                    var total = report.ProgramCost + report.HotelCost + report.LastNightCost + report.TransitionsCost +report.TicketsCost;
                    var row = new DataGridViewRow();
                    row.CreateCells(FinanceData,i++, report.Training.Name,total ,report.FoodCost,report.OthersCost,report.TotalCost,report.Men+report.Women,report.Men,report.Women,
                        report.Training.From.Date.ToString("yyyy/MM/dd")+" : "+report.Training.To.Date.ToString("MM/dd"),report.Training.ProgramType.Type,report.Training.DepartmentName,report.ProgramOrganizer);
                    FinanceData.Rows.Add(row);
                }
            }

            else
            {
                UserMessages.Error("لا يوجد تدريبات مطابقة لعناصر البحث");
            }
        }
    }
}

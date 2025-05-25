using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Reports
{
    public partial class FinanceReport : UserControl
    {
        private FollowingReportService service;
        private ProgramTypeService programTypeService;
        ExcelExporter excelExporter;
        public FinanceReport()
        {
            InitializeComponent();
            service = new FollowingReportService();
            excelExporter = new ExcelExporter();
            programTypeService=new ProgramTypeService();
        }
        public void LoadData() 
        {
            var ProgramsTypes = programTypeService.GetAll<ProgramType>().Select(x => x.Type);
        
            ProgramTypeBox.Items.Clear();
            ProgramTypeBox.Items.Add("كل الأنواع");
            ProgramTypeBox.Items.AddRange(ProgramsTypes.ToArray());
            ProgramTypeBox.SelectedIndex = 0;
          
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
            var reports = service.GetAllWithNestedInclude(x=>x.Include(y=>y.Training).ThenInclude(t=>t.TrainingType)).Where(x=>x.Training.From.Date>=startDate.Date&&x.Training.To.Date<=endDate.Date).ToList();
            if (ProgramTypeBox.SelectedIndex>0)
            {
                var programType = ProgramTypeBox.SelectedItem.ToString();
                reports = reports.Where(x => x.Training.TrainingType.Name == programType).ToList();
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
                        report.Training.From.Date.ToString("yyyy/MM/dd")+" : "+report.Training.To.Date.ToString("MM/dd"),report.Training.TrainingType.Name,report.Training.DepartmentName,report.ProgramOrganizer);
                    FinanceData.Rows.Add(row);
                }
            }

            else
            {
                UserMessages.Error("لا يوجد تدريبات مطابقة لعناصر البحث");
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            excelExporter.GenerateFinanceReport(StartDate.Value,EndDate.Value,ProgramTypeBox.SelectedItem?.ToString()??"");
        }
public void PrintReport(List<dynamic> data, string searchTerm, string filterInfo)
{
    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
    string filename = "Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
    PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
    document.Open();

    // Title and filter info
    Paragraph header = new Paragraph("Report Page - " + this.Name + "\n"
        + "Search: " + searchTerm + "\n"
        + "Filter: " + filterInfo + "\n"
        + "Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
    header.SpacingAfter = 10f;
    document.Add(header);

    PdfPTable table = new PdfPTable(3); // adjust number of columns
    table.AddCell("Column1");
    table.AddCell("Column2");
    table.AddCell("Column3");

    foreach (var item in data)
    {
        table.AddCell(item.Prop1);
        table.AddCell(item.Prop2);
        table.AddCell(item.Prop3.ToString());
    }

    document.Add(table);
    document.Close();

    MessageBox.Show("Report saved to PDF: " + filename);
}
    }
}


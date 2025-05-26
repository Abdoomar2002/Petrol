using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Petrol.SubPages.Reports
{
    public partial class MangmentReport : UserControl
    {
        private FollowingReportService service;
        private ProgramTypeService programTypeService;
        private ExcelExporter excelExporter;
        public MangmentReport()
        {
            InitializeComponent();
            service = new FollowingReportService();
            excelExporter = new ExcelExporter();
            programTypeService=new ProgramTypeService();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form =(Form1)this.ParentForm;
            form.ReportsNavigation("Main");
        }
        public void LoadData() 
        {
            var ProgramsTypes = programTypeService.GetAll<ProgramType>().Select(x => x.Type);
            ProgramTypeBox.Items.Clear();
            ProgramTypeBox.Items.Add("كل الأنواع");
            ProgramTypeBox.Items.AddRange(ProgramsTypes.ToArray());
            ProgramTypeBox.SelectedIndex = 0;
          

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
            var reports = service.GetAllWithNestedInclude(x => x.Include(y => y.Training).ThenInclude(t => t.TrainingType).Include(r=>r.Training).ThenInclude(y=>y.Program).ThenInclude(o=>o.ProgramType).Include(d=>d.DepartmentsPresenceNumber).ThenInclude(r=>r.Department)).Where(x => x.Training.From.Date >= startDate.Date && x.Training.To.Date <= endDate.Date).ToList();
            if (ProgramTypeBox.SelectedIndex > 0)
            {
                var programType = ProgramTypeBox.SelectedItem.ToString();
                reports = reports.Where(x => x.Training.Program.ProgramType.Type == programType).ToList();
            }
            if (reports.Count > 0)
            {
                int i = 1;
                MangmentData.Rows.Clear();
                foreach (var report in reports)
                {
                   
                    var row = new DataGridViewRow();
                    row.CreateCells(MangmentData, i++, report.Training.Name, report.Training.TrainingType.Name);
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

        private void PrintBtn_Click(object sender, EventArgs e)
        {

            var startDate = StartDate.Value.Date;
            var endDate = EndDate.Value.Date;
            if (startDate > endDate)
            {
                UserMessages.Error("يجب أن يكون تاريخ البداية اصغر من تاريخ النهاية");
                return;
            }
            excelExporter.GenerateManagementReport(StartDate.Value.Date,EndDate.Value.Date,ProgramTypeBox?.SelectedItem?.ToString()??"");
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



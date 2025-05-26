using Guna.UI2.WinForms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Petrol.Models;
using Petrol.Services;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Document = iTextSharp.text.Document;
using Font = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;
using Paragraph = iTextSharp.text.Paragraph;
namespace Petrol.Utils
{
    public class PdfGenerator
    {

        public static void GeneratePdf(string mainTitle, string subtitle1="", string subtitle2 = "", Guna2DataGridView dataGridView=null)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = "Report.pdf"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            Document document = new Document(PageSize.A4, 50, 50, 50, 50);
            using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
            {
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();
                string arialPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont cairoFont = BaseFont.CreateFont(arialPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                var headerFont1 = new Font(cairoFont, 18f, Font.BOLD);
                var headerFont2 = new Font(cairoFont, 14f, Font.BOLD);
                var headerFont3 = new Font(cairoFont, 12f, Font.BOLD);
                // Add header (logo + brand title)
                PdfPTable headerTable = new PdfPTable(2)
                {
                    WidthPercentage = 100
                };
                headerTable.SetWidths(new float[] { 1f, 3f }); // Adjust logo and brand title width

                // Logo
                string logoPath = "logo.jpg"; // Path to your logo file
                if (File.Exists(logoPath))
                {
                    Image logo = Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(50f, 50f);
                    PdfPCell logoCell = new PdfPCell(logo)
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };
                    headerTable.AddCell(logoCell);
                }
                else
                {
                    headerTable.AddCell(""); // Empty if logo is missing
                }

                // Brand title
                Font brandFont = headerFont2;
                PdfPCell brandCell = new PdfPCell(new Phrase("وحدة التدريب", brandFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    RunDirection = PdfWriter.RUN_DIRECTION_RTL
                    
                };
                headerTable.AddCell(brandCell);

                document.Add(headerTable);
                document.Add(new Paragraph(" ")); // Spacer

                // Main title and subtitles
                Font mainFont =headerFont1;
                PdfPTable title = new PdfPTable(1);

                title.AddCell(new PdfPCell(new Paragraph(mainTitle, mainFont))
                {
                    RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Border=Rectangle.NO_BORDER
                });
              

                Font subFont = headerFont3;
                if (!string.IsNullOrWhiteSpace(subtitle1))
                {
                    title.AddCell(new PdfPCell(new Paragraph(subtitle1, subFont))
                    {
                        RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Border = Rectangle.NO_BORDER
                    });
                }
                if (!string.IsNullOrWhiteSpace(subtitle2))
                {
                    title.AddCell(new PdfPCell(new Paragraph(subtitle2, subFont))
                    {
                        RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Border = Rectangle.NO_BORDER
                    });
                }
                document.Add(title);

                document.Add(new Paragraph(" ")); // Spacer

                // Create table from DataGridView
                PdfPTable table = new PdfPTable(dataGridView.Columns.Count)
                {
                    WidthPercentage = 100,
                    RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                };

                // Header row
                foreach (DataGridViewColumn column in dataGridView.Columns)
                { 
                    
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, subFont))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                    };
                    table.AddCell(cell);
                }

                // Data rows
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.IsNewRow) continue;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellText = cell.Value?.ToString() ?? "";
                        table.AddCell(new PdfPCell(new Phrase(cellText, subFont)) {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                          
                        });
                    }
                }

                document.Add(table);

                document.Close();
                writer.Close();
                stream.Close();

                MessageBox.Show("PDF generated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void GenerateFilledFollowingReportDoc(FollowingReport report, string templatePath, string outputPath)
        {
            using (var doc = DocX.Load(templatePath))
            {
                // Replace basic placeholders
                doc.ReplaceText("اسم البرنامج :", $"اسم البرنامج : {report.Training.Name}");
                doc.ReplaceText("المنعقـــــد بـــ :", $"المنعقـــــد بـــ : {report.Training.Place.Name}");

                string fromDate = report.Training.From.ToString("yyyy/MM/dd");
                string toDate = report.Training.To.ToString("yyyy/MM/dd");
                doc.ReplaceText("from", fromDate);
                doc.ReplaceText("to", toDate);
                doc.ReplaceText("men", report.Men.ToString());
                doc.ReplaceText("wo", report.Women.ToString());

                // Find the department section table
                 // Find the department section table
                var table = doc.Tables.FirstOrDefault(t => t.Rows.Any(r => r.Cells.Any(c => c.Paragraphs.Any(p => p.Text.Contains("الإدارة")))));
                var sectable = doc.Tables.FirstOrDefault(t => t.Rows.Any(r => r.Cells.Any( c=> c.Paragraphs.Any(p => p.Text.Contains("الإدارة"))))&&t!=table);
                doc.Tables[2].Remove();
                if (table != null)
                {
                    // Remove all rows except header
                    while (table.RowCount > 1)
                        table.RemoveRow(1);

                    // Clear header cells
                    foreach (var cell in table.Rows[0].Cells)
                        cell.Paragraphs[0].RemoveText(0);

                    // Remove extra header cells
                    

                    // Departments list
                    var Departments = new DepartmentService().GetAll<Department>();
                    int maxCols = 14;
                    int colCounter = 1;

                    // Keep track of the current row indexes
                    int headerRowIdx = 0;
                    int dataRowIdx = 1;

                    // Ensure second row exists
                    if (table.RowCount == 1)
                        table.InsertRow();
                 
                    table.Rows[0].Cells[0].Paragraphs[0].Append("الادارات").Bold();
                    table.Rows[1].Cells[0].Paragraphs[0].Append("العدد").Bold();
                   
                    foreach (var dp in report.DepartmentsPresenceNumber)
                    {
                        if (colCounter == maxCols)
                        {
                            // Insert two new rows: header and data
                            table.InsertRow();
                            table.InsertRow();
                            headerRowIdx = table.RowCount - 2;
                            dataRowIdx = table.RowCount - 1;
                            table.Rows[headerRowIdx].Cells[0].Paragraphs[0].Append("الادارات").Bold();
                            table.Rows[dataRowIdx].Cells[0].Paragraphs[0].Append("العدد").Bold();

                           
                            colCounter = 1;
                        }

                            table.Rows[headerRowIdx].Cells.Add(null);
                        var headerCell = table.Rows[headerRowIdx].Cells[colCounter];
                        // Add header cell
                        headerCell.Paragraphs[0].Append(Departments.FirstOrDefault(x => x.Id == dp.DepartmentId)?.Name ?? "Unknown").Bold();

                        // Add data cell
                            table.Rows[dataRowIdx].Cells.Add(null);
                        var dataCell = table.Rows[dataRowIdx].Cells[colCounter];
                        dataCell.Paragraphs[0].Append(dp.PresenceNumber.ToString());
                        
                        colCounter++;
                    }
                    table.Rows[headerRowIdx].Cells[14].Paragraphs[0].Append("الاجمالي").Bold();
                    table.Rows[dataRowIdx].Cells[14].Paragraphs[0].Append((report.Men+report.Women).ToString()).Bold();

                }

                // Replace costs and supervisor
                doc.ReplaceText("program", report.ProgramCost.ToString());
                doc.ReplaceText("food", report.FoodCost.ToString());
                doc.ReplaceText("stay", report.HotelCost.ToString());
                doc.ReplaceText("transition", report.TransitionsCost.ToString());
                doc.ReplaceText("tickets", report.TicketsCost.ToString());
                doc.ReplaceText("last", report.LastNightCost.ToString());
                doc.ReplaceText("others", report.OthersCost.ToString());
                doc.ReplaceText("res", report.TotalCost.ToString());
                doc.ReplaceText("organizer", report.ProgramOrganizer);

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Word Document (*.docx)|*.docx";
                    saveFileDialog.FileName = "FilledReport.docx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        doc.SaveAs(saveFileDialog.FileName);
                        UserMessages.Info("تم حفظ الملف بنجاح");
                    }
                }

              //  MessageBox.Show("Document generated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

}
}

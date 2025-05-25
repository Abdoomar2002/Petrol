using Guna.UI2.WinForms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms;
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

                // Add header (logo + brand title)
                PdfPTable headerTable = new PdfPTable(2)
                {
                    WidthPercentage = 100
                };
                headerTable.SetWidths(new float[] { 1f, 3f }); // Adjust logo and brand title width

                // Logo
                string logoPath = "logo.png"; // Path to your logo file
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
                Font brandFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                PdfPCell brandCell = new PdfPCell(new Phrase("Brand Title", brandFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                headerTable.AddCell(brandCell);

                document.Add(headerTable);
                document.Add(new Paragraph(" ")); // Spacer

                // Main title and subtitles
                Font mainFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                document.Add(new Paragraph(mainTitle, mainFont));
                document.Add(new Paragraph(" ")); // Spacer

                Font subFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                if (!string.IsNullOrWhiteSpace(subtitle1))
                {
                    document.Add(new Paragraph(subtitle1, subFont));
                }
                if (!string.IsNullOrWhiteSpace(subtitle2))
                {
                    document.Add(new Paragraph(subtitle2, subFont));
                }

                document.Add(new Paragraph(" ")); // Spacer

                // Create table from DataGridView
                PdfPTable table = new PdfPTable(dataGridView.Columns.Count)
                {
                    WidthPercentage = 100
                };

                // Header row
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, subFont))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER
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
                        table.AddCell(new Phrase(cellText, subFont));
                    }
                }

                document.Add(table);

                document.Close();
                writer.Close();
                stream.Close();

                MessageBox.Show("PDF generated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}

﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using Petrol.Models;
using Petrol.Services;
using Petrol.SubPages.Finances;
using Petrol.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Reports
{
    public partial class FollowingReport : UserControl
    {
        private FollowingReportService _followingReportService;
        private DepartmentService DepartmentService;
        private TrainingService TrainingService;
        private DepartmentPresenceNumberService DepartmentPresenceNumberService;
        private bool isProgramming;
        List<Training> Trainings;
        List<Department> Departments;
        public FollowingReport()
        {
            InitializeComponent();
            _followingReportService = new FollowingReportService();
            DepartmentService = new DepartmentService();
            TrainingService = new TrainingService();
            DepartmentPresenceNumberService = new DepartmentPresenceNumberService();
        }
        public void LoadData()
        {
    
            Departments = DepartmentService.GetAll<Department>().ToList();
            DepartmentData.Columns.Clear();
            foreach (var department in Departments)
            {
                DepartmentData.Columns.Add(department.Name, department.Name);
            }
            DepartmentData.Columns.Add("Total", "الاجمالي");
            DepartmentData.Rows.Clear();
            DepartmentData.Rows.Add();
            Trainings = TrainingService.GetAllWithInclude(x => x.Place, y => y.TrainingType).ToList();
            TrainingIdTxt.AutoCompleteCustomSource.Clear();
            TrainingIdTxt.AutoCompleteCustomSource.AddRange(Trainings.Select(x => x.Name).ToArray());
            TrainingNameTxt.AutoCompleteCustomSource.Clear();
            TrainingNameTxt.AutoCompleteCustomSource.AddRange(Trainings.Select(x => x.Name).ToArray());
            FinanceData.Rows.Clear();
            FinanceData.Rows.Add();
            var types = TrainingService.GetAll<TrainingType>().Select(x => x.Name).ToArray();
            TrainingTypeBox.Items.Clear();
            TrainingTypeBox.Items.Add("كل الأنواع");
            TrainingTypeBox.Items.AddRange(types);


        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ReportsNavigation("Main");
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TrainingIdTxt.Text) || string.IsNullOrEmpty(TrainingNameTxt.Text) || string.IsNullOrEmpty(PlaceTxt.Text) || string.IsNullOrEmpty(TrainingTypeBox.Text) || string.IsNullOrEmpty(MenTxt.Text) || string.IsNullOrEmpty(WomenTxt.Text) || string.IsNullOrEmpty(OrganizerTxt.Text))
            {
                UserMessages.Error("من فضلك ادخل جميع البيانات");
                return;
            }
            //check that total number of men and women is equal to the total number in departmentData
            var Total = 0;
            var men = 0; var women = 0;
            int.TryParse(MenTxt.Text, out men);
            int.TryParse(WomenTxt.Text, out women);
            Total = men + women;
            var tableTotal = 0;
            int.TryParse(DepartmentData.Rows[0].Cells["Total"].Value?.ToString() ?? "0", out tableTotal);
            if (Total != tableTotal)
            {
                UserMessages.Error("عدد الرجال والنساء لا يتطابق مع العدد الكلي في الجدول");
                return;
            }
            // check that there is data in departmentData and financeData
            if (DepartmentData.Rows.Count == 0 || FinanceData.Rows.Count == 0)
            {
                UserMessages.Error("من فضلك ادخل بيانات في الجدول");
                return;
            }
            try
            {
                // check the training name is exist in the database
                int.TryParse(TrainingIdTxt.Text, out int trainingId);
                var training = Trainings.FirstOrDefault(x => x.Id == trainingId);
                if (training == null)
                {
                    UserMessages.Error("اسم التدريب غير موجود في قاعدة البيانات");
                    return;
                }
                // check the training has following report or not
                var CheckfollowingReport = _followingReportService.GetAll<Models.FollowingReport>().FirstOrDefault(x => x.TrainingId == trainingId);
                if (CheckfollowingReport != null)
                {
                    var result=UserMessages.Warning("التقرير موجود بالفعل في قاعدة البيانات\nهل تريد تحديثه");
                    if (result == DialogResult.Yes) 
                    {
                        CheckfollowingReport.ProgramCost = double.TryParse(FinanceData.Rows[0].Cells[0].Value?.ToString() ?? "0", out double CprogramCost) ? CprogramCost : 0;
                        CheckfollowingReport.FoodCost = double.TryParse(FinanceData.Rows[0].Cells[1].Value?.ToString() ?? "0", out double CfoodCost) ? CfoodCost : 0;
                        CheckfollowingReport.HotelCost = double.TryParse(FinanceData.Rows[0].Cells[2].Value?.ToString() ?? "0", out double ChotelCost) ? ChotelCost : 0;
                        CheckfollowingReport.TransitionsCost = double.TryParse(FinanceData.Rows[0].Cells[3].Value?.ToString() ?? "0", out double CtransitionsCost) ? CtransitionsCost : 0;
                        CheckfollowingReport.TicketsCost = double.TryParse(FinanceData.Rows[0].Cells[4].Value?.ToString() ?? "0", out double CticketsCost) ? CticketsCost : 0;
                        CheckfollowingReport.LastNightCost = double.TryParse(FinanceData.Rows[0].Cells[5].Value?.ToString() ?? "0", out double ClastNightCost) ? ClastNightCost : 0;
                        CheckfollowingReport.OthersCost = double.TryParse(FinanceData.Rows[0].Cells[6].Value?.ToString() ?? "0", out double CothersCost) ? CothersCost : 0;
                        CheckfollowingReport.TotalCost = double.TryParse(FinanceData.Rows[0].Cells[7].Value?.ToString() ?? "0", out double CtotalCost) ? CtotalCost : 0;
                        CheckfollowingReport.ProgramOrganizer = OrganizerTxt.Text;
                        CheckfollowingReport.Men = MenTxt.Text == "" ? 0 : int.Parse(MenTxt.Text);
                        CheckfollowingReport.Women = WomenTxt.Text == "" ? 0 : int.Parse(WomenTxt.Text);
                   
                    }
                   
                }

                // add the data to the database
                var followingReport = new Models.FollowingReport()
                {
                    ProgramCost = double.TryParse(FinanceData.Rows[0].Cells[0].Value?.ToString() ?? "0", out double programCost) ? programCost : 0,
                    FoodCost = double.TryParse(FinanceData.Rows[0].Cells[1].Value?.ToString() ?? "0", out double foodCost) ? foodCost : 0,
                    HotelCost = double.TryParse(FinanceData.Rows[0].Cells[2].Value?.ToString() ?? "0", out double hotelCost) ? hotelCost : 0,
                    TransitionsCost = double.TryParse(FinanceData.Rows[0].Cells[3].Value?.ToString() ?? "0", out double transitionsCost) ? transitionsCost : 0,
                    TicketsCost = double.TryParse(FinanceData.Rows[0].Cells[4].Value?.ToString() ?? "0", out double ticketsCost) ? ticketsCost : 0,
                    LastNightCost = double.TryParse(FinanceData.Rows[0].Cells[5].Value?.ToString() ?? "0", out double lastNightCost) ? lastNightCost : 0,
                    OthersCost = double.TryParse(FinanceData.Rows[0].Cells[6].Value?.ToString() ?? "0", out double othersCost) ? othersCost : 0,
                    TotalCost = double.TryParse(FinanceData.Rows[0].Cells[7].Value?.ToString() ?? "0", out double totalCost) ? totalCost : 0,
                    ProgramOrganizer = OrganizerTxt.Text,
                    Men = MenTxt.Text == "" ? 0 : int.Parse(MenTxt.Text),
                    Women = WomenTxt.Text == "" ? 0 : int.Parse(WomenTxt.Text),
                    TrainingId = trainingId,
                };
                var departmentPresenceNumbers = new List<DepartmentPresenceNumber>();
                for (int i = 0; i < DepartmentData.Columns.Count - 1; i++)
                {
                    var departmentName = DepartmentData.Columns[i].HeaderText;
                    var department = Departments.FirstOrDefault(x => x.Name == departmentName);
                    if (department != null)
                    {
                        var departmentPresenceNumber = new DepartmentPresenceNumber()
                        {
                            DepartmentId = department.Id,
                            PresenceNumber = int.TryParse(DepartmentData.Rows[0].Cells[i].Value?.ToString() ?? "0", out int number) ? number : 0,
                        };
                        departmentPresenceNumbers.Add(departmentPresenceNumber);
                    }
                }
                if (CheckfollowingReport != null)
                {
                    CheckfollowingReport.DepartmentsPresenceNumber = departmentPresenceNumbers;
                    _followingReportService.Update(CheckfollowingReport);

                }
                else
                {
                    followingReport.DepartmentsPresenceNumber = departmentPresenceNumbers;
                    _followingReportService.Add(followingReport);
                }
                _followingReportService.SaveChanges();
                UserMessages.Info("تم حفظ البيانات بنجاح");
               
                // print it
            }
            catch (Exception ex)
            {
                UserMessages.Error("خطأ اثناء حفظ البيانات");
            }


        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (DeleteBtn.Text == "مسح") 
            {
                var result = UserMessages.Warning("هل انت متاكد من انك تريد مسح التقرير");
                if (result == DialogResult.Yes) 
                {
                    try
                    {
                        var training = Trainings.FirstOrDefault(x => x.Id.ToString() == TrainingIdTxt.Text);
                        if (training != null)
                        {
                            var following = _followingReportService.GetAll<Models.FollowingReport>().FirstOrDefault(x => x.TrainingId == training.Id);
                            if (following != null)
                            {
                                _followingReportService.Delete(following);
                                _followingReportService.SaveChanges();
                                UserMessages.Info("تم مسح التقرير بنجاح");
                            }
                            else
                            {
                                UserMessages.Error("لا يوجد تقرير متابعة لهذا التدريب");
                                return;
                            }
                        }
                        else
                        {
                            UserMessages.Error("لا يوجد تدريب بهذه البيانات");
                            return;
                        }
                    }
                    catch (Exception ex) {
                        UserMessages.Error("خطأ اثناء مسح البيانات");
                        return;
                    }
                }
            }


            TrainingIdTxt.Text = string.Empty;
            TrainingNameTxt.Text = string.Empty;
            PlaceTxt.Text = string.Empty;
            TrainingTypeBox.Text = string.Empty;
            StartDate.Value = DateTime.Now;
            EndDate.Value = DateTime.Now;
            MenTxt.Text = string.Empty;
            WomenTxt.Text = string.Empty;
            OrganizerTxt.Text = string.Empty;
            DepartmentData.Rows.Clear();
            FinanceData.Rows.Clear();
            TrainingTypeBox.SelectedIndex = -1;
            LoadData();
        }

        private void TrainingNameTxt_TextChanged(object sender, EventArgs e)
        {
            if(!isProgramming)
            { 
            var training = Trainings.FirstOrDefault(x => x.Name == TrainingNameTxt.Text);
            if (training != null)
            {
                    isProgramming = true;
                TrainingIdTxt.Text = training.Id.ToString();
                PlaceTxt.Text = training.Place.Name;
                TrainingTypeBox.Text = training.TrainingType.Name;
                StartDate.Value = training.From;
                EndDate.Value = training.To;
                DeleteBtn.Text = "مسح";
                    isProgramming = false;

            }
            else
            {
                    isProgramming = true;
                TrainingIdTxt.Text = string.Empty;
                PlaceTxt.Text = string.Empty;
                TrainingTypeBox.Text = string.Empty;
                StartDate.Value = DateTime.Now;
                EndDate.Value = DateTime.Now;
                DeleteBtn.Text = "تفريغ";
                    isProgramming = false;
            }

            }
        }

        private void TrainingIdTxt_TextChanged(object sender, EventArgs e)
        {
            if(!isProgramming)
            {

            var training = Trainings.FirstOrDefault(x => x.Id.ToString() == TrainingIdTxt.Text);
            if (training != null)
            {
                    isProgramming = true;
                TrainingNameTxt.Text = training.Name;
                PlaceTxt.Text = training.Place.Name;
                TrainingTypeBox.Text = training.TrainingType.Name;
                StartDate.Value = training.From;
                EndDate.Value = training.To;
                DeleteBtn.Text = "مسح";
                    isProgramming = false;
            }
            else
            {
                    isProgramming = true;
                TrainingNameTxt.Text = string.Empty;
                PlaceTxt.Text = string.Empty;
                TrainingTypeBox.Text = string.Empty;
                StartDate.Value = DateTime.Now;
                EndDate.Value = DateTime.Now;
                DeleteBtn.Text = "تفريغ";
                    isProgramming = false;
            }
            }
        }

        private void FinanceData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex == FinanceData.Columns.Count - 1) return; // Skip if invalid row or Total column

            DataGridViewRow row = FinanceData.Rows[e.RowIndex];
            double total = 0;

            // Assuming columns 1 to 4 (index 1 to 4) are numeric values to sum
            for (int i = 0; i < FinanceData.Columns.Count - 1; i++) // Exclude the first column (index 0) and Total column
            {
                if (row.Cells[i].Value != null && double.TryParse(row.Cells[i].Value.ToString(), out double value))
                {
                    total += value;
                }
            }

            // Update the Total column (last column)
            row.Cells["Total"].Value = total.ToString("F2");
        }

        private void DepartmentData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex == DepartmentData.Columns.Count - 1) return; // Skip if invalid row or Total column
            DataGridViewRow row = DepartmentData.Rows[e.RowIndex];
            double total = 0;
            // Assuming columns 1 to 4 (index 1 to 4) are numeric values to sum
            for (int i = 0; i < DepartmentData.Columns.Count - 1; i++) // Exclude the first column (index 0) and Total column
            {
                if (row.Cells[i].Value != null && int.TryParse(row.Cells[i].Value.ToString(), out int value))
                {
                    total += value;
                }
            }
            // Update the Total column (last column)
            row.Cells["Total"].Value = total.ToString();

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




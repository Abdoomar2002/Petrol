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
    public partial class AddProgramToEmployee : UserControl
    {
        private EmployeeService employeeService;
        private Employee EditedEmployee;
        private TrainingService trainingService;
        private List<Training> Trainings;
        private List<Place> places;
        public AddProgramToEmployee()
        {
            InitializeComponent();
            employeeService = new EmployeeService();
            trainingService = new TrainingService();
        }
        public void SetEmployeeId(int id)
        {
            var employee = employeeService.GetAllWithNestedInclude(x => x.Include(y => y.Trainings)).FirstOrDefault(e => e.Id == id);
            if (employee == null) return;
            EditedEmployee = employee;
            places = new PlaceService().GetAll<Place>().ToList();
            Trainings = trainingService.GetAllWithInclude(x => x.Place, y => y.ProgramType).ToList();
            // load the trainings
            label19.Text = $"اضافة تدريب الي {EditedEmployee.Name}";
            TrainingId.AutoCompleteCustomSource.AddRange(Trainings.Select(x => x.Id.ToString()).ToArray());
            TrainingName.AutoCompleteCustomSource.AddRange(Trainings.Select(x => x.Name.ToString()).ToArray());
            Location.AutoCompleteCustomSource.AddRange(places.Select(x => x.Name.ToString()).ToArray());
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.EmployeeNavigation("Edit",EditedEmployee.Id);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (IsAnyBoxEmpty())
            {
                UserMessages.Error("من فضلك املئ البيانات بالكامل");
                return;
            }
            var trainId = 0; int.TryParse(TrainingId.Text, out trainId);
            var training = Trainings.FirstOrDefault(x => x.Id == trainId);
            // check if the training id is already in the db
            if (training != null)
            {
                UserMessages.Error("كود التدريب غير موجود مسبقا");
                return;
            }
            //get the training
            var check = EditedEmployee.Trainings.FirstOrDefault(x => x.TrainingId == training.Id);
            if (training == null)
            {
                UserMessages.Error("الموظف مسجل بالفعل في هذا التدريب");
                return;
            }
            // add the training to the employee
            var employeeTraining = new EmployeeTraining()
            {
                EmployeeId = EditedEmployee.Id,
                TrainingId = training.Id,
                Employee = EditedEmployee,
                Training = training,
            };
            var employeeTrainingService = new EmployeeTrainingService();
            try
            {

                employeeTrainingService.Add(employeeTraining);
                employeeTrainingService.SaveChanges();
                UserMessages.Info("تم اضافة التدريب بنجاح");
            }
            catch (Exception ex)
            {
                UserMessages.Error("حدث خطأ اثناء اضافة التدريب");
                return;
            }

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            TrainingId.Text = "";
            TrainingName.Text = "";
            FromDate.Value = DateTime.Now;
            ToDate.Value = DateTime.Now;
            Location.Text = "";
            TrainingType.Text = "";

        }

        private void TrainingId_TextChanged(object sender, EventArgs e)
        {
            int test = 0;
            if (int.TryParse(TrainingId.Text, out test))
            {
                var training = Trainings.FirstOrDefault(x => x.Id == test);
                if (training != null)
                {
                    TrainingName.Text = training.Name;
                    FromDate.Value = training.From;
                    ToDate.Value = training.To;
                    Location.Text = training.Place.Name;
                    TrainingType.Text = training.ProgramType.Type;
                }
            }
        }

        private void TrainingName_TextChanged(object sender, EventArgs e)
        {
            var training = Trainings.FirstOrDefault(x => x.Name == TrainingName.Text);
            if (training != null)
            {
                TrainingId.Text = training.Id.ToString();
                FromDate.Value = training.From;
                ToDate.Value = training.To;
                Location.Text = training.Place.Name;
                TrainingType.Text = training.ProgramType.Type;
            }
        }
        private bool IsAnyBoxEmpty()
        {
            if (string.IsNullOrEmpty(TrainingId.Text) || string.IsNullOrEmpty(TrainingName.Text) || string.IsNullOrEmpty(Location.Text) || string.IsNullOrEmpty(TrainingType.Text))
            {
                return true;
            }
            return false;

        }
    }
}

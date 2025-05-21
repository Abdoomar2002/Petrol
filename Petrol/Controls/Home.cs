using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petrol.Controls
{
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        public void usersPage_Click(object sender, EventArgs e)
        {
            mainUsers1.BringToFront();
            mainUsers1.LoadData();
        }

        public void EmployeesBtn_Click(object sender, EventArgs e)
        {
            var btn = (Guna2GradientButton)sender;
            ChangeColor(btn);
            mainEmployee1.BringToFront();
            mainEmployee1.LoadData();
        }

        public void DepartmentsBtn_Click(object sender, EventArgs e)
        {
            var btn = (Guna2GradientButton)sender;
            ChangeColor(btn);
            mainDepartments1.BringToFront();
            mainDepartments1.LoadData();
        }

        public void ProgramsBtn_Click(object sender, EventArgs e)
        {
            var btn = (Guna2GradientButton)sender;
            ChangeColor(btn);
            mainPrograms1.BringToFront();
            mainPrograms1.LoadData();
        }

        public void PlacesBtn_Click(object sender, EventArgs e)
        {
            var btn = (Guna2GradientButton)sender;
            ChangeColor(btn);
            mainPlaces1.BringToFront();
            mainPlaces1.LoadData();
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.ParentForm;
            form1.ShowLogin();
        }
        private void ChangeColor(Guna2GradientButton btn)
        {
            var buttons = new List<Guna2GradientButton>
            {
                EmployeesBtn,
                DepartmentsBtn,
                ProgramsBtn,
                PlacesBtn,
                FinanceBtn,
                ReportBtn,
                
            };
            foreach (var button in buttons)
            {
                button.FillColor = Color.Black;
               
            }
            btn.FillColor = Color.Navy;
           
        }
        public void EmployeeNavigation(string target="",int id=0) 
        {
            switch (target) 
            {
                case "Add":
                    addEmployee1.BringToFront();
                    addEmployee1.LoadData();
                    break;
                case "Edit":
                    editEmployee1.BringToFront();
                    editEmployee1.SetEmployeeId(id);
                    break;
                case "Programs":
                    employeeData1.BringToFront();
                    employeeData1.SetEmployeeId(id);
                    break;
                case "Main":
                     mainEmployee1.BringToFront();
                    break;
                case "AddProgram":
                    addProgramToEmployee1.BringToFront();
                    addProgramToEmployee1.SetEmployeeId(id);
                    break;
                default:
                    mainEmployee1.BringToFront();
                    break;
            }
        }
        public void DepartmentsNavigation(string target="", int id = 0)
        {
            switch (target)
            {
                case "Add":
                    addDepartment1.BringToFront();
                    addDepartment1.LoadData();
                    break;
                case "Edit":
                    editDepartment1.BringToFront();
                     editDepartment1.SetDepartmentID(id);
                    break;
                case "Programs":
                    departmentData1.BringToFront();
                    departmentData1.SetDepartmentID(id);
                    break;
                case "Main":
                    mainDepartments1.BringToFront();
                    mainDepartments1.LoadData();
                    break;
             
                default:
                    mainDepartments1.BringToFront();
                    mainDepartments1.LoadData();
                    break;
            }
        }
        public void FinancesNavigation(string target="", int id = 0)
        {
            switch (target)
            {
                case "General":
                    generalCost1.BringToFront();
                    break;
                case "Employee":
                    employeeCost1.BringToFront();
                    // editEmployee1.SetEmployeeId(id);
                    break;
                case "Program":
                    programCost1.BringToFront();
                    // employeeData1.SetEmployeeId(id);
                    break;
                case "Main":
                    mainFinances1.BringToFront();
                    break;
                default:
                    mainFinances1.BringToFront();
                    break;
            }
        }
        public void PlacesNavigation(string target="", int id = 0)
        {
            switch (target)
            {
                case "Add":
                    addPlace1.BringToFront();
                    addPlace1.LoadData();
                    break;
                case "Edit":
                    editPlace1.BringToFront();
                    editPlace1.SetPlaceId(id);
                    break;
                case "Programs":
                    placeData1.BringToFront();
                    placeData1.SetPlaceId(id);
                    break;
                case "Main":
                    mainPlaces1.BringToFront();
                    mainPlaces1.LoadData();
                    break;
                
                default:
                    mainPlaces1.BringToFront();
                    mainPlaces1.LoadData();
                    break;
            }
        }
        public void ProgramsNavigation(string target = "", int id = 0)
        {
            switch (target)
            {
                case "Add":
                    addProgram1.BringToFront();
                    break;
                case "Edit":
                    editProgram1.BringToFront();
                    // editEmployee1.SetEmployeeId(id);
                    break;
                case "Add Training":
                    addTraining1.BringToFront();
                    // employeeData1.SetEmployeeId(id);
                    break;
                case "Main":
                    mainPrograms1.BringToFront();
                    mainPrograms1.LoadData();
                    break;
                case "Edit Training":
                    editTrainigData1.BringToFront();
                    // addProgramToEmployee1.SetEmployeeId(id);
                    break;
                case "Training":
                    traningData1.BringToFront();
                    // addProgramToEmployee1.SetEmployeeId(id);
                    break;
                default:
                    mainPrograms1.BringToFront();
                    break;
            }
        }
        public void ReportsNavigation(string target = "", int id = 0)
        {
            switch (target)
            {
                case "Employee":
                    employeeReport1.BringToFront();
                    break;
                case "Finance":
                    financeReport1.BringToFront();
                    // editEmployee1.SetEmployeeId(id);
                    break;
                case "Following":
                    followingReport1.BringToFront();
                    // employeeData1.SetEmployeeId(id);
                    break;
                case "Main":
                    mainReports1.BringToFront();
                    break;
                case "Mangment":
                    mangmentReport1.BringToFront();
                    // addProgramToEmployee1.SetEmployeeId(id);
                    break;
                default:
                    mainReports1.BringToFront();
                    break;
            }
        }
        public void UsersNavigation(string target = "", int id = 0)
        {
            switch (target)
            {
                case "Add":
                    addUser1.BringToFront();
                    break;
                case "Edit":
                    editUser1.BringToFront();
                    editUser1.SetEmployeeId(id);
                    break;
                case "Main":
                    mainUsers1.BringToFront();
                    mainUsers1.LoadData();
                    break;
                default:
                    mainUsers1.BringToFront();
                    mainUsers1.LoadData();
                    break;
            }
        }

        private void FinanceBtn_Click(object sender, EventArgs e)
        {
            var btn = (Guna2GradientButton)sender;
            ChangeColor(btn);
            mainFinances1.BringToFront();
        }

        private void ReportBtn_Click(object sender, EventArgs e)
        {
            var btn = (Guna2GradientButton)sender;
            ChangeColor(btn);
            mainReports1.BringToFront();
        }
    }
}

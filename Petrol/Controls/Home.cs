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
        }

        public void ProgramsBtn_Click(object sender, EventArgs e)
        {
            var btn = (Guna2GradientButton)sender;
            ChangeColor(btn);
            mainPrograms1.BringToFront();
        }

        public void PlacesBtn_Click(object sender, EventArgs e)
        {
            var btn = (Guna2GradientButton)sender;
            ChangeColor(btn);
            mainPlaces1.BringToFront();
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
                
            };
            foreach (var button in buttons)
            {
                button.FillColor = Color.Black;
               
            }
            btn.FillColor = Color.Navy;
           
        }
        public void EmployeeNavigation(string target,int id=0) 
        {
            switch (target) 
            {
                case "Add":
                    addEmployee1.BringToFront();
                    break;
                case "Edit":
                    editEmployee1.BringToFront();
                    // editEmployee1.SetEmployeeId(id);
                    break;
                case "Programs":
                    employeeData1.BringToFront();
                    // employeeData1.SetEmployeeId(id);
                    break;
                case "Main":
                     mainEmployee1.BringToFront();
                    break;
                case "AddProgram":
                    addProgramToEmployee1.BringToFront();
                    // addProgramToEmployee1.SetEmployeeId(id);
                    break;
                default:
                    break;
            }
        }
        
    }
}

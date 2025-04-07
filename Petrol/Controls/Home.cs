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

        private void usersPage_Click(object sender, EventArgs e)
        {
            mainUsers1.BringToFront();
        }

        private void EmployeesBtn_Click(object sender, EventArgs e)
        {
            var btn = (Guna2GradientButton)sender;
            ChangeColor(btn);
            mainEmployee1.BringToFront();
        }

        private void DepartmentsBtn_Click(object sender, EventArgs e)
        {
            var btn = (Guna2GradientButton)sender;
            ChangeColor(btn);
            mainDepartments1.BringToFront();
        }

        private void ProgramsBtn_Click(object sender, EventArgs e)
        {
            var btn = (Guna2GradientButton)sender;
            ChangeColor(btn);
            mainPrograms1.BringToFront();
        }

        private void PlacesBtn_Click(object sender, EventArgs e)
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
    }
}

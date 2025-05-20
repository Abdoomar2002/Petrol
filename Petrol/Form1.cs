
using System.Windows.Forms;

namespace Petrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void ShowLogin()
        {
            login1.BringToFront();
        }
        public void ShowHome()
        {
            home1.BringToFront();
        }
        public void ShowEmployeeData()
        {
            home1.EmployeeNavigation("Programs");
        }
        public void ShowEmployeeAdd() 
        {
            home1.EmployeeNavigation("Add");
        }
        public void ShowEmployeeEdit()
        {
            home1.EmployeeNavigation("Edit");
        }
        public void ShowEmployeeAddProgram()
        {
            home1.EmployeeNavigation("AddProgram");
        }
        public void ShowEmployeeMain()
        {
            home1.EmployeeNavigation("Main");
        }
        public void DeparmentNavigation(string target,int id=0) 
        {
           home1.DepartmentsNavigation(target,id);
        }
        public void FinanceNavigation(string target)
        {
           home1.FinancesNavigation(target);
        }
        public void PlacesNavigation(string target,int id=0)
        {
            home1.PlacesNavigation(target,id);
        }
        public void ProgramNavigation(string target)
        {
            home1.ProgramsNavigation(target);
        }
        public void ReportsNavigation(string target)
        {
            home1.ReportsNavigation(target);
        }
        public void UsersNavigation(string target,int id=0)
        {
            home1.UsersNavigation(target,id);
        }

    }
}

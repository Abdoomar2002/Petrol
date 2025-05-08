
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


    }
}

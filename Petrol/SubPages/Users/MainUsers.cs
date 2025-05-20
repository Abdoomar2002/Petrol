using Petrol.Models;
using Petrol.Services;
using Petrol.SubPages.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petrol.SubPages.Users
{
    public partial class MainUsers : UserControl
    {
        private UserService service;
        public MainUsers()
        {
            InitializeComponent();
            service = new UserService();
        }

        private void AddUserBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.UsersNavigation("Add");
        }
        public void LoadData()
        {
            UsersData.Rows.Clear();
          
            var users = service.GetAll<User>();
            var i = 1;
            foreach (var user in users) {
                UsersData.Rows.Add(user.Id,i++, user.FinanceNumber, user.Name, user.Username, user.Role);
            }
           
        }

        private void UsersData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var id = int.Parse(UsersData.Rows[e.RowIndex].Cells[0].Value?.ToString()??"0");
            if (id == 0)
                return;
           
            var form = (Form1)this.ParentForm;
            form.UsersNavigation("Edit",id);
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            var results=service.Search(SearchTxt.Text);
            UsersData.Rows.Clear();
            var i = 1;
            foreach (var user in results)
            {
                UsersData.Rows.Add(user.Id, i++, user.FinanceNumber, user.Name, user.Username, user.Role);
            }

        }
    }
}

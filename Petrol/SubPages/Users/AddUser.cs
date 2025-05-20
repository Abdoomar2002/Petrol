using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
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
    public partial class AddUser : UserControl
    {
        private UserService service;
        public AddUser()
        {
            InitializeComponent();
            service = new UserService();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.UsersNavigation("Main");
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (IsAnyBoxesEmpty())
            {
                UserMessages.Error("من فضلك املئ البيانات بالكامل");
                return;
            }
            try
            {

                // check if the finance number is already in the db
                if (service.IsFinanceNumberExists(FinanceNumTxt.Text))
                {
                    UserMessages.Error("رقم الموظف موجود مسبقا");
                    return;
                }
                // check if the user name is already in the db
                if (service.IsUserNameExists(UserNameTxt.Text))
                {
                    UserMessages.Error("اسم المستخدم موجود مسبقا");
                    return;
                }
                // check if the password and re password are equal
                if (PasswordTxt.Text != RePasswordTxt.Text)
                {
                    UserMessages.Error("كلمة المرور غير متطابقة");
                    return;
                }

                // copy the data from the boxes to user object to save in the db
                User user = new User()
                {
                    FinanceNumber = FinanceNumTxt.Text,
                    Name = NameTxt.Text,
                    Password = PasswordTxt.Text,
                    Role = RoleBox.Text,
                    Username = UserNameTxt.Text,
                };

                service.Add(user);
                service.SaveChanges();
                UserMessages.Info("تم حفظ المستخدم بنجاح");
            }
            catch (Exception ex)
            {
                UserMessages.Error($"حدث خطأ أثناء حفظ البيانات\n{ex.Message}");
            }
        }
        private bool IsAnyBoxesEmpty() 
        {
            return string.IsNullOrEmpty(UserNameTxt.Text.Trim()) ||
                   string.IsNullOrEmpty(PasswordTxt.Text.Trim()) ||
                   string.IsNullOrEmpty(NameTxt.Text.Trim()) ||
                   string.IsNullOrEmpty(FinanceNumTxt.Text.Trim()) ||
                   string.IsNullOrEmpty(RePasswordTxt.Text.Trim()) ||
                   RoleBox.SelectedIndex==-1;
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            UserNameTxt.Text = "";
            PasswordTxt.Text = "";
            NameTxt.Text = "";
            FinanceNumTxt.Text = "";
            RePasswordTxt.Text = "";
            RoleBox.SelectedIndex = -1;
        }
    }
}

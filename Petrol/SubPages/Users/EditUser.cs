using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Windows.Forms;

namespace Petrol.SubPages.Users
{
    public partial class EditUser : UserControl
    {
        private UserService service;
        private User EditedUser;
        private bool IsAnyBoxEmpty()
        {
            if (FinanceNumTxt.Text.Trim().Length == 0 ||
                NameTxt.Text.Trim().Length == 0 ||
                UserNameTxt.Text.Trim().Length == 0 ||
                PasswordTxt.Text.Trim().Length == 0 ||
                RePasswordTxt.Text.Trim().Length == 0 ||
                RoleBox.SelectedItem == null)
            {
                return true;
            }
            return false;
        }
        public EditUser()
        {
            InitializeComponent();
            service = new UserService();
        }
        public void SetEmployeeId(int id)
        {
            var user = service.GetById<User>(id);
            if (user == null) return;
            EditedUser = user;
            FinanceNumTxt.Text = user.FinanceNumber;
            NameTxt.Text = user.Name;
            UserNameTxt.Text = user.Username;
            PasswordTxt.Text = user.Password;
            RePasswordTxt.Text = user.Password;
            RoleBox.SelectedItem = user.Role;
        }
        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.UsersNavigation("Main");
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (IsAnyBoxEmpty())
            {
                UserMessages.Error("من فضلك املئ البيانات بالكامل");
                return;
            }
            try
            {

                // check if the finance number is already in the db
                if (service.IsFinanceNumberExists(FinanceNumTxt.Text) && FinanceNumTxt.Text != EditedUser.FinanceNumber)
                {
                    UserMessages.Error("رقم الموظف موجود مسبقا");
                    return;
                }
                // check if the user name is already in the db
                if (service.IsUserNameExists(UserNameTxt.Text) && UserNameTxt.Text != EditedUser.Username)
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
                EditedUser.FinanceNumber = FinanceNumTxt.Text;
                EditedUser.Name = NameTxt.Text;
                EditedUser.Password = PasswordTxt.Text;
                EditedUser.Role = RoleBox.Text;
                EditedUser.Username = UserNameTxt.Text;
                service.Update(EditedUser);
                service.SaveChanges();
                UserMessages.Info($"تم تعديل بيانات المستخدم بنجاح");
            }
            catch (Exception ex) 
            {
                UserMessages.Error("حدث خطأ أثناء حفظ البيانات");
                Console.WriteLine(ex.Message);
            }
            
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            //confirm delete using messages
            var result = UserMessages.Warning("هل انت متأكد من حذف المستخدم");
            if (result == DialogResult.Yes)
            {
                try
                {
                    service.Delete(EditedUser);
                    service.SaveChanges();
                    UserMessages.Info($"تم حذف المستخدم بنجاح");
                    var form = (Form1)this.ParentForm;
                    form.UsersNavigation("Main");
                }
                catch (Exception ex)
                {
                    UserMessages.Error("حدث خطأ أثناء حذف المستخدم");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

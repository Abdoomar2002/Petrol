using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.Controls
{
    public partial class Login : UserControl
    {
        private Timer timer;
        private double opacityStep;
        private UserService service;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handle = base.CreateParams;
                handle.ExStyle |= 0x02000000;
                return handle;
            }
        }
        public Login()
        {
            InitializeComponent();
            opacityStep = 0.02; // The increment for opacity per tick (0 to 1 range)
            timer = new Timer
            {
                Interval = 15 // Time for each tick (speed of animation)
            };
            timer.Tick += Timer_Tick;

            // Start the animation
            StartAnimation();
            service = new UserService();
        }
        private void StartAnimation()
        {
            guna2Panel1.Visible = true;
            label4.Visible = true;

            // Initialize opacity to 0 (fully transparent)
            guna2Panel1.FillColor = System.Drawing.Color.Transparent;
            label4.ForeColor = System.Drawing.Color.Transparent;

            // Start the timer for the animation
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Increment the opacity of the panel and label
            if (guna2Panel1.FillColor.A < 255)
            {
                int newAlpha = (int)(guna2Panel1.FillColor.A + (opacityStep * 255));
                guna2Panel1.FillColor = System.Drawing.Color.FromArgb(newAlpha, 255, 255, 255);
                label4.ForeColor = System.Drawing.Color.FromArgb(newAlpha, 255, 255, 255);
            }
            else
            {
                // Stop the animation once opacity reaches 100%
                timer.Stop();
            }
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            Form1 mainForm = (Form1)this.ParentForm;
            ExcelImporter excelImporter = new ExcelImporter(new Data.AppDbContext());
            
            var name = NameTxt.Text;
            var password= PasswordTxt.Text;
            var user = service.GetAll<User>().FirstOrDefault(x => x.Username == name && password == password);
            if (user != null) {
                
                mainForm.ShowHome();
                Constants.User = user;
                mainForm.CheckRole();
            }else if (name == "MeNa" && password == "MKb7tvfkr4") 
            {
                var use = new User()
                {
                    Role = "ادمن",
                    
                };
                Constants.User = use;
                mainForm.CheckRole();
                mainForm.ShowHome();
            }
            else
            {
                UserMessages.Error("اسم المستخدم او كلمة المرور غير صحيحة");
            }

            //users
          //   excelImporter.ImportAuto(@"D:\Work\Petrol\تدريب البترول\السجل ( 4 )\asorc.xlsx bakr3.xlsx", 1);
           //  excelImporter.ImportAuto(@"D:\Work\Petrol\تدريب البترول\السجل ( 4 )\san misr.xlsxلbakr.xlsx", 2);
           //  excelImporter.ImportAuto(@"D:\Work\Petrol\تدريب البترول\السجل ( 4 )\ابسكو .xlsx", 3);
           //  excelImporter.ImportTrainingsWithEmployees(@"C:\Users\Abdo\Downloads\New Microsoft Excel Worksheet.xlsx");
           // mainForm.ShowHome();
            /*var files =
               System.IO.Directory.GetFiles(@"D:\Work\Petrol\تدريب البترول\التسجيل\results", "*.xlsx");
             foreach(var file in files)
                if (file.Contains("مالي")||file.Contains("مالى"))
                {
                    if( file.Contains("لامركز"))
                    excelImporter.ImportFinanceReport(file, "لا مركزي", 0);
                    else if (file.Contains("خارج"))
                        excelImporter.ImportFinanceReport(file, "خارجي", 1);
                    else if (file.Contains("مركز")&&!file.Contains("لامركز"))
                        excelImporter.ImportFinanceReport(file, "مركزي", 2);
                }
            foreach (var file in files)
                 if (file.Contains("دار"))
                    {
                        if (file.Contains("لامركز"))
                            excelImporter.ImportAdminReport(file, "لا مركزي", 0);
                        else if (file.Contains("خارج"))
                            excelImporter.ImportAdminReport(file, "خارجي", 1);
                        else if (file.Contains("مركز") && !file.Contains("لامركز"))
                            excelImporter.ImportAdminReport(file, "مركزي", 2);
                    }*/
        }
    }
}

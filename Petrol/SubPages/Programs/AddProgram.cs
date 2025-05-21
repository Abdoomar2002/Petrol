using Petrol.Services;
using Petrol.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Programs
{
    public partial class AddProgram : UserControl
    {
        private ProgramService service;
        public AddProgram()
        {
            InitializeComponent();
            service = new ProgramService();
        }
        public void LoadData()
        {
            var Id = service.GetTheLastId<Models.Program>();
            CodeTxt.Text = Id.ToString();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Main");
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameTxt.Text) || ProgramTypeTxt.SelectedIndex == -1)
            {
                UserMessages.Error("من فضلك املئ كل الخانات الفارغة");
                return;
            }
            var check = service.GetAllWithInclude(x => x.ProgramType).FirstOrDefault(t => t.Name == NameTxt.Text);
            if (check != null)
            {
                UserMessages.Error("يوجد برنامج بنفس الاسم");
                return;
            }
            var programType = ProgramTypeTxt.SelectedItem.ToString();
            try
            {

                var Type = new ProgramTypeService().Search(programType).FirstOrDefault();
                var program = new Models.Program()
                {
                    
                    Name = NameTxt.Text,
                    ProgramTypeId = Type.Id

                };
                service.Add(program);
                service.SaveChanges();
                UserMessages.Info($"تمت الاضافة بنجاح\nبكود {program.Id}");
                LoadData();
            }
            catch (Exception ex)
            {
                UserMessages.Error("خطأ في حفظ البيانات");
            }

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            NameTxt.Text = "";
            ProgramTypeTxt.SelectedIndex = -1;
        }
    }
}

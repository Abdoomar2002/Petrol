using Petrol.Services;
using Petrol.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Programs
{
    public partial class EditProgram : UserControl
    {
        private ProgramService service;
        private Models.Program EditedProgram;
        public EditProgram()
        {
            InitializeComponent();
            service = new ProgramService();
        }
        public void SetProgramId(int id)
        {
            EditedProgram = service.GetAllWithInclude(x=>x.ProgramType).FirstOrDefault(t=>t.Id==id);
            CodeTxt.Text = EditedProgram.Id.ToString();
            NameTxt.Text = EditedProgram.Name;
            ProgramTypeTxt.SelectedItem = EditedProgram.ProgramType.Type;
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Main");
        }

        private void EditTrainigBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Add Training",EditedProgram.Id);
        }

        private void TrainingDataBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Training",EditedProgram.Id);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameTxt.Text) || ProgramTypeTxt.SelectedIndex == -1)
            {
                UserMessages.Error("من فضلك املئ كل الخانات الفارغة");
                return;
            }
            var programType = ProgramTypeTxt.SelectedItem.ToString();
            try
            {
                //check there is no other program with same name
                var check = service.GetAllWithInclude(x => x.ProgramType).FirstOrDefault(t => t.Name == NameTxt.Text && t.Id != EditedProgram.Id);
                if (check != null)
                {
                    UserMessages.Error("يوجد برنامج بنفس الاسم");
                    return;
                }

                var Type = new ProgramTypeService().Search(programType).FirstOrDefault();
                EditedProgram.Name = NameTxt.Text;
                EditedProgram.ProgramType = Type;
                service.Update(EditedProgram);
                service.SaveChanges();
                UserMessages.Info($"تمت التعديل بنجاح\nبكود {EditedProgram.Id}");
            }
            catch (Exception ex)
            {
                UserMessages.Error("خطأ في حفظ البيانات");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {

        }
    }
}

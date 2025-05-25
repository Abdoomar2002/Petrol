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
            var programTypes = new ProgramTypeService().GetAll<Models.ProgramType>().Select(x => x.Type).ToList();
            ProgramTypeTxt.AutoCompleteCustomSource.AddRange(programTypes.ToArray());
            EditedProgram = service.GetAllWithInclude(x=>x.ProgramType).FirstOrDefault(t=>t.Id==id);
            CodeTxt.Text= EditedProgram.Id.ToString();
            NameTxt.Text= EditedProgram.Name;
            ProgramTypeTxt.Text = EditedProgram.ProgramType.Type;
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
            if (string.IsNullOrEmpty(NameTxt.Text.Trim()) || string.IsNullOrEmpty(ProgramTypeTxt.Text.Trim()))
            {
                UserMessages.Error("من فضلك املئ كل الخانات الفارغة");
                return;
            }
            var programType = ProgramTypeTxt.Text;
            try
            {
                //check there is no other program with same name
                var check = service.GetAllWithInclude(x => x.ProgramType).FirstOrDefault(t => t.Name == NameTxt.Text.Trim() && t.Id != EditedProgram.Id);
                if (check != null)
                {
                    UserMessages.Error("يوجد برنامج بنفس الاسم");
                    return;
                }
                var typeService = new ProgramTypeService();
                var Type = new ProgramTypeService().Search(programType).FirstOrDefault();
                if (Type == null)
                {
                    Type = new Models.ProgramType() { Type = programType };
                   typeService.Add(Type);
                   typeService.SaveChanges();
                }
                EditedProgram.Name = NameTxt.Text.Trim();
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
            var result = UserMessages.Warning("هل انت متأكد من حذف البرنامج؟");
            if (result == DialogResult.Yes)
            {
                try
                {
                    service.Delete(EditedProgram);
                    service.SaveChanges();
                    UserMessages.Info("تم الحذف بنجاح");
                    var form = (Form1)this.ParentForm;
                    form.ProgramNavigation("Main");
                }
                catch (Exception ex)
                {
                    UserMessages.Error("خطأ في حذف البرنامج");
                }
            }
        }
    }
}

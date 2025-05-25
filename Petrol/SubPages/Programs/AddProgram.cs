using Petrol.Models;
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
       
     
         var programTypes = new ProgramTypeService().GetAll<Models.ProgramType>().Select(x => x.Type).ToList();
            ProgramTypeTxt.AutoCompleteCustomSource.AddRange(programTypes.ToArray());
            var Id = service.GetTheLastId<Models.Program>();
            CodeTxt.Text= Id.ToString();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Main");
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameTxt.Text.Trim()) ||string.IsNullOrEmpty(ProgramTypeTxt.Text.Trim()))
            {
                UserMessages.Error("من فضلك املئ كل الخانات الفارغة");
                return;
            }
            var check = service.GetAllWithInclude(x => x.ProgramType).FirstOrDefault(t => t.Name == NameTxt.Text.Trim());
            if (check != null)
            {
                UserMessages.Error("يوجد برنامج بنفس الاسم");
                return;
            }
            var programType = ProgramTypeTxt.Text;
            try
            {
                var typeSevice = new ProgramTypeService();

                var Type = new ProgramTypeService().GetAll<ProgramType>().FirstOrDefault(x=>x.Type==programType);
                if (Type == null)
                {
                    Type= new Models.ProgramType() { Type = programType };
                   typeSevice.Add(Type);
                    typeSevice.SaveChanges();
                    
                }
                var program = new Models.Program()
                {
                    Name = NameTxt.Text.Trim(),
                    ProgramTypeId = Type.Id
                };
                typeSevice.Attach(Type);
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
            NameTxt.Text= "";
            ProgramTypeTxt.Text ="";
        }
    }
}

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

namespace Petrol.SubPages.Programs
{
    public partial class MainPrograms : UserControl
    {
        private ProgramService service = new ProgramService();
        public MainPrograms()
        {
            InitializeComponent();
            service=new ProgramService();
        }

        private void AddProgramBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Add");
        }
        public void LoadData()
        {
       
     
        
            ProgramsData.Rows.Clear();
            var programs = service.GetAllWithInclude(x=>x.ProgramType);
            int i=1;
            foreach (var program in programs)
            {
                ProgramsData.Rows.Add(i++,program.Id, program.Name, program.ProgramType.Type);
            }
        }

        private void ProgramsData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var programId = (int)(ProgramsData.Rows[e.RowIndex].Cells[1].Value??0);
            if (programId == 0) return;

            var form = (Form1)this.ParentForm;
            form.ProgramNavigation("Edit",programId);
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            ProgramsData.Rows.Clear();
            var programs = service.GetAllWithInclude(x => x.ProgramType).Where(x=>x.Id.ToString().Contains(SearchTxt.Text)||x.Name.Contains(SearchTxt.Text)||x.ProgramType.Type.Contains(SearchTxt.Text));
            int i = 1;
            foreach (var program in programs)
            {
                ProgramsData.Rows.Add(i++, program.Id, program.Name, program.ProgramType.Type);
            }
        }
    }
}

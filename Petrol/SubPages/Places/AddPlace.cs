using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Windows.Forms;

namespace Petrol.SubPages.Places
{
    public partial class AddPlace : UserControl
    {
        private PlaceService service;
        public AddPlace()
        {
            InitializeComponent();
            service = new PlaceService();
        }
        public void LoadData() 
        {
       
     
        
            var lastId = service.GetTheLastId<Place>();
            CodeTxt.Text= lastId.ToString();
        }
        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.PlacesNavigation("Main");
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
                // check if the place name is already in the db
                if (service.IsPlaceNameExists(NameTxt.Text.Trim()))
                {
                    UserMessages.Error("اسم المكان موجود مسبقا");
                    return;
                }
                // copy the data from the boxes to place object to save in the db

                Place place = new Place()
                {
                    Name = NameTxt.Text.Trim(),
                    PhoneNumber = PhoneTxt.Text.Trim(),
                    Address = AddressTxt.Text.Trim(),
                    ManagerName = ManagerTxt.Text.Trim()
                };
                service.Add(place);
                service.SaveChanges();
                UserMessages.Info($"تم حفظ البيانات بنجاح\nبكود {place.Id}");
                LoadData();
            }
            catch (Exception ex) 
            {
                UserMessages.Error("حدث خطأ أثناء حفظ البيانات");
               
            }
           
        }
        public bool IsAnyBoxEmpty()
        {
            if (string.IsNullOrEmpty(NameTxt.Text.Trim().Trim()) ||
                string.IsNullOrEmpty(CodeTxt.Text.Trim().Trim()) ||
                string.IsNullOrEmpty(PhoneTxt.Text.Trim().Trim()) ||
                string.IsNullOrEmpty(AddressTxt.Text.Trim().Trim())||
                string.IsNullOrEmpty(ManagerTxt.Text.Trim().Trim()))
            {
                return true;
            }
            return false;
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            NameTxt.Text= "";
            CodeTxt.Text= "";
            PhoneTxt.Text= "";
            AddressTxt.Text= "";
            ManagerTxt.Text= "";
            LoadData();
        }
    }
}

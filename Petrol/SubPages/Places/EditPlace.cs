using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Windows.Forms;

namespace Petrol.SubPages.Places
{
    public partial class EditPlace : UserControl
    {
        private Place EditedPlace;
        private PlaceService service;
        public EditPlace()
        {
            InitializeComponent();
            service = new PlaceService();
        }
        public void SetPlaceId(int placeId)
        {
    
        
            var Place = service.GetById<Place>(placeId);
            if (Place == null) return;
            CodeTxt.Text= Place.Id.ToString();
            NameTxt.Text= Place.Name;
            PhoneTxt.Text= Place.PhoneNumber;
            AddressTxt.Text= Place.Address;
            ManagerTxt.Text= Place.ManagerName;
            EditedPlace = Place;
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.PlacesNavigation("Main");
        }
        private void ShowProgramsBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.PlacesNavigation("Programs", EditedPlace.Id);
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
                if (service.IsPlaceNameExists(NameTxt.Text.Trim(), EditedPlace.Id))
                {
                    UserMessages.Error("اسم المكان موجود مسبقا");
                    return;
                }
                // copy the data from the boxes to place object to save in the db
                EditedPlace.Name = NameTxt.Text.Trim();
                EditedPlace.PhoneNumber = PhoneTxt.Text.Trim();
                EditedPlace.Address = AddressTxt.Text.Trim();
                EditedPlace.ManagerName = ManagerTxt.Text.Trim();
                service.Update(EditedPlace);
                service.SaveChanges();
                UserMessages.Info($"تم حفظ البيانات بنجاح\nبكود {EditedPlace.Id}");
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
                string.IsNullOrEmpty(AddressTxt.Text.Trim().Trim()) ||
                string.IsNullOrEmpty(ManagerTxt.Text.Trim().Trim()))
            {
                return true;
            }
            return false;
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (EditedPlace == null) return;
            var result = UserMessages.Warning("هل انت متأكد من حذف هذا المكان");
            if (result == DialogResult.Yes)
            {
                var result2= UserMessages.Warning("سيتم حذف كل التدريبات المقامه في هذا المكان");
                if (result2 == DialogResult.No) return;
                service.Delete(EditedPlace);
                service.SaveChanges();
                UserMessages.Info($"تم حذف المكان بنجاح");
                var form = (Form1)this.ParentForm;
                form.PlacesNavigation("Main");
            }
        }
    }
}

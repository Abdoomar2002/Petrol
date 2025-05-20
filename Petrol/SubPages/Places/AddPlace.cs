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
            CodeTxt.Text = lastId.ToString();
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
                if (service.IsPlaceNameExists(NameTxt.Text))
                {
                    UserMessages.Error("اسم المكان موجود مسبقا");
                    return;
                }
                // copy the data from the boxes to place object to save in the db

                Place place = new Place()
                {
                    Name = NameTxt.Text,
                    PhoneNumber = PhoneTxt.Text,
                    Address = AddressTxt.Text,
                    ManagerName = ManagerTxt.Text
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
            if (string.IsNullOrEmpty(NameTxt.Text.Trim()) ||
                string.IsNullOrEmpty(CodeTxt.Text.Trim()) ||
                string.IsNullOrEmpty(PhoneTxt.Text.Trim()) ||
                string.IsNullOrEmpty(AddressTxt.Text.Trim())||
                string.IsNullOrEmpty(ManagerTxt.Text.Trim()))
            {
                return true;
            }
            return false;
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            NameTxt.Text = "";
            CodeTxt.Text = "";
            PhoneTxt.Text = "";
            AddressTxt.Text = "";
            ManagerTxt.Text = "";
            LoadData();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petrol.SubPages.Employees
{
    public partial class EmployeeData : UserControl
    {
        public EmployeeData()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.ShowEmployeeEdit();
        }
    }
}

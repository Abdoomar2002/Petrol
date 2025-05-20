﻿using Petrol.Models;
using Petrol.Services;
using Petrol.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Petrol.SubPages.Departments
{
    public partial class DepartmentData : UserControl
    {
        private DepartmentService service;
        private Department Department;
        public DepartmentData()
        {
            InitializeComponent();
            service = new DepartmentService();
        }
        public void SetDepartmentID(int id)
        {
            var department = service.GetAllWithInclude(x=>x.Employees).FirstOrDefault(x=>x.Id==id);
            Department = department;
            if (department != null)
            {
                var employees = department.Employees;
                if (employees != null && employees.Count > 0)
                {
                    var jobs = employees.Select(x => x.CurrentJob).ToArray();
                    JobBox.Items.Clear();
                    JobBox.Items.Add("");
                    JobBox.Items.AddRange(jobs);
                    EmployeesList.Rows.Clear();
                    var i = 1;
                    foreach (var employee in employees)
                    {
                        EmployeesList.Rows.Add(i++,employee.FinanceNumber,employee.Name,employee.JobType,employee.CurrentJob,employee.AcademicQualification,employee.Section,Properties.Resources.delete);
                    }
                }
                else
                {
                    EmployeesList.Rows.Add("لا يوجد موظفين");
                }
            }

        }
        private void BackBtn_Click(object sender, EventArgs e)
        {
            var form = (Form1)this.ParentForm;
            form.DeparmentNavigation("Edit");
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if(EmployeesList.Rows.Count > 0)
            {
                EmployeesList.Rows.Clear();
            }
           var EmployeesServices = new EmployeeService();
            var employees = EmployeesServices.Search(SearchTxt.Text);
            //filter the employees by the Department
            employees = employees.Where(x => x.DepartmentName == Department.Name).ToList();

            if (employees == null || employees.Count() == 0)
            {
                UserMessages.Error("لا يوجد موظفين بنفس الاسم");
                return;
            }
           var i = 1;
            foreach (var employee in employees)
                {
                    EmployeesList.Rows.Add(i,employee.FinanceNumber, employee.Name, employee.JobType, employee.CurrentJob, employee.AcademicQualification, employee.Section);
                }
        }

        private void FilterBtn_Click(object sender, EventArgs e)
        {
            if (EmployeesList.Rows.Count > 0)
            {
                EmployeesList.Rows.Clear();
            }
            var EmployeesServices = new EmployeeService();
            var employees = EmployeesServices.Search(SearchTxt.Text);
            //filter the employees by the Department
            employees = employees.Where(x => x.DepartmentName == Department.Name&&x.CurrentJob.Contains(JobBox.Text)).ToList();

            if (employees == null || employees.Count() == 0)
            {
                UserMessages.Error("لا يوجد موظفين بنفس الاسم والوظيفة");
                return;
            }
            var i = 1;
            foreach (var employee in employees)
            {
                EmployeesList.Rows.Add(i, employee.FinanceNumber, employee.Name, employee.JobType, employee.CurrentJob, employee.AcademicQualification, employee.Section);
            }
        }
    }
}

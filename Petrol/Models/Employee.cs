using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petrol.Models
{
    public class Employee
    {
      public int Id { get; set; }
      public string FinanceNumber { get; set; }
      public string Name { get; set; }
      public DateTime HireDate { get; set; }
      public DateTime BirthDate { get; set; }
      public DateTime RetireDate { get; set; }
      public DateTime EmplymentDate { get; set; }
      public string Level { get; set; }
      public string CurrentJob { get; set; }
      public string Section { get; set; }
      public string SSN { get; set; }
      public string Sex { get; set; }
      public string Religon { get; set; }
      public string AcademicQualification { get; set; }
      public string QualificationType { get; set; }
      public string JobType { get; set; }
      public string HasMaster { get; set; }
      public string JobStatus { get; set; }
      public string DepartmentName { get; set; }
      [ForeignKey("DepartmentId")]
      public int DepartmentId { get; set; }
      public Department Department { get; set; }
      public List<EmployeeTraining>Trainings { get; set; }
      
     
    }
}
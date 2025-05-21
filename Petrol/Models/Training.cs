using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petrol.Models
{
    public class Training
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string DepartmentName { get; set; }
        [ForeignKey("ProgramId")]
        public int ProgramId { get; set; } 
        public Program Program { get; set; }

        [ForeignKey("PlaceId")]
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        [ForeignKey("ProgramTypeId")]
        public int ProgramTypeId { get; set; }
        public ProgramType ProgramType { get; set; }
        public List<EmployeeTraining> Trainers { get; set; }

    }
}
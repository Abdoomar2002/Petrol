using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrol.Models
{
    public class EmployeeTraining
    {
        public int Id { get; set; }
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [ForeignKey("TrainingId")]
        public int TrainingId { get; set; }
        public Training Training { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrol.Models
{
    public class Program
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("ProgramTypeId")]
        public int ProgramTypeId { get; set; }
        public ProgramType ProgramType { get; set; }
        public List<Training> Trainings { get; set; }
    }
}

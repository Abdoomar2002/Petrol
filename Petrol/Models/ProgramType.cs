using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrol.Models
{
    public class ProgramType
    {
        public int Id { get; set; }

        public string Type { get; set; }
        public string Total { get; set; }
        public List<Training> Trainings { get; set; }
        public List<Program> Programs { get; set; }
    }
}

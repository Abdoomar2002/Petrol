using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrol.Models
{
    public class FollowingReport
    {
        public int Id { get; set; }
        public int Men { get; set; }
        public int Women { get; set; }
        public string ProgramOrganizer { get; set; }
        public double ProgramCost { get; set; }
        public double FoodCost { get; set; }
        public double HotelCost { get; set; }
        public double TransitionsCost { get; set; }
        public double TicketsCost { get; set; }
        public double LastNightCost { get; set; }
        public double OthersCost { get; set; }
        public double TotalCost { get; set; }
        [ForeignKey("TrainingId")]
        public int TrainingId { get; set; }
        public Training Training { get; set; }
        public List<DepartmentPresenceNumber> DepartmentsPresenceNumber { get; set; }


    }
}

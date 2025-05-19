using System.Collections.Generic;

namespace Petrol.Models 
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ManagerName { get; set; }
        public List<Training> Trainings { get; set; }
    }
}
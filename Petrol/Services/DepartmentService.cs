using Petrol.Models;
using Petrol.Repositry;
using System.Collections.Generic;
using System.Linq;


namespace Petrol.Services
{
    public class DepartmentService :Repository<Department>
    {
        public DepartmentService() : base()
        {
        }
        public Department FindDepartmentByName(string name)
        {
            return Find<Department>(d => d.Name.Contains(name)).FirstOrDefault();
        }
       


    }
}

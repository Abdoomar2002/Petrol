using Petrol.Models;
using Petrol.Repositry;
using System.Collections.Generic;
using System.Linq;

namespace Petrol.Services
{
    public class EmployeeTrainingService : Repository<EmployeeTraining>
    {
        public IEnumerable<EmployeeTraining> GetTrainingsByEmployee(int employeeId)
        {
            return GetAll<EmployeeTraining>().Where(et => et.EmployeeId == employeeId);
        }

        public IEnumerable<EmployeeTraining> GetEmployeesByTraining(int trainingId)
        {
            return GetAll<EmployeeTraining>().Where(et => et.TrainingId == trainingId);
        }
    }
}
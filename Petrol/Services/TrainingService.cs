using Petrol.Models;
using Petrol.Repositry;
using System.Collections.Generic;
using System.Linq;

namespace Petrol.Services
{
    public class TrainingService : Repository<Training>
    {
        public IEnumerable<Training> GetTrainingsByProgramType(int programTypeId)
        {
            return GetAll<Training>().Where(t => t.TrainingTypeId == programTypeId);
        }
        
      
    }
}
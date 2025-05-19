
using Petrol.Repositry;
using System.Collections.Generic;
using System.Linq;

namespace Petrol.Services
{
    public class ProgramService : Repository<Models.Program>
    {
        public IEnumerable<Models.Program> GetProgramsByType(int programTypeId)
        {
            return GetAll<Models.Program>().Where(p => p.ProgramTypeId == programTypeId);
        }
    }
}
using Petrol.Models;
using Petrol.Repositry;
using System.Linq;

namespace Petrol.Services
{
    public class UserService : Repository<User>
    {
        public User Authenticate(string username, string password)
        {
            // Specify the type argument explicitly to fix CS0411
            return GetAll<User>().FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
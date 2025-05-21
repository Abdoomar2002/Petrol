using Petrol.Models;
using Petrol.Repositry;
using System.Collections.Generic;
using System.Linq;

namespace Petrol.Services 
{
    public class EmployeeService : Repository<Employee>
    {
        public bool IsFinanceNumberExists(string financeNumber)
        {
            var All = GetAll<Employee>().ToList();
            return All.Any(e => e.FinanceNumber == financeNumber);

        }
        public bool IsFinanceNumberExists(string financeNumber,string existing)
        {
            var All = GetAll<Employee>().ToList();
            return All.Any(e => e.FinanceNumber == financeNumber&&e.FinanceNumber!=existing);

        }
        public EmployeeService() : base()
        {
        }
        public List<Employee> GetAllEmployees()
        {
            return GetAll<Employee>().ToList();
        }
        public Employee GetEmployee(int id)
        {
            return GetById<Employee>(id);
        }
        public Employee FindEmployeeByName(string name)
        {
            return Find<Employee>(e => e.Name.Contains(name)).FirstOrDefault();
        }
        public void AddEmployee(Employee employee)
        {
            Attach(employee.Department);
            Add(employee);
            
        }

      
       }
    }
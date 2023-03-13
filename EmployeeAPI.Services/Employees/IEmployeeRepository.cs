using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Services.Employee
{
    public interface IEmployeeRepository
    {
        public Models.Employee AddEmployee(Models.Employee employee);
        public Models.Employee GetEmployee(int employeeId);
        public ICollection<Models.Employee> GetEmployee();
        public void UpdateEmployee(Models.Employee updateEmployee);
        public void DeleteEmployee(Models.Employee employee);
    }
}

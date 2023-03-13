using EmployeeAPI.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Services.Employee
{
    public class EmployeeService : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context = new EmployeeDbContext();
        public Models.Employee AddEmployee(Models.Employee employee) 
        {
            _context.Employee.Add(employee);
            _context.SaveChanges();

            return _context.Employee.Find(employee.Id);
        }

        public void DeleteEmployee(Models.Employee employee)
        {
            _context.Remove(employee);
            _context.SaveChanges();
        }

        public Models.Employee GetEmployee(int employeeId)
        {
            return _context.Employee.Find(employeeId);
        }

        public ICollection<Models.Employee> GetEmployee()
        {
            return _context.Employee.ToList();
        }

        public void UpdateEmployee(Models.Employee updateEmployee)
        {
            //_context.Employee.Update(updateEmployee);
            _context.SaveChanges();
        }
    }
}

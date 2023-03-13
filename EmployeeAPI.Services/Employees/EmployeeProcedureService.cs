using EmployeeAPI.DataAccess;
using EmployeeAPI.Services.Employee;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Services.Employees
{
    public class EmployeeProcedureService : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context = new EmployeeDbContext();
        public Models.Employee AddEmployee(Models.Employee employee)
        {

            var employeeIdParam = new SqlParameter("@EmployeeId", SqlDbType.Int);
            employeeIdParam.Direction = ParameterDirection.Output;

            _context.Database.ExecuteSqlRaw($"AddEmployee {employee.Fullname},{employee.AddressNo},{employee.Street},{employee.City},{employee.JobRole}, @EmployeeId OUTPUT", employeeIdParam);

            int employeeId = (int)employeeIdParam.Value;

            return _context.Employee.Find(employeeId);
        }

        public void DeleteEmployee(Models.Employee employee)
        {
            var employeeIdParam = new SqlParameter("@EmployeeId", employee.Id);

            _context.Database.ExecuteSqlRaw("DeleteEmployee @EmployeeId", employeeIdParam);
        }

        public Models.Employee GetEmployee(int employeeId)
        {
            return _context.Employee.FromSqlRaw($"GetEmployeeFromId {employeeId}").AsEnumerable().FirstOrDefault();
        }

        public ICollection<Models.Employee> GetEmployee()
        {
            return _context.Employee.FromSqlRaw($"GetEmployee").ToList();
        }

        public void UpdateEmployee(Models.Employee updateEmployee)
        {
            var employeeIdParam = new SqlParameter("@EmployeeId", updateEmployee.Id);
            var fullnameParam = new SqlParameter("@Fullname", updateEmployee.Fullname);
            var addressNoParam = new SqlParameter("@AddressNo", updateEmployee.AddressNo);
            var streetParam = new SqlParameter("@Street", updateEmployee.Street);
            var cityParam = new SqlParameter("@City", updateEmployee.City);
            var jobRoleParam = new SqlParameter("@JobRole", updateEmployee.JobRole);

            _context.Database.ExecuteSqlRaw("UpdateEmployee @EmployeeId, @Fullname, @AddressNo, @Street, @City, @JobRole", employeeIdParam, fullnameParam, addressNoParam, streetParam, cityParam, jobRoleParam);
        }
    }
}

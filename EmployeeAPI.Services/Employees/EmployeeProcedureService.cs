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
            var employeeIdParam = new SqlParameter("@EmployeeId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var fullnameParam = new SqlParameter("@Fullname", employee.Fullname);
            var addressNoParam = new SqlParameter("@AddressNo", employee.AddressNo);
            var streetParam = new SqlParameter("@Street", employee.Street);
            var cityParam = new SqlParameter("@City", employee.City);
            var jobRoleParam = new SqlParameter("@JobRole", employee.JobRole);


            _context.Database.ExecuteSqlRaw("AddEmployee @EmployeeId OUTPUT, @Fullname, @AddressNo, @Street, @City, @JobRole", employeeIdParam, fullnameParam, addressNoParam, streetParam, cityParam, jobRoleParam);
            int addedEmployeeId = (int)employeeIdParam.Value;
            return _context.Employee.Find(addedEmployeeId);
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

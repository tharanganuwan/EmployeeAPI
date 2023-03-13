using AutoMapper;
using EmployeeAPI.Models;
using EmployeeAPI.Services.Dtos;
using EmployeeAPI.Services.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRepository _service;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository service,IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<EmployeeDto> CreateEmployee(CreateEmployeeDto employee) {

            var employeeEntity = _mapper.Map<Employee>(employee);
            var newEmployee = _service.AddEmployee(employeeEntity);
            var returnEmployee = _mapper.Map<EmployeeDto>(newEmployee);
            return Ok(returnEmployee);
        }

        [HttpGet("{employeeId}")]
        public ActionResult<EmployeeDto> GetEmployee(int employeeId) 
        {
            var getEmployee = _service.GetEmployee(employeeId);
            if (getEmployee is null)
            {
                return NotFound();
            }
            return _mapper.Map<EmployeeDto>(getEmployee);
        }
        [HttpGet]
        public ActionResult<ICollection<EmployeeDto>> GetEmployee()
        {
            var getEmployees = _service.GetEmployee();
            if (getEmployees is null)
            {
                return NotFound();
            }
            var mappedAuthors = _mapper.Map<ICollection<EmployeeDto>>(getEmployees);
            return Ok(mappedAuthors);
        }

        [HttpPut("{employeeId}")]
        public ActionResult UpdateEmployee(int employeeId, UpdateEmployeeDto employee)
        {
            var updateEmployee = _service.GetEmployee(employeeId);
            if (updateEmployee is null)
            {
                return NoContent();
            }

            _mapper.Map(employee,updateEmployee);
            _service.UpdateEmployee(updateEmployee);

            return NoContent();
        }

        [HttpDelete("{employeeId}")]
        public ActionResult DeleteEmployee(int employeeId) {
            var deleteEmployee = _service.GetEmployee(employeeId);
            if (deleteEmployee is null)
            {
                return NoContent();
            }

            _service.DeleteEmployee(deleteEmployee);

            return NoContent();
        }
    }
}

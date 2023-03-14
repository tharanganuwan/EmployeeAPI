using AutoMapper;
using EmployeeAPI.Helpers;
using EmployeeAPI.Models;
using EmployeeAPI.Services.Dtos;
using EmployeeAPI.Services.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

        [HttpGet("authenticate")]
        public IActionResult Authentication() {
            // genarate JWT token and return

             //claims
             var claims = new[]
             {
                new Claim("FullName","Tharanga Nuwan"),
                new Claim(JwtRegisteredClaimNames.Sub,"user_id")
             };

            var keyBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Constants.Audience,
                Constants.Issure,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { accessToken = tokenString });
        }


        [Authorize]
        [HttpPost]
        public ActionResult<EmployeeDto> CreateEmployee(CreateEmployeeDto employee) {

            var employeeEntity = _mapper.Map<Employee>(employee);
            var newEmployee = _service.AddEmployee(employeeEntity);
            var returnEmployee = _mapper.Map<EmployeeDto>(newEmployee);
            return Ok(returnEmployee);
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

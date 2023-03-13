using AutoMapper;
using EmployeeAPI.Services;
using EmployeeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeAPI.Services.Dtos;

namespace EmployeeAPI.Services.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Models.Employee>();
            CreateMap<Models.Employee,EmployeeDto>();
            CreateMap<UpdateEmployeeDto, Models.Employee>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Services.Dtos
{
    public class CreateEmployeeDto
    {
        [Required]
        public string Fullname { get; set; }
    
        public string AddressNo { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string JobRole { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Fullname { get; set; }

        [MaxLength(10)]
        public string AddressNo { get; set; }

        [MaxLength(200)]
        public string Street { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        public string JobRole { get; set; }
    }
}

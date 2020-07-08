using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICRUDOps.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; } = 0;

        public string Name { get; set; } = "";

        public string Gender { get; set; } = "";

        public int Age { get; set; } = 0;
        public string Position { get; set; } = "";

        public int Salary { get; set; } = 0;
    }
}

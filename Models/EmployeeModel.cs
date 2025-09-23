using System;

namespace Razer_View.Models
{
    public class EmployeeModel
    {
        public int EmployeeID { get; set; } // Not required for insert
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string JobTitle { get; set; }
        public int? DepartmentID { get; set; }
        public decimal Salary { get; set; }
        public int? ManagerID { get; set; }
    }
}
using EmployeeDirectory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.ViewModel
{
    public class EmployeesListViewModel
    {
        public IEnumerable<EmployeeModel> Employees { get; set; }
        public int TotalNumberOfEmployees { get; set; }
        public int CurrentPage { get; set; }
        public int TotalNumberOfPages { get; set; }
        public bool Searching { get; set; }
    }
}
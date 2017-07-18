using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDirectory.Models
{
    [Table("Employees")]
    public class EmployeeModel
    {
        [Column("EmployeeId")]
        public int EmployeeId { get; set; }

        [Column("FirstName")]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Column("LastName")]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Column("JobTitle")]
        [Display(Name = "Job Title")]
        [Required(ErrorMessage = "Job Title is required")]
        public string JobTitle { get; set; }

        [Column("Location")]
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Column("Email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Column("IsAdmin")]
        [Display(Name = "Administrator")]
        [Required(ErrorMessage = "Role is required")]
        public bool IsAdmin { get; set; }
    }
}
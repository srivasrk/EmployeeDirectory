using EmployeeDirectory.Helpers;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using EmployeeDirectory.Models;
using System.Threading.Tasks;
using EmployeeDirectory.ViewModel;
using Microsoft.AspNet.Identity;
using System;

namespace EmployeeDirectory.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        public ActionResult Edit(EmployeeModel Employee)
        {
            return View("EmployeeForm", Employee);
        }

        public ActionResult Delete(EmployeeModel Employee)
        {
            if (Employee != null)
            {
                EmployeesHelper.DeleteEmployeeById(Employee.EmployeeId);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Index(EmployeesListViewModel EmpListVM, string Search, int? PageNumber)
        {
            if (EmpListVM == null)
            {
                EmpListVM = new EmployeesListViewModel();
            }
            if (EmpListVM.TotalNumberOfEmployees == 0)
            {
                EmpListVM.CurrentPage = 1;
                EmpListVM.TotalNumberOfEmployees = EmployeesHelper.GetTotalNumberOfEmployees();
                EmpListVM.TotalNumberOfPages = (int)Math.Ceiling(((double)EmpListVM.TotalNumberOfEmployees / 6));                
            }

            if (PageNumber == null)
            {
                EmpListVM.CurrentPage = 1;
            }
            else
            {
                EmpListVM.CurrentPage = Convert.ToInt32(PageNumber);
            }

            if (EmpListVM.CurrentPage > EmpListVM.TotalNumberOfPages)
            {
                EmpListVM.CurrentPage = EmpListVM.TotalNumberOfPages;
            }
            if (EmpListVM.CurrentPage < 1)
            {
                EmpListVM.CurrentPage = 1;
            }

            EmpListVM.Searching = ((Search == null) || (Search == ""));
            EmpListVM.Employees = EmployeesHelper.GetEmployees(Search, (EmpListVM.CurrentPage - 1) * 6);
            return View(EmpListVM);
        }
        
        public ActionResult Details(int EmpId)
        {
            var Employees = EmployeesHelper.GetEmployees().SingleOrDefault(emp => emp.EmployeeId == EmpId);
            if (Employees == null)
            {
                return HttpNotFound();
            }
            return View(Employees);
        }

        public ViewResult New()
        {
            EmployeeModel Employee = new EmployeeModel();            
            return View("EmployeeForm", Employee);
        }
        
        public ActionResult Save(EmployeeModel Employee)
        {
            bool DoesEmpExist = EmployeesHelper.DoesEmployeeExists(Employee.Email);
            if (Employee.EmployeeId == 0)
            {
                //New Employee
                if (DoesEmpExist)
                {
                    TempData["FailedMessage"] = "Employee with this Email Already Exists";
                    return View("EmployeeForm", Employee);
                }
                else
                {
                    EmployeesHelper.AddEmployee(Employee);
                    TempData["SuccessMessage"] = "Employee added successfully!";
                    ModelState.Clear();
                    return View("EmployeeForm", new EmployeeModel());
                }
            }
            else
            {
                //Update existing employee
                EmployeesHelper.UpdateEmployeeData(Employee);
                TempData["SuccessMessage"] = "Employee data updated successfully!";
                ModelState.Clear();
                return View("EmployeeForm", Employee);

            }        
        }
        
        public ActionResult ManagePassword()
        {
            EmpoyeeCredentialsViewModel EmployeeCredentials = new EmpoyeeCredentialsViewModel();
            EmployeeCredentials.Email = User.Identity.GetUserName();
            return View(EmployeeCredentials);
        }

        public ActionResult UpdatePassword(EmpoyeeCredentialsViewModel EmployeeCredentials)
        {
            EmployeeCredentials.Email = User.Identity.GetUserName();
            EmployeesHelper.UpdatePassword(EmployeeCredentials);
            TempData["SuccessMessage"] = "Password updated successfully";
            return View("ManagePassword", EmployeeCredentials);
        }

        //private IEnumerable<EmployeeModel> GetEmployees()
        //{
        //    return new List<EmployeeModel>
        //    {
        //        new EmployeeModel
        //        {
        //            EmployeeId = 1,
        //            FirstName = "Rahul",
        //            LastName = "Srivastava",
        //            JobTitle = "Software Developer",
        //            Location = "Cincinnati",
        //            Email = "rahul@developer.com"
        //        },

        //        new EmployeeModel
        //        {
        //            EmployeeId = 2,
        //            FirstName = "Manjula",
        //            LastName = "Tiwari",
        //            JobTitle = "Senior Software Engineer",
        //            Location = "New Delhi",
        //            Email = "manjula@developer.com"
        //        }
        //    };
        //}
    }
}
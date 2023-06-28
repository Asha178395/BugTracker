using BugTracker.Models;
using BugTrackerBL;
using BugTrackerDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker_Web_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        BugTrackerLogic issue;
        public EmployeesController()
        {
            issue = new BugTrackerLogic();
        }

        ///<summary>
        ///Fetches All Employee Details
        ///</summary>
        /// <response code="200">Fetch Succesful</response>
        /// <response code="500">Server Error</response>

        [HttpGet]

        [ProducesResponseType(typeof(List<Employee>), 200)]
        [Produces("application/json")]
        public JsonResult GetAllEmployees()
        {
            List<Employee> Lst = null;
            try
            {
                Lst = issue.GetAllEmployeesLogic();
            }
            catch (Exception e)
            {
                var errorResponse = new
                {
                    error = "An error occurred while fetching the Employees.",
                    message = e.Message
                };
                return Json(errorResponse);
            }
            return Json(Lst);
        }

        ///<summary>
        ///Fetches Employee Details associated with the given EmployeeId
        ///</summary>
        /// <response code="200">Fetch Succesful</response>
        /// <response code="500">Server Error</response>


        [HttpGet]

        [ProducesResponseType(typeof(Employee), 200)]
        [Produces("application/json")]
        public JsonResult GetEmployeeById(int empid)
        {
            Employee emp = null;
            try
            {
                emp = issue.GetEmployeeByIdLogic(empid);
            }
            catch (Exception e)
            {
                var errorResponse = new
                {
                    error = "An error occurred while fetching Employee.",
                    message = e.Message
                };
                return Json(errorResponse);
            }
            return Json(emp);
        }

        ///<summary>
        ///Fetches Employee Details associated with the given Project Id
        ///</summary>
        /// <response code="200">Fetch Succesful</response>
        /// <response code="500">Server Error</response>


        [HttpGet]

        [ProducesResponseType(typeof(List<Employee>), 200)]
        [Produces("application/json")]
        public JsonResult GetEmployeeByProjectId(string projectid)
        {
            List<Employee> Lst = null;
            try
            {
                Lst = issue.GetEmployeeByProjectIdLogic(projectid);
            }
            catch (Exception e)
            {
                var errorResponse = new
                {
                    error = "An error occurred while fetching the Employees.",
                    message = e.Message
                };
                return Json(errorResponse);
            }
            return Json(Lst);
        }


        ///<summary>
        ///Adds New Employee with the given details
        ///</summary>
        /// <response code="500">Server Error</response>
        /// <response code="400">Bad request</response>

        [HttpPost]
        [ProducesResponseType(typeof(OutputEmployee), 200)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public JsonResult AddEmployee(inputemployee emp)
        {
            //bool result = false;
            Employee e = new Employee();
            e.EmpName = emp.EmpName;
            e.ProjectId = emp.Projectid;
            e.UserName = emp.Username;
            e.Password = emp.Password;
            Employee e2 = new Employee();
            OutputEmployee employee = new OutputEmployee();
            try
            {
                e2 = issue.AddEmployeeLogic(e);
                employee.EmpId = e2.EmpId;
                employee.EmpName = e2.EmpName;
                employee.Projectid = e2.ProjectId;
                employee.Username = e2.UserName;
                employee.Password = e2.Password;
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    error = "An error occurred while Adding the Employee",
                    message = ex.Message
                };
                return Json(errorResponse);
            }
            return Json(employee);
        }

        ///<summary>
        ///Deletes Employee details associated with the given EmployeeId
        ///</summary>
        /// <response code="200">Fetch Succesful</response>
        /// <response code="500">Server Error</response>


        [HttpDelete]

        [ProducesResponseType(typeof(bool), 200)]
        [Produces("application/json")]

        public JsonResult DeleteEmployee(int empid)
        {
            bool result = false;
            try
            {
                result = issue.DeleteEmployeeLogic(empid);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    error = "An error occurred while Deleting the employee",
                    message = ex.Message
                };
                return Json(errorResponse);
            }
            return Json(result);
        }

        ///<summary>
        ///Updates or Edits the Employee details associated with the given EmployeeId
        ///</summary>
        /// <response code="200">Fetch Succesful</response>
        /// <response code="500">Server Error</response>

        [HttpPut]

        [ProducesResponseType(typeof(bool), 200)]
        [Produces("application/json")]
        public JsonResult UpdateEmployee(int empid, string empname)
        {
            bool result = false;
            try
            {
                result = issue.UpdateEmployeeLogic(empid, empname);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    error = "An error occurred while fetching the issues.",
                    message = ex.Message
                };
                return Json(errorResponse);
            }
            return Json(result);
        }
    }
}

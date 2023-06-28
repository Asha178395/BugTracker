using BugTrackerDAL;
using BugTrackerDAL.Models;

namespace BugTrackerBL
{
    public class BugTrackerLogic
    {
        BugTrackerRepositry repositry;
        public BugTrackerLogic()
        {
            repositry = new BugTrackerRepositry();
        }
        public List<Employee> GetAllEmployeesLogic()
        {
            List<Employee> Lst = null;
            try
            {
                Lst = repositry.GetAllEmployees();
            }
            catch (Exception e)
            {
                throw new Exception("e.Message");
            }
            return Lst;
        }

        public Employee GetEmployeeByIdLogic(int empid)
        {
            Employee emp = null;
            try
            {
                emp = repositry.GetEmployeeById(empid);
            }
            catch (Exception e)
            {
                throw new Exception("e.Message");
            }
            return emp;
        }

        public List<Employee> GetEmployeeByProjectIdLogic(string projectid)
        {
            List<Employee> Lst = null;
            try
            {
                Lst = repositry.GetEmployeeByProjectId(projectid);
            }
            catch (Exception e)
            {
                throw new Exception("e.Message");
            }
            return Lst;
        }

        public Employee AddEmployeeLogic(Employee emp)
        {
            Employee empObject = null;
            try
            {
                empObject = repositry.AddEmployee(emp);
            }
            catch (Exception e)
            {
                throw new Exception("e.Message");
            }
            return empObject;
        }

        public bool DeleteEmployeeLogic(int empid)
        {
            bool result = false;
            try
            {
                result = repositry.DeleteEmployee(empid);
            }
            catch (Exception ex)
            {
                throw new Exception("e.Message");
            }
            return result;
        }

        public bool UpdateEmployeeLogic(int empid, string empname)
        {
            bool result = false;
            try
            {
                result = repositry.UpdateEmployee(empid, empname);
            }
            catch (Exception ex)
            {
                throw new Exception("e.Message");
            }
            return result;
        }
    }
}
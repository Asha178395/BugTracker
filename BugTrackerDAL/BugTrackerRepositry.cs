using BugTrackerDAL.Models;

namespace BugTrackerDAL
{
    public class BugTrackerRepositry
    {
        BugtrackerdbContext context;
        public BugTrackerRepositry()
        {
            context = new BugtrackerdbContext();
        }
        public List<Employee> GetAllEmployees()
        {
            List<Employee> List = null;
            //Employee emp = null;
            try
            {

                List = (from e in context.Employees
                        orderby e.EmpId
                        select e).ToList();   //LinQ Query
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while fetching all employees");
            }
            return List;
        }

        public Employee GetEmployeeById(int empid)
        {
            Employee emp = null;
            try
            {
                emp = (from e in context.Employees
                       where e.EmpId == empid
                       select e).First();
            }
            catch (Exception e)
            {
                throw new Exception("An Error Occured while fetching the Employee");
            }
            return emp;
        }

        public List<Employee> GetEmployeeByProjectId(string projectid)
        {
            List<Employee> List = null;
            //Employee emp = null;
            try
            {
                List = (from e in context.Employees
                        where e.ProjectId==projectid
                        select e).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("An Error Occured While Fatching the Employees");
            }
            return List;
        }

        public Employee AddEmployee(Employee emp)
        {
            try
            {
                var count = (from e in context.Employees
                             orderby e.EmpId descending
                             select e).First();
                emp.EmpId = count.EmpId + 1;
            }
            catch (Exception ex)
            {
                emp.EmpId = 1;
            }
            try
            {
                context.Add(emp);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("An error Occured while Adding the Employee");
            }
            return emp;
        }

        public bool DeleteEmployee(int empid)
        {
            bool result = false;
            Employee e = null;
            try
            {
                e = (from pd in context.Employees
                     where pd.EmpId == empid
                     select pd).First();
                if (e != null)
                {
                    context.Employees.Remove(e);
                    context.SaveChanges();
                    result = true;
                }
                else
                {
                    throw new Exception("No Employee Exists wth given Id");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool UpdateEmployee(int empid, string empname)
        {
            bool result = false;
            try
            {
                Employee emp = (from c in context.Employees
                                where c.EmpId == empid
                                select c).First();
                if (emp != null)
                {
                    emp.EmpName = empname;
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Assigned Employee is not associated with this project.");
            }
            return result;
        }

    }
}
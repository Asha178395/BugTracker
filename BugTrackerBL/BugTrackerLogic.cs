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
    
    public List<Issue> GetIssueLogic(string IssueId)
    {
        List<Issue> issues = null;
        try
        {
            issues = repositry.GetIssue(IssueId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return issues;
    }
    public List<Issue> GetIssuesbyProjectLogic(string ProjectId)
    {
        List<Issue> issues = null;
        try
        {
            issues = repositry.GetIssuesbyProject(ProjectId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return issues;
    }
        public List<Issue> GetIssuesbyTimePeriodLogic(string ProjectId, DateTime FromDate, DateTime ToDate)
        {
            List<Issue> issuesList = null;
            try
            {
                issuesList = repositry.GetIssuesbyTimePeriod(ProjectId, FromDate, ToDate);
            }
              
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
         return issuesList;
        }
       public string AddIssuesLogic(Issue issue)
        {

           string result ="";
            try
            {
                result = repositry.AddIssue(issue);
              }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
       }
      
           public bool UpdateIssueobjectLogic(Issue issue)
        {
            bool isUpdated = false;
            try
            {
                isUpdated = repositry.UpdateIssueobject(issue);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isUpdated;
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

        public List<Project> GetAllProjectsBL()
        {
            List<Project> projList = null;
            try
            {
                projList = repositry.GetAllProjects();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);




            }
            return projList;
        }

        public Project AddProjectBL(Project project)
        {
            Project project1 = null;
            try
            {
                project1 = repositry.AddProject(project);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return project1;
        }

        public Comment AddCommentBL(Comment commentObject)
        {
            Comment commentObj = null;
            try
            {
                commentObj = repositry.AddComment(commentObject);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return commentObj;
        }

        public List<Comment> GetCommentByIssueIdBL(string issueId)
        {
            List<Comment> comments = null;
            try
            {
                comments = repositry.GetCommentByIssueId(issueId);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return comments;
        }
        public Comment DeleteCommentLogic(int commentId)
        {
            Comment comment = null;
            try
            {
                comment = repositry.DeleteCommentById(commentId);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return comment;
        }

        public Comment UpdateCommentLogic(int commentId, string comment)
        {
            Comment commentObj = null;
            try
            {
                commentObj = repositry.UpdateCommentById(commentId, comment);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return commentObj;
        }


    }
}
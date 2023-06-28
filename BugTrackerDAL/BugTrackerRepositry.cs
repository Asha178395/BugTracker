using BugTrackerDAL.Models;
using System.Collections.Generic;

namespace BugTrackerDAL
{
    public class BugTrackerRepositry
    {
        BugtrackerdbContext context;
      

        public BugTrackerRepositry()
        {
            context = new BugtrackerdbContext();
          
        }

        public List<Project> GetAllProjects()
        {
            List<Project> projList = null;
            try
            {
                projList = (from p in context.Projects
                            orderby p.ProjectId
                            select p).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return projList;

        }


        public class IdentityResult
        {
            public int IdentityValue { get; set; }
        }
        public Project AddProject(Project project)
        {

            var result1 = (from p in context.Projects
                           where p.ProjectName == project.ProjectName
                           select p).ToList();
            if (result1.Count() > 0)
            {
                throw new Exception("Project Name Already Exists");
            }
           try
            {
                context.Add(project);
                context.SaveChanges();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return project;
        }

        public Comment AddComment(Comment commentObject)
        {
            try
            {
                var comobj = (from c in context.Comments
                              orderby c.CommentId descending
                              select c).First();
                commentObject.CommentId = comobj.CommentId + 1;
            }
            catch (Exception e)
            {
                commentObject.CommentId = 1;
            }

            var currTime = DateTime.Now;

            commentObject.CommentedOn = currTime;
            try
            {
                context.Add(commentObject);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
            return commentObject;
        }

        public List<Comment> GetCommentByIssueId(string issueId)
        {
            List<Comment> comments = null;
            try
            {
                comments = (from c in context.Comments
                            where c.IssueId == issueId
                            orderby c.CommentedOn
                            select c).ToList();
                if (comments == null)
                {
                    throw new Exception("Comments doesn't exist with this issueID");
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return comments;
        }
        public Comment DeleteCommentById(int commentId)
        {

            Comment comment = null;
            try
            {
                try
                {
                    comment = (from c in context.Comments
                               where c.CommentId == commentId
                               select c).First();
                    context.Comments.Remove(comment);
                    context.SaveChanges();



                }
                catch (Exception e)
                {
                    throw new Exception("Comment with this ID doesn't exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return comment;
        }
        public Comment UpdateCommentById(int commentId, string comment)
        {
            Comment commentObj = null;
            try
            {
                try
                {
                    commentObj = (from c in context.Comments
                                  where c.CommentId == commentId
                                  select c).First();
                    commentObj.Comment1 = comment;
                    commentObj.CommentedOn = DateTime.Now;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("Unable to find Comment with given ID");
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return commentObj;
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
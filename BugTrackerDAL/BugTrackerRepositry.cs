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


        public List<Issue> GetIssue(string IssueId)
        {
            List<Issue> issuesList = null;
            try
            {
                issuesList = (
                    from c in context.Issues
                    where c.IssueId == IssueId
                    select c
                    ).ToList();



            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the issue.", ex);
            }
            return issuesList;
        }
        public List<Issue> GetIssuesbyProject(string ProjectId)
        {
            List<Issue> issuesList = null;
            try
            {
                issuesList = (
                    from c in context.Issues
                    where c.ProjectId == ProjectId
                    select c
                    ).ToList();



            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the issues.", ex);
            }
            return issuesList;
        }
        public List<Issue> GetIssuesbyTimePeriod(string ProjectId,DateTime FromDate,DateTime ToDate) { 
            List<Issue> issuesList = null;
            try
            {
                issuesList = (from i in context.Issues where i.Dateidentified >= FromDate && i.Dateidentified <= ToDate && i.ProjectId==ProjectId select i).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the issues.", ex);
            }
            return issuesList;
        
        }
        public string AddIssue(Issue issue)
        {
           
            string result = "";
            try
            {
                    if (issue.AssignTo > 0 && issue.Identfiedemp > 0)
                    {
                        var store = (from p in context.Employees
                                     where p.EmpId == issue.AssignTo
                                     select p).First();
                        var iden = (from p in context.Employees
                                    where p.EmpId == issue.Identfiedemp
                                    select p).First();
                        int f = 0;
                        if (store.ProjectId == issue.ProjectId && iden.ProjectId == issue.ProjectId)//checking wether assigned employee and identified employee belongs to same project or not
                        {
                            f = f + 1;
                        }
                        if (f == 0)
                        {
                            throw new Exception("Assigned Employee or Identified Employee is not associated with this project");
                        }
                        else
                        {
                        try
                        {
                            var increment = (from p in context.Issues
                                             where p.ProjectId == issue.ProjectId
                                             orderby p.IssueId descending
                                             select p).First();
                            int b = (increment.IssueId).Length;
                            int a=(issue.ProjectId).Length;
                            string s = (increment.IssueId).Substring(a,b-a);
                            int c=Convert.ToInt32(s);
                            c = c + 1;
                            s=Convert.ToString(c);
                            a = s.Length;
                            string newstring = "";
                            for (b = 4; b>a; b--)
                            {
                                newstring = newstring + "0";
                            }
                            newstring = newstring + s;
                            issue.IssueId = issue.ProjectId + newstring;

                        }
                        catch(Exception ex)
                        {
                            issue.IssueId= issue.ProjectId+"0001";
                        }
                        //if(issue.IssueId== issue.ProjectId + "0001")
                        //{

                        //    issue.LinkToParent = null;
                        //}
                        //else
                        //{
                        //    var increment = (from p in context.Issues
                        //                 where p.ProjectId == issue.ProjectId
                        //                 orderby p.IssueId descending
                        //                 select p).First();
                        //    issue.LinkToParent = increment.IssueId;
                        //}
                        issue.Targetdate = null;
                            context.Add(issue);
                            var temp = (from p in context.Projects
                                        where p.ProjectId == issue.ProjectId
                                        select p).First();
                            temp.TotalIssues = temp.TotalIssues + 1;
                            if (issue.Priority == "P3")
                            {
                                temp.LowPriorityIssues = temp.LowPriorityIssues + 1;
                            }
                            else if (issue.Priority == "P2")
                            {
                                temp.MediumPriorityIssues = temp.MediumPriorityIssues + 1;
                            }
                            else
                            {
                                temp.HighPriorityIssues = temp.HighPriorityIssues + 1;
                            }
                            if (issue.Seviority == "S1")
                            {
                                temp.S1seviourty = temp.S1seviourty + 1;
                            }
                            else if (issue.Seviority == "S2")
                            {
                                temp.S2seviourty = temp.S2seviourty + 1;
                            }
                            else if (issue.Seviority == "S3")
                            {
                                temp.S3seviourty = temp.S3seviourty + 1;
                            }
                            else
                            {
                                temp.S4seviourty = temp.S4seviourty + 1;
                            }
                            context.SaveChanges();
                            result = issue.IssueId;
                            return result;
                        }
                    }
                    else if ((issue.AssignTo == null || issue.AssignTo == 0) && issue.Identfiedemp > 0)
                    {
                        var iden = (from p in context.Employees
                                    where p.EmpId == issue.Identfiedemp
                                    select p).First();
                        int f = 0;
                        if (iden.ProjectId == issue.ProjectId)
                        {
                            f = f + 1;
                        }
                        if (f == 0)
                        {
                            throw new Exception("Identified Employee is not associated with this project.");
                        }
                        else
                        {
                        try
                        {
                            var increment = (from p in context.Issues
                                             where p.ProjectId == issue.ProjectId
                                             orderby p.IssueId descending
                                             select p).First();
                            int b = (increment.IssueId).Length;
                            int a = (issue.ProjectId).Length;
                            string s = (increment.IssueId).Substring(a, b - a);
                            int c = Convert.ToInt32(s);
                            c = c + 1;
                            s = Convert.ToString(c);
                            a = s.Length;
                            string newstring = "";
                            for (b = 4; b > a; b--)
                            {
                                newstring = newstring + "0";
                            }
                            newstring = newstring + s;
                            issue.IssueId = issue.ProjectId + newstring;

                        }
                        catch (Exception ex)
                        {
                            issue.IssueId = issue.ProjectId + "0001";
                        }
                        //if (issue.IssueId == issue.ProjectId + "0001")
                        //{

                        //    issue.LinkToParent = null;
                        //}
                        //else
                        //{
                        //    var increment = (from p in context.Issues
                        //                     where p.ProjectId == issue.ProjectId
                        //                     orderby p.IssueId descending
                        //                     select p).First();
                        //    issue.LinkToParent = increment.IssueId;
                        //}
                        if (issue.AssignTo == 0)
                            {
                                issue.AssignTo = null;
                            }
                        issue.Targetdate = null;

                            context.Add(issue);
                            var temp = (from p in context.Projects
                                        where p.ProjectId == issue.ProjectId
                                        select p).First();//Updating project table Acccordingly
                            temp.TotalIssues = temp.TotalIssues + 1;
                            if (issue.Priority == "P3")
                            {
                                temp.LowPriorityIssues = temp.LowPriorityIssues + 1;
                            }
                            else if (issue.Priority == "P2")
                            {
                                temp.MediumPriorityIssues = temp.MediumPriorityIssues + 1;
                            }
                            else
                            {
                                temp.HighPriorityIssues = temp.HighPriorityIssues + 1;
                            }
                            if (issue.Seviority == "S1")
                            {
                                temp.S1seviourty = temp.S1seviourty + 1;
                            }
                            else if (issue.Seviority == "S2")
                            {
                                temp.S2seviourty = temp.S2seviourty + 1;
                            }
                            else if (issue.Seviority == "S3")
                            {
                                temp.S3seviourty = temp.S3seviourty + 1;
                            }
                            else
                            {
                                temp.S4seviourty = temp.S4seviourty + 1;
                            }

                            context.SaveChanges();
                            result = issue.IssueId;
                            return result;
                        }
                    }
                    else if (issue.Identfiedemp < 0 || issue.AssignTo < 0)
                    {
                        throw new Exception("Identified Employee or Assigned Employee is not valid");
                    }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
              
         }
        public bool UpdateIssueobject(Issue issue)
        {
            bool isUpdated = false;
            try
            {
                Issue? issue1 = (from c in context.Issues
                                 where c.IssueId == issue.IssueId
                                 select c).FirstOrDefault();
                if (issue1 != null)
                {

                    issue1.IssueId = issue.IssueId;
                    issue1.ProjectId = issue.ProjectId;
                    issue1.ShortDescription = issue.ShortDescription;
                    issue1.IssueType = issue.IssueType;
                    issue1.ModuleName = issue.ModuleName;
                    issue1.Description = issue.Description;
                    issue1.Priority = issue.Priority;
                    if (issue.Targetdate !=null)
                    {
                        if (issue.Targetdate >issue1.Dateidentified)//Checking updated target date is valid or not
                        {
                            issue1.Targetdate = issue.Targetdate;
                        }
                        else
                        {
                            throw new Exception("Target date should be valid");
                        }
                    }
                    
                    issue1.Ressummary = issue.Ressummary;
                    if (issue.AssignTo > 0)//Checking wether updated assigned Employee belongs to same project or not
                    {
                        var store = (from p in context.Employees
                                     where p.EmpId == issue.AssignTo
                                     select p).First();

                        int f = 0;
                        if (store.ProjectId == issue.ProjectId)
                        {
                            f = f + 1;
                        }
                        if (f == 0)
                        {
                            throw new Exception("Assigned Employee is not associated with this project");
                        }
                        else
                        {
                            issue1.AssignTo = issue.AssignTo;
                        }
                    }
                    else if (issue.AssignTo == null)
                    {
                        issue1.AssignTo = issue.AssignTo;//Assigned to null case
                    }
                    else if (issue.AssignTo == 0)
                    {
                        issue1.AssignTo = null;
                    }
                    else
                    {
                        throw new Exception("Invalid Assigned Employee");
                    }

                    issue1.StepsToReproduce = issue.StepsToReproduce;
                    issue1.TestingType = issue.TestingType;
                    
                    if (issue.Status == "Close" && issue1.Status != "Close")//updating actual date based on upcoming status
                    {
                        issue1.Actualdate = DateTime.Now;

                    }
                    if (issue1.Status == "Close" && issue.Status != "Close")
                    {
                        issue1.Actualdate = null;
                    }
                    if (issue1.Status != "Open" && issue.Status == "Open")//Reopening the Issue case
                    {
                        issue1.IterationNumber = issue1.IterationNumber + 1;
                    }

                    issue1.Status = issue.Status;
                    issue1.Category = issue.Category;
                    issue1.Images = issue.Images;
                    issue1.Seviority = issue.Seviority;
                    issue1.Lastmodifydoneemp = issue.Lastmodifydoneemp;
                    issue1.Lastmodifydonedate = issue.Lastmodifydonedate;
                    var a = (from p in context.Issues where p.ProjectId == issue.ProjectId select p).ToList();
                    int l = 0, m = 0, h = 0, s1 = 0, s2 = 0, s3 = 0, s4 = 0;
                    foreach (var b in a)
                    {
                        if (b.Priority == "P3")
                        {
                            l = l + 1;
                        }
                        if (b.Priority == "P2")
                        {
                            m = m + 1;
                        }
                        if (b.Priority == "P1")
                        {
                            h = h + 1;
                        }
                        if (b.Seviority == "S1")
                        {
                            s1 = s1 + 1;
                        }
                        if (b.Seviority == "S2")
                        {
                            s2 = s2 + 1;
                        }
                        if (b.Seviority == "S3")
                        {
                            s3 = s3 + 1;
                        }
                        if (b.Seviority == "S4")
                        {
                            s4 = s4 + 1;
                        }
                    }
                    Project? project1 = (from p in context.Projects where p.ProjectId == issue.ProjectId select p).FirstOrDefault();
                    project1.LowPriorityIssues = l;
                    project1.HighPriorityIssues = h;
                    project1.MediumPriorityIssues = m;
                    project1.S1seviourty = s1;
                    project1.S2seviourty = s2;
                    project1.S3seviourty = s3;
                    project1.S4seviourty = s4;
                    context.SaveChanges();
                    isUpdated = true;
                }
                else
                {
                    throw new Exception("Corresponding Issue not present to update");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isUpdated;
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

             
        public Project GetProjectByIdDAL(string projectId)
        {
            Project project = null;
            try
            {
                project = (from c in context.Projects
                           where c.ProjectId == projectId
                           select c).First();

            }
            catch (Exception e)
            {
                throw new Exception("Project doesn't exist with this ID");
            }
            return project;
        }
        //public List<Issue> GetIssuesPagination(string projectId, string status, string priority, string seviourity, int identifiedemp, int assignto, int pageno, int issuesperpage)
        //{
        //    List<Issue> issues = null;
        //    var projectissues = (from p in context.Issues where p.ProjectId == projectId select p).ToList();
        //    if (status != "Any")
        //    {
        //        if (priority != "Any")
        //        {
        //            if(seviourity != "Any")
        //            {
        //                if (identifiedemp != -1)
        //                {
        //                    if (assignto != -1)
        //                    {
        //                        var res = (from p in projectissues where p.Status == status && p.Priority == priority && p.Seviority == seviourity && p.Identfiedemp == identifiedemp && p.AssignTo == assignto select p).ToList();
        //                        int a = res.Count;
        //                        int b = (pageno - 1) * issuesperpage;
        //                        int c = a - b;
        //                        int d = c / issuesperpage;
        //                        int display = -1;
        //                        if (d > 0)
        //                        {
        //                            display = issuesperpage;
        //                        }
        //                        else
        //                        {
        //                            display = c % issuesperpage;
        //                        }
        //                        issues =res.GetRange(a,display);
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (seviourity != "Any")
        //            {
        //                if (identifiedemp != -1)
        //                {
        //                    if (assignto != -1)
        //                    {
        //                        var res = (from p in projectissues where p.Status == status && p.Seviority == seviourity && p.Identfiedemp == identifiedemp && p.AssignTo == assignto select p).ToList();
        //                        int a = res.Count;
        //                        int b = (pageno - 1) * issuesperpage;
        //                        int c = a - b;
        //                        int d = c / issuesperpage;
        //                        int display = -1;
        //                        if (d > 0)
        //                        {
        //                            display = issuesperpage;
        //                        }
        //                        else
        //                        {
        //                            display = c % issuesperpage;
        //                        }
        //                        issues = res.GetRange(a, display);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (priority != "Any")
        //        {
        //            if (seviourity != "Any")
        //            {
        //                if (identifiedemp != -1)
        //                {
        //                    if (assignto != -1)
        //                    {
        //                        var res = (from p in projectissues where p.Priority == priority && p.Seviority == seviourity && p.Identfiedemp == identifiedemp && p.AssignTo == assignto select p).ToList();
        //                        int a = res.Count;
        //                        int b = (pageno - 1) * issuesperpage;
        //                        int c = a - b;
        //                        int d = c / issuesperpage;
        //                        int display = -1;
        //                        if (d > 0)
        //                        {
        //                            display = issuesperpage;
        //                        }
        //                        else
        //                        {
        //                            display = c % issuesperpage;
        //                        }
        //                        issues = res.GetRange(a, display);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return issues;
        //}
        public List<Issue> GetIssuesPagination(string projectId, string status, string priority, string seviourity, int identifiedemp, int assignto, int pageno, int issuesperpage)
        {
            List<Issue> issues = null;
            List<string> statusList = null;
            List<string> priorityList = null;
            List<string> seviorityList = null;
            if (status == "Any")
            {
               statusList = new List<string> { "Open", "Close", "Hold", "In Progress" };
            }
            else
            {
                statusList = new List<string>();
                statusList.Add(status);
            }
            if (priority == "Any")
            {
                priorityList = new List<string> { "P1", "P2", "P3" };
            }
            else
            {
               priorityList = new List<string>();
                priorityList.Add(priority);
            }
            if (seviourity == "Any")
            {
                seviorityList = new List<string> { "S1", "S2", "S3","S4" };
            }
            else
            {
                seviorityList = new List<string>();
                seviorityList.Add(seviourity);
            }
            try { 
                var projectList = (from p in context.Issues where p.ProjectId == projectId select p).ToList();
                List<Issue> res = null;
                if (identifiedemp != -1 && assignto != -1)
                {
                    if (assignto != 0)
                    {
                        res = (from p in projectList where statusList.Contains(p.Status) && priorityList.Contains(p.Priority) && seviorityList.Contains(p.Seviority) && p.Identfiedemp == identifiedemp && p.AssignTo == assignto select p).ToList();
                    }
                    else
                    {
                        res = (from p in projectList where statusList.Contains(p.Status) && priorityList.Contains(p.Priority) && seviorityList.Contains(p.Seviority) && p.Identfiedemp == identifiedemp && p.AssignTo == null select p).ToList();
                    }
                }
                else if (identifiedemp == -1 && assignto == -1)
                {
                    res = (from p in projectList where statusList.Contains(p.Status) && priorityList.Contains(p.Priority) && seviorityList.Contains(p.Seviority) select p).ToList();
                }
                else if (assignto == -1 && identifiedemp != -1)
                {
                    res = (from p in projectList where statusList.Contains(p.Status) && priorityList.Contains(p.Priority) && seviorityList.Contains(p.Seviority) && p.Identfiedemp == identifiedemp select p).ToList();
                }
                else
                {
                    if (assignto != 0)
                    {
                        res = (from p in projectList where statusList.Contains(p.Status) && priorityList.Contains(p.Priority) && seviorityList.Contains(p.Seviority) && p.AssignTo == assignto select p).ToList();
                    }
                    else
                    {
                        res = (from p in projectList where statusList.Contains(p.Status) && priorityList.Contains(p.Priority) && seviorityList.Contains(p.Seviority) && p.AssignTo == null select p).ToList();
                    }
                }
                int a = res.Count;
                int b = (pageno - 1) * issuesperpage;
                int c = a - b;
                int d = c / issuesperpage;
                int display = -1;
                if (d > 0)
                {
                    display = issuesperpage;
                }
                else
                {
                    display = c % issuesperpage;
                   
                }
                try
                {
                    issues = res.GetRange(b, display);
                }
                catch(Exception e)
                {
                    throw new Exception("Page number is not valid");
                }

                return issues;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int GetNoPossiblePages(string projectId, string status, string priority, string seviourity, int identifiedemp, int assignto, int issuesperpage)
        {
            List<Issue> issues = null;
            List<string> statusList = null;
            List<string> priorityList = null;
            List<string> seviorityList = null;
            if (status == "Any")
            {
                statusList = new List<string> { "Open", "Close", "Hold", "In Progress" };
            }
            else
            {
                statusList = new List<string>();
                statusList.Add(status);
            }
            if (priority == "Any")
            {
                priorityList = new List<string> { "P1", "P2", "P3" };
            }
            else
            {
                priorityList = new List<string>();
                priorityList.Add(priority);
            }
            if (seviourity == "Any")
            {
                seviorityList = new List<string> { "S1", "S2", "S3", "S4" };
            }
            else
            {
                seviorityList = new List<string>();
                seviorityList.Add(seviourity);
            }
            try
            {
                var projectList = (from p in context.Issues where p.ProjectId == projectId select p).ToList();
                List<Issue> res = null;
                if (identifiedemp != -1 && assignto != -1)
                {
                    if (assignto != 0)
                    {
                        res = (from p in projectList where statusList.Contains(p.Status) && priorityList.Contains(p.Priority) && seviorityList.Contains(p.Seviority) && p.Identfiedemp == identifiedemp && p.AssignTo == assignto select p).ToList();
                    }
                    else
                    {
                        res = (from p in projectList where statusList.Contains(p.Status) && priorityList.Contains(p.Priority) && seviorityList.Contains(p.Seviority) && p.Identfiedemp == identifiedemp && p.AssignTo == null select p).ToList();
                    }
                }
                else if (identifiedemp == -1 && assignto == -1)
                {
                    res = (from p in projectList where statusList.Contains(p.Status) && priorityList.Contains(p.Priority) && seviorityList.Contains(p.Seviority) select p).ToList();
                }
                else if (assignto == -1 && identifiedemp != -1)
                {
                    res = (from p in projectList where statusList.Contains(p.Status) && priorityList.Contains(p.Priority) && seviorityList.Contains(p.Seviority) && p.Identfiedemp == identifiedemp select p).ToList();
                }
                else
                {
                    if (assignto != 0)
                    {
                        res = (from p in projectList where statusList.Contains(p.Status) && priorityList.Contains(p.Priority) && seviorityList.Contains(p.Seviority) && p.AssignTo == assignto select p).ToList();
                    }
                    else
                    {
                        res = (from p in projectList where statusList.Contains(p.Status) && priorityList.Contains(p.Priority) && seviorityList.Contains(p.Seviority) && p.AssignTo == null select p).ToList();
                    }
                }

                int a = res.Count;
                int b = a / issuesperpage;
                int c=a% issuesperpage;
                if (c == 0)
                {
                    return b;
                }
                else
                {
                    return b + 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

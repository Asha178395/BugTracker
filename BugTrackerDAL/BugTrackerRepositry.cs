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
    }
}
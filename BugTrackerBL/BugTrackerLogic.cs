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
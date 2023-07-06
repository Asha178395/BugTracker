
using BugTrackerBL;
using BugTrackerDAL.Models;
using IssueTracking_web_API.Models;
using IssueTracking_Web_API.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIComments.models;

namespace WebAPIComments.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class comments : Controller

    {
        public BugTrackerLogic bLObject;
        public comments()
        {
            bLObject = new BugTrackerLogic();
        }

        ///<summary>
        /// Retrieves comments based on a specific issue ID
        ///</summary>
        /// <response code="200">Fetch Succesful</response>
        /// <response code="500">Server Error</response>

        [HttpGet]

        [ProducesResponseType(typeof(List<NewComment>), 200)]
        [Produces("application/json")]
        public JsonResult GetCommentByIssueId(string issueId)
        {
            List<Comment> result = null;
            List<NewComment> output = new List<NewComment>();
            try
            {
                result = bLObject.GetCommentByIssueIdBL(issueId);
                foreach (Comment comment in result)
                {
                    NewComment newComment = new NewComment();
                    newComment.Comment1 = comment.Comment1;
                    newComment.CommentedOn = comment.CommentedOn;
                    newComment.CommentId = comment.CommentId;
                    newComment.IssueId = comment.IssueId;
                    newComment.EmpId = comment.EmpId;
                    newComment.ParentCommentId = comment.ParentCommentId;
                    output.Add(newComment);
                }

            }
            catch (Exception e)
            {
                var errorResponse = new
                {
                    error = "Error while fetching comments data from database",
                    message = e.Message
                };



                return Json(errorResponse);

            }
            return Json(output);

        }

        ///<summary>
        ///Add a comment to a specific issue
        ///</summary>

        /// <response code="500">Server Error</response>
        /// <response code="400">Bad request</response>

        [HttpPost]
        [ProducesResponseType(typeof(NewComment), 200)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public JsonResult AddComment(InputComment commentObject)
        {

            Comment commentObj = null;
            Comment comment = new Comment();
            comment.Comment1 = commentObject.Comment1;
            comment.IssueId = commentObject.IssueId;
            comment.EmpId = commentObject.EmpId;
            comment.ParentCommentId= commentObject.ParentCommentId;
            NewComment newComment = new NewComment();
            try
            {
                commentObj = bLObject.AddCommentBL(comment);
                newComment.CommentId = commentObj.CommentId;
                newComment.CommentedOn = commentObj.CommentedOn;
                newComment.Comment1 = commentObj.Comment1;
                newComment.IssueId = commentObj.IssueId;
                newComment.EmpId = commentObj.EmpId;
                newComment.ParentCommentId = commentObj.ParentCommentId;


            }
            catch (Exception e)
            {

                var errorResponse = new
                {
                    error = "Error while fetching comments data from database",
                    message = e.Message
                };



                return Json(errorResponse);
            }
            return Json(newComment);
        }



        ///<summary>
        /// Delete a comment associated with a specific identifier
        ///</summary>

        /// <response code="500">Server Error</response>
        /// <response code="400">Bad request</response>

        
        [ProducesResponseType(typeof(NewComment), 200)]
        [Produces("application/json")]
        

        [HttpDelete]
        public JsonResult DeleteComment(int commentId)
        {
            List<Comment> comment = null;
            List<NewComment> commentsList = new List<NewComment>();
            try
            {
                comment = bLObject.DeleteCommentLogic(commentId);
                foreach (Comment obj in comment)
                {
                    NewComment newComment = new NewComment();
                    newComment.CommentId = obj.CommentId;
                    newComment.Comment1 = obj.Comment1;
                    newComment.IssueId = obj.IssueId;
                    newComment.EmpId = obj.EmpId;
                    newComment.CommentedOn = obj.CommentedOn;
                    commentsList.Add(newComment);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(commentsList);



        }


       
        ///<summary>
        /// Update or Edit  a comment associated with a specific identifier.
         ///</summary>

         /// <response code="500">Server Error</response>
         /// <response code="400">Bad request</response>

        
        [ProducesResponseType(typeof(NewComment), 200)]
        [Consumes("application/json")]
        [Produces("application/json")]

        [HttpPut]
        public JsonResult UpdateComment(InputCommentUpdate updatedComment)
        {
            Comment comment = new Comment();
            NewComment newComment = new NewComment();
            try
            {
                comment = bLObject.UpdateCommentLogic(updatedComment.CommentId, updatedComment.Comment1);
                newComment.CommentId = comment.CommentId;
                newComment.Comment1 = comment.Comment1;
                newComment.IssueId = comment.IssueId;
                newComment.EmpId = comment.EmpId;
                newComment.CommentedOn = comment.CommentedOn;



            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(newComment);



        }


    }
}

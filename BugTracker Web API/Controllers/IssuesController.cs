using BugTracker_Web_API.Models;
using BugTrackerBL;
using BugTrackerDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker_Web_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IssuesController : Controller
    {
        BugTrackerLogic cartLogic;
        public IssuesController()
        {
            cartLogic = new BugTrackerLogic();
        }

        ///<summary>
        ///Fetches Issue corresponding to given IssueId
        ///</summary>
        /// <response code="200">Fetch Succesfull</response>
        /// <response code="500">Server Error</response>


        [HttpGet]

        [ProducesResponseType(typeof(List<Issue>), 200)]
        [Produces("application/json")]
        public JsonResult FetchIssue(string IssueId)
        {
            List<Issue> catList = null;
            try
            {
                catList = cartLogic.GetIssueLogic(IssueId);

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
            return Json(catList);
        }
        ///<summary>
        ///Fetch Issues Corresponding to given project Id
        ///</summary>
        /// <response code="200">Fetch Succesfull</response>
        /// <response code="500">Server Error</response>


        [HttpGet]

        [ProducesResponseType(typeof(List<Issue>), 200)]
        [Produces("application/json")]
        public JsonResult FetchIssuesbyProject(string ProjectId)
        {
            List<Issue> catList = null;
            try
            {
                catList = cartLogic.GetIssuesbyProjectLogic(ProjectId);

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
            return Json(catList);
        }
        ///<summary>
        ///Fetch Issues Corresponding to given project Id which are Identified in given Time period
        ///</summary>
        /// <response code="200">Fetch Succesfull</response>
        /// <response code="500">Server Error</response>

        [HttpGet]
        [ProducesResponseType(typeof(List<Issue>), 200)]
        [Produces("application/json")]
        public JsonResult FetchIssuesByTimePeriod(string projectid,DateTime FromDate,DateTime ToDate)
        {
            List<Issue> issuesList = null;
            try
            {
                issuesList = cartLogic.GetIssuesbyTimePeriodLogic(projectid, FromDate, ToDate);
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
            return Json(issuesList);
        }
        ///<summary>
        ///Adds New Issue with given Details 
        ///</summary>

        /// <response code="500">Server Error</response>
        /// <response code="400">Bad request</response>

        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]

        [Consumes("application/json")]
        public JsonResult AddIssue(InputIssue issue)
        {

            string? result = "";
            try
            {

                if (ModelState.IsValid)
                {
                    Issue issue1 = new Issue();

                    issue1.ProjectId = issue.ProjectId;
                    issue1.ShortDescription = issue.ShortDescription;
                    issue1.IssueType = issue.IssueType;
                    issue1.ModuleName = issue.ModuleName;
                    issue1.Description = issue.Description;
                    issue1.Identfiedemp = issue.Identfiedemp;
                    issue1.Dateidentified = DateTime.Now;
                    if (issue.Priority == "High")
                    {
                        issue1.Priority = "P1";
                    }
                    else if (issue.Priority == "Medium")
                    {
                        issue1.Priority = "P2";
                    }
                    else
                    {
                        issue1.Priority = "P3";
                    }
                    issue1.AssignTo = issue.AssignTo;
                    issue1.TestingType = issue.TestingType;
                    issue1.IterationNumber = 1;
                    issue1.Status = "Open";
                    issue1.Category = issue.Category;
                    issue1.Images = issue.Images;
                    issue1.Seviority = issue.Seviority;
                    issue1.Lastmodifydonedate = DateTime.Now;
                    issue1.Lastmodifydoneemp = issue.Identfiedemp;
                   

                    result = cartLogic.AddIssuesLogic(issue1);
                    //issue1.IssueId = result;
                    //return Json(result);
                    return Json(true);

                }
            }
            catch (Exception ex)
            {
                var errorresponse = new
                {
                    error = "an error occurred while adding the issues.",
                    message = ex.Message
                };

                return Json(errorresponse);
            }
            return Json(result);

        }
        ///<summary>
        ///Updates or Edit Corresponding Issue by Issue Id with given Details
        ///</summary>

        /// <response code="500">Server Error</response>
        /// <response code="400">Bad request</response>
        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]

        [Consumes("application/json")]
        public JsonResult UpdateentireIssue(OutputIssue issue)
        {
            bool result = false;
            try
            {
                if (ModelState.IsValid)
                {
                    Issue issue1 = new Issue();
                    issue1.IssueId = issue.IssueId;
                    issue1.ProjectId = issue.ProjectId;
                    issue1.ShortDescription = issue.ShortDescription;
                    issue1.IssueType = issue.IssueType;
                    issue1.ModuleName = issue.ModuleName;
                    issue1.Description = issue.Description;
                    issue1.Category = issue.Category;
                    issue1.Priority = issue.Priority;
                    issue1.Targetdate = issue.Targetdate;
                    issue1.Ressummary = issue.Ressummary;
                    issue1.AssignTo = issue.AssignTo;
                    issue1.StepsToReproduce = issue.StepsToReproduce;
                    issue1.TestingType = issue.TestingType;
                    issue1.Status = issue.Status;
                    issue1.Images = issue.Images;
                    issue1.Seviority = issue.Seviority;
                    issue1.Lastmodifydoneemp = issue.Lastmodifydoneemp;
                    issue1.Lastmodifydonedate = DateTime.Now;
                    result = cartLogic.UpdateIssueobjectLogic(issue1);
                }
                else
                {
                    throw new Exception("Please provide valid Issue data to update");
                }

            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    error = "An error occurred while updating the issues.",
                    message = ex.Message
                };

                return Json(errorResponse);
            }
            return Json(result);



        }
        ///<summary>
        ///Fetches Issues Corresponding to given project Id,filters,page number and number of issues per page
        ///</summary>
        /// <response code="200">Fetch Succesfull</response>
        /// <response code="500">Server Error</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Issue>), 200)]
        [Produces("application/json")]
        public JsonResult issuesPaginationByFilters(string projectId,string status,string priority,string seviourity,int identifiedemp,int assignto,int pageno,int issuesperpage)
        {
            List<Issue> issues = null;
            try
            {
                issues = cartLogic.issuesPaginationByFiltersLogic(projectId, status, priority, seviourity, identifiedemp, assignto, pageno, issuesperpage);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    error = "An error occurred while updating the issues.",
                    message = ex.Message
                };

                return Json(errorResponse);
            }
            return Json(issues);
        }

    }
}

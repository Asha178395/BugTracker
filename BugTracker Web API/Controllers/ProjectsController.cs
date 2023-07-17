
using BugTracker_Web_API.Models;
using BugTrackerBL;
using BugTrackerDAL.Models;
using IssueTracking_web_API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Models;
using static Azure.Core.HttpHeader;

namespace WebApplication1.Controllers


{

    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class projectController : Controller
    {
        ///<summary>
        ///Fetches All Project details
        ///</summary>
        /// <response code="200">Fetch Succesful</response>
        /// <response code="500">Server Error</response>


        [HttpGet]

        [ProducesResponseType(typeof(List<OutputProject>), 200)]
        [Produces("application/json")]
        public JsonResult GetAllProjects()
        {
            BugTrackerLogic buglogic = new BugTrackerLogic();

            List<Project> projList = null;
            List<OutputProject> outputProjects = new List<OutputProject>();
            try { 
          
                projList = buglogic.GetAllProjectsBL();
                foreach (Project project in projList)
                {
                    OutputProject outputProject = new OutputProject();
                    outputProject.Projectid = project.ProjectId;
                    outputProject.Projectname = project.ProjectName;
                    outputProject.TotalIssues = project.TotalIssues;
                    outputProject.MediumPriorityIssues = project.MediumPriorityIssues;
                    outputProject.LowPriorityIssues = project.LowPriorityIssues;
                    outputProject.HighPriorityIssues = project.HighPriorityIssues;
                    outputProject.S1seviourty = project.S1seviourty;
                    outputProject.S2seviourty = project.S2seviourty;
                    outputProject.S3seviourty = project.S3seviourty;
                    outputProject.S4seviourty = project.S4seviourty;
                    outputProjects.Add(outputProject);
                }

            }
            catch (Exception e)
            {
                var errorResponse = new
                {
                    error = "Error while fetching projects data from database",
                    message = e.Message
                };



                return Json(errorResponse);
            }
            return Json(outputProjects);

        }




        ///<summary>
        ///Add New Project
        ///</summary>

        /// <response code="500">Server Error</response>
        /// <response code="400">Bad request</response>

        [HttpPost]
        [ProducesResponseType(typeof(OutputProject), 200)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public JsonResult AddProject(InputProject project)
        {
            BugTrackerLogic buglogic = new BugTrackerLogic();

            Project project1 = new Project();
            project1.ProjectName = project.ProjectName;
            //project1.ProjectId = project.ProjectId;
            Project project2 = null;
            OutputProject outputProject = new OutputProject();
            try
            {
                project2 = buglogic.AddProjectBL(project1);
                outputProject.Projectid = project2.ProjectId;
                outputProject.Projectname = project2.ProjectName;
                outputProject.TotalIssues = project2.TotalIssues;
                outputProject.HighPriorityIssues = project2.HighPriorityIssues;
                outputProject.MediumPriorityIssues  = project2.MediumPriorityIssues;
                outputProject.LowPriorityIssues = project2.LowPriorityIssues;
                outputProject.S1seviourty   = project2.S1seviourty;
                outputProject.S2seviourty   = project2.S2seviourty;
                outputProject.S3seviourty = project2.S3seviourty;
                outputProject.S4seviourty = project2.S4seviourty;

            }
            catch (Exception e)
            {
                var errorResponse = new
                {
                    error = "Error while adding project data to database",
                    message = e.Message
                };



                return Json(errorResponse);
            }
            return Json(outputProject);
        }
        /// <summary>
        /// Fetch Project Name by ProjectId
        /// </summary>
        
        [HttpGet]

        [ProducesResponseType(typeof(OutputProjectById), 200)]
        [Produces("application/json")]
        public JsonResult GetProjectById(string projectid)
        {
            BugTrackerLogic buglogic = new BugTrackerLogic();

            Project projDetails = null;
            OutputProjectById outputProject = new OutputProjectById();
            try
            {

                projDetails= buglogic.GetProjectbyIdBL(projectid);
                outputProject.Projectname = projDetails.ProjectName;

            }
            catch (Exception e)
            {
                var errorResponse = new
                {
                    error = "Error while fetching projects data from database",
                    message = e.Message
                };



                return Json(errorResponse);
            }
            return Json(outputProject);

        }
    }
}


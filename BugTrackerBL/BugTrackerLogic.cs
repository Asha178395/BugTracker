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
    }
}
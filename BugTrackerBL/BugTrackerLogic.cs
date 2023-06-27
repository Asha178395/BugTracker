using BugTrackerDAL;

namespace BugTrackerBL
{
    public class BugTrackerLogic
    {
        BugTrackerRepositry repositry;
        public BugTrackerLogic()
        {

            repositry = new BugTrackerRepositry();
        }
    }
}
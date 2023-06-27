using BugTrackerDAL.Models;

namespace BugTrackerDAL
{
    public class BugTrackerRepositry
    {
        BugtrackerdbContext context;
        public BugTrackerRepositry()
        {
            context = new BugtrackerdbContext();
        }
    }
}
using TimeManagementLib.Models;
using TimeManagementLib.Singleton;

namespace Time_Management_App.Classes
{
    public class StudiedClass
    {
        private readonly UserDetails userDetails = UserDetails.Instance;
        public IList<StudentModule>? GetModuleCodes()
        {
            try
            {
                TimeManagementAppContext context = new TimeManagementAppContext();
                return context.StudentModules.Where(x => x.StudentId == userDetails.StudentId).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Study(string code, int hours)
        {

            // Updates the remaining hours in the database
            TimeManagementAppContext context = new TimeManagementAppContext();
            var module = context.StudentModules.Where(x => x.StudentId == userDetails.StudentId && x.Code == code).FirstOrDefault()!;
            // Checks if the week is equal to the number of weeks of the student table
            if (module.Week == userDetails.NumOfWeeks)
            {
                return;
            }
            module.RemainingHours -= hours;
            context.SaveChanges();

            // Updates the remaining weeks in the database if the remaining hours is 0
            if (module.RemainingHours <= 0)
            {
                module.Week += 1;
                module.RemainingHours = module.SelfStudyHours;
                context.SaveChanges();
            }
        }
    }
}

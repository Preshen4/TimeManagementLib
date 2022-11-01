using Time_Management_App.Abstract;
using TimeManagementLib.Models;
using TimeManagementLib.Singleton;

namespace Time_Management_App.Classes
{
    public class LoginClass : UserLogin
    {
        // Check if the password is correct
        private readonly UserDetails userDetails = UserDetails.Instance;
        public bool PasswordMatches(string studentID, string password)
        {
            TimeManagementAppContext context = new TimeManagementAppContext();

            if (context.Students.Find(studentID)?.Password == HashPassword(password))
            {
                userDetails.StudentId = studentID;
                new Thread(() => userDetails.NumOfWeeks = context.Students.Find(studentID)?.NumberOfWeeks ?? 0).Start();
                return true;
            }
            return false;

        }
    }
}

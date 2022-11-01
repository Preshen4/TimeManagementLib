using Time_Management_App.Abstract;
using TimeManagementLib.Models;
using TimeManagementLib.Singleton;

namespace Time_Management_App.Classes
{
    public class SignUpClass : UserLogin
    {
        private readonly UserDetails userDetails = UserDetails.Instance;
        public bool PasswordsMatch(string password1, string password2)
        {
            if (password1.Equals(password2))
            {
                return true;
            }
            return false;
        }

        public bool AddUser(Student student)
        {
            TimeManagementAppContext context = new TimeManagementAppContext();
            userDetails.StudentId = student.StudentId;
            student.Password = HashPassword(student.Password!);
            // Add the student to the database
            context.Students.Add(student);
            context.SaveChanges();
            return true;
        }
    }
}

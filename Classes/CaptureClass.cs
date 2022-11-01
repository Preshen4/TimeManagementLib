using TimeManagementLib.Models;
using TimeManagementLib.Singleton;

namespace Time_Management_App.Classes
{
    public class CaptureClass
    {
        // Method to add a module to the Modules and StudentModules tables in the database
        // User details instance
        private readonly UserDetails userDetails = UserDetails.Instance;
        public void AddNewModule(Module module)
        {
            StudentModule studentModule = CreateStudentModule(module);
            TimeManagementAppContext context = new TimeManagementAppContext();

            context.Modules.Add(module);
            context.SaveChanges();

            context.StudentModules.Add(studentModule);
            context.SaveChanges();
        }

        // Method to create a StudentModule object
        private StudentModule CreateStudentModule(Module module)
        {
            StudentModule studentModule = new StudentModule();
            studentModule.StudentId = userDetails.StudentId;
            studentModule.Code = module.Code;
            studentModule.SelfStudyHours = studentModule.CalSelfStudyHours(userDetails.NumOfWeeks, module.Credits, module.HoursPerWeek);
            studentModule.RemainingHours = studentModule.SelfStudyHours;
            studentModule.Week = 1;

            return studentModule;
        }

        // Method to add a module to the Modules table in the database
        public void AddOldModule(Module module)
        {
            StudentModule studentModule = CreateStudentModule(module);
            TimeManagementAppContext context = new TimeManagementAppContext();
            context.StudentModules.Add(studentModule);
            context.SaveChanges();

        }

        // Method to check if the module code already exists in the database
        public bool CheckIfCodeExists(string code)
        {
            TimeManagementAppContext context = new TimeManagementAppContext();

            if (context.Modules.Find(code) != null)
            {
                return true;
            }

            return false;
        }

        // Method to check if the module code already exists for the student in the database
        public bool CheckIfStudentHasCode(string code)
        {

            TimeManagementAppContext context = new TimeManagementAppContext();

            if (context.StudentModules.
                Where(x => x.StudentId == userDetails.StudentId
                && x.Code == code).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }
    }
}

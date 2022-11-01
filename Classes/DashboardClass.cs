using TimeManagementLib.Models;
using TimeManagementLib.Singleton;

namespace Time_Management_App.Classes
{
    public class DashboardClass
    {
        //Method to get the most self study hours for a module
        private readonly UserDetails userDetails = UserDetails.Instance;
        public string MostHours()
        {
            try
            {
                TimeManagementAppContext context = new TimeManagementAppContext();
                // Gets the module name and Remaining hours of the module with the most remaining time left
                var mostHours = context.StudentModules.
                    Where(x => x.StudentId == userDetails.StudentId).
                    OrderByDescending(x => x.RemainingHours).
                    Select(x => new { x.Code, x.RemainingHours }).
                    FirstOrDefault();
                return $"The module with the most hours of self studying remaining is : {mostHours?.Code} with {mostHours?.RemainingHours} hours left";
            }
            catch (Exception)
            {
                return "An error occurred";
            }
        }

        //Method to get the most credits for a module
        public string MostCredits()
        {
            try
            {
                TimeManagementAppContext context = new TimeManagementAppContext();
                // Gets the module name and credits of the module with the most credits
                var mostCredits = context.StudentModules.
                    Where(x => x.StudentId == userDetails.StudentId).
                    OrderByDescending(x => x.CodeNavigation.Credits).
                    Select(x => new { x.Code, x.CodeNavigation.Credits }).
                    FirstOrDefault();
                return $"The module with the most credits remaining is : {mostCredits?.Code} with {mostCredits?.Credits} credits left";
            }
            catch (Exception)
            {
                return "An error occurred";
            }
        }

        // Method to delete a module from the database
        public bool DeleteModule(string module)
        {
            TimeManagementAppContext context = new TimeManagementAppContext();
            // Deletes the module from the database         
            try
            {
                context.StudentModules.
                    Remove(context.StudentModules.
                    Where(x => x.StudentId == userDetails.StudentId && x.Code == module).
                    FirstOrDefault()!);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        // Code Attribution 
        // Link: https://www.geeksforgeeks.org/c-sharp-anonymous-types/ 
        // Author : Ankita_Saini 
        // Code : dynamic          
        // Code Attribution 

        // Method to return the modules of the user
        public dynamic? GetModules()
        {
            try
            {
                TimeManagementAppContext context = new TimeManagementAppContext();
                // Gets the modules for the current student
                var modules = context.StudentModules.
                    Where(x => x.StudentId == userDetails.StudentId).
                    Select(x => new { x.Code, x.CodeNavigation.Name, x.CodeNavigation.Credits, x.SelfStudyHours, x.RemainingHours, x.Week }).
                    ToList();

                return modules;
            }
            catch (Exception)
            {
                return null;
            }
        }


        // Code Attribution
        // Link: https://www.c-sharpcorner.com/UploadFile/87b416/export-data-from-datagrid-to-excel-sheet/
        // Author: Kailash Chandra Behera 
        // Use: Referencing Microsoft.Office.Interop.Excel

        // Method to export datagrid to excel
        public void ExportToExcel()
        {
            // Code Attribution
            // Author: Github Copilot 
            // Start
            TimeManagementAppContext context = new TimeManagementAppContext();
            // Gets the modules for the current student
            var modules = context.StudentModules.
                Where(x => x.StudentId == userDetails.StudentId).
                Select(x => new { x.Code, x.CodeNavigation.Name, x.CodeNavigation.Credits, x.SelfStudyHours, x.RemainingHours, x.Week }).
                ToList();

            // Creates a new excel workbook
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;
            ws.Name = "Modules";

            // Adds the headers to the excel sheet
            ws.Cells[1, 1] = "Module Code";
            ws.Cells[1, 2] = "Module Name";
            ws.Cells[1, 3] = "Module Credits";
            ws.Cells[1, 4] = "Self Study Hours";
            ws.Cells[1, 5] = "Remaining Hours";
            ws.Cells[1, 6] = "Week";

            // Adds the data to the excel sheet
            for (int i = 0; i < modules.Count; i++)
            {
                ws.Cells[i + 2, 1] = modules[i].Code;
                ws.Cells[i + 2, 2] = modules[i].Name;
                ws.Cells[i + 2, 3] = modules[i].Credits;
                ws.Cells[i + 2, 4] = modules[i].SelfStudyHours;
                ws.Cells[i + 2, 5] = modules[i].RemainingHours;
                ws.Cells[i + 2, 6] = modules[i].Week;
            }
        }
        // End
    }
}

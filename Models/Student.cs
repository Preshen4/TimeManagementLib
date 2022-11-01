using System;
using System.Collections.Generic;

namespace TimeManagementLib.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentModules = new HashSet<StudentModule>();
        }

        public string StudentId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Password { get; set; }
        public int NumberOfWeeks { get; set; }
        public DateTime StartDate { get; set; }

        public virtual ICollection<StudentModule> StudentModules { get; set; }
    }
}

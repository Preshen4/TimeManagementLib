namespace TimeManagementLib.Models
{
    public partial class Module
    {
        public Module()
        {
            StudentModules = new HashSet<StudentModule>();
        }

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Credits { get; set; }
        public int HoursPerWeek { get; set; }

        public virtual ICollection<StudentModule> StudentModules { get; set; }
    }
}

namespace TimeManagementLib.Models
{
    public partial class StudentModule
    {
        public string StudentId { get; set; } = null!;
        public string Code { get; set; } = null!;
        public int SelfStudyHours { get; set; }
        public int RemainingHours { get; set; }
        public int Week { get; set; }
        public int Id { get; set; }

        public virtual Module CodeNavigation { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;

        public int CalSelfStudyHours(int numOfWeeks, int credits, int hours)
        {
            int selfStudyHours = credits * 10;
            selfStudyHours = selfStudyHours / numOfWeeks;
            selfStudyHours = selfStudyHours - hours;
            return selfStudyHours;
        }
    }
}

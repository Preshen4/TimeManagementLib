namespace TimeManagementLib.Singleton
{
    public sealed class UserDetails
    {
        private static UserDetails instance = null;
        public string StudentId { get; set; } = null!;
        public int NumOfWeeks { get; set; }

        private UserDetails()
        {
        }

        public static UserDetails Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDetails();
                }
                return instance;
            }
        }
    }
}

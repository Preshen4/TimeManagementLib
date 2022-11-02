namespace TimeManagementLib.Singleton
{
    // Code Attribution
    // How to make a singleton class
    // Link : https://csharpindepth.com/articles/singleton
    // Author : NA
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

namespace BasicToAdvanceByKudvenkatCollegeApp.Models
{
    public static class CollegeRepository
    {
        public static List<Student> student { get; set; } = new List<Student>()
        {
            new Student
            {
                Id = 1,
                StudentName = "Ashita",
                Email = "Ashita@gmail.com",
                Address = "goa"
            },
             new Student
             {
                 Id = 2,
                 StudentName = "Heena",
                 Email = "heena@gmail.com",
                 Address = "India"
             }
        };
    }
}

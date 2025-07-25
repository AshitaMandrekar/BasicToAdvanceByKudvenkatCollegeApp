using BasicToAdvanceByKudvenkatCollegeApp.Data.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BasicToAdvanceByKudvenkatCollegeApp.Data
{
    public class CollegeDBContext : DbContext // inherit => :DBContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base (options) // add this ctor
        {
            
        }
        public  DbSet<Student> Students { get; set; } // => Students is the table name to be created
                                              // Adding data to tables from here // enter protected override onmodelcreating and enter

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This is for 1 table
            //below is commented as added in studentConfig.cs
            // modelBuilder.Entity<Student>().HasData(new List<Student>()
            //{
            //    new Student
            //    {
            //        Id = 1,
            //        StudentName = "Ashita",
            //        Email = "ashita@gmail.com",
            //        Address = "Goa",
            //        DOB = new DateTime(1993,10,10)
            //    },
            //     new Student
            //    {
            //        Id = 2,
            //        StudentName = "Heena",
            //        Email = "heena@gmail.com",
            //        Address = "Goa",
            //        DOB = new DateTime(1993,10,10)
            //    }
            //});

            // below code is kind of validation and schema structure 
            // below is commented as it is moved to studenconfig.cs
            //modelBuilder.Entity<Student>(entity =>
            //{
            // moved this to separate file StudentConfig.cs
            //entity.Property(n => n.StudentName).IsRequired();
            //entity.Property(n => n.StudentName).HasMaxLength(50);
            //entity.Property(n => n.Address).IsRequired(false).HasMaxLength(200);
            //entity.Property(n => n.Email).IsRequired().HasMaxLength(250);
            // });


            // comment above code and add code below to apply studentconfig.cs configuration

            modelBuilder.ApplyConfiguration(new StudentConfig()); // this for table 1
            //modelBuilder.ApplyConfiguration(new ConfigName()); // for table 2 in future likewise for n number of tbls
        }
    }
}

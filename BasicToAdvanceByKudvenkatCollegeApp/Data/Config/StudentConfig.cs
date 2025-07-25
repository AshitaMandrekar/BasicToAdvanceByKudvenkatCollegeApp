using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicToAdvanceByKudvenkatCollegeApp.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student> // added this base class
    {
        public void Configure(EntityTypeBuilder<Student> builder) // created automatically when above base is added
        {
            // creating schema for tbl . this code is moved from collegeDBContext.cs to here
            builder.ToTable("Students");
            builder.HasKey(x => x.Id); // for primary key
            builder.Property(x => x.Id).UseIdentityColumn(); // identity column
            builder.Property(n => n.StudentName).IsRequired();
            builder.Property(n => n.StudentName).HasMaxLength(50);
            builder.Property(n => n.Address).IsRequired(false).HasMaxLength(200);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);

            // inserting data into tbl . this code is moved from collegeDBContext.cs to here
            builder.HasData(new List<Student>()
           {
               new Student
               {
                   Id = 1,
                   StudentName = "Ashita",
                   Email = "ashita@gmail.com",
                   Address = "Goa",
                   DOB = new DateTime(1993,10,10)
               },
                new Student
               {
                   Id = 2,
                   StudentName = "Heena",
                   Email = "heena@gmail.com",
                   Address = "Goa",
                   DOB = new DateTime(1993,10,10)
               }
           });
        }
    }
}

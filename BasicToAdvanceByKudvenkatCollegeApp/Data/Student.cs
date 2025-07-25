using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicToAdvanceByKudvenkatCollegeApp.Data
{
    public class Student // this is ur actual table contents 
    {
        // [Key] // primary key // key is removed as added in StudentConfig.cs
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // identity is removed as added in StudentConfig.cs
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
    }
}

using AutoMapper; // added automatically when profile is inherited
using BasicToAdvanceByKudvenkatCollegeApp.Data;
using BasicToAdvanceByKudvenkatCollegeApp.Models;

namespace BasicToAdvanceByKudvenkatCollegeApp.Configurations
{
    public class AutoMapperConfig : Profile // inherits from Profile
    {
        public AutoMapperConfig() // creating a constructor
        {
            // create a first map
          //  CreateMap<Student, StudentDTO>(); // create a map between student and studentDTO, where student is src class and studentdto is dest class.
            // creating a second map
            //CreateMap<StudentDTO, Student>();

            // so instead of writing above two lines u can even do 
            CreateMap<Student, StudentDTO>().ReverseMap();

            // basically u map student fields with studentDTO field names. both the field names should be same then only mapping will happen.
            // if u have student with StudentName as name and StudentDTO with StudentName as StudentName then the mapping of data between names will not happen.
            // in this case you can use. Config for dfrnt property names
            // CreateMap<Student, StudentDTO>().ReverseMap().ForMember(x => x.Name, opt => opt.MapFrom(x => x.StudentName));

            // ignore
            //CreateMap<Student, StudentDTO>().ReverseMap().ForMember(x => x.StudentName, opt => opt.Ignore());

            // Transform some property

            // CreateMap<Student, StudentDTO>().ReverseMap().AddTransform<string>(n => string.IsNullOrEmpty(n) ? "no address" : n);

            // transforming some specific property

            CreateMap<Student, StudentDTO>().ReverseMap()
           .ForMember(n => n.Address, opt => opt.MapFrom(n => string.IsNullOrEmpty(n.Address) ? "no adddress found" : n.Address));

        }
    }
}

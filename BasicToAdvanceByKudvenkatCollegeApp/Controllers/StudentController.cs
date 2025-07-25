using System.Net;
using AutoMapper;
using BasicToAdvanceByKudvenkatCollegeApp.Data;
using BasicToAdvanceByKudvenkatCollegeApp.Data.Repository;
using BasicToAdvanceByKudvenkatCollegeApp.Models;
//using BasicToAdvanceByKudvenkatCollegeApp.MyLogging; // coommented for part 51,52 repo design pattern
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Http.HttpResults; 
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore; // coommented for part 51,52 repo design pattern


namespace BasicToAdvanceByKudvenkatCollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // if u have this then no need to use (ModelState.IsValid) as this statmnt will check for model validation 
    public class StudentController : ControllerBase
    {
        //private readonly CollegeDBContext _dbContext; // using EF - part 43 // part 51 removed from here and added to StudentRepository controller to implement repository design pattern
        private readonly IMapper _mapper; // this is part 47 adding autoMapper;
        // this  code was for DI [starts] // using tightly couple// 07/19/25
        //private readonly IMyLogger _myLogger;

        //public StudentController() 
        //{
        //    _myLogger = new LogToFile();
        //}
        // this  code was for DI [ends]

        // Implementing in built loggings [start] // 07202025

       
        private readonly IStudentRepository _studRepository; // Implementing Repository design pattern using Student Interface - part 51,52

        private readonly ILogger<StudentController> _logger;
        public StudentController(ILogger<StudentController> logger, /*CollegeDBContext dbContext,*/ IMapper mapper, IStudentRepository studRepository) // added to get DI instance - part 43 // mapper dependency injection added// part 51 removing to studentRepository to implement repo design pattern
        {
            _logger = logger;
            // _dbContext = dbContext; // assigning to local variable - // using EF - part 43 // part 51 removing to studentRepository to implement repo design pattern
            _mapper = mapper; // part 47
            _studRepository = studRepository; // Implementing Repository design pattern using Student Interface - part 51,52
        }

        // check program.cs file if u want to comment or remove inbuilt loggers
        // Implementing in built loggings [ends] // 07202025


        [HttpGet]
       // [Route("api/[controller]", Name = "all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public  async Task<ActionResult<IEnumerable<StudentDTO>>> GetAllStudentsAsync() 
        {

            // _myLogger.Log("Your Message"); // using tightly couple //// 07/19/25
            _logger.LogInformation("GetStudents method started executing"); // 07202025



            // part 29 - log levels [starts] -> in program.cs added 07202025
            // builder.Logging.AddDebug();  
           // builder.Logging.AddConsole();
            _logger.LogTrace("Log Message from Trace Mathod");
            _logger.LogDebug("Log Message from Debug Mathod");
            _logger.LogInformation("Log Message from Information Mathod");
            _logger.LogWarning("Log Message from Warning Mathod");
            _logger.LogError("Log Message from Error Mathod");
            _logger.LogCritical("Log Message from Critical Mathod");
            // part 29 - log levels [ends] 07202025

            #region when using in memory repository - comented to use EF // using EF - part 43 
            // var students = CollegeRepository.student.Select(x => new StudentDTO 
            //{
            //    Id = x.Id,
            //    StudentName = x.StudentName,
            //    Email = x.Email,
            //    Address = x.Address
            //});
            #endregion

            //var students = _dbContext.Students.Select(x => new StudentDTO // using EF - part 43 
            //{
            //    Id = x.Id,
            //    StudentName = x.StudentName,
            //    Email = x.Email,
            //    Address = x.Address
            //});

            #region adding automapper and commenting above code of EF - part 43

            //var students = await _dbContext.Students.ToListAsync();
            //var studentData = _mapper.Map<List<StudentDTO>>(students); // added mapper 

            #endregion
            
            var students = await _studRepository.GetAllStudents();
            var studentData = _mapper.Map<List<StudentDTO>>(students);
            return Ok(studentData);
        }
        [HttpGet("{id:int}", Name = "GetStudentById")]
        // [Route("api/[controller]/{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // implementing async and await part 46 ,when u implement asyn await, action result automatically becomes async Task<ActionResult<StudentDTO>>
        public async Task<ActionResult<StudentDTO>> GetStudentByIdAsync(int id) // implementing async and await part 46
        {
            if (id <= 0) 
            {
                _logger.LogWarning("Bad Request");// 07202025
                return BadRequest(); 
            }
            else
            {
                // var dto = CollegeRepository.student.Where(x => x.Id == id).FirstOrDefault();
                //var students = await _dbContext.Students.Where(x => x.Id == id).FirstOrDefaultAsync(); // using EF - part 43 // commented for repository design pattern
                var students = await _studRepository.GetStudentByIdAsync(id); // Implementing Repository design pattern using Student Interface - part 51,52
                if (students == null) {
                    _logger.LogError($"student not found with {id}");
                    return NotFound(); }
                else
                {
                    #region binding data 
                    //StudentDTO dt = new StudentDTO
                    //{
                    //    Id = dto.Id,
                    //    StudentName = dto.StudentName,
                    //    Email = dto.Email,
                    //    Address = dto.Address
                    //};
                    #endregion

                    #region adding mapper part 48 
                    var dt = _mapper.Map<StudentDTO>(students);
                    #endregion

                    return Ok(dt);
                }
            }
        }
        [HttpGet]
        [Route("{name:alpha}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDTO>> GetStudentByNameAsync(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            else
            {
                // var dt = CollegeRepository.student.Where(x => x.StudentName == name).FirstOrDefault();
                // var students = await _dbContext.Students.Where(x => x.StudentName == name).FirstOrDefaultAsync();// using EF - part 43 // commented for implementing repository design pattern

                var students = await _studRepository.GetStudentByNameAsync(name); // Implementing Repository design pattern using Student Interface - part 51,52
                if (students == null)
                {
                    return NotFound();
                }
                else
                {
                    // commented below code to add mapper
                    //StudentDTO dto = new StudentDTO
                    //{
                    //    Id = dt.Id,
                    //    StudentName = dt.StudentName,
                    //    Email = dt.Email,
                    //    Address = dt.Address
                    //};
                    #region adding mapper 
                    var studentData = _mapper.Map<StudentDTO>(students);
                    #endregion

                    return studentData;
                }
            }

        }
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteStudentbyIdAsync(int id)
        {
            // var dt = CollegeRepository.student.Where(x => x.Id == id).FirstOrDefault();
            // var dt = await _dbContext.Students.Where(x => x.Id == id).FirstOrDefaultAsync();// using EF - part 43 // commented for implementing repo design pattern
            var dt = await _studRepository.GetStudentByIdAsync(id); // Implementing Repository design pattern using Student Interface - part 51,52
            if (dt == null)
                return NotFound($"The student with id {id} not found");
            await _studRepository.DeleteStudentbyIdAsync(dt);
            #region commented for implementing repo design pattern
            //if (dt == null)
            //{
            //    return NotFound();
            //}
            //else
            //{
            //    //CollegeRepository.student.Remove(dt);
            //    _dbContext.Students.Remove(dt);
            //    await _dbContext.SaveChangesAsync();   // using EF - part 43 
            //    return Ok(true);
            //}

            #endregion
            return Ok(true);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<ActionResult<StudentDTO>> AddStudentAsync([FromBody] StudentDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            if (dto.AdmissionDate < DateTime.Now)
            {
                // 1. directly adding error message to to modelstate
                //ModelState.AddModelError("Admission Date error", "Admission date must be greater than or equal to today's date");
                //return BadRequest(ModelState);


                // 2. using custom Validation
                // by creating separte validation folder and class and writing custom code inside and calling that calssname as 
                // validation in model class.
            }
            {
                //int newid = CollegeRepository.student.LastOrDefault().Id + 1;
                //int newid = _dbContext.Students.LastOrDefault().Id + 1;            // using EF - part 43 
                //Student dt = new Student // commented for mapper add
                //{
                //    //Id = newid, // this is bcoz to put the next id only and to not insrt id coming from model body
                //    StudentName = model.StudentName,
                //    Email = model.Email,
                //    Address = model.Address
                //};
                //   CollegeRepository.student.Add(dt);

                Student dt = _mapper.Map<Student>(dto); // added mapper

                //try
                //{
                    //await _dbContext.Students.AddAsync(dt);  // using EF - part 43 // commented for repo design pattern
                    //await _dbContext.SaveChangesAsync();
                   var id= await _studRepository.CreateAsync(dt);
               // }
                //catch (Exception e)
                //{

                //}                         // using EF - part 43 
                dto.Id = id;//dt.Id;
                // return Ok(dt); 
                return CreatedAtRoute("GetStudentById", new { id = dto.Id }, dt);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> updateAsync([FromBody] StudentDTO dto)
        {
            if (dto == null && dto.Id <= 0)
            {
                return BadRequest();
            }
            else
            {
                // var abc = CollegeRepository.student.Where(x => x.Id == model.Id).FirstOrDefault();
                //var abc = _dbContext.Students.AsNoTracking().Where(x => x.Id == dto.Id).FirstOrDefault(); // with below code , u get error as u cannot track same id again n again , so using asnoTracking()
                //var abc = _dbContext.Students.Where(x => x.Id == model.Id).FirstOrDefault();          // using EF - part 43 // commented for repo design pattern part 51,52
                var abc = _studRepository.GetStudentByIdAsync(dto.Id , true); // implementing repo design pattern part 51,52                                                                               //   return Ok(abc);
                if (abc!=null)
                {
                    //var newRecord = new Student() // commented for mapper add
                    //{
                    //    Id = abc.Id,
                    //    StudentName = model.StudentName,
                    //    Email = model.Email,
                    //    Address = model.Address

                    //};

                    var newRecord = _mapper.Map<Student>(dto);
                    

                    // _dbContext.Students.Update(newRecord);// implementing repo design pattern part 51,52
                    await _studRepository.updateAsync(newRecord);
                }
               
               // await  _dbContext.SaveChangesAsync();   // using EF - part 43 //// implementing repo design pattern part 51,52 
                return NoContent();
            }
        }

        [HttpPatch]
        [Route("{id:int}/Update")] // new added
        // api/student/id/UpdatePartial
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
      
        public async Task<ActionResult> updatePatchAsync (int id, JsonPatchDocument<StudentDTO> patchStudent)
        {
            if (patchStudent == null)
                return BadRequest();
            // var existingdata = CollegeRepository.student.Where(x => x.Id == id).FirstOrDefault();
            //var existingdata = await _dbContext.Students.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();      // using EF - part 43 
            var existingdata = await _studRepository.GetStudentByIdAsync(id, true);
            if (existingdata == null)
            { return NotFound(); }
            //StudentDTO newData = new StudentDTO()  // commented for mapper
            //{
            //    Id = existingdata.Id,
            //    StudentName = existingdata.StudentName,
            //    Email = existingdata.Email,
            //    Address = existingdata.Address
            //};

            var newData = _mapper.Map<StudentDTO>(existingdata); //binding existing data to dto

            patchStudent.ApplyTo(newData, ModelState); // this is main line for updating
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            existingdata = _mapper.Map<Student>(newData);
            // commented for mapper addition
            //existingdata.Id = newData.Id;
            //existingdata.StudentName = newData.StudentName;
            //existingdata.Email = newData.Email;
            //existingdata.Address = newData.Address;
            // _dbContext.Students.Update(existingdata); // using EF - part 43 // commented for repository design pattern part 51,52
            // await _dbContext.SaveChangesAsync();   // using EF - part 43 // commented for repository design pattern part 51,52
            await _studRepository.updateAsync(existingdata);
            return NoContent();
        }
    }
}

////
//So in the output window you have to folllow these syntax:
//    [
//      {
//        "path" :"/studentName",
//        "op" : "replace",
//        "value" : "Ashita New"
//    },
//    {
//    "path" :"/studentName",  // which field u want to update , field name
//        "op" : "replace",   // what operation u want to perform
//        "value" : "Ashita New" // new name
//}
//    ]


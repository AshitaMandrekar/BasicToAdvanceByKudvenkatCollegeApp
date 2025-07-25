using BasicToAdvanceByKudvenkatCollegeApp.MyLogging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicToAdvanceByKudvenkatCollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class dependencyInjectionDemo : ControllerBase
    {
        //1. stronglycoupled or tightly coupled

         private readonly IMyLogger _myLogger; // interface variable
        // Tightly coupled DI
        //public dependencyInjectionDemo() 
        //{
        //    _myLogger = new LogToFile(); // instantiate obj using new keyword
        //                                 // _myLogger = new LogToDB(); // calling the log method
        //                                 // _myLogger = new LogToServerMemory(); // calling the log method

        //    // Here everytime u have to create and instatiate a new method using "new" keyword which makes it tightly couple
        //    // As everywhere u need to then change this instantiation in every file
        //}




        // 2. Loosely couple -> add parameter as IMyLogger _myLogger to constructor
        //     go to program.cs and add builder.Services.AddScoped<IMyLogger, LogToFile>();
        public dependencyInjectionDemo(IMyLogger _myLoggerObj) // In loosely couple , DI will inject IMyLogger _myLogger as parameter in Constructor
        {
            _myLogger = _myLoggerObj; 
        }
        [HttpGet]
        public ActionResult Index()
        {
            _myLogger.Log("Index method started"); // callig the Log Method here
            return Ok();
        }
    }
}

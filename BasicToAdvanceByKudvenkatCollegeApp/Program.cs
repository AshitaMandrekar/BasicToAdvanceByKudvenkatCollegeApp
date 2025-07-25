using BasicToAdvanceByKudvenkatCollegeApp.Configurations;
using BasicToAdvanceByKudvenkatCollegeApp.Data;
using BasicToAdvanceByKudvenkatCollegeApp.Data.Repository;
using BasicToAdvanceByKudvenkatCollegeApp.MyLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog; // Serilog is 3rd party log provider
using static BasicToAdvanceByKudvenkatCollegeApp.Configurations.AutoMapperConfig;

var builder = WebApplication.CreateBuilder(args); // what and why builder 

#region Serilog Settings
// code to implement Serilog [start] 07202025 part 30
// steps 1. install package - Serilog.AspNetCore 2. Serilog.Sinks.File (if want to implemt file log)
// comment lines builder.Logging.ClearProviders(); builder.Logging.AddDebug(); builder.Logging.AddConsole();
// add below lines -> using Serilog;

//Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
//            .WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Minute)
//            .CreateLogger();
////builder.Host.UseSerilog(); // this will override all default loggins like console and debug loggss
//builder.Logging.AddSerilog();  // this will take into account all default(inbuilt) and serilog loggers also

// this will create log folder in solution explorer and log texts file will be generated.

// code to implement Serilog [ends] 07202025
#endregion 

// 072025 In built loggers part 28 - starts
//builder.Logging.ClearProviders(); // clear inbuilt providers
//builder.Logging.AddDebug();   // this adds only debug logging, u will see this in output window in vstudio ->debug
//builder.Logging.AddConsole();// this info is shown in console window

// 072025 In built loggers part 28 - ends


//builder.Services.AddControllers().AddNewtonsoftJson(); // AddNewtonsoftJson() is added when you want to implement Patch method [HttpPatch]

// Add services to the container.

//builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson(); // AddNewtonSoftJson for HttpPatch 
// add options => options.ReturnHttpNotAcceptable = true , when you want the data to be displyed in xml format but xml format is not available on postman
// so if this application/xml format is not acceptable then it will display error message as
// "406 Not Acceptable"


// to support xml data we need to add following method
// so above line is rewritten below 

//builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// above line is used when u want result in xml format and its returng 406 error, to add xml format
builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); //----------------->comment when not using Swagger
builder.Services.AddSwaggerGen(); // -----------------> comment when not using Swagger

//builder.Services.AddScoped<IMyLogger, LogToFile>(); // here loosely couple DI pointing to LogToFile, when u need to change to logToServer //071925
// just change here . It says when this type of interface is used , return the instance of LogToFile Class.


#region start for auto mapper config
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
#endregion

builder.Services.AddTransient<IStudentRepository, StudentRepository>(); // part 51,52 repo design pattern


#region connection to db using code first approach
//to create databse from code first approach add connection string as below
builder.Services.AddDbContext<CollegeDBContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeDbConn"))
);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => options.OpenApiVersion =
Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0);    //added options as swagger was not opening due to version mismatch            //  -----------------> comment when not using Swagger
    app.UseSwaggerUI(); // -----------------> comment when not using Swagger API
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

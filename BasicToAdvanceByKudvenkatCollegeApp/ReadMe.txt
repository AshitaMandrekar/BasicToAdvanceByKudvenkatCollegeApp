HttpPut - this will send the entire model data for updation even if u have just 2 or 3 fields to be updated. This consumes bandwith and network

to overcome this , we use HttpPatch

With HttpPatch , u can send only the required data to server thats how we can optimization and efficient execution of the endpoint.

to use HttpPatch - go to nuget package manager -
download two files 1. Microsoft.AspNetCore.JsonPatch
                   2. Microsoft.AspNetCore.MVC.NewtonSoftJson


go to program.cs file and add this with

builder.services.AddControllers.AddNewtonSoftJSON();

Write the code as below:

  [HttpPatch]
  [Route("{id:int}/UpdatePartial")] // new added
  // api/student/id/UpdatePartial
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public ActionResult UpdatePartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument) // pass id fr updating
  {
      if (patchDocument == null)
      {
          return BadRequest();
      }
      var existingDetails = CollegeRepository.student.Where(x => x.Id == id).FirstOrDefault();
      if (existingDetails == null)
      {
          return NotFound();
      }

      StudentDTO dto = new StudentDTO()
      {
          Id = existingDetails.Id,
          StudentName = existingDetails.StudentName,
          Email = existingDetails.Email,
          Address = existingDetails.Address
      };

      patchDocument.ApplyTo(dto, ModelState);
      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }
      existingDetails.Id = dto.Id;
      existingDetails.StudentName = dto.StudentName;
      existingDetails.Email = dto.Email;
      existingDetails.Address = dto.Address;

      return NoContent();
      //  return Ok(existingDetails);
  }



Now in the output window modify the details as follows for the field u need to update

    [
       {
        "path" :"/studentName",
        "op" : "replace",
        "value" : "Ashita New"
       },
       {
        "path" :"/studentName",  // which field u want to update , field name
        "op" : "replace",   // what operation u want to perform
        "value" : "Ashita New" // new name
        }
    ]

---------------------------------------------------------------------------------------------------------------------------------------
Implementing Dependency Injection

To implement dependency injection 
1. Create folder MyLogging
      2. Create 4 files 
         IMyLogger.cs
         LogToDB.cs
         LogtoFile.cs
         LogToServerMemory.cs


check code for the ouput







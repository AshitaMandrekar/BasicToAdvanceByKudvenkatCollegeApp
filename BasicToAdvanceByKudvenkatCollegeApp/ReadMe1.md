HttpPut - this will send the entire model data for updation even if u have just 2 or 3 fields to be updated. This consumes bandwith and network



to overcome this , we use HttpPatch



**With HttpPatch , u can send only the required data to server thats how we can optimization and efficient execution of the endpoint.**



to use HttpPatch - go to nuget package manager -

download two files 1. Microsoft.AspNetCore.JsonPatch

&nbsp;                  2. Microsoft.AspNetCore.MVC.NewtonSoftJson





go to program.cs file and add this with



builder.services.AddControllers.AddNewtonSoftJSON();



Write the code as below:



&nbsp; \[HttpPatch]

&nbsp; \[Route("{id:int}/UpdatePartial")] // new added

&nbsp; // api/student/id/UpdatePartial

&nbsp; \[ProducesResponseType(StatusCodes.Status400BadRequest)]

&nbsp; \[ProducesResponseType(StatusCodes.Status200OK)]

&nbsp; \[ProducesResponseType(StatusCodes.Status204NoContent)]

&nbsp; public ActionResult UpdatePartial(int id, \[FromBody] JsonPatchDocument<StudentDTO> patchDocument) // pass id fr updating

&nbsp; {

&nbsp;     if (patchDocument == null)

&nbsp;     {

&nbsp;         return BadRequest();

&nbsp;     }

&nbsp;     var existingDetails = CollegeRepository.student.Where(x => x.Id == id).FirstOrDefault();

&nbsp;     if (existingDetails == null)

&nbsp;     {

&nbsp;         return NotFound();

&nbsp;     }



&nbsp;     StudentDTO dto = new StudentDTO()

&nbsp;     {

&nbsp;         Id = existingDetails.Id,

&nbsp;         StudentName = existingDetails.StudentName,

&nbsp;         Email = existingDetails.Email,

&nbsp;         Address = existingDetails.Address

&nbsp;     };



&nbsp;     patchDocument.ApplyTo(dto, ModelState);

&nbsp;     if (!ModelState.IsValid)

&nbsp;     {

&nbsp;         return BadRequest(ModelState);

&nbsp;     }

&nbsp;     existingDetails.Id = dto.Id;

&nbsp;     existingDetails.StudentName = dto.StudentName;

&nbsp;     existingDetails.Email = dto.Email;

&nbsp;     existingDetails.Address = dto.Address;



&nbsp;     return NoContent();

&nbsp;     //  return Ok(existingDetails);

&nbsp; }







Now in the output window modify the details as follows for the field u need to update



&nbsp;   \[

&nbsp;      {

&nbsp;       "path" :"/studentName",

&nbsp;       "op" : "replace",

&nbsp;       "value" : "Ashita New"

&nbsp;      },

&nbsp;      {

&nbsp;       "path" :"/studentName",  // which field u want to update , field name

&nbsp;       "op" : "replace",   // what operation u want to perform

&nbsp;       "value" : "Ashita New" // new name

&nbsp;       }

&nbsp;   ]






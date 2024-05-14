using crud_backend.Data;
using crud_backend.Models;
using crud_backend.Models.Domain;
using crud_backend.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crud_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DataContext dataContext;

        public StudentsController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        // Get all Students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            // Get data from database - domain model
           var studentsDomain =  dataContext.Students.ToList();

            // Map Domain Models to DTO
            var studentDTO = new List<StudentDTO>();
            foreach (var studentDomain in studentsDomain) 
            {
                studentDTO.Add(new StudentDTO()
                {
                    Id = studentDomain.Id,
                    FirstName = studentDomain.FirstName,
                    LastName = studentDomain.LastName,
                    Email = studentDomain.Email,
                    Program = studentDomain.Program,
                    address = studentDomain.address,
                    Age = studentDomain.Age,
                });
            }

            // Return DTO
           return Ok(studentDTO);
        }

        // Get single Student by ID
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetStudent([FromRoute] Guid id) 
        {

            // Get data from database - domain model
            var studentDomain = dataContext.Students.FirstOrDefault(x => x.Id == id);

            if (studentDomain == null)
            {
                return NotFound();
            }

            // Map Domain Models to DTO
            var studentDTO = new StudentDTO
            {
                Id = studentDomain.Id,
                FirstName = studentDomain.FirstName,
                LastName = studentDomain.LastName,
                Email = studentDomain.Email,
                Program = studentDomain.Program,
                address = studentDomain.address,
                Age = studentDomain.Age,
            };

            return Ok(studentDTO);
        }

        // Get Student by name
        [HttpGet]
        [Route("{name}")]
        public IActionResult GetStudentByName([FromRoute] string name) 
        {
            var studentDomain = dataContext.Students.FirstOrDefault(x => x.FirstName == name || x.LastName == name);
            if (studentDomain == null)
            {
                return NotFound();
            }

            // Map Domain Models to DTO
            var studentDTO = new StudentDTO
            {
                Id = studentDomain.Id,
                FirstName = studentDomain.FirstName,
                LastName = studentDomain.LastName,
                Email = studentDomain.Email,
                Program = studentDomain.Program,
                address = studentDomain.address,
                Age = studentDomain.Age,
            };

            return Ok(studentDTO);
        }


        // Create a new Student
        [HttpPost]
        public IActionResult CreateStudent([FromBody] CreateStudentDTO createStudentDTO)
        {
            // Map or Convert DTO to Domain Model
            var studentDomain = new Student
            {
                FirstName = createStudentDTO.FirstName,
                LastName = createStudentDTO.LastName,
                Email = createStudentDTO.Email,
                Program = createStudentDTO.Program,
                address = createStudentDTO.address,
                Age = createStudentDTO.Age,
            };

            // Use Domain Model to create Student
            dataContext.Students.Add(studentDomain);
            dataContext.SaveChanges();

            // Map Domain model to DTO
            var studentDTO = new StudentDTO
            {
                Id = studentDomain.Id,
                FirstName = studentDomain.FirstName,
                LastName = studentDomain.LastName,
                Email = studentDomain.Email,
                Program = studentDomain.Program,
                address = studentDomain.address,
                Age = studentDomain.Age,
            };

            return CreatedAtAction(nameof(GetStudent), new {id = studentDTO.Id}, studentDTO);
        }


        // Update a Student
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateStudent(Guid id, [FromBody] UpdateStudentDTO updateStudentDTO)
        {
            // Retrieve the student from the database
            var studentDomain = dataContext.Students.FirstOrDefault(x => x.Id == id);

            // Check if student exists
            if (studentDomain == null)
            {
                return NotFound();
            }

            // Update the student properties with DTO data if present
            if (updateStudentDTO.FirstName != null)
            {
                studentDomain.FirstName = updateStudentDTO.FirstName;
            }
            if (updateStudentDTO.LastName != null)
            {
                studentDomain.LastName = updateStudentDTO.LastName;
            }
            if (updateStudentDTO.Email != null)
            {
                studentDomain.Email = updateStudentDTO.Email;
            }
            if (updateStudentDTO.Program != null)
            {
                studentDomain.Program = updateStudentDTO.Program;
            }
            if (updateStudentDTO.address != null)
            {
                studentDomain.address = updateStudentDTO.address;
            }
            if (updateStudentDTO.Age != null)
            {
                studentDomain.Age = updateStudentDTO.Age.Value;
            }

            dataContext.SaveChanges();

            return Ok(updateStudentDTO);
        }



        // Delete a Student
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteStudent(Guid id) 
        {
            // Retrive Student from Domain Model
            var studentModel = dataContext.Students.FirstOrDefault(x => x.Id==id);

            if (studentModel == null)
            {
                return NotFound();
            }

            // Remove Student
            dataContext.Students.Remove(studentModel);
            dataContext.SaveChanges(true);

            return Ok(studentModel);

        }
    }
}

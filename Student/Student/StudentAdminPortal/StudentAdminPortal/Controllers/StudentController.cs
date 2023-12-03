using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.Models;
using StudentAdminPortal.Repository;
using StudentAdminPortal.ViewModel;

namespace StudentAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository) 
        {

            _studentRepository = studentRepository;
        }


        [HttpPost("AddStudent")]
        public async Task<ActionResult> AddStudent([FromBody] UpdateStudentViewModel studentView)
        {
            var student = _studentRepository.AddStudent(studentView);
            return Ok(student); 

        }


        [HttpGet("GetAllStudent")]
        public async Task<ActionResult<Student>> GetAllStudent()
        {
            return Ok( await _studentRepository.GetAllStudent()); 
        }

        [HttpGet("GetStudent/{id:int}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            return Ok(await _studentRepository.GetStudent(id));
        }


        [HttpPut("UpdateStudent/{id:int}")]
        public async Task<ActionResult> UpdateStudent(int id, [FromBody] UpdateStudentViewModel studentView)
        {
            if(await _studentRepository.StudentAlredyExist(id))
            {
                var updateStudent = _studentRepository.UpdateStudent(id, studentView);
                if(updateStudent != null)
                {
                    return Ok(updateStudent);
                }
               
            }

            return NotFound();  
            
        }


        [HttpDelete("DeleteStudent/{id:int}")]
        public async Task<ActionResult> DeleteStudent([FromRoute]int id)
        {
            if (await _studentRepository.StudentAlredyExist(id))
            {
                var student = _studentRepository.DeleteStudentAsync(id);
                return Ok(student); 

            }

            return NotFound();

        }

        //[HttpPost("ImageUpload/{id:int}")]
        //public async Task<IActionResult> ImageUpload([FromRoute] int id,IFormFile profil)
        //{
        //   if(await _studentRepository.StudentAlredyExist(id))
        //   {
        //        var absoluteFilename = await _studentRepository.UploadInage(profil);
        //        if(await _studentRepository.UpdateImageUri(id, absoluteFilename))
        //        {
        //            return Ok(absoluteFilename);    
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError);   
        //        }
        //   }
        //    return NotFound(id);

        //}

        [HttpPost("ImageUpload/{id:int}")]
        public async Task<IActionResult> ImageUpload([FromRoute] int id, IFormFile profil)
        {
            if (await _studentRepository.StudentAlredyExist(id))
            {
                var azureBlobUrl = await _studentRepository.UploadInage(profil);
                if (await _studentRepository.UpdateImageUri(id, azureBlobUrl))
                {
                    return Ok(azureBlobUrl);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return NotFound(id);
        }




    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.Models;
using StudentAdminPortal.Repository;

namespace StudentAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public GenderController(IStudentRepository studentRepository)
        {

            _studentRepository = studentRepository;
        }
        [HttpGet("GetAllGender")]
        public async Task<ActionResult<Gender>> GetAllGender()
        {
            return Ok(await _studentRepository.GetGenderASync());
        }
    }
}

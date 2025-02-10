using Appliction.Interfaces.Student;
using Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet("GetAllCourse")]
        public async  Task<IActionResult> GetAllCourse() { 
            var course=await _courseService.GetAllCoursesAsync();
            if (course == null || course.Count == 0)
            {
                return NotFound("No courses found.");
            }
            return Ok(course);
        
        }
    }
}

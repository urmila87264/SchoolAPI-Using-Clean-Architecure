using Appliction.Interfaces.Student;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {

            _courseRepository = courseRepository;

        }
        //public async Task<Courses> GetAllCoursesAsync()
        //{
        //    return await _courseRepository.GetAllCoursesAsync();
        //}

        public async Task<List<Courses>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllCoursesAsync();
        }
    }
}

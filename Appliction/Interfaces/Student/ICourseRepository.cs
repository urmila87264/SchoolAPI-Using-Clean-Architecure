using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Interfaces.Student
{
    public interface ICourseRepository
    {
        Task<List<Courses>> GetAllCoursesAsync();
    }
}

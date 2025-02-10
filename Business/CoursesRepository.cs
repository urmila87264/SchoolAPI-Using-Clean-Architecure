using Appliction.Interfaces.Student;
using Appliction.Procedure;
using DBHelper;
using Domain.Common;
using Microsoft.Azure.Documents;
using System.Data;
using System.Data.SqlClient;
namespace Infrasture
{
    public class CoursesRepository : ICourseRepository
    {
        private readonly ISQLHelper _sqlHelper;
        public CoursesRepository(ISQLHelper sQLHelper)
        {
            _sqlHelper= sQLHelper;
        }
        public async Task<List<Courses>> GetAllCoursesAsync()
        {
            System.Data.SqlClient.SqlParameter[] para = { };
            DataSet ds = await _sqlHelper.ExecuteDataSetAsync(StudentProcedure.Course, para);

            List<Courses> courseList = new List<Courses>();

            if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    courseList.Add(new Courses
                    {
                        CourseID = Convert.ToInt32(row["CourseID"]),
                        CourseName = row["CourseName"].ToString()
                    });
                }
            }

            return courseList;
        }

    }
}

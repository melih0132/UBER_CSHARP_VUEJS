using Microsoft.AspNetCore.Mvc;
using UberApi.Models.EntityFramework;

namespace UberApi.Models.Repository
{
    public interface ICourseRepository : IDataRepository<Course>
    {
        Task<ActionResult<IEnumerable<Course>>> GetByStringStatutCourseAsync(string statutCourse);
    }
}

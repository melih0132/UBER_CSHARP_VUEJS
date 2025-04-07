using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Models.DataManager
{
    public class CourseManager : ICourseRepository
    {

        readonly S221UberContext? s221UberContext;
        public CourseManager() { }
        public CourseManager(S221UberContext context)
        {
            s221UberContext = context;
        }
        public async Task<ActionResult<IEnumerable<Course>>> GetAllAsync()
        {
            return await s221UberContext.Courses.ToListAsync();
        }
        public async Task<ActionResult<Course>> GetByIdAsync(int id)
        {
            return await s221UberContext.Courses.FirstOrDefaultAsync(u => u.IdCourse == id);
        }
        public async Task<ActionResult<Course>> GetByStringAsync(string statutCourse)
        {
            return await s221UberContext.Courses.FirstOrDefaultAsync(u => u.StatutCourse.ToUpper() == statutCourse.ToUpper());
        }
        public async Task<ActionResult<IEnumerable<Course>>> GetByStringStatutCourseAsync(string statutCourse)
        {
            return await s221UberContext.Courses.Where(u => u.StatutCourse.ToUpper() == statutCourse.ToUpper()).ToListAsync();
        }

        public async Task AddAsync(Course entity)
        {
            await s221UberContext.Courses.AddAsync(entity);
            await s221UberContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Course newCourse, Course entity)
        {
            s221UberContext.Entry(newCourse).State = EntityState.Modified;
            newCourse.IdCourse = entity.IdCourse;
            newCourse.IdCoursier = entity.IdCoursier;
            newCourse.IdCb = entity.IdCb;
            newCourse.IdAdresse = entity.IdAdresse;
            newCourse.IdReservation = entity.IdReservation;
            newCourse.AdrIdAdresse = entity.AdrIdAdresse;
            newCourse.IdPrestation = entity.IdPrestation;
            newCourse.DateCourse = entity.DateCourse;
            newCourse.HeureCourse = entity.HeureCourse;
            newCourse.PrixCourse = entity.PrixCourse;
            newCourse.StatutCourse = entity.StatutCourse;
            newCourse.NoteCourse = entity.NoteCourse;
            newCourse.CommentaireCourse = entity.CommentaireCourse;
            newCourse.Pourboire = entity.Pourboire;
            newCourse.Distance = entity.Distance;
            newCourse.Temps = entity.Temps;
            await s221UberContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Course course)
        {
            s221UberContext.Courses.Remove(course);
            await s221UberContext.SaveChangesAsync();
        }
    }
}

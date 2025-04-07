using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly ICourseRepository dataRepository;

        public CoursesController(ICourseRepository dataRepo)
        {
            dataRepository = dataRepo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesAsync()
        {
            return await dataRepository.GetAllAsync();
        }



        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Course>> GetCourseAsync(int id)
        {
            var course = await dataRepository.GetByIdAsync(id);

            if (course.Value == null)
            {
                return NotFound();
            }
            return course;

        }


        [HttpGet]
        [Route("[action]/{statut}")]
        [ActionName("GetByStatut")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Course>>> GetByStatut(string statut)
        {
            return await dataRepository.GetByStringStatutCourseAsync(statut);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCourseAsync(int id, Course course)
        {
            if (id != course.IdCourse)
            {
                return BadRequest();
            }
            var userToUpdate = await dataRepository.GetByIdAsync(id);
            if (userToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, course);
                return NoContent();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Course>> PostCourseAsync(Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(course);
            return CreatedAtAction("GetById", new { id = course.IdCourse }, course); // GetById : nom de l’action
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCourseAsync(int id)
        {
            var course = await dataRepository.GetByIdAsync(id);
            if (course.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(course.Value);
            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.DataManager;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using UberApi.Models.Repository;

namespace UberApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntretiensController : ControllerBase
    {
        private readonly IDataRepository<Entretien> dataRepository;

        public EntretiensController(IDataRepository<Entretien> dataRepo)
        {
            dataRepository = dataRepo;
        }   




        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entretien>>> GetEntretiensAsync()
        {
            return await dataRepository.GetAllAsync();
        }





        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Entretien>> GetEntretienAsync(int id)
        {
            var entretien = await dataRepository.GetByIdAsync(id);

            if (entretien.Value == null)
            {
                return NotFound();
            }
            return entretien;

        }


        [HttpGet]
        [Route("[action]/{idCoursier}")]
        [ActionName("GetByResultatEntretien")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Entretien>> GetEntretienByIdCoursierAsync(string idCoursier)
        {
            var entretien = await dataRepository.GetByStringAsync(idCoursier);
            if (entretien.Value == null)
            {
                return NotFound();
            }
            return entretien;
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutEntretienAsync(int id, Entretien entretien)
        {
            if (id != entretien.IdEntretien)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, entretien);
                return NoContent();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Entretien>> PostEntretienAsync(Entretien entretien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(entretien);
            return CreatedAtAction("GetById", new { id = entretien.IdEntretien }, entretien); // GetById : nom de l’action
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEntretienAsync(int id)
        {
            var entretien = await dataRepository.GetByIdAsync(id);
            if (entretien.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(entretien.Value);
            return NoContent();
        }
    }
}

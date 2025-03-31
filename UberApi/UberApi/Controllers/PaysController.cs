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
    public class PaysController : ControllerBase
    {
        private readonly IDataRepository<Pays> dataRepository;

        public PaysController(IDataRepository<Pays> dataRepo)
        {
            dataRepository = dataRepo;
        }   




        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pays>>> GetPaysAsync()
        {
            return await dataRepository.GetAllAsync();
        }





        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pays>> GetPaysAsync(int id)
        {
            var pays = await dataRepository.GetByIdAsync(id);

            if (pays.Value == null)
            {
                return NotFound();
            }
            return pays;

        }


        [HttpGet]
        [Route("[action]/{nomPays}")]
        [ActionName("GetByLibellePays")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pays>> GetPaysByNomPaysAsync(string nomPays)
        {
            var pays = await dataRepository.GetByStringAsync(nomPays);
            if (pays.Value == null)
            {
                return NotFound();
            }
            return pays;
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPaysAsync(int id, Pays pays)
        {
            if (id != pays.IdPays)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, pays);
                return NoContent();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pays>> PostPaysAsync(Pays pays)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(pays);
            return CreatedAtAction("GetById", new { id = pays.IdPays }, pays); // GetById : nom de l’action
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePaysAsync(int id)
        {
            var pays = await dataRepository.GetByIdAsync(id);
            if (pays.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(pays.Value);
            return NoContent();
        }
    }
}

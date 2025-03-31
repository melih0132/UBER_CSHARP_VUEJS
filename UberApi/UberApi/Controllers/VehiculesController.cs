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
    public class VehiculesController : ControllerBase
    {
        private readonly IDataRepository<Vehicule> dataRepository;

        public VehiculesController(IDataRepository<Vehicule> dataRepo)
        {
            dataRepository = dataRepo;
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicule>>> GetVehiculesAsync()
        {
            return await dataRepository.GetAllAsync();
        }





        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Vehicule>> GetVehiculeAsync(int id)
        {
            var vehicule = await dataRepository.GetByIdAsync(id);

            if (vehicule.Value == null)
            {
                return NotFound();
            }
            return vehicule;

        }


        [HttpGet]
        [Route("[action]/{email}")]
        [ActionName("GetByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Vehicule>> GetVehiculeByImmatriculationAsync(string immatriculation)
        {
            var utilisateur = await dataRepository.GetByStringAsync(immatriculation);
            if (utilisateur.Value == null)
            {
                return NotFound();
            }
            return utilisateur;
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutVehiculeAsync(int id, Vehicule vehicule)
        {
            if (id != vehicule.IdVehicule)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, vehicule);
                return NoContent();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Vehicule>> PostVehiculeAsync(Vehicule vehicule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(vehicule);
            return CreatedAtAction("GetById", new { id = vehicule.IdVehicule }, vehicule); // GetById : nom de l’action
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVehiculeAsync(int id)
        {
            var vehicule = await dataRepository.GetByIdAsync(id);
            if (vehicule.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(vehicule.Value);
            return NoContent();
        }


    }
}

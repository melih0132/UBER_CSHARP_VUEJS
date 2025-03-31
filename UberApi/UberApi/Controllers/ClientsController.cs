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
using Microsoft.AspNetCore.Authorization;

namespace UberApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly IDataRepository<Client> dataRepository;

        public ClientsController(IDataRepository<Client> dataRepo)
        {
            dataRepository = dataRepo;
        }   

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsAsync()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client>> GetClientAsync(int id)
        {
            var client = await dataRepository.GetByIdAsync(id);

            if (client.Value == null)
            {
                return NotFound();
            }
            return client;

        }

        [HttpGet]
        [Route("[action]/{email}")]
        [ActionName("GetByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client>> GetUtilisateurByEmailAsync(string email)
        {
            var utilisateur = await dataRepository.GetByStringAsync(email);
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
        public async Task<IActionResult> PutClientAsync(int id, Client client)
        {
            if (id != client.IdClient)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, client);
                return NoContent();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Client>> PostClientAsync(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(client);
            return CreatedAtAction("GetById", new { id = client.IdClient }, client);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            var client = await dataRepository.GetByIdAsync(id);
            if (client.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(client.Value);
            return NoContent();
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Client>> RegisterClientAsync(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Vérifier si l'email est déjà utilisé
            var existingClient = await dataRepository.GetByStringAsync(client.EmailUser);
            if (existingClient.Value != null)
            {
                return Conflict("Un compte avec cet email existe déjà.");
            }

            // Ajouter le client en base
            await dataRepository.AddAsync(client);

            return CreatedAtAction("GetById", new { id = client.IdClient }, client);
        }
    }
}

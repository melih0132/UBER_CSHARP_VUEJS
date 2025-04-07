using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UberApi.Models.EntityFramework;
using UberApi.Models.DataManager;
using UberApi.Models;
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
        public async Task<IActionResult> Register([FromBody] ClientDTO clientDto)
        {
            if (clientDto == null)
            {
                return BadRequest(new { message = "Données invalides." });
            }

            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - clientDto.DateNaissance.Year;

            if (clientDto.DateNaissance > today.AddYears(-age))
            {
                age--;
            }

            if (age < 18)
            {
                return BadRequest(new { message = "Vous devez avoir au moins 18 ans pour vous inscrire." });
            }

            try
            {
                var newClient = new Client
                {
                    NomUser = clientDto.NomUser,
                    PrenomUser = clientDto.PrenomUser,
                    GenreUser = clientDto.GenreUser,
                    DateNaissance = clientDto.DateNaissance,
                    Telephone = clientDto.Telephone,
                    EmailUser = clientDto.EmailUser,
                    MotDePasseUser = clientDto.MotDePasseUser,
                    TypeClient = clientDto.TypeClient
                };

                await dataRepository.AddAsync(newClient);

                return CreatedAtAction(nameof(Register), new { id = newClient.IdClient }, new { message = "Inscription réussie !" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}

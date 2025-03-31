using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;

namespace UberApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RestaurateursController : ControllerBase
    {
        private readonly IDataRepository<Restaurateur> dataRepository;

        public RestaurateursController(IDataRepository<Restaurateur> dataRepo)
        {
            dataRepository = dataRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurateur>>> GetRestaurateurs()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Restaurateur>> GetRestaurateurById(int id)
        {
            var restaurateur = await dataRepository.GetByIdAsync(id);

            if (restaurateur.Value == null)
            {
                return NotFound();
            }
            return restaurateur;

        }

        [HttpGet]
        [Route("[action]/{email}")]
        [ActionName("GetByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Restaurateur>> GetRestaurateurByEmail(string email)
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
        public async Task<IActionResult> PutRestaurateur(int id, Restaurateur restaurateur)
        {
            if (id != restaurateur.IdRestaurateur)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, restaurateur);
                return NoContent();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Restaurateur>> PostRestaurateur(Restaurateur restaurateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(restaurateur);
            return CreatedAtAction("GetById", new { id = restaurateur.IdRestaurateur }, restaurateur);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurateur(int id)
        {
            var restaurateur = await dataRepository.GetByIdAsync(id);
            if (restaurateur.Value == null)
            {
                return NotFound();

            }
            await dataRepository.DeleteAsync(restaurateur.Value);
            return NoContent();
        }
    }
}

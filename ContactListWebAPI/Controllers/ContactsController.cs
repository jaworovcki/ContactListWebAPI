using ContactListWebAPI.Interfaces;
using ContactListWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactListWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsRepository _repository;

        public ContactsController(IContactsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Person>))]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllPeople()
        {
            var people = await _repository.GetPeopleAsync();
            return Ok(people);
        }

        [HttpGet("{id}", Name = nameof(GetPersonById))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Person))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Person>> GetPersonById(int id)
        {
            try
            {
                var person = await _repository.GetPersonByIdAsync(id);
                if (person is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(person);
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("findByName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Person))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Person>> GetPersonByName([FromQuery] string nameFilter)
        {
            try
            {
                var person = await _repository.GetPersonByNameAsync(nameFilter);
                if (person is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(person);
                }
            }
            catch(ArgumentNullException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Person))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Person>> AddPerson([FromBody] Person person)
        {
            try
            {
                var newPerson = await _repository.AddPersonAsync(person);
                return CreatedAtAction(nameof(GetPersonById), new { id = newPerson.Id }, newPerson);

            }
            catch(ArgumentNullException)
            {
                return BadRequest();   
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeletePerson(int id)
        {
            try
            {
                await _repository.DeletePersonAsync(id);
                return NoContent();
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}

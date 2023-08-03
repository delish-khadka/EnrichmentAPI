using AutoMapper;
using EnrichmentAPI.DTO;
using EnrichmentAPI.Interfaces;
using EnrichmentAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnrichmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public PersonController(IPersonRepository personRepository, IMapper mapper, ICountryRepository countryRepository)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;

        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Person>))]
        public IActionResult GetPersons()
        {
            var owners = _mapper.Map<List<PersonDTO>>(_personRepository.GetAll());
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Person))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int personId)
        {
            var owner = _mapper.Map<PersonDTO>(
                _personRepository.GetById(personId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] PersonDTO ownerCreate)
        {
            if (ownerCreate == null)
            {
                return BadRequest(ModelState);
            }

            var ownerMap = _mapper.Map<Person>(ownerCreate);
            ownerMap.Country = _countryRepository.GetById(countryId);
            _personRepository.AddPerson(ownerMap);

            return Ok("Successfully created owner");
        }

        //[HttpPut("{ownerId}")]
        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePerson([FromQuery] int ownerId, [FromBody] PersonDTO personDTO)
        {
            if (personDTO == null)
            {
                return BadRequest(ModelState);
            }
            if (ownerId != personDTO.Id)
            {
                return BadRequest(ModelState);
            }
            //if (!_ownerRepository.OwnerExists(ownerId))
            //{
            //    return NotFound();
            //}
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var personMap = _mapper.Map<Person>(personDTO);
            _personRepository.UpdatePerson(personMap);
            

            return NoContent();
        }

        [HttpDelete("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int ownerId)
        {
            //if (!_ownerRepository.OwnerExists(ownerId))
            //    return NoContent();

            var personToDelete = _personRepository.GetById(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _personRepository.DeletePerson(personToDelete);
 
            return NoContent();
        }

    }
}

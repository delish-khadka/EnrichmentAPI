using AutoMapper;
using EnrichmentAPI.DTO;
using EnrichmentAPI.Interfaces;
using EnrichmentAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace EnrichmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDTO>>(_countryRepository.GetAll());
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            var country = _mapper.Map<CountryDTO>(_countryRepository.GetById(countryId));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(country);
        }

        [HttpGet("/persons/{personId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountryOfAnOwner(int personId)
        {
            var country = _mapper.Map<CountryDTO>(
                _countryRepository.GetCountryByPerson(personId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(country);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDTO countryCreate)
        {
            if (countryCreate == null)
            {
                return BadRequest(ModelState);
            }
            var country = _countryRepository.GetAll().
                Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }
            var countryMap = _mapper.Map<Country>(countryCreate);
            _countryRepository.AddCountry(countryMap);
            
            return Ok("Successfully created country");
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryDTO updateCountry)
        {
            if (updateCountry == null)
            {
                return BadRequest(ModelState);
            }
            if (countryId != updateCountry.Id)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var countryMap = _mapper.Map<Country>(updateCountry);
            _countryRepository.UpdateCountry(countryMap);

            return NoContent();
        }


        [HttpDelete("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCountry(int countryId)
        {

            var countryToDelete = _countryRepository.GetById(countryId);

            if (!ModelState.IsValid)
                return NotFound();

            _countryRepository.DeleteCountry(countryToDelete);
            return NoContent();
        }
    }
}

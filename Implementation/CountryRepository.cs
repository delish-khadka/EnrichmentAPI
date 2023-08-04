using EnrichmentAPI.Data;
using EnrichmentAPI.Interfaces;
using EnrichmentAPI.Models;

namespace EnrichmentAPI.Implementation
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _context;
        public CountryRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddCountry(Country country)
        {
            _context.Add(country);
            _context.SaveChanges();
        }

        public bool CountryExists(int countryId)
        {
            return _context.Countries.Any(x => x.Id == countryId);
        }

        public void DeleteCountry(Country country)
        {
            _context.Remove(country);
            _context.SaveChanges();
        }

        public List<Country> GetAll()
        {
            var allItems = _context.Countries.ToList();
            return allItems;
        }

        public Country GetById(int id)
        {
            var countryId = _context.Countries.Where(x=>x.Id == id).FirstOrDefault();
            return countryId;
        }

        public Country GetCountryByPerson(int personId)
        {
            return _context.Persons.Where(x => x.Id == personId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Person> GetPersonsFromACountry(int countryId)
        {
            return _context.Persons.Where(c => c.Country.Id == countryId).ToList();
        }

        public void UpdateCountry(Country country)
        {
            _context.Update(country);
            _context.SaveChanges();
        }
    }
}

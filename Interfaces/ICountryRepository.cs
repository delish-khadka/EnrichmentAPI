using EnrichmentAPI.Models;

namespace EnrichmentAPI.Interfaces
{
    public interface ICountryRepository
    {
        List<Country> GetAll();
        Country GetById(int id);
        void AddCountry(Country country);
        void UpdateCountry(Country country);
        void DeleteCountry(Country country);
        Country GetCountryByPerson(int ownerId);
        ICollection<Person> GetPersonsFromACountry(int countryId);
    }
}

using EnrichmentAPI.Models;

namespace EnrichmentAPI.Interfaces
{
    public interface IPersonRepository
    {
        List<Person> GetAll();
        Person GetById(int id);
        void AddPerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
    }
}

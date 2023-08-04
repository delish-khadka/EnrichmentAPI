using EnrichmentAPI.Data;
using EnrichmentAPI.Interfaces;
using EnrichmentAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace EnrichmentAPI.Implementation
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;
        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddPerson(Person person)
        {
            _context.Add(person);
            _context.SaveChanges();
        }

        public void DeletePerson(Person person)
        {
            _context.Add(person);
            _context.SaveChanges();
        }

        public List<Person> GetAll()
        {
            var allItems = _context.Persons.ToList();
            return allItems;
        }

        public Person GetById(int id)
        {
            var personId = _context.Persons.Where(x => x.Id == id).FirstOrDefault();
            return personId;
        }

        public bool PersonExists(int personId)
        {
            return _context.Persons.Any(x => x.Id == personId);


        }

        public void UpdatePerson(Person person)
        {
            _context.Add(person);
            _context.SaveChanges();
        }
    }
}

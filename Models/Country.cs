using System.ComponentModel.DataAnnotations;

namespace EnrichmentAPI.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Person> Person { get; set; }
    }
}

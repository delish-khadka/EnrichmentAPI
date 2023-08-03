using System.ComponentModel.DataAnnotations;

namespace EnrichmentAPI.DTO
{
    public class CountryDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

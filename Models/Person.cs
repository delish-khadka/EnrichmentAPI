namespace EnrichmentAPI.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Education { get; set; } 
        public int Age { get; set; }
        public string Address { get; set; }
        public Country Country { get; set; }
    }
}

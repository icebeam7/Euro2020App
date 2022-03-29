namespace Euro2020App.Models
{
    public class EuroCoach
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }

        public int EuroCountryId { get; set; }
        public EuroCountry FK_Country { get; set; }
    }
}

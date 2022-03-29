namespace Euro2020App.Models
{
    public class EuroReferee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }

        public int EuroCountryId { get; set; }
        public EuroCountry FK_Country { get; set; }

        public int Games { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
    }
}

namespace Euro2020App.Models
{
    public class EuroStadium
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }

        public int EuroCityId { get; set; }
        public EuroCity FK_City { get; set; }

        public int Capacity { get; set; }
    }
}

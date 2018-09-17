
namespace CompanyNetCore.Model
{
    public class Address
    {
        public Address(int id, int postcode, string city, string street, string country)
        {
            this.Id = id;
            this.Postcode = postcode;
            this.City = city;
            this.Street = street;
            this.Country = country;
        }
        public int Id { get; set; }
        public int Postcode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
    }
}

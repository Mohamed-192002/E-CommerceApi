namespace ECommerce.Core.Entities.Orders
{
    public class ShipAddress
    {
        public ShipAddress()
        {
            
        }
        public ShipAddress(string city, string state, string zipCode)
        {
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

    }
}
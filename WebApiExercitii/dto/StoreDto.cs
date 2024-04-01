using WebApiExercitii.Models;

namespace WebApiExercitii.dto
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public string Name {  get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public int MonthlyIncome { get; set; }
        public string OwnerName { get; set; }

        public DateTime ActiveSince { get; set; }

        //public List<Inventory> Inventories { get; set; }
        public StoreDto() { }
        public StoreDto(Store s)
        {
            Id = s.Id;
            ActiveSince = s.ActiveSince;
            Name = s.Name;
            Country = s.Country;
            City = s.City;
            MonthlyIncome = s.MonthlyIncome;
            OwnerName = s.OwnerName;
            //Inventories = s.Inventories;
        }
    }
}

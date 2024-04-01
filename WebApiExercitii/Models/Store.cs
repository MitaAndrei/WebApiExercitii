using WebApiExercitii.dto;

namespace WebApiExercitii.Models
{
    public class Store
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public string City { get; set; }
        public int MonthlyIncome { get; set; }

        public string OwnerName { get; set; }
        public DateTime ActiveSince { get; set; }

        public List<Product> Products { get; set; }
        public List<Inventory> Inventories { get; set; }
        public Store() { } 
        public Store(StoreDto s)
        {
            Id = new Guid();
            Name = s.Name;
            Country = s.Country;
            City = s.City;
            MonthlyIncome = s.MonthlyIncome;
            OwnerName = s.OwnerName;
            ActiveSince = s.ActiveSince;
            //Inventories = s.Inventories;
        }
    }
}

/* - Id - Guid
- Name - string
- Country - string
- City - string
- MonthlyIncome - int
- OwnerName - string
- ActiveSince - Datetime
*/
using WebApiExercitii.dto;

namespace WebApiExercitii.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] Ratings { get; set; }

        public DateTime CreatedOn { get; set; }

        public List<Store> Stores {  get; set; }
        //public List<Inventory> Inventories { get; set; }
        public Product()
        {
            CreatedOn = DateTime.Now;
        }
        public Product(ProductDto p)
        {
            Id = new Guid();
            Name = p.Name;
            Description = p.Description;
            Ratings = p.Ratings;
            CreatedOn = DateTime.Now;
            //Inventories = p.Inventories;
        }

    }
}

/* - Id - Guid (must be unique)
- Name - string
- Description - string
- Ratings - int[] (array of ratings between 1 and 5)
- CreatedOn - Datetime (when it was added to the list) -> automatically generated
*/
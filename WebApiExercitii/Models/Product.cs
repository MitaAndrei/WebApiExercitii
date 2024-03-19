namespace WebApiExercitii.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] Ratings { get; set; }

        public DateTime CreatedOn { get; set; }

        public Product()
        {
            CreatedOn = DateTime.Now;
        }
    }
}

/* - Id - Guid (must be unique)
- Name - string
- Description - string
- Ratings - int[] (array of ratings between 1 and 5)
- CreatedOn - Datetime (when it was added to the list) -> automatically generated
*/
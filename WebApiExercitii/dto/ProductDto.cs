using WebApiExercitii.Models;
namespace WebApiExercitii.dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int[]? Ratings { get; set; }
        public DateTime? CreatedOn {  get; set; }
        //public List<Inventory> Inventories { get; set; }
        public ProductDto() { }
        public ProductDto(Product p)
        {
            Id = p.Id;
            Name = p.Name;
            Description = p.Description;
            Ratings = p.Ratings;
            CreatedOn = p.CreatedOn;
        }
    }
}

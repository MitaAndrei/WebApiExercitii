using WebApiExercitii.Models;

namespace WebApiExercitii.dto
{
    public class InventoryDto
    {
        public StoreDto StoreDto { get; set; }
        public ProductDto ProductDto { get; set; }
        public int NrOfProducts { get; set; }

        public InventoryDto(Inventory i)
        {
            StoreDto = new StoreDto(i.Store);
            ProductDto = new ProductDto(i.Product);
            NrOfProducts = i.NrOfProducts;
        }
    }
}

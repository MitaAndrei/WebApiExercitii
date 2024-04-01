using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using WebApiExercitii.Data;
using WebApiExercitii.Models;
using WebApiExercitii.dto;
using Microsoft.EntityFrameworkCore;
namespace WebApiExercitii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        /*public static readonly List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Description = "First Product",
                Ratings = new[] {5,3,2,5}


            },

            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 2",
                Description = "Second Product",
                Ratings = new[] {1,4,1,5}

            }


        };*/

        RetailDbContext context;
        public ProductsController(RetailDbContext context)
        {
            this.context = context;
        }

        [HttpPost("create")]
        public IActionResult CreateProduct([FromBody] ProductDto product)
        {
            if (product != null)
            {
                context.Products.Add(new Product(product));
                context.SaveChanges();
                return Ok();
            }
            return BadRequest("product is null");

        }

        [HttpPut("edit/{productId}")]
        public IActionResult Edit(Guid productId, [FromBody] ProductDto product)
        {

            try
            {
                var entity = context.Products.Single(p => p.Id == productId);
                entity.Name = product.Name;
                entity.Description = product.Description;
                entity.Ratings = product.Ratings;
                context.Products.Update(entity);
                context.SaveChanges();
                return Ok();
            }
            catch(InvalidOperationException)
            {
                return NotFound("No product with such id");
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteStore(Guid id)
        {

            try
            {
                context.Products.Remove(context.Products.Single(p => p.Id == id));
                context.SaveChanges();
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound("no product with such id");
            }

        }

        [HttpGet("get-all")]
        public ProductDto[] GetAllProducts()
        {
            // .Include(p => p.Inventories).ThenInclude(i=>i.Store)
            var products = context.Products;
            var productsDTO =  products.Select(p => new ProductDto(p)).ToArray();
            /*return _products.ToArray();*/
            return productsDTO;
        }

        [HttpGet("get-by-keyword/{keyword}")]
        public Product[] GetByKeyword(string keyword)
        { 
            return context.Products.Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword)).ToArray();
        }

        [HttpGet("sort-by-avg-rating/{ascending}")]
        public IActionResult SortByAverageRating(bool ascending = true)
        {
            if (ascending)
                return Ok(context.Products.OrderBy(p => p.Ratings.Average()).ToArray());
         
            else
                return Ok(context.Products.OrderByDescending(p => p.Ratings.Average()).ToArray());

        }

        [HttpGet("get-most-recent-product")]
        public IActionResult GetMostRecentProduct()
        {


            if (context.Products.Any())
            {
                return Ok(context.Products.OrderByDescending(p => p.CreatedOn).First());
            }
            return BadRequest("there are no products");
        }
    }
}

            /*List<Product> sorted = new List<Product>(_products);

            for (int i = 0; i < sorted.Count; i++)
            {
                var product = sorted[i];
                double avgRating = product.Ratings.Sum() / product.Ratings.Length;
                for (int j = 0; j < sorted.Count; j++)
                {
                    var other = sorted[j];
                    double otherAvg = other.Ratings.Sum() / other.Ratings.Length;
                    if (avgRating < otherAvg)
                        (sorted[i], sorted[j]) = (sorted[j], sorted[i]);

                }

            }

            if (ascending)
                return Ok(sorted.ToArray());
            else
            {
                sorted.Reverse();
                return Ok(sorted.ToArray());
            }
*/
            /*if (_products.Count > 0)
            {
                Product mostRecent = _products[0];
                foreach (var store in _products)
                {
                    if (store.CreatedOn > mostRecent.CreatedOn)
                        mostRecent = store;
                }
                return Ok(mostRecent);
            }
            return BadRequest("There are no products.");*/
            /*foreach (var existingProduct in _products)
            {
                if (id == existingProduct.Id)
                {
                    _products.Remove(existingProduct);
                    return Ok();
                }
            }
            return NotFound("Store not found"); */
            /*foreach (var existingProduct in _products)
            {
                if (productId == existingProduct.Id)
                {
                    existingProduct.Name = name;
                    return Ok();
                }
            }
            return NotFound("Store not found");*/
            /*if (product == null)
            {
                return BadRequest("Store is null");
            }
            foreach (var existingProduct in _products)
            {
                if (product.Id == existingProduct.Id)
                {
                    return BadRequest("Store with the same id already exists");
                }
            }
            _products.Add(product);
            return Ok(product);*/
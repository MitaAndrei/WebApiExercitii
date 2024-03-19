using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using WebApiExercitii.Models;

namespace WebApiExercitii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public static readonly List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Description = "First Product",
                Ratings = new[] {1,3,2,5}


            },

            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 2",
                Description = "Second Product",
                Ratings = new[] {1,4,1,5}

            }


        };

        [HttpPost("create")]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (product == null)
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
            return Ok(product);
        }

        [HttpPut("edit/{productId}")]
        public IActionResult EditName(Guid productId, [FromBody] string name)
        {

            foreach (var existingProduct in _products)
            {
                if (productId == existingProduct.Id)
                {
                    existingProduct.Name = name;
                    return Ok();
                }
            }
            return NotFound("Store not found");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteStore(Guid id)
        {

            foreach (var existingProduct in _products)
            {
                if (id == existingProduct.Id)
                {
                    _products.Remove(existingProduct);
                    return Ok();
                }
            }
            return NotFound("Store not found");
        }

        [HttpGet("get-all")]
        public Product[] GetAllProducts()
        {
            return _products.ToArray();
        }

        [HttpGet("get-by-keyword")]
        public Product[] GetByKeyword(string keyword)
        {
            List<Product> prod = new List<Product>();
            foreach (var product in _products)
            {
                var propertyValues = product.GetType()
                     .GetProperties()
                     .Select(property => property.GetValue(product))
                     .ToList();

                foreach (var value in propertyValues)
                {
                    if (value.ToString().Contains(keyword))
                    {
                        prod.Add(product);
                    }
                }
            }
            return prod.ToArray();
        }

        [HttpGet("avg-rating-asc")]
        public Product[] SortRatingAsc()
        {
            List<Product> sorted = new List<Product>(_products);
            if (_products.Count > 0)
            {
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
            }
            return sorted.ToArray();

        }

        [HttpGet("get-most-recent-product")]

        public IActionResult GetMostRecentProduct()
        {

            if (_products.Count > 0)
            {
                Product mostRecent = _products[0];
                foreach (var store in _products)
                {
                    if (store.CreatedOn > mostRecent.CreatedOn)
                        mostRecent = store;
                }
                return Ok(mostRecent);
            }
            return BadRequest("There are no products.");
        }
    }
}

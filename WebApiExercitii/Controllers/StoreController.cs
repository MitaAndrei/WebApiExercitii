using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using WebApiExercitii.Data;
using WebApiExercitii.dto;
using WebApiExercitii.Models;

namespace WebApiExercitii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        /*public static readonly List<Store> _stores = new List<Store>
        {
            new Store
            {
                Id = Guid.NewGuid(),
                Name = "Store 1",
                Country = "Country 1",
                City = "City 1",
                MonthlyIncome = 20,
                OwnerName = "Owner 1",
                ActiveSince = DateTime.Now

            },

            new Store
            {
                Id = Guid.NewGuid(),
                Name = "Store 2",
                Country = "Country 2",
                City =  "City 2",
                MonthlyIncome = 2,
                OwnerName = "Owner 1",
                ActiveSince = DateTime.Now

            }
        };



*/

        RetailDbContext context;
        public StoreController(RetailDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public StoreDto[] GetAllStores()
        {
            return context.Stores.Select(s => new StoreDto(s)).ToArray();
        }

        [HttpPost]
        public IActionResult CreateStore([FromBody]StoreDto store)
        {
            if (store != null)
            {
                context.Stores.Add(new Store(store));
                context.SaveChanges();
                return Ok();
            }
            return BadRequest("product is null");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStore(Guid id)
        {
            try
            {
                context.Stores.Remove(context.Stores.Single(s => s.Id == id));
                context.SaveChanges();
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

        }

        [HttpPut("transfer-ownership/{storeId}")]
        public IActionResult TransferOwnership(Guid storeId, [FromBody] string name)
        {
            try
            {
                var entity = context.Stores.Single(s => s.Id == storeId);
                entity.OwnerName = name;
                context.Update(entity);
                context.SaveChanges();
                return Ok();
            }
            catch(InvalidOperationException)
            {
                return NotFound("No store with such id");
            }
        }

        [HttpGet("search/{keyword}")]
        public IActionResult Search(string keyword)
        {
            return Ok(context.Stores.Where(p => p.Name.Contains(keyword) || p.Country.Contains(keyword) || p.City.Contains(keyword)));
        }

/*        [HttpGet("get-by-country")]
        public IActionResult GetByCountry(string keyword)
        {
            return Ok(context.Stores.Where(p => p.Country.Contains(keyword)));
        }

        [HttpGet("get-by-city")]
        public IActionResult GetByCity(string keyword)
        {
            return Ok(context.Stores.Where(p => p.City.Contains(keyword)));
        }*/

        [HttpGet("get-by-sorted-income")]
        public IActionResult GetBySortedIncome()
        {
            return Ok(context.Stores.OrderBy(store => store.MonthlyIncome).ToArray());
        }


        [HttpGet("get-oldest-store")]
        public IActionResult GetOldestStore()
        {

            if (context.Stores.Any())
            {
                return Ok(context.Stores.OrderBy(s => s.ActiveSince).First());
            }
            return BadRequest("there are no stores.");
        }

        [HttpPut("edit/{storeId}")]
        public IActionResult Edit(Guid storeId, [FromBody] StoreDto store)
        {

            try
            {
                var entity = context.Stores.Single(p => p.Id == storeId);
                entity.Name = store.Name;
                entity.City = store.City;
                entity.Country = store.Country;
                entity.MonthlyIncome = store.MonthlyIncome;
                entity.OwnerName = store.OwnerName;
                context.Stores.Update(entity);
                context.SaveChanges();
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound("No product with such id");
            }
        }
    
    }





}
